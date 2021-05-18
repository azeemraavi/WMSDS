using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wmsds.Entities;
using Wmsds.Entities.WC;

namespace Wmsds.Bll.Watercourse
{
    public interface IWaterCourseService
    {
        Task<WmsdsResponse<int>> AddIdentification(WcIdentification wcIdentification);

        /// <summary>
        /// GetIdentification by Id
        /// </summary>
        /// <param name="wcIdentificationId"></param>
        /// <returns></returns>
        Task<WmsdsResponse<WcIdentification>> GetIdentificationById(int wcIdentificationId);

        /// <summary>
        /// Get Water Course Identifications based on different parameters. Skip parameter for empty value.
        /// </summary>
        /// <param name="districtId"></param>
        /// <param name="tehsilId"></param>
        /// <param name="channelId"></param>
        /// <param name="improveYearId"></param>
        /// <returns></returns>
        Task<List<WcIdentification>> GetWcIdentifications(int districtId=0, int tehsilId=0, int channelId=0, int improveYearId=0, string improvementType=null);

        //To add Identification Detail

        Task<WmsdsResponse<WcIdentificationDetail>> GetIdentificationDetailById(int wcIdentifictionDetailId);

        Task<WmsdsResponse<WcIdentificationDetail>> GetIdentificationDetailByMasterId(int wcIdentificationId);

        Task<WmsdsResponse<WcIdentificationDetail>> AddBasicInformation(WcIdentificationDetail wcIdentificationDetail);
        Task<WmsdsResponse<int>> UpdateBasicInformation(WcIdentificationDetail wcIdentificationDetail);
        Task<WmsdsResponse<int>> AddUpdateDesignParameters(WcIdentificationDetail wcIdentificationDetail);
        Task<WmsdsResponse<int>> AddUpdateICR1AndICR2(WcIdentificationDetail wcIdentificationDetail);
        Task<WmsdsResponse<int>> AddUpdateFCR(WcIdentificationDetail wcIdentificationDetail);



    }
}
