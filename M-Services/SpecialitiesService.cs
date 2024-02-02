
using M_Core.Dtos.SpecialityDto;
using M_Core.Resources;
using M_Core.Statics;
using M_EF.Entities;
using M_Services.IServices;
using Microsoft.Extensions.Localization;
using Repos.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_Services
{
    public class SpecialitiesService : ISpecialitiesService
    {
        private readonly ISpecialitiesRepo _specialitiesRepo;
        private readonly IServicesRepo _servicesRepo;
        private readonly IDoctorsRepo _doctorsRepo;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public SpecialitiesService(ISpecialitiesRepo specialitiesRepo, IStringLocalizer<SharedResources> localizer, IServicesRepo serviceRepo, IDoctorsRepo doctorsRepo)
        {
            _specialitiesRepo = specialitiesRepo;
            _localizer = localizer;
            _servicesRepo = serviceRepo;
            _doctorsRepo = doctorsRepo;
        }

        public async Task<List<SpecialtyModel>> GetAllSpecialities()
        {
            return await _specialitiesRepo.GetAllSpecialities();
        }
        

        public async Task<int> GetSpecialitiesCount()
        {
            return await _specialitiesRepo.GetSpecialitiesCount();
        }

        public async Task<ReadSpecialityDto> AddSpeciality(SpecialityDto dto)
        {
            var special = await _specialitiesRepo.AddSpeciality(new SpecialtyModel
            {
                Title = dto.Title,
                Discription = dto.Discription,
            });
            
            var readSpeciality = new ReadSpecialityDto
            {
                Message = "Speciality Added..",
                Id = special.Id,
                Title = special.Title,
                Discription = special.Discription,
            };
            return readSpeciality;
        }

        public async Task<string> DeleteSpeciality(int id)
        {
            var specialitiy = await _specialitiesRepo.GetSpeciality(s => s.Id == id);
            if (specialitiy is null)
                return "Invalid Speciality";

            specialitiy.IsDeleted = true;

            var services = await _servicesRepo.GetSpecialityServices(s => s.SpecialityId == specialitiy.Id);
            if(services is not null)
            {
                foreach (var service in services)
                {
                    service.IsDeleted = true;
                    _servicesRepo.UpdateService(service);
                }

                var doctors = await _doctorsRepo.GetSpecialityDoctors(d => d.SpecialityId == specialitiy.Id);
                if(doctors is not null)
                {
                    foreach(var doctor in doctors)
                    {
                        doctor.IsDeleted = true;
                        _doctorsRepo.UpdateDoctor(doctor);
                    }
                }
            }
            _specialitiesRepo.UpdateSpeciality(specialitiy);

            return "Speciality deleted Successfuly with all related services and doctors..";
        }

        public async Task<ReadSpecialityDto> EditSpeciality(EditSpecialityDto dto)
        {
            var readSpeciality = new ReadSpecialityDto();
            if (dto is null)
            {
                readSpeciality.Message = "No Property To Edit";
                return readSpeciality;
            }
            var speciality = await _specialitiesRepo.GetSpeciality(s => s.Id == dto.Id);
            if(speciality is null)
            {
                readSpeciality.Message = "Invalid Speciality";
                return readSpeciality;
            }
            speciality.Title = dto.Title ?? speciality.Title;
            speciality.Discription = dto.Discription ?? speciality.Discription;

            _specialitiesRepo.UpdateSpeciality(speciality);
            readSpeciality.Message = " Edited Succeffuly";
            readSpeciality.Id = speciality.Id;
            readSpeciality.Title = speciality.Title;
            readSpeciality.Discription = speciality.Discription;

            return readSpeciality;
        }
    }
}