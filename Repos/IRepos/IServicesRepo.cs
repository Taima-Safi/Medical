using M_EF.Entities;
using System.Linq.Expressions;

namespace Repos.IRepos
{
    public interface IServicesRepo
    {
        Task<ServiceModel> AddService(ServiceModel model);
        Task<ServiceModel> GetService(Expression<Func<ServiceModel, bool>> predicate);
        Task<List<ServiceModel>> GetSpecialityServices(Expression<Func<ServiceModel, bool>> predicate);
        void UpdateService(ServiceModel model);
    }
}