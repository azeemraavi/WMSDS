using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wmsds.Dal;
using Wmsds.Entities;
using Wmsds.Entities.ViewModels;
using Wmsds.Entities.WC;

namespace Wmsds.Bll.Watercourse
{
    public class WaterCourseService : IWaterCourseService
    {
        /// <summary>
        /// Add Identification of a water course
        /// </summary>
        /// <param name="wcIdentification"></param>
        /// <returns></returns>
        public async Task<WmsdsResponse<int>> AddIdentification(WcIdentification wcIdentification)
        {
            var wmsdResponse = new WmsdsResponse<int>();
            if (wcIdentification == null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }

            if (wcIdentification.DistrictId <= 0 || string.IsNullOrEmpty(wcIdentification.DistrictName))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "DistrictId is a required field.";
                return wmsdResponse;
            }

            if (wcIdentification.TehsilId <= 0 || string.IsNullOrEmpty(wcIdentification.TehsilName))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "TehsilId is a required field.";
                return wmsdResponse;
            }

            if (wcIdentification.ChannelId <= 0 || string.IsNullOrEmpty(wcIdentification.ChannelName))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "Channel Name is a required field.";
                return wmsdResponse;
            }
            if (wcIdentification.WaterCourseId <= 0 || string.IsNullOrEmpty(wcIdentification.WaterCourseNo))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "WaterCourse No is a required field.";
                return wmsdResponse;
            }

            //Check If WcIdentification Already Exists with Regular Status
            //if (wcIdentification.ImprovementType == EnumWcImprovementType.Regular.ToString() && await IsWcIdentificationAlreadyExist(wcIdentification))
            //{
            //    wmsdResponse.ResponseCode = EnumStatus.AlreadyExist;
            //    wmsdResponse.ResponseMessage = "Watercourse already exists.";
            //    return wmsdResponse;
            //}

            //if (wcIdentification.ImprovementType.Equals(EnumWcImprovementType.Additional.ToString(), StringComparison.OrdinalIgnoreCase))
            //{
            //    if (!await IsWcIdentificationAlreadyExist(wcIdentification))
            //    {
            //        wmsdResponse.ResponseCode = EnumStatus.NotFound;
            //        wmsdResponse.ResponseMessage = "No Watercourse found with Regular Status.";
            //        return wmsdResponse;
            //    }
            //}

            if (await IsWcIdentificationAlreadyExist(wcIdentification))
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
                    _dbContext.WcIdentifications.Add(wcIdentification);
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

        /// <summary>
        /// Get List of Identifications
        /// </summary>
        /// <param name="districtId"></param>
        /// <param name="tehsilId"></param>
        /// <param name="channelId"></param>
        /// <param name="improveYearId"></param>
        /// <returns></returns>
        public async Task<WmsdsResponse<WcIdentificationLightModel>> GetWcIdentifications(int currentPageIndex = 1, int districtId = 0, int tehsilId = 0, int channelId = 0, int improveYearId = 0, string improvementType = null, string channalName = null, string wcNo = null)
        {
            var wcIdentificationOut = new WmsdsResponse<WcIdentificationLightModel>();
            using (var _dbContext = new EntityContext())
            {
                try
                {
                    int maxRows = 10;
                    wcIdentificationOut.Collections = await (from ep in _dbContext.WcIdentifications
                                                   join d in _dbContext.WcIdentificationDetails on ep.Id equals d.WcIdentificationId                                                                                                  
                                                   select new WcIdentificationLightModel
                                                   {
                                                       Id = ep.Id,
                                                       DistrictName = ep.DistrictName,
                                                       DistrictId = ep.DistrictId,
                                                       TehsilName = ep.TehsilName,
                                                       TehsilId = ep.TehsilId,
                                                       ChannelName = ep.ChannelName,
                                                       ChannelId = ep.ChannelId,
                                                       WaterCourseNo = ep.WaterCourseNo,
                                                       ImprovementType = d.ImprovementType,
                                                       ImprovementYear = d.ImprovementYear
                                                   }).Where(c => districtId == 0 || c.DistrictId == districtId)
                                        .Where(c => tehsilId == 0 || c.TehsilId == tehsilId)
                                        .Where(c => channelId == 0 || c.ChannelId == channelId)
                                        .Where(c => channalName == null || c.ChannelName == channalName)
                                        .Where(c => wcNo == null || c.WaterCourseNo == wcNo)
                                        .OrderBy(x => x.Id)
                                          .Skip((currentPageIndex - 1) * maxRows)
                              .Take(maxRows).ToListAsync();

                    var rowCount = await (from ep in _dbContext.WcIdentifications
                                                   join d in _dbContext.WcIdentificationDetails on ep.Id equals d.WcIdentificationId                                                   
                                                   select new WcIdentificationLightModel
                                                   {
                                                       Id = ep.Id,
                                                       DistrictName = ep.DistrictName,
                                                       DistrictId = ep.DistrictId,
                                                       TehsilName = ep.TehsilName,
                                                       TehsilId = ep.TehsilId,
                                                       ChannelName = ep.ChannelName,
                                                       ChannelId = ep.ChannelId,
                                                       WaterCourseNo = ep.WaterCourseNo,
                                                       ImprovementType = d.ImprovementType,
                                                       ImprovementYear = d.ImprovementYear
                                                   }).Where(c => districtId == 0 || c.DistrictId == districtId)
                                        .Where(c => tehsilId == 0 || c.TehsilId == tehsilId)
                                        .Where(c => channelId == 0 || c.ChannelId == channelId)
                                        .Where(c => channalName == null || c.ChannelName == channalName)
                                        .Where(c => wcNo == null || c.WaterCourseNo == wcNo)
                                        .CountAsync();


                    wcIdentificationOut.TotalRecords = rowCount;
                    double pageCount = (double)((decimal)rowCount / Convert.ToDecimal(maxRows));
                    wcIdentificationOut.PageCount = (int)Math.Ceiling(pageCount);
                    wcIdentificationOut.CurrentPageIndex = currentPageIndex;
                    return wcIdentificationOut;
                }
                catch (Exception ex)
                {
                    return null;
                }
              
            }
        }

        /// <summary>
        /// Get WcIdentification by Id
        /// </summary>
        /// <param name="wcIdentificationId"></param>
        /// <returns></returns>
        public async Task<WmsdsResponse<WcIdentification>> GetIdentificationById(int wcIdentificationId)
        {
            var wmsdResponse = new WmsdsResponse<WcIdentification>();
            if (wcIdentificationId <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.RequiredField;
                wmsdResponse.ResponseMessage = "Water Course Id is missing.";
                return wmsdResponse;
            }

            using (var _dbContext = new EntityContext())
            {
                try
                {
                    var wcIdentification = await _dbContext.WcIdentifications
                                    .Where(c => c.Id == wcIdentificationId)
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

        /// <summary>
        /// Is watercourse exists with Regular status
        /// </summary>
        /// <param name="wcIdentification"></param>
        /// <returns></returns>
        private async Task<bool> IsWcIdentificationAlreadyExist(WcIdentification wcIdentification)
        {
            using (var _dbContext = new EntityContext())
            {
                var wcIdentificationDb = await _dbContext.WcIdentifications
                                    .Where(c => c.DistrictId == wcIdentification.DistrictId)
                                        .Where(c => c.TehsilId == wcIdentification.TehsilId)
                                        .Where(c => c.ChannelId == wcIdentification.ChannelId)
                                        .Where(c => c.WaterCourseId == wcIdentification.WaterCourseId)
                                        .FirstOrDefaultAsync();
                return wcIdentificationDb == null ? false : true;
            }
        }

        //TODO: Add Step wise status in detail
        /// <summary>
        /// Step1: Add BasicInformation
        /// </summary>
        /// <param name="wcIdentificationDetail"></param>
        /// <returns></returns>        
        public async Task<WmsdsResponse<WcIdentificationDetail>> AddBasicInformation(WcIdentificationDetail wcIdentificationDetail)
        {
            var wmsdResponse = new WmsdsResponse<WcIdentificationDetail>();
            if (wcIdentificationDetail == null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }

            if (wcIdentificationDetail.WcIdentificationId <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Parent Identification key is empty.";
                return wmsdResponse;
            }

            try
            {
                using (var _dbContext = new EntityContext())
                {
                    _dbContext.WcIdentificationDetails.Add(wcIdentificationDetail);
                    var response = await _dbContext.SaveChangesAsync();

                    if (response > 0)
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Success;
                        wmsdResponse.DataObject = new WcIdentificationDetail();
                        wmsdResponse.DataObject.Id = wcIdentificationDetail.Id;
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
        /// <summary>
        /// Step1: Update BasicInformation
        /// </summary>
        /// <param name="wcIdentificationDetail"></param>
        /// <returns></returns>
        public async Task<WmsdsResponse<int>> UpdateBasicInformation(WcIdentificationDetail wcIdentificationDetail)
        {
            var wmsdResponse = new WmsdsResponse<int>();
            if (wcIdentificationDetail == null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }

            if (wcIdentificationDetail.WcIdentificationId <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Parent Identification key is empty.";
                return wmsdResponse;
            }
            if (wcIdentificationDetail.Id <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Id is missing.";
                return wmsdResponse;
            }

            try
            {
                using (var _dbContext = new EntityContext())
                {
                    var basicInfoDb = await _dbContext.WcIdentificationDetails
                        .Where(c => c.Id == wcIdentificationDetail.Id
                        && c.WcIdentificationId == wcIdentificationDetail.WcIdentificationId)
                        .SingleOrDefaultAsync();
                    if (basicInfoDb != null)
                    {

                        basicInfoDb.ProjectId = wcIdentificationDetail.ProjectId <= 0
                            ? basicInfoDb.ProjectId : wcIdentificationDetail.ProjectId;

                        basicInfoDb.ProjectName = string.IsNullOrEmpty(wcIdentificationDetail.ProjectName)
                            ? basicInfoDb.ProjectName : wcIdentificationDetail.ProjectName;

                        basicInfoDb.VillageName = string.IsNullOrEmpty(wcIdentificationDetail.VillageName)
                            ? basicInfoDb.VillageName : wcIdentificationDetail.VillageName;

                        basicInfoDb.UC = string.IsNullOrEmpty(wcIdentificationDetail.UC)
                             ? basicInfoDb.UC : wcIdentificationDetail.UC;

                        basicInfoDb.GCA = wcIdentificationDetail.GCA <= 0
                              ? basicInfoDb.GCA : wcIdentificationDetail.GCA;

                        basicInfoDb.CCA = wcIdentificationDetail.CCA <= 0
                              ? basicInfoDb.CCA : wcIdentificationDetail.CCA;

                        basicInfoDb.SanctionedDischargeLPS = wcIdentificationDetail.SanctionedDischargeLPS <= 0
                             ? basicInfoDb.SanctionedDischargeLPS : wcIdentificationDetail.SanctionedDischargeLPS;

                        basicInfoDb.DesignDischargeLPS = wcIdentificationDetail.DesignDischargeLPS <= 0
                             ? basicInfoDb.DesignDischargeLPS : wcIdentificationDetail.DesignDischargeLPS;

                        basicInfoDb.MoghaType = string.IsNullOrEmpty(wcIdentificationDetail.MoghaType)
                             ? basicInfoDb.MoghaType : wcIdentificationDetail.MoghaType;

                        basicInfoDb.GroundwaterQuality = string.IsNullOrEmpty(wcIdentificationDetail.GroundwaterQuality)
                              ? basicInfoDb.GroundwaterQuality : wcIdentificationDetail.GroundwaterQuality;

                        basicInfoDb.WUAChairman = string.IsNullOrEmpty(wcIdentificationDetail.WUAChairman)
                               ? basicInfoDb.WUAChairman : wcIdentificationDetail.WUAChairman;

                        basicInfoDb.NoOfBeneficiaries = wcIdentificationDetail.NoOfBeneficiaries <= 0
                                ? basicInfoDb.NoOfBeneficiaries : wcIdentificationDetail.NoOfBeneficiaries;

                        basicInfoDb.TotalLengthM = wcIdentificationDetail.TotalLengthM <= 0
                                ? basicInfoDb.TotalLengthM : wcIdentificationDetail.TotalLengthM;

                        basicInfoDb.PreviousLinedLengthM = wcIdentificationDetail.PreviousLinedLengthM <= 0
                                ? basicInfoDb.PreviousLinedLengthM : wcIdentificationDetail.PreviousLinedLengthM;

                        basicInfoDb.Latitude = wcIdentificationDetail.Latitude <= 0
                                ? basicInfoDb.Latitude : wcIdentificationDetail.Latitude;

                        basicInfoDb.Longitude = wcIdentificationDetail.Longitude <= 0
                                ? basicInfoDb.Longitude : wcIdentificationDetail.Longitude;

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
        /// Step2: Add/Update Design Parameters
        /// </summary>
        /// <param name="wcIdentificationDetail"></param>
        /// <returns></returns>
        public async Task<WmsdsResponse<int>> AddUpdateDesignParameters(WcIdentificationDetail wcIdentificationDetail)
        {
            var wmsdResponse = new WmsdsResponse<int>();
            if (wcIdentificationDetail == null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }

            if (wcIdentificationDetail.WcIdentificationId <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Parent Identification key is empty.";
                return wmsdResponse;
            }
            if (wcIdentificationDetail.Id <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Id is missing.";
                return wmsdResponse;
            }

            try
            {
                using (var _dbContext = new EntityContext())
                {
                    var basicInfoDb = await _dbContext.WcIdentificationDetails
                        .Where(c => c.Id == wcIdentificationDetail.Id
                        && c.WcIdentificationId == wcIdentificationDetail.WcIdentificationId)
                        .SingleOrDefaultAsync();
                    if (basicInfoDb != null)
                    {

                        basicInfoDb.DesignApproved = string.IsNullOrEmpty(wcIdentificationDetail.DesignApproved)
                            ? basicInfoDb.DesignApproved : wcIdentificationDetail.DesignApproved;

                        basicInfoDb.MaterialCost = wcIdentificationDetail.MaterialCost <= 0
                            ? basicInfoDb.MaterialCost : wcIdentificationDetail.MaterialCost;

                        basicInfoDb.EarthenWorks = wcIdentificationDetail.EarthenWorks <= 0
                            ? basicInfoDb.EarthenWorks : wcIdentificationDetail.EarthenWorks;

                        basicInfoDb.MasonryWorks = wcIdentificationDetail.MasonryWorks <= 0
                             ? basicInfoDb.MasonryWorks : wcIdentificationDetail.MasonryWorks;

                        basicInfoDb.EarthenLengthM = wcIdentificationDetail.EarthenLengthM <= 0
                              ? basicInfoDb.EarthenLengthM : wcIdentificationDetail.EarthenLengthM;

                        basicInfoDb.LiningLengthM = wcIdentificationDetail.LiningLengthM <= 0
                              ? basicInfoDb.LiningLengthM : wcIdentificationDetail.LiningLengthM;

                        basicInfoDb.PcpSegmentSize = string.IsNullOrEmpty(wcIdentificationDetail.PcpSegmentSize)
                             ? basicInfoDb.PcpSegmentSize : wcIdentificationDetail.PcpSegmentSize;

                        basicInfoDb.PcpSegment = wcIdentificationDetail.PcpSegment <= 0
                             ? basicInfoDb.PcpSegment : wcIdentificationDetail.PcpSegment;

                        basicInfoDb.PipeType = string.IsNullOrEmpty(wcIdentificationDetail.PipeType)
                             ? basicInfoDb.PipeType : wcIdentificationDetail.PipeType;

                        basicInfoDb.SizeOfPipeInch = wcIdentificationDetail.SizeOfPipeInch <= 0
                              ? basicInfoDb.SizeOfPipeInch : wcIdentificationDetail.SizeOfPipeInch;

                        basicInfoDb.LengthOfPipeM = wcIdentificationDetail.LengthOfPipeM <= 0
                               ? basicInfoDb.LengthOfPipeM : wcIdentificationDetail.LengthOfPipeM;

                        basicInfoDb.Nakkas = wcIdentificationDetail.Nakkas <= 0
                                ? basicInfoDb.Nakkas : wcIdentificationDetail.Nakkas;

                        basicInfoDb.Culverts = wcIdentificationDetail.Culverts <= 0
                                ? basicInfoDb.Culverts : wcIdentificationDetail.Culverts;

                        basicInfoDb.BuffaloWallow = wcIdentificationDetail.BuffaloWallow <= 0
                                ? basicInfoDb.BuffaloWallow : wcIdentificationDetail.BuffaloWallow;

                        basicInfoDb.DistributionBox = wcIdentificationDetail.DistributionBox <= 0
                                ? basicInfoDb.DistributionBox : wcIdentificationDetail.DistributionBox;

                        basicInfoDb.WaterStorageTank = wcIdentificationDetail.WaterStorageTank <= 0
                                ? basicInfoDb.WaterStorageTank : wcIdentificationDetail.WaterStorageTank;

                        basicInfoDb.DropStructure = wcIdentificationDetail.DropStructure <= 0
                                ? basicInfoDb.DropStructure : wcIdentificationDetail.DropStructure;

                        basicInfoDb.Others = wcIdentificationDetail.Others <= 0
                                ? basicInfoDb.Others : wcIdentificationDetail.Others;

                        var response = await _dbContext.SaveChangesAsync();
                        if (response > 0)
                        {
                            wmsdResponse.ResponseCode = EnumStatus.Success; ;
                            wmsdResponse.ResponseMessage = "Record has been updated successfully.";
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
        /// Step3: Add/Update ICR1 & ICR2
        /// </summary>
        /// <param name="wcIdentificationDetail"></param>
        /// <returns></returns>
        public async Task<WmsdsResponse<int>> AddUpdateICR1AndICR2(WcIdentificationDetail wcIdentificationDetail)
        {
            var wmsdResponse = new WmsdsResponse<int>();
            if (wcIdentificationDetail == null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }

            if (wcIdentificationDetail.WcIdentificationId <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Parent Identification key is empty.";
                return wmsdResponse;
            }
            if (wcIdentificationDetail.Id <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Id is missing.";
                return wmsdResponse;
            }

            try
            {
                using (var _dbContext = new EntityContext())
                {
                    var basicInfoDb = await _dbContext.WcIdentificationDetails
                        .Where(c => c.Id == wcIdentificationDetail.Id
                        && c.WcIdentificationId == wcIdentificationDetail.WcIdentificationId)
                        .SingleOrDefaultAsync();
                    if (basicInfoDb != null)
                    {

                        basicInfoDb.ICR1ApprovedStatus = string.IsNullOrEmpty(wcIdentificationDetail.ICR1ApprovedStatus)
                            ? basicInfoDb.ICR1ApprovedStatus : wcIdentificationDetail.ICR1ApprovedStatus;

                        basicInfoDb.ICR1ReleasedAmount = wcIdentificationDetail.ICR1ReleasedAmount <= 0
                            ? basicInfoDb.ICR1ReleasedAmount : wcIdentificationDetail.ICR1ReleasedAmount;

                        basicInfoDb.ICR2ApprovedStatus = string.IsNullOrEmpty(wcIdentificationDetail.ICR2ApprovedStatus)
                            ? basicInfoDb.ICR2ApprovedStatus : wcIdentificationDetail.ICR2ApprovedStatus;

                        basicInfoDb.ICR2ReleasedAmount = wcIdentificationDetail.ICR2ReleasedAmount <= 0
                             ? basicInfoDb.ICR2ReleasedAmount : wcIdentificationDetail.ICR2ReleasedAmount;


                        var response = await _dbContext.SaveChangesAsync();
                        if (response > 0)
                        {
                            wmsdResponse.ResponseCode = EnumStatus.Success; ;
                            wmsdResponse.ResponseMessage = "Record has been updated successfully.";
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
        /// Step4: Add Update FCR 
        /// </summary>
        /// <param name="wcIdentificationDetail"></param>
        /// <returns></returns>
        public async Task<WmsdsResponse<int>> AddUpdateFCR(WcIdentificationDetail wcIdentificationDetail)
        {
            var wmsdResponse = new WmsdsResponse<int>();
            if (wcIdentificationDetail == null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }

            if (wcIdentificationDetail.WcIdentificationId <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Parent Identification key is empty.";
                return wmsdResponse;
            }
            if (wcIdentificationDetail.Id <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Id is missing.";
                return wmsdResponse;
            }

            try
            {
                using (var _dbContext = new EntityContext())
                {
                    var basicInfoDb = await _dbContext.WcIdentificationDetails
                        .Where(c => c.Id == wcIdentificationDetail.Id
                        && c.WcIdentificationId == wcIdentificationDetail.WcIdentificationId)
                        .SingleOrDefaultAsync();
                    if (basicInfoDb != null)
                    {

                        basicInfoDb.FCRApprovedStatus = string.IsNullOrEmpty(wcIdentificationDetail.FCRApprovedStatus)
                            ? basicInfoDb.FCRApprovedStatus : wcIdentificationDetail.FCRApprovedStatus;

                        basicInfoDb.FCRLinedLength = wcIdentificationDetail.FCRLinedLength <= 0
                            ? basicInfoDb.FCRLinedLength : wcIdentificationDetail.FCRLinedLength;

                        basicInfoDb.LinedPercentage = wcIdentificationDetail.LinedPercentage <= 0
                            ? basicInfoDb.LinedPercentage : wcIdentificationDetail.LinedPercentage;

                        basicInfoDb.TotalCostOfCivilWrkVerfied = wcIdentificationDetail.TotalCostOfCivilWrkVerfied <= 0
                             ? basicInfoDb.TotalCostOfCivilWrkVerfied : wcIdentificationDetail.TotalCostOfCivilWrkVerfied;


                        basicInfoDb.FCREarthenWorks = wcIdentificationDetail.FCREarthenWorks <= 0
                             ? basicInfoDb.FCREarthenWorks : wcIdentificationDetail.FCREarthenWorks;

                        basicInfoDb.FCRMasonryWorks = wcIdentificationDetail.FCRMasonryWorks <= 0
                             ? basicInfoDb.FCRMasonryWorks : wcIdentificationDetail.FCRMasonryWorks;

                        basicInfoDb.TotalSchemeCost = wcIdentificationDetail.TotalSchemeCost <= 0
                             ? basicInfoDb.TotalSchemeCost : wcIdentificationDetail.TotalSchemeCost;

                        basicInfoDb.FCRPcpSegmentSize = string.IsNullOrEmpty(wcIdentificationDetail.FCRPcpSegmentSize)
                             ? basicInfoDb.FCRPcpSegmentSize : wcIdentificationDetail.FCRPcpSegmentSize;

                        basicInfoDb.FCRPcpSegment = wcIdentificationDetail.FCRPcpSegment <= 0
                             ? basicInfoDb.FCRPcpSegment : wcIdentificationDetail.FCRPcpSegment;

                        basicInfoDb.FCRPipeType = string.IsNullOrEmpty(wcIdentificationDetail.FCRPipeType)
                             ? basicInfoDb.FCRPipeType : wcIdentificationDetail.FCRPipeType;

                        basicInfoDb.FCRSizeOfPipeInch = wcIdentificationDetail.FCRSizeOfPipeInch <= 0
                             ? basicInfoDb.FCRSizeOfPipeInch : wcIdentificationDetail.FCRSizeOfPipeInch;

                        basicInfoDb.FCRLengthOfPipeM = wcIdentificationDetail.FCRLengthOfPipeM <= 0
                             ? basicInfoDb.FCRLengthOfPipeM : wcIdentificationDetail.FCRLengthOfPipeM;


                        basicInfoDb.FCRNakkas = wcIdentificationDetail.FCRNakkas <= 0
                             ? basicInfoDb.FCRNakkas : wcIdentificationDetail.FCRNakkas;

                        basicInfoDb.FCRCulverts = wcIdentificationDetail.FCRCulverts <= 0
                             ? basicInfoDb.FCRCulverts : wcIdentificationDetail.FCRCulverts;

                        basicInfoDb.FCRBuffaloWallow = wcIdentificationDetail.FCRBuffaloWallow <= 0
                             ? basicInfoDb.FCRBuffaloWallow : wcIdentificationDetail.FCRBuffaloWallow;

                        basicInfoDb.FCRDistributionBox = wcIdentificationDetail.FCRDistributionBox <= 0
                             ? basicInfoDb.FCRDistributionBox : wcIdentificationDetail.FCRDistributionBox;

                        basicInfoDb.FCRWaterStorageTank = wcIdentificationDetail.FCRWaterStorageTank <= 0
                             ? basicInfoDb.FCRWaterStorageTank : wcIdentificationDetail.FCRWaterStorageTank;

                        basicInfoDb.FCRDropStructure = wcIdentificationDetail.FCRDropStructure <= 0
                             ? basicInfoDb.FCRDropStructure : wcIdentificationDetail.FCRDropStructure;

                        basicInfoDb.FCROthers = wcIdentificationDetail.FCROthers <= 0
                             ? basicInfoDb.FCROthers : wcIdentificationDetail.FCROthers;


                        var response = await _dbContext.SaveChangesAsync();
                        if (response > 0)
                        {
                            wmsdResponse.ResponseCode = EnumStatus.Success; ;
                            wmsdResponse.ResponseMessage = "Record has been updated successfully.";
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

        public async Task<WmsdsResponse<WcIdentificationDetail>> GetIdentificationDetailById(int wcIdentifictionDetailId)
        {
            var wmsdResponse = new WmsdsResponse<WcIdentificationDetail>();
            if (wcIdentifictionDetailId <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.RequiredField;
                wmsdResponse.ResponseMessage = "Water Course Id is missing.";
                return wmsdResponse;
            }

            using (var _dbContext = new EntityContext())
            {
                try
                {
                    var wcIdentification = await _dbContext.WcIdentificationDetails
                                    .Where(c => c.Id == wcIdentifictionDetailId)
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

        public async Task<WmsdsResponse<WcIdentificationDetail>> GetIdentificationDetailByMasterId(int wcIdentificationId)
        {
            var wmsdResponse = new WmsdsResponse<WcIdentificationDetail>();
            if (wcIdentificationId <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.RequiredField;
                wmsdResponse.ResponseMessage = "Water Course Id is missing.";
                return wmsdResponse;
            }

            using (var _dbContext = new EntityContext())
            {
                try
                {
                    var wcIdentification = await _dbContext.WcIdentificationDetails
                                    .Where(c => c.WcIdentificationId == wcIdentificationId)
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
    }
}
