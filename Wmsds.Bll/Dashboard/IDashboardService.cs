using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wmsds.Entities;
using Wmsds.Entities.ViewModels;

namespace Wmsds.Bll.Dashboard
{
    public interface IDashboardService
    {
        Task<WmsdsResponse<WatercoureseStatusDto>> GetStatusOfWatercourses();
        /// <summary>
        /// Watercourse Improvement Status
        /// </summary>
        /// <returns></returns>
        Task<WmsdsResponse<WcImprovementStatusDto>> GetWcImprovementStatus();
        /// <summary>
        /// Length of Improved Watercourses (‘000’ KMs)
        /// </summary>
        /// <returns></returns>
        Task<WmsdsResponse<LengthOfWcDto>> GetLengthOfImprovedWc();
        /// <summary>
        /// ImplicationFinancial (Rs. Million)
        /// </summary>
        /// <returns></returns>
        Task<WmsdsResponse<ImplicationFinancialDto>> GetImplicationFinancial();
        /// <summary>
        /// DISTRICT WISE WATERCOURSE IMPROVEMENT STATUS
        /// </summary>
        /// <returns></returns>
        Task<WmsdsResponse<DistrictWiseWcImprDto>> GetDistrictWiseWcImprStatus();
        /// <summary>
        /// YEAR WISE WATERCOURSE IMPROVEMENT TREND
        /// </summary>
        /// <returns></returns>
        Task<WmsdsResponse<KeyValueDto>> GetYearWiseWcImprStatus();
        Task<WmsdsResponse<DistrictWiseDto>> GetDistrictWiseOverview(int districtId);
    }
}
