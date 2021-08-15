using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities.ViewModels
{
    public class WcIdentificationLightModel
    {
        public int Id { get; set; }

        public int DivisionId { get; set; }
        public string DivisionName { get; set; }

        public int DistrictId { get; set; }
        public string DistrictName { get; set; }

        public int TehsilId { get; set; }
        public string TehsilName { get; set; }

        public int ChannelId { get; set; }
        public string ChannelName { get; set; }

        public string ImprovementType { get; set; }
        public string ImprovementYear { get; set; }

        public int WaterCourseId { get; set; }
        public string WaterCourseNo { get; set; }
    }

    public class HeisIdentificationLightModel
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }

        public int TehsilId { get; set; }
        public string TehsilName { get; set; }

        public string FarmerName { get; set; }
        public string FarmerCNIC { get; set; }
        public string VillageName { get; set; }
        public string ContactNo { get; set; }
    }
}
