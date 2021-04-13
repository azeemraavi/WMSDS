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
        /// ICR-I approved
        /// </summary>
        public string ICR1ApprovedStatus { get; set; }
        /// <summary>
        /// ICR-I Released Amount (Rs.)
        /// </summary>
        public decimal ICR1ReleasedAmount { get; set; }
    }
}
