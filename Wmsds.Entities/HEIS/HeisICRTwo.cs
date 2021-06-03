using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities.HEIS
{
    public partial class HeisIdentificationDetail
    {
        #region Commissioning Verification (ICR-II)

        /// <summary>
        /// LOV
        /// Yes/No/ Offered/ Deferred
        /// </summary>
        public string CommissioningVerification { get; set; }
        /// <summary>
        /// Commissioning Verification Date
        /// </summary>
        public DateTime? CommissioningVerificationDate { get; set; }
        /// <summary>
        /// Get value from ICR-I (editable)
        /// Total Scheme Cost Verified (Rs.)
        /// </summary>
        public int TotalSchemeCostVerified { get; set; }
        /// <summary>
        /// Get value from ICR-I (editable)
        /// ICR-II Amount Verified (Rs.)
        /// </summary>
        public int ICRIIAmountVerified { get; set; }
        #endregion
    }
}
