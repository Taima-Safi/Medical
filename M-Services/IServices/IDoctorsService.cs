using M_Core.Dtos.DoctorDto;

namespace M_Services.IServices
{
    public interface IDoctorsService
    {
        Task<ReadDoctorDto> AddDoctor(DoctorDto dto);
        Task<string> DeleteDoctor(int id);
        Task<ReadDoctorDto> EditDoctor(EditDoctorDto dto);
    }
}