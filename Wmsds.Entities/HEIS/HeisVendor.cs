using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities.HEIS
{
    public class HeisVendor
    {
        public int Id { get; set; }
        public string SSCName { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public bool IsActive { get; set; }
    }
}
