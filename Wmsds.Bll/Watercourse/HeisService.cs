using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wmsds.Dal;
using Wmsds.Entities;
using Wmsds.Entities.HEIS;

namespace Wmsds.Bll.Watercourse
{
    public class HeisService : IHeisService
    {
        public async Task<WmsdsResponse<int>> AddHeisIdentification(HeisIdentification heisIdentification)
        {
            var wmsdResponse = new WmsdsResponse<int>();
            if (heisIdentification == null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }

            if (heisIdentification.DistrictId <= 0 || string.IsNullOrEmpty(heisIdentification.DistrictName))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "DistrictId is a required field.";
                return wmsdResponse;
            }

            if (heisIdentification.TehsilId <= 0 || string.IsNullOrEmpty(heisIdentification.TehsilName))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "TehsilId is a required field.";
                return wmsdResponse;
            }
            if (await IsHeisIdentificationAlreadyExist(heisIdentification))
            {
                wmsdResponse.ResponseCode = EnumStatus.AlreadyExist;
                wmsdResponse.ResponseMessage = "Watercourse already exists.";
                return wmsdResponse;
            }
            //Check if status is Addation than there must be a regular water course
            try
            {
                using (var _dbContext = new EntityContext())
                {
                    _dbContext.HeisIdentifications.Add(heisIdentification);
                    var response = await _dbContext.SaveChangesAsync();
                    if (response > 0)
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Success; ;
                        wmsdResponse.ResponseMessage = "Identicification has been added successfully.";
                        return wmsdResponse;
                    }
                    else
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Failed; ;
                        wmsdResponse.ResponseMessage = "Failed to add channel.";
                        return wmsdResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.InternalServer;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }
        }
        private async Task<bool> IsHeisIdentificationAlreadyExist(HeisIdentification heisIdentification)
        {
            using (var _dbContext = new EntityContext())
            {
                var wcIdentificationDb = await _dbContext.HeisIdentifications
                                    .Where(c => c.FarmerCNIC == heisIdentification.FarmerCNIC)                                        
                                        .FirstOrDefaultAsync();
                return wcIdentificationDb == null ? false : true;
            }
        }
        public async Task<WmsdsResponse<HeisIdentification>> GetHeisIdentificationById(int heisIdentificationId)
        {
            var wmsdResponse = new WmsdsResponse<HeisIdentification>();
            if (heisIdentificationId <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.RequiredField;
                wmsdResponse.ResponseMessage = "HEIS Identification Id is missing.";
                return wmsdResponse;
            }

            using (var _dbContext = new EntityContext())
            {
                try
                {
                    var wcIdentification = await _dbContext.HeisIdentifications
                                    .Where(c => c.Id == heisIdentificationId)
                                    .SingleOrDefaultAsync();
                    if (wcIdentification != null)
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Success;
                        wmsdResponse.DataObject = wcIdentification;
                        wmsdResponse.ResponseMessage = "Record Found.";
                        return wmsdResponse;
                    }
                    else
                    {
                        wmsdResponse.ResponseCode = EnumStatus.NotFound;
                        wmsdResponse.ResponseMessage = "No Record Found.";
                        return wmsdResponse;
                    }
                }
                catch (Exception ex)
                {
                    wmsdResponse.ResponseCode = EnumStatus.InternalServer;
                    wmsdResponse.ResponseMessage = ex.Message;
                    return wmsdResponse;
                }
            }
        }
        public async Task<List<HeisIdentification>> GetHeisIdentifications(int districtId = 0, int tehsilId = 0, string byName = null, string byCnic = null)
        {
            using (var _dbContext = new EntityContext())
            {
                var wcIdentifications = await _dbContext.HeisIdentifications
                                //.Where(c => districtId == 0 || c.DistrictId == districtId)
                                //    .Where(c => tehsilId == 0 || c.TehsilId == tehsilId)
                                //    .Where(c => byCnic == null || c.FarmerCNIC.Contains(byCnic))
                                //    .Where(c => byName == null || c.FarmerName.Contains(byName))
                                    .Include(x => x.HeisIdentificationDetails)
                                    .ToListAsync();
                return wcIdentifications;
            }
        }
        public async Task<WmsdsResponse<HeisIdentificationDetail>> GetHeisIdentificationDetailById(int heisIdentifictionDetailId)
        {
            var wmsdResponse = new WmsdsResponse<HeisIdentificationDetail>();
            if (heisIdentifictionDetailId <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.RequiredField;
                wmsdResponse.ResponseMessage = "HEIS Identification Detail Id is missing.";
                return wmsdResponse;
            }

            using (var _dbContext = new EntityContext())
            {
                try
                {
                    var wcIdentification = await _dbContext.HeisIdentificationDetails
                                    .Where(c => c.Id == heisIdentifictionDetailId)
                                    .SingleOrDefaultAsync();
                    if (wcIdentification != null)
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Success;
                        wmsdResponse.DataObject = wcIdentification;
                        wmsdResponse.ResponseMessage = "Record Found.";
                        return wmsdResponse;
                    }
                    else
                    {
                        wmsdResponse.ResponseCode = EnumStatus.NotFound;
                        wmsdResponse.ResponseMessage = "No Record Found.";
                        return wmsdResponse;
                    }
                }
                catch (Exception ex)
                {
                    wmsdResponse.ResponseCode = EnumStatus.InternalServer;
                    wmsdResponse.ResponseMessage = ex.Message;
                    return wmsdResponse;
                }
            }
        }
        public async Task<WmsdsResponse<HeisIdentificationDetail>> GetHeisIdentificationDetailByMasterId(int heisMasterIdentificationId)
        {
            var wmsdResponse = new WmsdsResponse<HeisIdentificationDetail>();
            if (heisMasterIdentificationId <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.RequiredField;
                wmsdResponse.ResponseMessage = "HEIS Identification Id is missing.";
                return wmsdResponse;
            }

            using (var _dbContext = new EntityContext())
            {
                try
                {
                    var wcIdentification = await _dbContext.HeisIdentificationDetails
                                    .Where(c => c.HeisIdentificationId == heisMasterIdentificationId)
                                    .FirstOrDefaultAsync();
                    if (wcIdentification != null)
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Success;
                        wmsdResponse.DataObject = wcIdentification;
                        wmsdResponse.ResponseMessage = "Record Found.";
                        return wmsdResponse;
                    }
                    else
                    {
                        wmsdResponse.ResponseCode = EnumStatus.NotFound;
                        wmsdResponse.ResponseMessage = "No Record Found.";
                        return wmsdResponse;
                    }
                }
                catch (Exception ex)
                {
                    wmsdResponse.ResponseCode = EnumStatus.InternalServer;
                    wmsdResponse.ResponseMessage = ex.Message;
                    return wmsdResponse;
                }
            }
        }
        public async Task<WmsdsResponse<HeisIdentificationDetail>> AddHeisBasicInformation(HeisIdentificationDetail heisIdentificationDetail)
        {
            var wmsdResponse = new WmsdsResponse<HeisIdentificationDetail>();
            if (heisIdentificationDetail == null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }

            if (heisIdentificationDetail.HeisIdentificationId <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "HEIS Parent Identification key is empty.";
                return wmsdResponse;
            }

            try
            {
                using (var _dbContext = new EntityContext())
                {
                    _dbContext.HeisIdentificationDetails.Add(heisIdentificationDetail);
                    var response = await _dbContext.SaveChangesAsync();

                    if (response > 0)
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Success;
                        wmsdResponse.DataObject = new HeisIdentificationDetail();
                        wmsdResponse.DataObject.Id = heisIdentificationDetail.Id;
                        wmsdResponse.ResponseMessage = "Record has been added successfully.";
                        return wmsdResponse;
                    }
                    else
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Failed; ;
                        wmsdResponse.ResponseMessage = "Failed to update record.";
                        return wmsdResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.InternalServer;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }
        }
        public async Task<WmsdsResponse<int>> UpdateHeisBasicInformation(HeisIdentificationDetail heisIdentificationDetail)
        {
            var wmsdResponse = new WmsdsResponse<int>();
            if (heisIdentificationDetail == null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }

            if (heisIdentificationDetail.HeisIdentificationId <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Parent Identification key is empty.";
                return wmsdResponse;
            }
            if (heisIdentificationDetail.Id <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Id is missing.";
                return wmsdResponse;
            }

            try
            {
                using (var _dbContext = new EntityContext())
                {
                    var basicInfoDb = await _dbContext.HeisIdentificationDetails
                        .Where(c => c.Id == heisIdentificationDetail.Id
                        && c.HeisIdentificationId == heisIdentificationDetail.HeisIdentificationId)
                        .SingleOrDefaultAsync();
                    if (basicInfoDb != null)
                    {

                        basicInfoDb.InstallationType = string.IsNullOrEmpty(heisIdentificationDetail.InstallationType)
                            ? basicInfoDb.InstallationType : heisIdentificationDetail.InstallationType;

                        basicInfoDb.FiscalYear = string.IsNullOrEmpty(heisIdentificationDetail.FiscalYear)
                            ? basicInfoDb.FiscalYear : heisIdentificationDetail.FiscalYear;

                        basicInfoDb.FiscalYearId = heisIdentificationDetail.FiscalYearId <= 0
                            ? basicInfoDb.FiscalYearId : heisIdentificationDetail.FiscalYearId;

                        basicInfoDb.SystemType = string.IsNullOrEmpty(heisIdentificationDetail.SystemType)
                             ? basicInfoDb.SystemType : heisIdentificationDetail.SystemType;


                        basicInfoDb.SSCName = string.IsNullOrEmpty(heisIdentificationDetail.SSCName)
                            ? basicInfoDb.SSCName : heisIdentificationDetail.SSCName;
                        basicInfoDb.SSCId = heisIdentificationDetail.SSCId <= 0
                           ? basicInfoDb.SSCId : heisIdentificationDetail.SSCId;

                        basicInfoDb.ProjectName = string.IsNullOrEmpty(heisIdentificationDetail.ProjectName)
                            ? basicInfoDb.ProjectName : heisIdentificationDetail.ProjectName;
                        basicInfoDb.VillageName = string.IsNullOrEmpty(heisIdentificationDetail.VillageName)
                            ? basicInfoDb.VillageName : heisIdentificationDetail.VillageName;
                        basicInfoDb.ContactNumber = string.IsNullOrEmpty(heisIdentificationDetail.ContactNumber)
                            ? basicInfoDb.ContactNumber : heisIdentificationDetail.ContactNumber;

                        basicInfoDb.SoilType = string.IsNullOrEmpty(heisIdentificationDetail.SoilType)
                            ? basicInfoDb.SoilType : heisIdentificationDetail.SoilType;
                        basicInfoDb.SchemeArea = heisIdentificationDetail.SchemeArea <= 0
                          ? basicInfoDb.SchemeArea : heisIdentificationDetail.SchemeArea;
                        basicInfoDb.WaterSource = string.IsNullOrEmpty(heisIdentificationDetail.WaterSource)
                            ? basicInfoDb.WaterSource : heisIdentificationDetail.WaterSource;
                        basicInfoDb.WaterQuality = string.IsNullOrEmpty(heisIdentificationDetail.WaterQuality)
                            ? basicInfoDb.WaterQuality : heisIdentificationDetail.WaterQuality;

                        basicInfoDb.PowerSource = string.IsNullOrEmpty(heisIdentificationDetail.PowerSource)
                            ? basicInfoDb.PowerSource : heisIdentificationDetail.PowerSource;
                        basicInfoDb.CropCategory = string.IsNullOrEmpty(heisIdentificationDetail.CropCategory)
                            ? basicInfoDb.CropCategory : heisIdentificationDetail.CropCategory;
                        basicInfoDb.CropName = string.IsNullOrEmpty(heisIdentificationDetail.CropName)
                            ? basicInfoDb.CropName : heisIdentificationDetail.CropName;
                        basicInfoDb.PlansPerAcre = heisIdentificationDetail.PlansPerAcre <= 0
                         ? basicInfoDb.PlansPerAcre : heisIdentificationDetail.PlansPerAcre;
                        basicInfoDb.SystemClassification = string.IsNullOrEmpty(heisIdentificationDetail.SystemClassification)
                            ? basicInfoDb.SystemClassification : heisIdentificationDetail.SystemClassification;
                        basicInfoDb.Latitude = heisIdentificationDetail.Latitude <= 0
                                ? basicInfoDb.Latitude : heisIdentificationDetail.Latitude;
                        basicInfoDb.Longitude = heisIdentificationDetail.Longitude <= 0
                                ? basicInfoDb.Longitude : heisIdentificationDetail.Longitude;

                        var response = await _dbContext.SaveChangesAsync();
                        if (response > 0)
                        {
                            wmsdResponse.ResponseCode = EnumStatus.Success; ;
                            wmsdResponse.ResponseMessage = "Record has been added successfully.";
                            return wmsdResponse;
                        }
                        else
                        {
                            wmsdResponse.ResponseCode = EnumStatus.Failed; ;
                            wmsdResponse.ResponseMessage = "Failed to update record.";
                            return wmsdResponse;
                        }
                    }
                    else
                    {
                        wmsdResponse.ResponseCode = EnumStatus.NotFound; ;
                        wmsdResponse.ResponseMessage = "No Record found against given Ids.";
                        return wmsdResponse;
                    }

                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.InternalServer;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }
        }

        /// <summary>
        /// AddWorksExecuted will add/update below sections
        /// i-Design Parameters
        /// ii-Work Order
        /// iii-Material Verification (ICR-I)
        /// iv-Commissioning Verification (ICR-II)
        /// v-Third Installment (ICR-III)
        /// </summary>
        /// <param name="heisIdentificationDetail"></param>
        /// <returns></returns>
        public async Task<WmsdsResponse<int>> AddWorksExecuted(HeisIdentificationDetail heisIdentificationDetail)
        {
            var wmsdResponse = new WmsdsResponse<int>();
            if (heisIdentificationDetail == null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }

            if (heisIdentificationDetail.HeisIdentificationId <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Parent Identification key is empty.";
                return wmsdResponse;
            }
            if (heisIdentificationDetail.Id <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Id is missing.";
                return wmsdResponse;
            }

            try
            {
                using (var _dbContext = new EntityContext())
                {
                    var basicInfoDb = await _dbContext.HeisIdentificationDetails
                        .Where(c => c.Id == heisIdentificationDetail.Id
                        && c.HeisIdentificationId == heisIdentificationDetail.HeisIdentificationId)
                        .SingleOrDefaultAsync();
                    if (basicInfoDb != null)
                    {
                        #region Design Parameters

                        basicInfoDb.DesignApproved = string.IsNullOrEmpty(heisIdentificationDetail.DesignApproved)
                            ? basicInfoDb.DesignApproved : heisIdentificationDetail.DesignApproved;

                        basicInfoDb.DesignApprovalDate = basicInfoDb.DesignApprovalDate == null
                            ? basicInfoDb.DesignApprovalDate : heisIdentificationDetail.DesignApprovalDate;

                        basicInfoDb.TotalApprovedProjectCost = heisIdentificationDetail.TotalApprovedProjectCost <= 0
                            ? basicInfoDb.TotalApprovedProjectCost : heisIdentificationDetail.TotalApprovedProjectCost;
                        
                        basicInfoDb.TotalDynamicHead = heisIdentificationDetail.TotalDynamicHead <= 0
                           ? basicInfoDb.TotalDynamicHead : heisIdentificationDetail.TotalDynamicHead;

                        basicInfoDb.PumpType = string.IsNullOrEmpty(heisIdentificationDetail.PumpType)
                           ? basicInfoDb.PumpType : heisIdentificationDetail.PumpType;

                        basicInfoDb.PumpFlowRate = heisIdentificationDetail.PumpFlowRate <= 0
                           ? basicInfoDb.PumpFlowRate : heisIdentificationDetail.PumpFlowRate;

                        basicInfoDb.PowerSourceEfficiency = heisIdentificationDetail.PowerSourceEfficiency <= 0
                           ? basicInfoDb.PowerSourceEfficiency : heisIdentificationDetail.PowerSourceEfficiency;
                        #endregion

                        #region Work Order
                        basicInfoDb.WorkOrderIssued = string.IsNullOrEmpty(heisIdentificationDetail.WorkOrderIssued)
                            ? basicInfoDb.WorkOrderIssued : heisIdentificationDetail.WorkOrderIssued;
                        basicInfoDb.WorkOrderIssueDate = basicInfoDb.WorkOrderIssueDate == null
                            ? basicInfoDb.WorkOrderIssueDate : heisIdentificationDetail.WorkOrderIssueDate;
                        basicInfoDb.WorkOrderAmount = heisIdentificationDetail.WorkOrderAmount <= 0
                            ? basicInfoDb.WorkOrderAmount : heisIdentificationDetail.WorkOrderAmount;
                        basicInfoDb.EstimatedGovtShare = heisIdentificationDetail.EstimatedGovtShare <= 0
                           ? basicInfoDb.EstimatedGovtShare : heisIdentificationDetail.EstimatedGovtShare;
                        basicInfoDb.EstimatedFamerShare = heisIdentificationDetail.EstimatedFamerShare <= 0
                           ? basicInfoDb.EstimatedFamerShare : heisIdentificationDetail.EstimatedFamerShare;
                        basicInfoDb.FarmerShareInCash = heisIdentificationDetail.FarmerShareInCash <= 0
                           ? basicInfoDb.FarmerShareInCash : heisIdentificationDetail.FarmerShareInCash;
                        basicInfoDb.FarmerShareInKind = heisIdentificationDetail.FarmerShareInKind <= 0
                           ? basicInfoDb.FarmerShareInKind : heisIdentificationDetail.FarmerShareInKind;
                        #endregion

                        #region Material Verification (ICR-I)

                        basicInfoDb.MaterialVerified = string.IsNullOrEmpty(heisIdentificationDetail.MaterialVerified)
                            ? basicInfoDb.MaterialVerified : heisIdentificationDetail.MaterialVerified;

                        basicInfoDb.MaterialVerifiedDate = basicInfoDb.MaterialVerifiedDate == null
                            ? basicInfoDb.MaterialVerifiedDate : heisIdentificationDetail.MaterialVerifiedDate;

                        basicInfoDb.ICRIAmountVerified = heisIdentificationDetail.ICRIAmountVerified <= 0
                            ? basicInfoDb.ICRIAmountVerified : heisIdentificationDetail.ICRIAmountVerified;

                        #endregion

                        #region Commissioning Verification (ICR-II)

                        basicInfoDb.CommissioningVerification = string.IsNullOrEmpty(heisIdentificationDetail.CommissioningVerification)
                            ? basicInfoDb.CommissioningVerification : heisIdentificationDetail.CommissioningVerification;

                        basicInfoDb.CommissioningVerificationDate = basicInfoDb.CommissioningVerificationDate == null
                            ? basicInfoDb.CommissioningVerificationDate : heisIdentificationDetail.CommissioningVerificationDate;

                        basicInfoDb.TotalSchemeCostVerified = heisIdentificationDetail.TotalSchemeCostVerified <= 0
                            ? basicInfoDb.TotalSchemeCostVerified : heisIdentificationDetail.TotalSchemeCostVerified;

                        basicInfoDb.ICRIIAmountVerified = heisIdentificationDetail.ICRIIAmountVerified <= 0
                           ? basicInfoDb.ICRIIAmountVerified : heisIdentificationDetail.ICRIIAmountVerified;

                        #endregion

                        #region Third Installment (ICR-III)

                        basicInfoDb.ICRIIIVerification = string.IsNullOrEmpty(heisIdentificationDetail.ICRIIIVerification)
                            ? basicInfoDb.ICRIIIVerification : heisIdentificationDetail.ICRIIIVerification;

                        basicInfoDb.TotalAmountVerified = heisIdentificationDetail.TotalAmountVerified <= 0
                            ? basicInfoDb.TotalAmountVerified : heisIdentificationDetail.TotalAmountVerified;

                        basicInfoDb.ICRIIIQualifyingDate = basicInfoDb.ICRIIIQualifyingDate == null
                            ? basicInfoDb.ICRIIIQualifyingDate : heisIdentificationDetail.ICRIIIQualifyingDate;
                             
                        #endregion

                        var response = await _dbContext.SaveChangesAsync();
                        if (response > 0)
                        {
                            wmsdResponse.ResponseCode = EnumStatus.Success; ;
                            wmsdResponse.ResponseMessage = "Record has been added successfully.";
                            return wmsdResponse;
                        }
                        else
                        {
                            wmsdResponse.ResponseCode = EnumStatus.Failed; ;
                            wmsdResponse.ResponseMessage = "Failed to update record.";
                            return wmsdResponse;
                        }
                    }
                    else
                    {
                        wmsdResponse.ResponseCode = EnumStatus.NotFound; ;
                        wmsdResponse.ResponseMessage = "No Record found against given Ids.";
                        return wmsdResponse;
                    }

                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.InternalServer;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }
        }
    }
}
