using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities.HEIS
{
    public class HeisIdentification
    {
        public int Id { get; set; }
        public string FarmerName { get; set; }
        /// <summary>
        /// With Dashes
        /// </summary>
        public string FarmerCNIC { get; set; }

        public int DistrictId { get; set; }
        public string DistrictName { get; set; }

        public int TehsilId { get; set; }
        public string TehsilName { get; set; }

        public List<HeisIdentificationDetail> HeisIdentificationDetails { get; set; }

    }
}
