using M_EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repos.IRepos
{
    public interface ISpecialitiesRepo
    {
        Task<SpecialtyModel> AddSpeciality(SpecialtyModel model);
        Task<SpecialtyModel> GetSpeciality(Expression<Func<SpecialtyModel, bool>> predicate);
        void UpdateSpeciality(SpecialtyModel model);
    }
}
