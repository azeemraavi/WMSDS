using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wmsds.Entities;
using Wmsds.Entities.Common;

namespace Wmsds.Bll.Common
{
    public interface ICommonService
    {
        
        Task<WmsdsResponse<int>> AddProvince(Province province);
        Task<List<Province>> GetAllProvinces();

        Task<WmsdsResponse<int>> AddDivision(Division province);
        Task<List<Division>> GetDivisionByProvinceId(int provinceId);
        Task<List<Division>> GetAllDivisions();

        Task<WmsdsResponse<int>> AddDistrict(District district);
        Task<List<District>> GetAllDistrictsByDivId(int divisionId);
        Task<List<District>> GetAllDistricts();

        Task<WmsdsResponse<int>> AddTehsil(Tehsil tehsil);
        Task<List<Tehsil>> GetAllTehsilsByDistrictId(int districtId);
        Task<List<Tehsil>> GetAllTehsils();

        Task<WmsdsResponse<int>> AddFinancialYear(FinancialYear financial);
        Task<List<FinancialYear>> GetAllFinancialYears();

        Task<WmsdsResponse<int>> AddChannel(Channel channel);
        Task<List<Channel>> GetChannelsByDistAndTehId(int districtId,int tehsilId);

        Task<WmsdsResponse<int>> AddWaterCourse(WaterCourse channel);
        Task<List<WaterCourse>> GetWaterCourses(int districtId, int tehsilId,int channelId);
        
    }
}
