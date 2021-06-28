using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities.ViewModels
{
    public class WatercoureseStatusDto
    {
        public int TotalWaterCourse { get; set; }
        public int ImprovedWaterCourse { get; set; }
        public int UnImprovedWaterCourse { get; set; }
    }
    public class KeyValueDto
    {
        public string name { get; set; }
        public int y { get; set; }
    }
    public class LengthOfWcDto
    {
        public int TotalLength { get; set; }
        public int Earthen { get; set; }
        public int Lined { get; set; }
    }
    public class ImplicationFinancialDto
    {
        public decimal TotalCost { get; set; }
        public decimal GovtAssistance { get; set; }
        public decimal FarmerContribution { get; set; }
    }

    public class DistrictWiseWcImprDto
    {
        public string District { get; set; }
        public string ImprovementType { get; set; }
        public int Value { get; set; }
    }

    public class DistrictWiseDto
    {
      
        public int ImprovedWaterCourse { get; set; }
        public int UnImprovedWaterCourse { get; set; }
        //Improvement Status
        public List<KeyValueDto> KeyValueDtos { get; set; }

        //Total Earthan
        public int TotalLength { get; set; }
        public int Earthen { get; set; }
        public int Lined { get; set; }

        //Tehsil Wise
        public List<TehsilWiseDto> TehsilWiseDtos { get; set; }
    }
    public class TehsilWiseDto
    {
        public string Tehsil { get; set; }
        public string ImprovementType { get; set; }
        public int Value { get; set; }
    }
}
