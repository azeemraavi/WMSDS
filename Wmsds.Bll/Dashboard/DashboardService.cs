using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wmsds.Dal;
using Wmsds.Entities;
using Wmsds.Entities.ViewModels;

namespace Wmsds.Bll.Dashboard
{
    public class DashboardService : IDashboardService
    {
        
        public async Task<WmsdsResponse<WatercoureseStatusDto>> GetStatusOfWatercourses()
        {
            var wmsdResponse = new WmsdsResponse<WatercoureseStatusDto>();
            wmsdResponse.DataObject = new WatercoureseStatusDto();
            try
            {
                using (var dbContext = new EntityContext())
                {
                    var improvedWcCount = await (from wc in dbContext.WcIdentifications
                                                 from wd in dbContext.WcIdentificationDetails
                                                 where wc.Id == wd.WcIdentificationId
                                                 select wc.Id).CountAsync();

                    var totalWcCourse = await dbContext.WaterCourses.CountAsync();

                    
                    wmsdResponse.DataObject.TotalWaterCourse = totalWcCourse;
                    wmsdResponse.DataObject.ImprovedWaterCourse = improvedWcCount;
                    wmsdResponse.DataObject.UnImprovedWaterCourse = totalWcCourse - improvedWcCount;
                    wmsdResponse.ResponseCode = EnumStatus.Success;
                    wmsdResponse.ResponseMessage = "Success";
                    return wmsdResponse;
                  

                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.Error;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }

        }
        public async Task<WmsdsResponse<WcImprovementStatusDto>> GetWcImprovementStatus()
        {

            var wmsdResponse = new WmsdsResponse<WcImprovementStatusDto>();
            wmsdResponse.DataObject = new WcImprovementStatusDto();
            try
            {

                using (var dbContext = new EntityContext())
                {
                    //var wcImprovementStatus = await (from wd in dbContext.WcIdentificationDetails
                    //                                 group wd by new { wd.ImprovementType } into g
                    //                                 select new KeyValueDto
                    //                                 {
                    //                                     name = g.Key.ImprovementType,
                    //                                     y = g.Count(x => x.WcIdentificationId != 0)
                    //                                 }).ToListAsync();

                    var regularCount = await dbContext.WcIdentificationDetails.Where(x=>x.ImprovementType== "Regular").CountAsync();
                    var addlCount = await dbContext.WcIdentificationDetails.Where(x => x.ImprovementType == "Addl.").CountAsync();
                    wmsdResponse.DataObject.RegularCount = regularCount;
                    wmsdResponse.DataObject.AddlCount = addlCount;
                    wmsdResponse.ResponseCode = EnumStatus.Success;                   
                    return wmsdResponse;
                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.Error;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }
        }
        public async Task<WmsdsResponse<LengthOfWcDto>> GetLengthOfImprovedWc()
        {
            var wmsdResponse = new WmsdsResponse<LengthOfWcDto>();
            wmsdResponse.DataObject = new LengthOfWcDto();
            try
            {

                using (var dbContext = new EntityContext())
                {
                    var earthenTotalLen = await dbContext.WcIdentificationDetails.SumAsync(x => x.EarthenLengthM);
                    var linedTotalLen = await dbContext.WcIdentificationDetails.SumAsync(x => x.LiningLengthM);

                    wmsdResponse.ResponseCode = EnumStatus.Success;
                    wmsdResponse.DataObject.Earthen= (int)earthenTotalLen/1000;//KM
                    //wmsdResponse.DataObject.Earthen = wmsdResponse.DataObject.Earthen / 1000;//"000KM"

                    wmsdResponse.DataObject.Lined = (int)linedTotalLen / 1000;//KM
                    //wmsdResponse.DataObject.Lined = wmsdResponse.DataObject.Lined / 1000;//"000KM"

                    wmsdResponse.DataObject.TotalLength = wmsdResponse.DataObject.Earthen + wmsdResponse.DataObject.Lined;
                    return wmsdResponse;
                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.Error;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }
        }
        public async Task<WmsdsResponse<ImplicationFinancialDto>> GetImplicationFinancial()
        {
            var wmsdResponse = new WmsdsResponse<ImplicationFinancialDto>();
            wmsdResponse.DataObject = new ImplicationFinancialDto();
            try
            {

                using (var dbContext = new EntityContext())
                {
                    //TODO: discuss about farmer share
                    var totalGovtShare = await dbContext.WcIdentificationDetails.SumAsync(x => x.TotalCostOfCivilWrkVerfied);//Govt
                    var linedTotalLen = await dbContext.WcIdentificationDetails.SumAsync(x => x.LiningLengthM);

                    wmsdResponse.ResponseCode = EnumStatus.Success;
                    //wmsdResponse.DataObject.GovtAssistance = (int)earthenTotalLen / 1000;//KM
                    //wmsdResponse.DataObject.Earthen = wmsdResponse.DataObject.Earthen / 1000;//"000KM"

                    //wmsdResponse.DataObject.Lined = (int)linedTotalLen / 1000;//KM
                    //wmsdResponse.DataObject.Lined = wmsdResponse.DataObject.Lined / 1000;//"000KM"

                    //wmsdResponse.DataObject.TotalLength = wmsdResponse.DataObject.Earthen + wmsdResponse.DataObject.Lined;
                    return wmsdResponse;
                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.Error;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }
        }
        public async Task<WmsdsResponse<DistrictWiseWcImprDto>> GetDistrictWiseWcImprStatus()
        {

            var wmsdResponse = new WmsdsResponse<DistrictWiseWcImprDto>();
            wmsdResponse.Collections = new List<DistrictWiseWcImprDto>();
            
            try
            {
                using (var dbContext = new EntityContext())
                {
                    var distWiseData = await (from wc in dbContext.WcIdentifications
                                              from wd in dbContext.WcIdentificationDetails
                                              where wc.Id == wd.WcIdentificationId
                                              group wd by new { wc.DistrictName, wd.ImprovementType } into g
                                              select new 
                                              {
                                                  District = g.Key.DistrictName,
                                                  ImprovementType = g.Key.ImprovementType,
                                                  Value = g.Count(x => x.WcIdentificationId != 0)
                                              }).ToListAsync();

                    
                    foreach (var district in distWiseData.GroupBy(x=>x.District).ToList())
                    {
                        var districtWiseWcDto = new DistrictWiseWcImprDto();
                        districtWiseWcDto.DistrictData = new List<DistrictWiseWcImprDtoLight>();
                        districtWiseWcDto.District = district.Key;
                        foreach (var item in district)
                        {
                            districtWiseWcDto.DistrictData.Add(new DistrictWiseWcImprDtoLight 
                            { ImprovementType = item.ImprovementType, Value = item.Value });
                        }
                        wmsdResponse.Collections.Add(districtWiseWcDto);
                    }
                    wmsdResponse.ResponseCode = EnumStatus.Success;
                   // wmsdResponse.Collections = distWiseData;
                    return wmsdResponse;
                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.Error;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }
        }
        public async Task<WmsdsResponse<KeyValueDto>> GetYearWiseWcImprStatus()
        {
            var wmsdResponse = new WmsdsResponse<KeyValueDto>();
            wmsdResponse.DataObject = new KeyValueDto();
            try
            {

                using (var dbContext = new EntityContext())
                {
                    var wcImprovementStatus = await (from wd in dbContext.WcIdentificationDetails
                                                     group wd by new { wd.ImprovementYear } into g
                                                     select new KeyValueDto
                                                     {
                                                         name = g.Key.ImprovementYear,
                                                         y = g.Count(x => x.WcIdentificationId != 0)
                                                     }).ToListAsync();

                    wmsdResponse.ResponseCode = EnumStatus.Success;
                    wmsdResponse.Collections = wcImprovementStatus;
                    return wmsdResponse;
                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.Error;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }
        }

        public async Task<WmsdsResponse<DistrictWiseDto>> GetDistrictWiseOverview(int districtId)
        {
            var wmsdResponse = new WmsdsResponse<DistrictWiseDto>();
            wmsdResponse.DataObject = new DistrictWiseDto();
            try
            {
                using (var dbContext = new EntityContext())
                {
                    var improvedWcCount = await (from wc in dbContext.WcIdentifications
                                                 from wd in dbContext.WcIdentificationDetails
                                                 where wc.Id == wd.WcIdentificationId
                                                 && wc.DistrictId==districtId
                                                 select wc.Id).CountAsync();

                    var totalWcCourse = await dbContext.WaterCourses.Where(wc=> wc.DistrictId == districtId).CountAsync();
                    wmsdResponse.DataObject.ImprovedWaterCourse = improvedWcCount;
                    wmsdResponse.DataObject.UnImprovedWaterCourse = totalWcCourse - improvedWcCount;

                    //Watercourse Improvement Status
                    var wcImprovementStatus = await (from wc in dbContext.WcIdentifications
                                                     from wd in dbContext.WcIdentificationDetails
                                                     where wc.Id == wd.WcIdentificationId
                                                     && wc.DistrictId==districtId
                                                     group wd by new { wd.ImprovementType } into g
                                                     select new KeyValueDto
                                                     {
                                                         name = g.Key.ImprovementType,
                                                         y = g.Count(x => x.WcIdentificationId != 0)
                                                     }).ToListAsync();

                    wmsdResponse.DataObject.KeyValueDtos = wcImprovementStatus;

                    //Get Improved Length
                    var wcImprovementLength = await (from wc in dbContext.WcIdentifications
                                                     from wd in dbContext.WcIdentificationDetails
                                                     where wc.Id == wd.WcIdentificationId
                                                     && wc.DistrictId == districtId                                                     
                                                     select wd).ToListAsync();

                    var earthenTotalLen = wcImprovementLength.Sum(x => x.EarthenLengthM);
                    var linedTotalLen = wcImprovementLength.Sum(x => x.LiningLengthM);

                    wmsdResponse.ResponseCode = EnumStatus.Success;
                    wmsdResponse.DataObject.Earthen = (int)earthenTotalLen / 1000;//KM
                    //wmsdResponse.DataObject.Earthen = wmsdResponse.DataObject.Earthen / 1000;//"000KM"

                    wmsdResponse.DataObject.Lined = (int)linedTotalLen / 1000;//KM
                  //  wmsdResponse.DataObject.Lined = wmsdResponse.DataObject.Lined / 1000;//"000KM"

                    wmsdResponse.DataObject.TotalLength = wmsdResponse.DataObject.Earthen + wmsdResponse.DataObject.Lined;

                    //Tehsil wise Watercourse Improvement
                    var tehsilWiseData = await (from wc in dbContext.WcIdentifications
                                              from wd in dbContext.WcIdentificationDetails
                                              where wc.Id == wd.WcIdentificationId
                                              && wc.DistrictId == districtId
                                              group wd by new { wc.TehsilName, wc.TehsilId } into g
                                              select new
                                              {
                                                  Tehsil = g.Key.TehsilName,
                                                  TehsilId = g.Key.TehsilId
                                              }).ToListAsync();


                    
                    wmsdResponse.DataObject.TehsilWiseDtos = new List<TehsilWiseDto>();
                    foreach (var tehsilWise in tehsilWiseData)
                    {
                        var tehsilWiseDto = new TehsilWiseDto();
                        var tehsilWiseCount = await (from wc in dbContext.WcIdentifications
                                                     from wd in dbContext.WcIdentificationDetails
                                                     where wc.Id == wd.WcIdentificationId
                                                     && wc.DistrictId == districtId
                                                     && wc.TehsilId==tehsilWise.TehsilId
                                                     select wc.Id).CountAsync();

                        var totalWcCourseThWise = await dbContext.WaterCourses
                            .Where(wc => wc.DistrictId == districtId && wc.TehsilId == tehsilWise.TehsilId).CountAsync();
                        tehsilWiseDto.ImprovedWaterCourse = tehsilWiseCount;
                        tehsilWiseDto.UnImprovedWaterCourse = totalWcCourseThWise - tehsilWiseCount;
                        tehsilWiseDto.Tehsil = tehsilWise.Tehsil;
                        wmsdResponse.DataObject.TehsilWiseDtos.Add(tehsilWiseDto);
                    }
                    

                    wmsdResponse.ResponseCode = EnumStatus.Success;
                    wmsdResponse.ResponseMessage = "Success";
                    return wmsdResponse;


                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.Error;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }
        }
    }
}
