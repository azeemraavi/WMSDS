using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities.HEIS
{
    public partial class HeisIdentificationDetail
    {
        /// <summary>
        /// Yes/No/Offered
        /// </summary>
        public string DesignApproved { get; set; }
        /// <summary>
        /// Design Approval Date
        /// </summary>
        public DateTime DesignApprovalDate { get; set; }
        /// <summary>
        /// Total Approved Project Cost 
        /// </summary>
        public int TotalApprovedProjectCost  { get; set; }
        /// <summary>
        /// Total Dynamic Head (meter)
        /// </summary>
        public int TotalDynamicHead { get; set; }
        /// <summary>
        /// LOV
        /// Centrifugal/ Submersible
        /// </summary>
        public string PumpType { get; set; }
        /// <summary>
        /// Pump Flow Rate (LPS)
        /// </summary>
        public int PumpFlowRate { get; set; }
        /// <summary>
        /// Power Source Efficiency (%)
        /// </summary>
        public int PowerSourceEfficiency { get; set; }

    }
}
