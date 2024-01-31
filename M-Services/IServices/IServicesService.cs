using M_Core.Dtos.ServiceDto;

namespace M_Services.IServices
{
    public interface IServicesService
    {
        Task<ReadServiceDto> AddService(ServiceDto dto);
        Task<string> DeleteService(int id);
        Task<ReadServiceDto> EditService(EditServiceDto dto);
    }
}