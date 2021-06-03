using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wmsds.Entities.HEIS;
using System.Web.Mvc;


namespace Wmsds.Web.Models
{
    public class HEISDataEntryDto
    {
        public HeisIdentification HeisIdentification { get; set; }
        public HeisIdentificationDetail HeisIdentificationDetail { get; set; }
    }
}