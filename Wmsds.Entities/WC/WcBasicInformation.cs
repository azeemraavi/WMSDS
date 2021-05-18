using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities.WC
{
    public partial class WcIdentificationDetail
    {
        public int Id { get; set; }

        public int WcIdentificationId { get; set; }
        public WcIdentification WcIdentification { get; set; }


        #region Basic Information
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }


        public int ImprovementYearId { get; set; }
        public string ImprovementYear { get; set; }

        /// <summary>
        /// e.g. Regular/Additional/Irrigation Scheme
        /// </summary>
        public string ImprovementType { get; set; }



        public string VillageName { get; set; }
        public string UC { get; set; }
        /// <summary>
        /// Gross Command Area
        /// </summary>
        public decimal GCA { get; set; }
        /// <summary>
        /// Canal Command Area
        /// </summary>
        public decimal CCA { get; set; }
        /// <summary>
        /// LPS stands for Letter Per Second
        /// Total Water that can pass through water course
        /// </summary>
        public decimal SanctionedDischargeLPS { get; set; }
        /// <summary>
        /// Total Water Can be passed after design
        /// </summary>
        public decimal DesignDischargeLPS { get; set; }
        /// <summary>
        /// AOSM,Scratchily,Pipe,Open Flume
        /// </summary>
        public string MoghaType { get; set; }
        /// <summary>
        /// Fresh/Saline
        /// </summary>
        public string GroundwaterQuality { get; set; }
        public string WUAChairman { get; set; }
        public string ChairmanContactNo { get; set; }
        public int NoOfBeneficiaries { get; set; }
        /// <summary>
        /// M stand for meter
        /// </summary>
        public decimal TotalLengthM { get; set; }
        /// <summary>
        /// M stands for meter
        /// </summary>
        public decimal PreviousLinedLengthM { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        #endregion
    }
}
