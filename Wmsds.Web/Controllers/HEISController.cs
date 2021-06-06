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
using Wmsds.Entities.HEIS;
using System.Globalization;

namespace Wmsds.Web.Controllers
{
    public class HEISController : Controller
    {
        public async Task<ActionResult> Index()
        {
            IHeisService heisCourseService = new HeisService();
            List<HeisIdentification> HeisIdentifications = await heisCourseService.GetHeisIdentifications();
            return View(HeisIdentifications);
        }

        [HttpPost]
        public async Task<ActionResult> Index(FormCollection formCollection)
        {

            int District = Convert.ToInt16(formCollection["ddlDistrict"]);
            int Tehsil = Convert.ToInt16(formCollection["ddlTehsil"]);
            //string ImprovementType = formCollection["ddlImprovementType"]+"";
            //int ImprovementYear = Convert.ToInt16(formCollection["ddlImprovementYear"]);

            IHeisService heisCourseService = new HeisService();
            List<HeisIdentification> HeisIdentifications = await heisCourseService.GetHeisIdentifications(0, 0, "","");
            return View(HeisIdentifications);
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

        public async Task<JsonResult> LoaCropNameByCategory(string CategoryName)
        {
            List<string> cropNames = new List<string>();
            if (CategoryName == "Row Crops")
            {
                cropNames.Add("Cotton");
                cropNames.Add("Maize");
                cropNames.Add("Sugarcane");
                cropNames.Add("Sugar Beat");
                cropNames.Add("Water Melon");
                cropNames.Add("Sunflower");
                cropNames.Add("Tobacco");
                cropNames.Add("Strawberry");
                cropNames.Add("Flowers");
                cropNames.Add("Nursery");
                cropNames.Add("Other Row Crops");
            }
            else if (CategoryName == "Field Crops") //
            {
                cropNames.Add("Wheat");
                cropNames.Add("Fodder");
                cropNames.Add("Gram");
                cropNames.Add("Peanuts");
                cropNames.Add("Other Field Crop");
            }
            else if (CategoryName == "Orchard") //
            {
                cropNames.Add("Citrus");
                cropNames.Add("Mango");
                cropNames.Add("Guava");
                cropNames.Add("Berries");
                cropNames.Add("Olive");
                cropNames.Add("Dates");
                cropNames.Add("Lichi");
                cropNames.Add("Grapes");
                cropNames.Add("Banana");
                cropNames.Add("Apicot");
                cropNames.Add("Almond");
                cropNames.Add("Pomegranate");
                cropNames.Add("Loquat");
                cropNames.Add("Jojoba");
                cropNames.Add("Papaya");
                cropNames.Add("Jatropha");
                cropNames.Add("Karonda");
                cropNames.Add("Peach");
                cropNames.Add("Palm");
                cropNames.Add("Other Orchards");

            }
            else if (CategoryName == "Vegetables") //
            {
                cropNames.Add("Cucumber");
                cropNames.Add("Capsicum");
                cropNames.Add("Tomato");
                cropNames.Add("Chillies");
                cropNames.Add("Potato");
                cropNames.Add("Cauliflower");
                cropNames.Add("Bitter Gourd");
                cropNames.Add("Brinjal");
                cropNames.Add("Carrot");
                cropNames.Add("Lady finger");
                cropNames.Add("Onion");
                cropNames.Add("Pumpkin");
                cropNames.Add("Other vegetables");

            }

            return Json(cropNames, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> DataEntry(int Id, bool IsNewEntry)
        {
            Wmsds.Web.Models.HEISDataEntryDto heisDataEntryDto = new Wmsds.Web.Models.HEISDataEntryDto();

            IHeisService heisCourseService = new HeisService();
            var responce = await heisCourseService.GetHeisIdentificationById(Id);
            if (responce.ResponseCode == EnumStatus.Success)
            {
                heisDataEntryDto.HeisIdentification = responce.DataObject;
            }
            else
            {
                heisDataEntryDto.HeisIdentification = new HeisIdentification();
            }


            var detailResponse = await heisCourseService.GetHeisIdentificationDetailByMasterId(Id);
            if (IsNewEntry == false && detailResponse.ResponseCode == EnumStatus.Success)
            {
                heisDataEntryDto.HeisIdentificationDetail = detailResponse.DataObject;
            }
            else
            {
                heisDataEntryDto.HeisIdentificationDetail = new HeisIdentificationDetail();
            }
            return View(heisDataEntryDto);
        }
        public async Task<JsonResult> GetHeisListing(HeisIdentification model)
        {
            IHeisService heisCourseService = new HeisService();
            List<HeisIdentification> HeisIdentifications = await heisCourseService.GetHeisIdentifications(model.DistrictId, model.TehsilId, model.FarmerName, model.FarmerCNIC);
            return Json(HeisIdentifications, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> AddHeisIdentification(HeisIdentification model)
        {
            try
            {
                //model.CreatedAt = DateTime.Now;
                IHeisService heisCourseService = new HeisService();
                var response = await heisCourseService.AddHeisIdentification(model);
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
        public async Task<ActionResult> AddUpdateHEISDetails(FormCollection formCollection)
        {
            try
            {
                int HeisIdentificationId = Convert.ToInt16(formCollection["hdHeisIdentificationIdBI"]);
                int HeisIdentificationDetailId = Convert.ToInt16(formCollection["hdHeisIdentificationDetailsIdBI"]);

                double Latitude = Convert.ToDouble(formCollection["txtLatitude"]);
                double Longitude = Convert.ToDouble(formCollection["txtLongitude"]);
                string InstallationType = formCollection["ddlInstallationType"] + "".Replace("--Select--", "");
                string FiscalYear = formCollection["ddlFiscalYear"] + "".Replace("--Select--", "");
                string SystemType = formCollection["ddlSystemType"] + "".Replace("--Select--", "");
                string SSCName = formCollection["ddlSSCName"] + "".Replace("--Select--", "");
                string ProjectName = formCollection["ddlProjectName"] + "".Replace("--Select--", "");
                string VillageName = formCollection["txtVillageName"];
                string ContactNumber = formCollection["txtContactNumber"];
                int SchemeArea = Convert.ToInt16(formCollection["txtSchemeArea"]);
                string WaterSource = formCollection["ddlWaterSource"] + "".Replace("--Select--", "");
                string WaterQuality = formCollection["ddlWaterQuality"] + "".Replace("--Select--", "");
                string PowerSource = formCollection["ddlPowerSource"] + "".Replace("--Select--", "");
                string CropCategory = formCollection["ddlCropCategory"] + "".Replace("--Select--", "");
                string CropName = formCollection["ddlCropName"] + "".Replace("--Select--", "");
                int PlansPerAcre = Convert.ToInt16(formCollection["txtPlansPerAcre"]);
                string SystemClassification = formCollection["ddlSystemClassification"] + "".Replace("--Select--", "");
                string WorkOrderIssued = formCollection["ddlWorkOrderIssued"] + "".Replace("--Select--", "");
                DateTime DesignApprovalDate = DateTime.ParseExact(formCollection["txtDesignApprovalDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                int TotalApprovedProjectCost = Convert.ToInt16(formCollection["txtTotalApprovedProjectCost"]);
                int TotalDynamicHead = Convert.ToInt16(formCollection["txtTotalDynamicHead"]);
                string PumpType = formCollection["ddlPumpType"] + "".Replace("--Select--", "");
                int PumpFlowRate = Convert.ToInt16(formCollection["txtPumpFlowRate"]);
                int PowerSourceEfficiency = Convert.ToInt16(formCollection["txtPowerSourceEfficiency"]);

                HeisIdentificationDetail heisIdentificationDetail = new HeisIdentificationDetail();
                heisIdentificationDetail.HeisIdentificationId = HeisIdentificationId;
                heisIdentificationDetail.Id = HeisIdentificationDetailId;

                heisIdentificationDetail.Latitude = Latitude;
                heisIdentificationDetail.Longitude = Longitude;
                heisIdentificationDetail.InstallationType = InstallationType;
                heisIdentificationDetail.FiscalYear = FiscalYear;
                heisIdentificationDetail.SystemType = SystemType;
                heisIdentificationDetail.SSCName = SSCName;
                heisIdentificationDetail.ProjectName = ProjectName;
                heisIdentificationDetail.VillageName = VillageName;
                heisIdentificationDetail.ContactNumber = ContactNumber;
                heisIdentificationDetail.SchemeArea = SchemeArea;
                heisIdentificationDetail.WaterSource = WaterSource;
                heisIdentificationDetail.WaterQuality = WaterQuality;
                heisIdentificationDetail.PowerSource = PowerSource;
                heisIdentificationDetail.CropCategory = CropCategory;
                heisIdentificationDetail.CropName = CropName;
                heisIdentificationDetail.PlansPerAcre = PlansPerAcre;
                heisIdentificationDetail.SystemClassification = SystemClassification;
                heisIdentificationDetail.WorkOrderIssued = WorkOrderIssued;
                heisIdentificationDetail.DesignApprovalDate = DesignApprovalDate;
                heisIdentificationDetail.TotalApprovedProjectCost = TotalApprovedProjectCost;
                heisIdentificationDetail.TotalDynamicHead = TotalDynamicHead;
                heisIdentificationDetail.PumpType = PumpType;
                heisIdentificationDetail.PumpFlowRate = PumpFlowRate;
                heisIdentificationDetail.PowerSourceEfficiency = PowerSourceEfficiency;


                IHeisService heisCourseService = new HeisService();
                var response = new WmsdsResponse<HeisIdentificationDetail>();
                var responseUpdate = new WmsdsResponse<int>();
                if (HeisIdentificationDetailId == 0)
                {
                    response = await heisCourseService.AddHeisBasicInformation(heisIdentificationDetail);
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    responseUpdate = await heisCourseService.UpdateHeisBasicInformation(heisIdentificationDetail);
                    return Json(responseUpdate, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { FormId = 0, HttpStatusCode = HttpStatusCode.NotImplemented });
            }
        }
        public async Task<ActionResult> AddUpdateWorksExecuted(FormCollection formCollection)
        {
            try
            {
                int HeisIdentificationId = Convert.ToInt16(formCollection["hdHeisIdentificationIdWorksExecuted"] + "");
                int HeisIdentificationDetailId = Convert.ToInt16(formCollection["hdHeisIdentificationDetailsIdWorksExecuted"] + "");

                string WorkOrderIssued = formCollection["ddlWorkOrderIssued"] + "".Replace("--Select--", "");
                DateTime WorkOrderIssueDate = DateTime.ParseExact(formCollection["txtWorkOrderIssueDate"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                decimal WorkOrderAmount = Convert.ToDecimal(formCollection["txtWorkOrderAmount"]);
                decimal EstimatedGovtShare = Convert.ToDecimal(formCollection["txtEstimatedGovtShare"]);
                decimal EstimatedFamerShare = Convert.ToDecimal(formCollection["txtEstimatedFamerShare"]);
                decimal FarmerShareInCash = Convert.ToDecimal(formCollection["txtFarmerShareInCash"]);
                decimal FarmerShareInKind = Convert.ToDecimal(formCollection["txtFarmerShareInKind"]);
                string MaterialVerified = formCollection["ddlMaterialVerified"] + "".Replace("--Select--", "");
                DateTime MaterialVerifiedDate = DateTime.ParseExact(formCollection["txtMaterialVerifiedDate"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                int ICRIAmountVerified = Convert.ToInt16(formCollection["txtICRIAmountVerified"]);
                string CommissioningVerification = formCollection["ddlCommissioningVerification"] + "".Replace("--Select--", "");
                DateTime CommissioningVerificationDate = DateTime.ParseExact(formCollection["txtCommissioningVerificationDate"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                int TotalSchemeCostVerified = Convert.ToInt16(formCollection["txtTotalSchemeCostVerified"]);
                int ICRIIAmountVerified = Convert.ToInt16(formCollection["txtICRIIAmountVerified"]);
                string ICRIIIVerification = formCollection["ddlICRIIIVerification"] + "".Replace("--Select--", "");
                int TotalAmountVerified = Convert.ToInt16(formCollection["txtTotalAmountVerified"]);
                DateTime ICRIIIQualifyingDate = DateTime.ParseExact(formCollection["txtICRIIIQualifyingDate"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                HeisIdentificationDetail heisIdentificationDetail = new HeisIdentificationDetail();

                heisIdentificationDetail.HeisIdentificationId = HeisIdentificationId;
                heisIdentificationDetail.Id = HeisIdentificationDetailId;
                heisIdentificationDetail.WorkOrderIssued = WorkOrderIssued;
                heisIdentificationDetail.WorkOrderIssueDate = WorkOrderIssueDate;
                heisIdentificationDetail.WorkOrderAmount = WorkOrderAmount;
                heisIdentificationDetail.EstimatedGovtShare = EstimatedGovtShare;
                heisIdentificationDetail.EstimatedFamerShare = EstimatedFamerShare;
                heisIdentificationDetail.FarmerShareInCash = FarmerShareInCash;
                heisIdentificationDetail.FarmerShareInKind = FarmerShareInKind;
                heisIdentificationDetail.MaterialVerified = MaterialVerified;
                heisIdentificationDetail.MaterialVerifiedDate = MaterialVerifiedDate;
                heisIdentificationDetail.ICRIAmountVerified = ICRIAmountVerified;
                heisIdentificationDetail.CommissioningVerification = CommissioningVerification;
                heisIdentificationDetail.CommissioningVerificationDate = CommissioningVerificationDate;
                heisIdentificationDetail.TotalSchemeCostVerified = TotalSchemeCostVerified;
                heisIdentificationDetail.ICRIIAmountVerified = ICRIIAmountVerified;
                heisIdentificationDetail.ICRIIIVerification = ICRIIIVerification;
                heisIdentificationDetail.TotalAmountVerified = TotalAmountVerified;
                heisIdentificationDetail.ICRIIIQualifyingDate = ICRIIIQualifyingDate;
                

                IHeisService heisService = new HeisService();
                var response = await heisService.AddWorksExecuted(heisIdentificationDetail);
                if (response.ResponseCode == Entities.EnumStatus.Success)
                {

                }

                return Json(new { FormId = 0, HttpStatusCode = (int)HttpStatusCode.OK });
            }
            catch (Exception ex)
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