using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wmsds.Entities.WC;
using Wmsds.Bll.Common;
using System.Threading.Tasks;
using Wmsds.Entities.ViewModels;
using Wmsds.Entities.Common;
using System.Net;
using Wmsds.Bll.Watercourse;
using Wmsds.Entities;
using Wmsds.Web.Models;

namespace Wmsds.Web.Controllers
{
    public class HEISController : Controller
    {
        public async Task<ActionResult> Index()
        {
            IWaterCourseService waterCourseService = new WaterCourseService();
            List<WcIdentification> WcIdentifications = await waterCourseService.GetWcIdentifications();
            return View(WcIdentifications);
        }

        [HttpPost]
        public async Task<ActionResult> Index(FormCollection formCollection)
        {

            int District = Convert.ToInt16(formCollection["ddlDistrict"]);
            int Tehsil = Convert.ToInt16(formCollection["ddlTehsil"]);
            //string ImprovementType = formCollection["ddlImprovementType"]+"";
            //int ImprovementYear = Convert.ToInt16(formCollection["ddlImprovementYear"]);

            IWaterCourseService waterCourseService = new WaterCourseService();
            List<WcIdentification> WcIdentifications = await waterCourseService.GetWcIdentifications(District, Tehsil, 0, 0);
            //return Json(WcIdentifications, JsonRequestBehavior.AllowGet);
            return View(WcIdentifications);
        }

        public async Task<JsonResult> LoadAllFilterData()
        {
            ICommonService commonService = new CommonService();
            WcIdentificationDto wcIdentificationDto = new WcIdentificationDto();
            wcIdentificationDto.Divisions = new List<Entities.Common.Division>();
            wcIdentificationDto.Divisions = await commonService.GetAllDivisions();

            wcIdentificationDto.Districts = new List<Entities.Common.District>();
            wcIdentificationDto.Districts = await commonService.GetAllDistricts();

            wcIdentificationDto.Tehsils = new List<Entities.Common.Tehsil>();
            wcIdentificationDto.Tehsils = await commonService.GetAllTehsils();

            wcIdentificationDto.FinancialYears = new List<Entities.Common.FinancialYear>();
            wcIdentificationDto.FinancialYears = await commonService.GetAllFinancialYears();

            //todo
            //wcIdentificationDto.WaterCourses = new List<Entities.Common.WaterCourse>();
            //wcIdentificationDto.WaterCourses = await commonService.GetAll();

            return Json(wcIdentificationDto, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> DataEntry(int Id, bool IsNewEntry)
        {
            Wmsds.Web.Models.WcDataEntryDto wcDataEntryDto = new Wmsds.Web.Models.WcDataEntryDto();

            IWaterCourseService waterCourseService = new WaterCourseService();
            var responce = await waterCourseService.GetIdentificationById(Id);
            if (responce.ResponseCode == EnumStatus.Success)
            {
                wcDataEntryDto.WcIdentification = responce.DataObject;
            }
            else
            {
                wcDataEntryDto.WcIdentification = new WcIdentification();
            }


            var detailResponse = await waterCourseService.GetIdentificationDetailByMasterId(Id);
            if (IsNewEntry == false && detailResponse.ResponseCode == EnumStatus.Success)
            {
                wcDataEntryDto.WcIdentificationDetail = detailResponse.DataObject;
            }
            else
            {
                wcDataEntryDto.WcIdentificationDetail = new WcIdentificationDetail();
            }
            return View(wcDataEntryDto);
        }

        public async Task<JsonResult> GetChannelsByDistAndTehId(int districtId, int tehsilId)
        {
            ICommonService commonService = new CommonService();
            List<Channel> Channels = await commonService.GetChannelsByDistAndTehId(districtId, tehsilId);
            return Json(Channels, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetWaterCourses(int districtId, int tehsilId, int channelId)
        {
            ICommonService commonService = new CommonService();
            List<WaterCourse> WaterCourses = await commonService.GetWaterCourses(districtId, tehsilId, channelId);
            return Json(WaterCourses, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetWcListing(WcIdentification model)
        {
            IWaterCourseService waterCourseService = new WaterCourseService();
            List<WcIdentification> WcIdentifications = await waterCourseService.GetWcIdentifications(model.DistrictId, model.TehsilId, model.ChannelId);
            return Json(WcIdentifications, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> AddWatercourseIdentification(WcIdentification model)
        {
            try
            {
                model.CreatedAt = DateTime.Now;
                IWaterCourseService waterCourseService = new WaterCourseService();
                var response = await waterCourseService.AddIdentification(model);
                if (response.ResponseCode == Entities.EnumStatus.Success)
                {

                }

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { FormId = 0, HttpStatusCode = HttpStatusCode.NotImplemented });
            }
        }

        public async Task<ActionResult> AddUpdateWatercourseDetails(FormCollection formCollection)
        {
            try
            {
                int WcIdentificationId = Convert.ToInt16(formCollection["hdWcIdentificationIdBI"]);
                int WcIdentificationDetailId = Convert.ToInt16(formCollection["hdWcIdentificationDetailsIdBI"]);

                int ImprovementYearId = Convert.ToInt16(formCollection["ddlImprovementYear"]);
                string ImprovementType = formCollection["ddlImprovementType"] + "";

                double Latitude = Convert.ToDouble(formCollection["txtLatitude"]);
                double Longitude = Convert.ToDouble(formCollection["txtLongitude"]);
                string ProjectName = formCollection["ddlProjectName"] + "".Replace("--Select--", "");
                string VillageName = formCollection["txtVillageName"] + "";
                string UC = formCollection["txtUC"] + "";
                decimal GCA = Convert.ToDecimal(formCollection["txtGCA"]);
                decimal CCA = Convert.ToDecimal(formCollection["txtCCA"]);
                decimal SanctionedDischarge = Convert.ToDecimal(formCollection["txtSanctionedDischarge"]);
                decimal DesignDischarge = Convert.ToDecimal(formCollection["txtDesignDischarge"]);
                string MoghaType = formCollection["ddlMoghaType"] + "".Replace("--Select--", "");
                string GroundwaterQuality = formCollection["ddlGroundwaterQuality"] + "".Replace("--Select--", "");
                string NameOfWUAChairman = formCollection["txtNameOfWUAChairman"] + "";
                string ContactOfWUAChairman = formCollection["txtContactOfWUAChairman"] + "";
                int NoOfBeneficiaries = Convert.ToInt16(formCollection["txtNoOfBeneficiaries"]);
                decimal TotalLength = Convert.ToDecimal(formCollection["txtTotalLength"]);

                WcIdentificationDetail wcIdentificationDetail = new WcIdentificationDetail();
                wcIdentificationDetail.WcIdentificationId = WcIdentificationId;
                wcIdentificationDetail.Id = WcIdentificationDetailId;
                wcIdentificationDetail.Latitude = Latitude;
                wcIdentificationDetail.Longitude = Longitude;
                wcIdentificationDetail.ProjectName = ProjectName;
                wcIdentificationDetail.VillageName = VillageName;
                wcIdentificationDetail.UC = UC;
                wcIdentificationDetail.GCA = GCA;
                wcIdentificationDetail.CCA = CCA;
                wcIdentificationDetail.SanctionedDischargeLPS = SanctionedDischarge;
                wcIdentificationDetail.DesignDischargeLPS = DesignDischarge;
                wcIdentificationDetail.MoghaType = MoghaType;
                wcIdentificationDetail.GroundwaterQuality = GroundwaterQuality;
                wcIdentificationDetail.WUAChairman = NameOfWUAChairman;
                wcIdentificationDetail.ChairmanContactNo = ContactOfWUAChairman;
                wcIdentificationDetail.TotalLengthM = TotalLength;
                wcIdentificationDetail.ImprovementYearId = ImprovementYearId;
                wcIdentificationDetail.ImprovementType = ImprovementType;



                IWaterCourseService waterCourseService = new WaterCourseService();
                var response = new WmsdsResponse<WcIdentificationDetail>();
                var responseUpdate = new WmsdsResponse<int>();
                if (WcIdentificationDetailId == 0)
                {
                    response = await waterCourseService.AddBasicInformation(wcIdentificationDetail);
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    responseUpdate = await waterCourseService.UpdateBasicInformation(wcIdentificationDetail);
                    return Json(responseUpdate, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json(new { FormId = 0, HttpStatusCode = HttpStatusCode.NotImplemented });
            }
        }

        public async Task<ActionResult> AddUpdateDesignParameters(FormCollection formCollection)
        {
            try
            {
                int WcIdentificationId = Convert.ToInt16(formCollection["hdWcIdentificationIdDP"]);
                int WcIdentificationDetailId = Convert.ToInt16(formCollection["hdWcIdentificationDetailsIdDP"]);

                string DesignApproved = formCollection["ddlDesignApproved"] + "".Replace("--Select--", "");
                decimal TSAmount = Convert.ToDecimal(formCollection["txtTSAmount"]);
                decimal LabourShareEarthenWorks = Convert.ToDecimal(formCollection["txtLabourShareEarthenWorks"]);
                decimal LabourShareMasonryWorks = Convert.ToDecimal(formCollection["txtLabourShareMasonryWorks"]);
                string LiningType = formCollection["ddlLiningType"] + "".Replace("--Select--", "");
                decimal EarthenLength = Convert.ToDecimal(formCollection["txtEarthenLength"]);
                decimal LiningLength = Convert.ToDecimal(formCollection["txtLiningLength"]);
                string PCPSegmentSize = formCollection["ddlPCPSegmentSize"] + "".Replace("--Select--", "");
                int PCPSegment = Convert.ToInt16(formCollection["txtPCPSegment"]);
                int Nakkas = Convert.ToInt16(formCollection["txtNakkas"]);
                int Culverts = Convert.ToInt16(formCollection["txtCulverts"]);
                int BuffaloWallow = Convert.ToInt16(formCollection["txtBuffaloWallow"]);
                int DistributionBox = Convert.ToInt16(formCollection["txtDistributionBox"]);
                int WaterStorageTank = Convert.ToInt16(formCollection["txtWaterStorageTank"]);
                int DropStructure = Convert.ToInt16(formCollection["txtDropStructure"]);
                int Others = Convert.ToInt16(formCollection["txtOthers"]);

                WcIdentificationDetail wcIdentificationDetail = new WcIdentificationDetail();
                wcIdentificationDetail.WcIdentificationId = WcIdentificationId;
                wcIdentificationDetail.Id = WcIdentificationDetailId;
                wcIdentificationDetail.DesignApproved = DesignApproved;
                wcIdentificationDetail.MaterialCost = TSAmount;
                wcIdentificationDetail.EarthenWorks = LabourShareEarthenWorks;
                wcIdentificationDetail.MasonryWorks = LabourShareMasonryWorks;
                //wcIdentificationDetail.LiningType = LiningType;
                wcIdentificationDetail.EarthenLengthM = EarthenLength;
                wcIdentificationDetail.LiningLengthM = LiningLength;
                wcIdentificationDetail.PcpSegmentSize = PCPSegmentSize;
                wcIdentificationDetail.PcpSegment = PCPSegment;
                wcIdentificationDetail.Nakkas = Nakkas;
                wcIdentificationDetail.Culverts = Culverts;
                wcIdentificationDetail.BuffaloWallow = BuffaloWallow;
                wcIdentificationDetail.DistributionBox = DistributionBox;
                wcIdentificationDetail.WaterStorageTank = WaterStorageTank;
                wcIdentificationDetail.DropStructure = DropStructure;
                wcIdentificationDetail.Others = Others;

                IWaterCourseService waterCourseService = new WaterCourseService();
                var response = await waterCourseService.AddUpdateDesignParameters(wcIdentificationDetail);
                if (response.ResponseCode == Entities.EnumStatus.Success)
                {

                }

                return Json(new { FormId = 0, HttpStatusCode = (int)HttpStatusCode.OK });
            }
            catch (Exception)
            {
                return Json(new { FormId = 0, HttpStatusCode = HttpStatusCode.NotImplemented });
            }
        }

        public async Task<ActionResult> AddUpdateICR1AndICR2(FormCollection formCollection)
        {
            try
            {
                int WcIdentificationId = Convert.ToInt16(formCollection["hdWcIdentificationIdICR"]);
                int WcIdentificationDetailId = Convert.ToInt16(formCollection["hdWcIdentificationDetailsIdICR"]);

                string ICR1Approved = formCollection["ddlICR1"] + "".Replace("--Select--", "");
                decimal ICR1ReleasedAmount = Convert.ToDecimal(formCollection["txtICR1ReleasedAmount"]);
                string ICR2Approved = formCollection["ddlICR2"] + "".Replace("--Select--", "");
                decimal ICR2ReleasedAmount = Convert.ToDecimal(formCollection["txtICR2ReleasedAmount"]);

                WcIdentificationDetail wcIdentificationDetail = new WcIdentificationDetail();
                wcIdentificationDetail.WcIdentificationId = WcIdentificationId;
                wcIdentificationDetail.Id = WcIdentificationDetailId;
                wcIdentificationDetail.ICR1ApprovedStatus = ICR1Approved;
                wcIdentificationDetail.ICR1ReleasedAmount = ICR1ReleasedAmount;
                wcIdentificationDetail.ICR2ApprovedStatus = ICR2Approved;
                wcIdentificationDetail.ICR2ReleasedAmount = ICR2ReleasedAmount;

                IWaterCourseService waterCourseService = new WaterCourseService();
                var response = await waterCourseService.AddUpdateICR1AndICR2(wcIdentificationDetail);
                if (response.ResponseCode == Entities.EnumStatus.Success)
                {

                }

                return Json(new { FormId = 0, HttpStatusCode = (int)HttpStatusCode.OK });
            }
            catch (Exception)
            {
                return Json(new { FormId = 0, HttpStatusCode = HttpStatusCode.NotImplemented });
            }
        }

        public async Task<ActionResult> AddUpdateFCR(FormCollection formCollection)
        {
            try
            {
                int WcIdentificationId = Convert.ToInt16(formCollection["hdWcIdentificationIdFCR"] + "");
                int WcIdentificationDetailId = Convert.ToInt16(formCollection["hdWcIdentificationDetailsIdFCR"] + "");

                string FCRApproved = formCollection["ddlFCRApproved"] + "".Replace("--Select--", "");
                decimal FCRLinedlength = Convert.ToDecimal(formCollection["txtFCRLinedlength"]);
                decimal FCRLined = Convert.ToDecimal(formCollection["txtFCRLined"]);
                int FCRTotalCostOfCivilWorksVerified = Convert.ToInt16(formCollection["txtFCRTotalCostOfCivilWorksVerified"]);
                int FCRLabourShareVerifiedEarthen = Convert.ToInt16(formCollection["txtFCRLabourShareVerifiedEarthen"]);
                int FCRLabourShareVerifiedMasonry = Convert.ToInt16(formCollection["txtFCRLabourShareVerifiedMasonry"]);
                int FCRTotalSchemeCost = Convert.ToInt16(formCollection["txtFCRTotalSchemeCost"]);
                string FCRPCPSegmentSize = formCollection["ddlFCRPCPSegmentSize"] + "".Replace("--Select--", "");

                int FCRPCPSegmentNo = Convert.ToInt16(formCollection["txtFCRPCPSegmentNo"]);
                int FCRNakkas = Convert.ToInt16(formCollection["txtFCRNakkas"]);
                int FCRCulverts = Convert.ToInt16(formCollection["txtFCRCulverts"]);
                int FCRBuffaloWallow = Convert.ToInt16(formCollection["txtFCRBuffaloWallow"]);
                int FCRDistributionBox = Convert.ToInt16(formCollection["txtFCRDistributionBox"]);
                int FCRWaterStorageTank = Convert.ToInt16(formCollection["txtFCRWaterStorageTank"]);
                int FCRDropStructure = Convert.ToInt16(formCollection["txtFCRDropStructure"]);
                int FCROthers = Convert.ToInt16(formCollection["txtFCROthers"]);

                WcIdentificationDetail wcIdentificationDetail = new WcIdentificationDetail();
                wcIdentificationDetail.WcIdentificationId = WcIdentificationId;
                wcIdentificationDetail.Id = WcIdentificationDetailId;
                wcIdentificationDetail.FCRApprovedStatus = FCRApproved;
                wcIdentificationDetail.FCRLinedLength = FCRLinedlength;
                wcIdentificationDetail.LinedPercentage = FCRLined;
                wcIdentificationDetail.TotalCostOfCivilWrkVerfied = FCRTotalCostOfCivilWorksVerified;
                wcIdentificationDetail.FCREarthenWorks = FCRLabourShareVerifiedEarthen;
                wcIdentificationDetail.FCRMasonryWorks = FCRLabourShareVerifiedMasonry;
                wcIdentificationDetail.TotalSchemeCost = FCRTotalSchemeCost;
                wcIdentificationDetail.FCRPcpSegmentSize = FCRPCPSegmentSize;
                wcIdentificationDetail.FCRPcpSegment = FCRPCPSegmentNo;
                wcIdentificationDetail.FCRNakkas = FCRNakkas;
                wcIdentificationDetail.FCRCulverts = FCRCulverts;
                wcIdentificationDetail.FCRBuffaloWallow = FCRBuffaloWallow;
                wcIdentificationDetail.FCRDistributionBox = FCRDistributionBox;
                wcIdentificationDetail.FCRWaterStorageTank = FCRWaterStorageTank;
                wcIdentificationDetail.FCRDropStructure = FCRDropStructure;
                wcIdentificationDetail.FCROthers = FCROthers;


                IWaterCourseService waterCourseService = new WaterCourseService();
                var response = await waterCourseService.AddUpdateFCR(wcIdentificationDetail);
                if (response.ResponseCode == Entities.EnumStatus.Success)
                {

                }

                return Json(new { FormId = 0, HttpStatusCode = (int)HttpStatusCode.OK });
            }
            catch (Exception)
            {
                return Json(new { FormId = 0, HttpStatusCode = HttpStatusCode.NotImplemented });
            }
        }


        public async Task<JsonResult> LoadFinancialYears()
        {
            ICommonService commonService = new CommonService();
            WcIdentificationDto wcIdentificationDto = new WcIdentificationDto();

            wcIdentificationDto.FinancialYears = new List<Entities.Common.FinancialYear>();
            wcIdentificationDto.FinancialYears = await commonService.GetAllFinancialYears();

            return Json(wcIdentificationDto, JsonRequestBehavior.AllowGet);
        }
    }
}