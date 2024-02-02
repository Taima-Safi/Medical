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
    public class DoctorsRepo : IDoctorsRepo
    {
        private readonly AppDbContext _context;

        public DoctorsRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DoctorModel> GetDoctor(Expression<Func<DoctorModel, bool>> predicate)
        {
            return await _context.Doctors.FirstOrDefaultAsync(predicate);

        }

        public async Task<List<DoctorModel>> DoctorsForSpeciality(int specialityId)
        {
            var doctors = await _context.Doctors.Where(d => d.SpecialityId == specialityId).ToListAsync();
            return doctors;
        }

        public async Task<List<DoctorModel>> GetSpecialityDoctors(Expression<Func<DoctorModel, bool>> predicate)
        {
            return await _context.Doctors.Where(predicate).ToListAsync();
        }

        public async Task<DoctorModel> AddDoctor(DoctorModel model)
        {
            var doctor = await _context.Doctors.AddAsync(model);
            if (doctor is null)
                return null;

            _context.SaveChanges();

            return doctor.Entity;
        }

        public void UpdateDoctor(DoctorModel model)
        {
            _context.Doctors.Update(model);
            _context.SaveChanges();
        }

    }
}
