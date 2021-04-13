using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities.WC
{
    public class WcProject
    {
        public int Id { get; set; }
        /// <summary>
        /// PIPIP/NPIW-II/JIP
        /// </summary>
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }      
        public string Duration { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
