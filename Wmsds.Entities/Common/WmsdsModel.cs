using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities.Common
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public List<Division> Divisions { get; set; }

    }
    public class Division
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int ProvinceId { get; set; }
        public Province Province { get; set; }        
        public List<District> Districts { get; set; }
    }
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public int? DivisionId { get; set; }
        public Division Division { get; set; }
        public List<Tehsil> Tehsils { get; set; }
    }
    public class Tehsil
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }
        
    }
    public class FinancialYear
    {
        public int Id { get; set; }
        /// <summary>
        /// For example 2021-22, 2020-21
        /// </summary>
        public string Year { get; set; }
    }

    public class Channel
    {
        public int Id { get; set; }

        public int DivisionId { get; set; }
        public string DivisionName { get; set; }


        [Required]
        public int DistrictId { get; set; }
        [Required]
        public string DistrictName { get; set; }

        [Required]
        public int TehsilId { get; set; }
        [Required]
        public string TehsilName { get; set; }

        //TODO: We have to create catalog tables for below mentioned attributes
        public int MainCanalId { get; set; }
        public string MainCanalName { get; set; }

        public int BranchCanalId { get; set; }
        public string BranchCanalName { get; set; }

        public int DistributaryId { get; set; }
        public string DistributaryName { get; set; }

        public int MinorCanalId { get; set; }
        public string MinorCanalName { get; set; }

        public int SubMinorId { get; set; }
        public string SubMinorName { get; set; }

        public int EscapeId { get; set; }
        public string EscapeName { get; set; }

        /// <summary>
        /// This field will be used as general beacuse we may not get complete data as per above
        /// hirerachary
        /// </summary>
        [Required]
        public string ChannelName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }

    }

    public class WaterCourse
    {
        public int Id { get; set; }

        public int DivisionId { get; set; }
        public string DivisionName { get; set; }

        [Required]
        public int DistrictId { get; set; }
        [Required]
        public string DistrictName { get; set; }

        [Required]
        public int TehsilId { get; set; }
        [Required]
        public string TehsilName { get; set; }

        [Required]
        public int ChannelId { get; set; }
        [Required]
        public string ChannelName { get; set; }

        public string WcNo { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
    }
}
