using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M_Core.Dtos.ServiceDto;
using M_Core.Dtos.SpecialityDto;
using M_Core.Resources;
using M_EF.Entities;
using M_Services.IServices;
using Microsoft.Extensions.Localization;
using Repos;
using Repos.IRepos;

namespace M_Services
{
    public class ServicesService : IServicesService
    {
        private readonly IServicesRepo _servicesRepo;
        private readonly ISpecialitiesRepo _specialitiesRepo;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ServicesService(IServicesRepo servicesRepo, IStringLocalizer<SharedResources> localizer, ISpecialitiesRepo specialitiesRepo)
        {
            _servicesRepo = servicesRepo;
            _localizer = localizer;
            _specialitiesRepo = specialitiesRepo;
        }

        public async Task<ReadServiceDto> AddService(ServiceDto dto)
        {
            var readService = new ReadServiceDto();
            var speciality = await _specialitiesRepo.GetSpeciality(s => s.Id == dto.SpecialityId);
            if (speciality is null)
            {
                readService.Message = $"No Speciality with {dto.SpecialityId} Id.";
                return readService;
            }
            var service = await _servicesRepo.AddService(new ServiceModel
            {
                Title = dto.Title,
                Discription = dto.Discription,
                SpecialityId = dto.SpecialityId
            });
            if (service is null)
            {
                 readService.Message = "Not Added";
                return readService;

            }

            readService.Message = "Service Added Successfuly";
            readService.Id = service.Id;
            readService.Title = service.Title;
            readService.Discription = service.Discription;
            readService.SpecialityId = service.SpecialityId;

            return readService;
        }

        public async Task<ReadServiceDto> EditService(EditServiceDto dto)
        {
            var readService = new ReadServiceDto();
            if (dto is null)
            {
                readService.Message = "No Property To Edit";
                return readService;
            }

            var service = await _servicesRepo.GetService(s => s.Id == dto.Id);
            if (service is null)
            {
                readService.Message = "Invalid service";
                return readService;
            }

            if(dto.SpecialityId is not null)
            {
                var speciality = await _specialitiesRepo.GetSpeciality(s => s.Id == dto.SpecialityId);
                if (speciality is null)
                {
                    readService.Message = "invalid specialityId";
                    return readService;
                }
            }
           
            service.Title = dto.Title ?? service.Title;
            service.Discription = dto.Discription ?? service.Discription;
            service.SpecialityId = dto.SpecialityId ?? service.SpecialityId;

            _servicesRepo.UpdateService(service);
            readService.Message = " Edited Succeffuly";
            readService.Id = service.Id;
            readService.Title = service.Title;
            readService.Discription = service.Discription;

            return readService;
        }

        public async Task<string> DeleteService(int id)
        {
            var service = await _servicesRepo.GetService(s => s.Id == id);
            if (service is null)
                return "Invalid service";
            service.IsDeleted = true;
            _servicesRepo.UpdateService(service);

            return "Service Deleted Successfuly";
        }
    }
}
