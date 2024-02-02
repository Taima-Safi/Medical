using M_Core.Dtos.DoctorDto;
using M_EF.Entities;

namespace M_Services.IServices
{
    public interface IDoctorsService
    {
        Task<ReadDoctorDto> AddDoctor(DoctorDto dto);
        Task<string> DeleteDoctor(int id);
        Task<List<DoctorModel>> DoctorsForSpeciality(int specialityId);
        Task<ReadDoctorDto> EditDoctor(EditDoctorDto dto);
    }
}