using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wmsds.Entities.Common;

namespace Wmsds.Entities.ViewModels
{
    public class WcIdentificationDto
    {
        public List<Division> Divisions { get; set; }
        public List<District> Districts { get; set; }
        public List<Tehsil> Tehsils { get; set; }
        public List<FinancialYear> FinancialYears { get; set; }
        //todo
        public List<WaterCourse> WaterCourses { get; set; }
        public List<Channel> Channels { get; set; }

    }
}
