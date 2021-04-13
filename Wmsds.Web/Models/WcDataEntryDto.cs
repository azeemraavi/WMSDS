using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wmsds.Entities.WC;
using System.Web.Mvc;

namespace Wmsds.Web.Models
{
    public class WcDataEntryDto
    {
        public WcIdentification WcIdentification { get; set; }
        public WcIdentificationDetail WcIdentificationDetail { get; set; }

        public List<SelectListItem> ProjectName;
    }
}