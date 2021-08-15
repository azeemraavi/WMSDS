using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities.Common
{
    public class AppUser
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string ContanctNo { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string UserType { get; set; }

        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }

        public int DivisionId { get; set; }
        public string DivisionName { get; set; }

        public int DistrictId { get; set; }
        public string DistrictName { get; set; }

        public string Email { get; set; }
        [Required]
        public string OfficeType { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public DateTime CreateAt { get; set; }

       
    }
}
