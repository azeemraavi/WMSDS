using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities.ViewModels
{
    public class FilterRequest
    {
        public int PageIndex { get; set; }
        public string DistrictName { get; set; }
        public int DistrictId { get; set; }
        public string TehsilName { get; set; }
        public int TehsilId { get; set; }
        public string ChannelName { get; set; }
        public string WaterCourseNo { get; set; }
        public string sortName { get; set; }
        public string sortDirection { get; set; }
    }
}
