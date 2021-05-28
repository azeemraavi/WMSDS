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
        /// LOV
        /// ICR-III Verification
        /// Yes/No/Offered
        /// </summary>
        public string ICRIIIVerification { get; set; }
        /// <summary>
        /// Get from ICR-II (editable)
        /// Total Amount Verified (Rs.)
        /// </summary>
        public int TotalAmountVerified { get; set; }
        /// <summary>
        /// ICR-III Qualifying Date
        /// </summary>
        public DateTime ICRIIIQualifyingDate { get; set; }
    }
}
