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
    public class ServicesRepo : IServicesRepo
    {
        private readonly AppDbContext _context;

        public ServicesRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceModel> GetService(Expression<Func<ServiceModel, bool>> predicate)
        {
           return await _context.Services.FirstOrDefaultAsync(predicate);
        }
        
        public async Task<List<ServiceModel>> GetSpecialityServices(Expression<Func<ServiceModel, bool>> predicate)
        {
           return await _context.Services.Where(predicate).ToListAsync();
        }

        public async Task<ServiceModel> AddService(ServiceModel model)
        {
            var service = await _context.Services.AddAsync(model);
            if (service is null)
                return null;

            _context.SaveChanges();
            return service.Entity;
        }

        public void UpdateService(ServiceModel model)
        {
            _context.Services.Update(model);
            _context.SaveChanges();
        }

    }
}
