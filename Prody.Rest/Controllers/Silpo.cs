using Prody.Rest.Contracts.Models.Silpo;
using Prody.Rest.Controllers.Interfaces;
using Prody.Rest.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prody.Rest.Controllers
{
    public class Silpo : ISilpo
    {
        private readonly string catalogUrl = "https://api.catalog.ecom.silpo.ua/api/2.0/exec/EcomCatalogGlobal";
        private readonly string globalUrl = "https://api.ecom.silpo.ua/api/2.0/exec/EComGlobal";

        private readonly IRequestBuilderFactory requestBuilderFactory;

        private SilpoDefaultSettings defaultSettings;

        public Silpo(IRequestBuilderFactory requestBuilderFactory)
        {
            this.requestBuilderFactory = requestBuilderFactory;
        }

        public async Task<SilpoGetByCategoryResponse> GetProductsByCategory(int categoryId, int from, int to)
        {
            await Initialize();

            HttpClient client = new HttpClient();
            IRequestBuilder requestBuilder = requestBuilderFactory.Get();

            // define needed body parameter
            SilpoRequestByCategory requestByCategory = new SilpoRequestByCategory(categoryId, defaultSettings.FilialId, from, to);
            SilpoGetBody<SilpoRequestByCategory> bodyParameter = new SilpoGetBody<SilpoRequestByCategory>(requestByCategory, SILPO_API_METHODS.GetSimpleCatalogItems);

            OperationResult<SilpoGetByCategoryResponse> result = await requestBuilder.SetInitialData(catalogUrl)
                .SetMethod(HttpMethod.Post)
                .AddBodyParameter(new RequestParameter<SilpoGetBody<SilpoRequestByCategory>>("", bodyParameter))
                .Build<SilpoGetByCategoryResponse>()
                .Execute(client);

            return result.Result;
        }

        public async Task<SilpoCategories> GetCategories()
        {
            await Initialize();

            HttpClient client = new HttpClient();
            IRequestBuilder requestBuilder = requestBuilderFactory.Get();

            // define needed body parameter
            SilpoGetBody<SilpoDefaultSettings> bodyParameter = new SilpoGetBody<SilpoDefaultSettings>(defaultSettings, SILPO_API_METHODS.GetCategories);

            OperationResult<SilpoCategories> result = await requestBuilder.SetInitialData(catalogUrl)
                .SetMethod(HttpMethod.Post)
                .AddBodyParameter(new RequestParameter<SilpoGetBody<SilpoDefaultSettings>>("", bodyParameter))
                .Build<SilpoCategories>()
                .Execute(client);

            return result.Result;
        }

        private async Task Initialize()
        {
            if (defaultSettings != null)
            {
                return;
            }

            HttpClient client = new HttpClient();
            IRequestBuilder requestBuilder = requestBuilderFactory.Get();

            // define needed body parameter
            SilpoRequestDefaultFilial silpoRequestDefaultFilial = new SilpoRequestDefaultFilial();
            SilpoGetBody<SilpoRequestDefaultFilial> silpoGetBody = new SilpoGetBody<SilpoRequestDefaultFilial>(silpoRequestDefaultFilial, SILPO_API_METHODS.GetDefaultFilial);

            OperationResult<SilpoDefaultResponse<SilpoDefaultSettings>> result = await requestBuilder.SetInitialData(globalUrl)
                .SetMethod(HttpMethod.Post)
                .AddBodyParameter(new RequestParameter<SilpoGetBody<SilpoRequestDefaultFilial>>("", silpoGetBody))
                .Build<SilpoDefaultResponse<SilpoDefaultSettings>>()
                .Execute(client);

            defaultSettings = result.Result.Data;
        }
    }
}
