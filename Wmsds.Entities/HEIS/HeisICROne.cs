using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities.HEIS
{
    public partial class HeisIdentificationDetail
    {
        #region Material Verification (ICR-I)
        /// <summary>
        /// Yes/No/Offered
        /// </summary>
        public string MaterialVerified { get; set; }
        /// <summary>
        /// Material Verified Date
        /// </summary>
        public DateTime? MaterialVerifiedDate { get; set; }
        /// <summary>
        /// ICR-I Amount Verified (Rs.)
        /// </summary>
        public int ICRIAmountVerified { get; set; }
        #endregion
    }
}
