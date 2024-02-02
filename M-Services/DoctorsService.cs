using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M_Core.Dtos.DoctorDto;
using M_EF.Entities;
using M_Services.IServices;
using Repos;
using Repos.IRepos;

namespace M_Services
{
    public class DoctorsService : IDoctorsService
    {
        private readonly IDoctorsRepo _doctorsRepo;
        private readonly ISpecialitiesRepo _specialitiesRepo;


        public DoctorsService(IDoctorsRepo doctorsRepo, ISpecialitiesRepo specialitiesRepo)
        {
            _doctorsRepo = doctorsRepo;
            _specialitiesRepo = specialitiesRepo;
        }

        public async Task<List<DoctorModel>> DoctorsForSpeciality(int specialityId)
        {
            var speciality = await _specialitiesRepo.GetSpeciality(s => s.Id == specialityId);
            if (speciality is null)
                return null;

            return await _doctorsRepo.DoctorsForSpeciality(specialityId);
        }

        public async Task<ReadDoctorDto> AddDoctor(DoctorDto dto)
        {
            var readDoctor = new ReadDoctorDto();
            var speciality = await _specialitiesRepo.GetSpeciality(s => s.Id == dto.SpecialityId);
            if (speciality is null)
            {
                readDoctor.Message = $"No Speciality with {dto.SpecialityId} Id.";
                return readDoctor;
            }
            var doctor = await _doctorsRepo.AddDoctor(new DoctorModel
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                SpecialityId = dto.SpecialityId,
                Email = dto.Email,
            });
            if(doctor is null)
            {
                readDoctor.Message = "Con not Added This Doctor";
                return readDoctor;
            }
            readDoctor.Message = "Doctor Added";
            readDoctor.Email = doctor.Email;
            readDoctor.FirstName = doctor.FirstName;
            readDoctor.LastName = doctor.LastName;
            readDoctor.SpecialityId = doctor.SpecialityId;

            return readDoctor;
        }

        public async Task<ReadDoctorDto> EditDoctor(EditDoctorDto dto)
        {
            var readDoctor = new ReadDoctorDto();
            if(dto is null)
            {
                readDoctor.Message = "No Property To Edit";
                return readDoctor;
            }
            var doctor = await _doctorsRepo.GetDoctor(d => d.Id == dto.Id);
            if(doctor is null)
            {
                readDoctor.Message = " Invalid Doctor";
                return readDoctor;
            }

            if (dto.SpecialityId is not null)
            {
                var speciality = await _specialitiesRepo.GetSpeciality(s => s.Id == dto.SpecialityId);
                if (speciality is null)
                {
                    readDoctor.Message = "invalid specialityId";
                    return readDoctor;
                }
            }

            doctor.FirstName = dto.FirstName ?? doctor.FirstName;
            doctor.LastName = dto.LastName ?? doctor.LastName;
            doctor.Email = dto.Email ?? doctor.Email;
            doctor.SpecialityId = dto.SpecialityId ?? doctor.SpecialityId;

            _doctorsRepo.UpdateDoctor(doctor);

            readDoctor.Message = "Edited Successfuly";
            readDoctor.FirstName = doctor.FirstName;
            readDoctor.LastName = doctor.LastName;
            readDoctor.Email = doctor.Email;
            readDoctor.SpecialityId = doctor.SpecialityId;

            return readDoctor;
        }

        public async Task<string> DeleteDoctor(int id)
        {
            var doctor = await _doctorsRepo.GetDoctor(s => s.Id == id);
            if (doctor is null)
                return "Invalid Doctor";
            doctor.IsDeleted = true;
            _doctorsRepo.UpdateDoctor(doctor);

            return "Doctor Deleted Successfuly";
        }
    }
}
