using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Trainers.Recommender;
using Newtonsoft.Json;
using Prody.BLL.DTOs;
using Prody.BLL.ML.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Microsoft.ML.DataOperationsCatalog;

namespace Prody.BLL.ML
{
    public class RecommendationService
    {
        private readonly ITransformer _trainedModel;
        private readonly MLContext _mlContext;
        private readonly string _folderPath;
        private const int _numOfProducts = 262111;

        public RecommendationService()
        {
            string fullPath = Path.GetFullPath(@"..\");
            _folderPath = Path.Combine(fullPath, "Prody.BLL\\ML\\");
            _mlContext = new MLContext();
            _trainedModel = _mlContext.Model.Load(Path.Combine(_folderPath, "Data\\model.zip"), out _);
        }

        public IEnumerable<ReadPrediction> GetPredictions(GetRecommendationsDto parameters)
        {
            PredictionEngine<ProductRecord, Prediction> predictionengine = _mlContext.Model.CreatePredictionEngine<ProductRecord, Prediction>(_trainedModel);

            IEnumerable<(int ProductID, float Score)> topN = (from m in Enumerable.Range(1, _numOfProducts)
                                                              let p = predictionengine.Predict(
                                                                 new ProductRecord()
                                                                 {
                                                                     ProductID = parameters.ProductId,
                                                                     CombinedProductID = (uint)m
                                                                 })
                                                              orderby p.Score descending
                                                              select (ProductID: m, p.Score)).Take(parameters.Num);

            return topN.Select(s => new ReadPrediction() { Score = s.Score, ProductID = s.ProductID });
        }

        public void Train()
        {
            string dataPath = Path.Combine(_folderPath, "Data\\Amazon0302.txt");

            IDataView traindata = _mlContext.Data.LoadFromTextFile<ProductRecord>(path: dataPath,
                                                      hasHeader: true,
                                                      separatorChar: '\t');

            TrainTestData trainTestSplit = _mlContext.Data.TrainTestSplit(traindata, testFraction: 0.2);

            MatrixFactorizationTrainer.Options options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "ProductIDEncoded",
                MatrixRowIndexColumnName = "CombinedProductIDEncoded",
                LabelColumnName = "CombinedProductID",
                LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass,
                NumberOfIterations = 20,
                Alpha = 0.00001,
                Lambda = 0.1,
            };
            MatrixFactorizationTrainer trainer = _mlContext.Recommendation().Trainers.MatrixFactorization(options);

            EstimatorChain<MatrixFactorizationPredictionTransformer> pipeline = _mlContext.Transforms.Conversion.MapValueToKey(
                                            inputColumnName: "ProductID",
                                            outputColumnName: "ProductIDEncoded")
                                        .Append(_mlContext.Transforms.Conversion.MapValueToKey(
                                            inputColumnName: "CombinedProductID",
                                            outputColumnName: "CombinedProductIDEncoded"))
                                        .Append(trainer);

            ITransformer model = pipeline.Fit(traindata);

            _mlContext.Model.Save(model, traindata.Schema, Path.Combine(_folderPath, "Data\\model.zip"));

            // Evaluate
            IDataView predictions = model.Transform(trainTestSplit.TestSet);
            RegressionMetrics metrics = _mlContext.Regression.Evaluate(predictions, labelColumnName: "CombinedProductID");
            double correctRMSE = calcRMSE(model, trainTestSplit.TestSet);

            string jsonMetrics = JsonConvert.SerializeObject(metrics);
            string jsonOptions = JsonConvert.SerializeObject(options);
            string timeStamp = $"{DateTime.Now:yyyyMMddHHmmssffff}";
            File.WriteAllText(Path.Combine(_folderPath, $"Data\\metricsReg{timeStamp}.json"), jsonMetrics);
            File.WriteAllText(Path.Combine(_folderPath, $"Data\\metricsRMSE{timeStamp}.txt"), correctRMSE.ToString());
            File.WriteAllText(Path.Combine(_folderPath, $"Data\\metricsOptions{timeStamp}.json"), jsonOptions);
        }

        private double calcRMSE(ITransformer model, IDataView valData)
        {
            PredictionEngine<ProductRecord, Prediction> predictionengine = _mlContext.Model.CreatePredictionEngine<ProductRecord, Prediction>(model);
            IEnumerable<ProductRecord> housingDataEnumerable =
                 _mlContext.Data.CreateEnumerable<ProductRecord>(valData, reuseRowObject: true);

            double sum = 0;
            // Iterate over each row
            foreach (ProductRecord row in housingDataEnumerable)
            {
                Prediction p = predictionengine.Predict(
                                                                 new ProductRecord()
                                                                 {
                                                                     ProductID = row.ProductID,
                                                                     CombinedProductID = row.CombinedProductID
                                                                 });
                sum += Math.Pow(1 - p.Score, 2);
            }
            return Math.Sqrt(sum / housingDataEnumerable.Count());
        }
    }
}
