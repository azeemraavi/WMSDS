using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities.WC
{
    public class WcIdentification
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

        public int WaterCourseId { get; set; }
        public string WaterCourseNo { get; set; }

        public int ImprovementYearId { get; set; }
        public string ImprovementYear { get; set; }

        /// <summary>
        /// e.g. Regular/Additional/Irrigation Scheme
        /// </summary>
        public string ImprovementType { get; set; }

        public int AdminDistrictId { get; set; }
        public string AdminDistrictName { get; set; }

        public int AdminTehsilId { get; set; }
        public string AdminTehsilName { get; set; }

        public List<WcIdentificationDetail> WcIdentificationDetails { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
