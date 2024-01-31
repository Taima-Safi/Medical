using M_Core.Dtos.SpecialityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_Services.IServices
{
    public interface ISpecialitiesService
    {
        Task<ReadSpecialityDto> AddSpeciality(SpecialityDto dto);
        Task<string> DeleteSpeciality(int id);
        Task<ReadSpecialityDto> EditSpeciality(EditSpecialityDto dto);
    }
}
