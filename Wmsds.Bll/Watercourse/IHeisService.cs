using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wmsds.Entities;
using Wmsds.Entities.HEIS;
using Wmsds.Entities.ViewModels;

namespace Wmsds.Bll.Watercourse
{
    public interface IHeisService
    {
        Task<WmsdsResponse<int>> AddHeisIdentification(HeisIdentification heisIdentification);
        /// <summary>
        /// GetHEISIdentification By Id
        /// </summary>
        /// <param name="heisIdentificationId"></param>
        /// <returns></returns>
        Task<WmsdsResponse<HeisIdentification>> GetHeisIdentificationById(int heisIdentificationId);
        Task<WmsdsResponse<HeisIdentificationLightModel>> GetHeisIdentifications(int currentPageIndex=1,int districtId = 0, int tehsilId = 0,string byName=null,string byCnic=null);

        Task<WmsdsResponse<HeisIdentificationDetail>> GetHeisIdentificationDetailById(int heisIdentifictionDetailId);
        Task<WmsdsResponse<HeisIdentificationDetail>> GetHeisIdentificationDetailByMasterId(int heisMasterIdentificationId);

        Task<WmsdsResponse<HeisIdentificationDetail>> AddHeisBasicInformation(HeisIdentificationDetail heisIdentificationDetail);
        Task<WmsdsResponse<int>> UpdateHeisBasicInformation(HeisIdentificationDetail heisIdentificationDetail);

        Task<WmsdsResponse<int>> AddWorksExecuted(HeisIdentificationDetail heisIdentificationDetail);

        #region Vendor Related

        Task<WmsdsResponse<int>> AddHeisVendor(HeisVendor heisVendor);
        Task<WmsdsResponse<int>> UpdateHeisVendor(HeisVendor heisVendor);
        Task<WmsdsResponse<int>> DeleteHeisVendor(int vendorId);
        Task<WmsdsResponse<HeisVendor>> GetAllHeisVendors();
        Task<WmsdsResponse<HeisVendor>> GetHeisVendorById(int vendorId);

        #endregion


    }
}
