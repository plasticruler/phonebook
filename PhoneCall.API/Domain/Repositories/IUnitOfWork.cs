using System.Threading.Tasks;

namespace PhoneCall.API.Domain.Repositories{
    public interface IUnitOfWork{
        Task CompleteAsync();
    }
}