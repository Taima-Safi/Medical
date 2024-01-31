using M_EF.Entities;
using System.Linq.Expressions;

namespace Repos.IRepos
{
    public interface IDoctorsRepo
    {
        Task<DoctorModel> AddDoctor(DoctorModel model);
        Task<DoctorModel> GetDoctor(Expression<Func<DoctorModel, bool>> expression);
        Task<List<DoctorModel>> GetSpecialityDoctors(Expression<Func<DoctorModel, bool>> predicate);
        void UpdateDoctor(DoctorModel model);
    }
}