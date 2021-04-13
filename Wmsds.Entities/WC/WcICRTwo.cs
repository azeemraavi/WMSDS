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
        /// ICR-2 approved
        /// </summary>
        public string ICR2ApprovedStatus { get; set; }
        /// <summary>
        /// ICR-2 Released Amount (Rs.)
        /// </summary>
        public decimal ICR2ReleasedAmount { get; set; }
    }
}
