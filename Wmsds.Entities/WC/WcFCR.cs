using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities.WC
{
    public partial class WcIdentificationDetail
    {
        public string FCRApprovedStatus { get; set; }
        /// <summary>
        /// Get from design parameters, allow changes
        /// </summary>
        public decimal FCRLinedLength { get; set; }
        /// <summary>
        /// Lined (%)
        /// Calculate from Lined length and Total length
        /// </summary>
        public decimal LinedPercentage { get; set; }
        /// <summary>
        /// Total cost of civil works verified (Rs.)
        /// Get from design parameters, allow changes
        /// </summary>
        public decimal TotalCostOfCivilWrkVerfied { get; set; }
        /// <summary>
        /// Labour share verified (Earthen Works) (Rs.)
        /// Get from design parameters, allow changes
        /// </summary>
        public decimal FCREarthenWorks { get; set; }
        /// <summary>
        /// Labour share verified (Masonry Works) (Rs.)
        /// Get from design parameters, allow changes
        /// </summary>
        public decimal FCRMasonryWorks { get; set; }
        /// <summary>
        /// Total scheme cost (Rs.)
        /// Sum above 3 items
        /// </summary>
        public decimal TotalSchemeCost { get; set; }
        /// <summary>
        /// PCPS(PCP Segment size)
        /// Get from design parameters portion, allow changes
        /// </summary>
        public string FCRPcpSegmentSize { get; set; }
        /// <summary>
        /// PCP Segment (Nos.)
        /// Get from design parameters portion, allow changes
        /// </summary>
        public int FCRPcpSegment { get; set; }
        /// <summary>
        /// Pipe for Irrigation Scheme
        /// Options will be Type (PVC/GI/RCC)
        /// Get from design parameters portion, allow changes
        /// </summary>
        public string FCRPipeType { get; set; }
        /// <summary>
        /// Size (Inches) For Irrigation Scheme
        /// Get from design parameters portion, allow changes
        /// </summary>
        public int FCRSizeOfPipeInch { get; set; }
        /// <summary>
        /// Length (M) For Irrigation Scheme
        /// Get from design parameters portion, allow changes
        /// </summary>
        public int FCRLengthOfPipeM { get; set; }
        /// <summary>
        /// Nakkas (Nos.)
        /// Get from design parameters portion, allow changes
        /// </summary>
        public int FCRNakkas { get; set; }
        /// <summary>
        /// Culverts (Nos.)
        /// Get from design parameters portion, allow changes
        /// </summary>
        public int FCRCulverts { get; set; }
        /// <summary>
        /// Buffalo Wallow (Nos.)
        /// Get from design parameters portion, allow changes
        /// </summary>
        public int FCRBuffaloWallow { get; set; }
        /// <summary>
        /// Distribution Box (Nos.)
        /// Get from design parameters portion, allow changes
        /// </summary>
        public int FCRDistributionBox { get; set; }
        /// <summary>
        /// Water Storage Tank (Nos.)
        /// </summary>
        public int FCRWaterStorageTank { get; set; }
        /// <summary>
        /// Drop Structure (Nos.)
        /// Get from design parameters portion, allow changes
        /// </summary>
        public int FCRDropStructure { get; set; }
        /// <summary>
        /// Others (Nos.)
        /// Get from design parameters portion, allow changes
        /// </summary>
        public int FCROthers { get; set; }
    }
}
