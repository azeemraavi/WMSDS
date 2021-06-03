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
        /// LOV or Radio Button
        /// Yes/No
        /// </summary>
        public string WorkOrderIssued { get; set; }
        /// <summary>
        /// Work Order Issue Date
        /// </summary>
        public DateTime? WorkOrderIssueDate { get; set; }
        /// <summary>
        /// Work Order Amount (Rs.)
        /// </summary>
        public decimal WorkOrderAmount { get; set; }
        /// <summary>
        /// Estimated Govt. Share (Rs.)
        /// </summary>
        public decimal EstimatedGovtShare { get; set; }
        /// <summary>
        /// Estimated Famer Share (Rs.)
        /// </summary>
        public decimal EstimatedFamerShare { get; set; }
        /// <summary>
        /// Farmer Share in Cash (Rs.)
        /// </summary>
        public decimal FarmerShareInCash { get; set; }
        /// <summary>
        /// Farmer Share in Kind (Rs.)
        /// </summary>
        public decimal FarmerShareInKind { get; set; }

    }
}
