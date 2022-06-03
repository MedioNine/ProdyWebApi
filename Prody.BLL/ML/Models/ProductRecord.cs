using Microsoft.ML.Data;

namespace Prody.BLL.ML.Models
{
    public class ProductRecord
    {
        //[KeyType(count: 262111)]
        [LoadColumn(0)]
        public float ProductID { get; set; }

        //[KeyType(count: 262111)]
        [LoadColumn(1)]
        public float CombinedProductID { get; set; }
    }
}
