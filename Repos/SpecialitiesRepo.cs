using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using M_EF.Data;
using M_EF.Entities;
using Microsoft.EntityFrameworkCore;
using Repos.IRepos;

namespace Repos
{
    public class SpecialitiesRepo : ISpecialitiesRepo
    {
        private readonly AppDbContext _context;

        public SpecialitiesRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SpecialtyModel> GetSpeciality(Expression<Func<SpecialtyModel, bool>> predicate)
        {
             var speciality = await _context.Specialities.FirstOrDefaultAsync(predicate);
            return speciality;
        }
        public async Task<SpecialtyModel> AddSpeciality(SpecialtyModel model)
        {
             var special = await _context.Specialities.AddAsync(model);
            if(special is null)
                return null;
             _context.SaveChanges();
            return special.Entity;
        }


        public void UpdateSpeciality(SpecialtyModel model)
        {
           _context.Specialities.Update(model);
            _context.SaveChanges();
        }

    }
}
