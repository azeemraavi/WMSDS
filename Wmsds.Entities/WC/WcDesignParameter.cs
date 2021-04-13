using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities.WC
{
    public partial class WcIdentificationDetail
    {
        /// <summary>
        /// Yes/No/Offered
        /// </summary>
        public string DesignApproved { get; set; }
        /// <summary>
        /// TS Amount/Material Cost (Rs.) PKR
        /// </summary>
        public decimal MaterialCost{ get; set; }

        /// <summary>
        /// Labour share (Earthen Works) (Rs.)
        /// </summary>
        public decimal EarthenWorks { get; set; }
        /// <summary>
        /// Labour share (Masonry Works) (Rs.)
        /// </summary>
        public decimal MasonryWorks { get; set; }
        /// <summary>
        /// M stands for Meter
        /// </summary>
        public decimal EarthenLengthM { get; set; }
        /// <summary>
        /// Lining Length (m)
        /// </summary>
        public decimal LiningLengthM { get; set; }
        /// <summary>
        /// PCPS(PCP Segment size)
        /// </summary>
        public string PcpSegmentSize { get; set; }
        /// <summary>
        /// PCP Segment (Nos.)
        /// </summary>
        public int PcpSegment { get; set; }
        /// <summary>
        /// Pipe for Irrigation Scheme
        /// Options will be Type (PVC/GI/RCC)
        /// </summary>
        public string PipeType { get; set; }
        /// <summary>
        /// Size (Inches) For Irrigation Scheme
        /// </summary>
        public int SizeOfPipeInch { get; set; }
        /// <summary>
        /// Length (M) For Irrigation Scheme
        /// </summary>
        public int LengthOfPipeM { get; set; }

        /// <summary>
        /// Nakkas (Nos.)
        /// </summary>
        public int Nakkas { get; set; }
        /// <summary>
        /// Culverts (Nos.)
        /// </summary>
        public int Culverts { get; set; }
        /// <summary>
        /// Buffalo Wallow (Nos.)
        /// </summary>
        public int BuffaloWallow { get; set; }
        /// <summary>
        /// Distribution Box (Nos.)
        /// </summary>
        public int DistributionBox { get; set; }
        /// <summary>
        /// Water Storage Tank (Nos.)
        /// </summary>
        public int WaterStorageTank { get; set; }
        /// <summary>
        /// Drop Structure (Nos.)
        /// </summary>
        public int DropStructure { get; set; }
        /// <summary>
        /// Others (Nos.)
        /// </summary>
        public int Others { get; set; }
    }
}
