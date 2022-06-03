using System.Net.Http;
using System.Threading.Tasks;

namespace Prody.Rest.Interfaces
{
    public interface IRequest<Target>
    {
        Task<OperationResult<Target>> Execute(HttpClient client);
    }
}
