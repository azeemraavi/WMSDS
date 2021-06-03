using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities.HEIS
{
    public partial class HeisIdentificationDetail
    {
        public int Id { get; set; }

        public int HeisIdentificationId { get; set; }
        public HeisIdentification HeisIdentification { get; set; }

        #region Basic Information
        /// <summary>
        /// LOV
        /// Regular/Extension
        /// </summary>
        public string InstallationType { get; set; }

        /// <summary>
        /// LOV
        /// </summary>
        public int FiscalYearId { get; set; }
        public string FiscalYear { get; set; }

        /// <summary>
        /// LOV
        /// Drip/Sprinkler
        /// </summary>
        public string SystemType { get; set; }

        /// <summary>
        /// LOV
        /// Supply and Service Company
        /// </summary>
        public int SSCId { get; set; }
        public string SSCName { get; set; }

        /// <summary>
        /// Set default value to PIPIP
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Village Name/Number
        /// </summary>
        public string VillageName { get; set; }
        /// <summary>
        /// Contact number
        /// </summary>
        public string ContactNumber { get; set; }

        /// <summary>
        /// LOV
        /// Loam/Sandy/Clay
        /// </summary>
        public string SoilType { get; set; }
        /// <summary>
        /// Scheme Area (Acres)
        /// </summary>
        public int SchemeArea { get; set; }
        /// <summary>
        /// LOV
        /// Tubewell/Canal/Canal+Tubewell/Others
        /// </summary>
        public string WaterSource { get; set; }
        /// <summary>
        /// LOV
        /// Fit/Unfit/Marginally Fit
        /// </summary>
        public string WaterQuality { get; set; }
        /// <summary>
        /// LOV
        /// Electric Motor/Diesel Engine/ Solar/Tractor PTO/Other
        /// </summary>
        public string PowerSource { get; set; }
        /// <summary>
        /// LOV
        /// Row Crops/Field Crops/Orchard/Vegetables
        /// </summary>
        public string CropCategory { get; set; }

        /// <summary>
        /// LOV
        /// Drop down from concerned Crop Category (attached)
        /// </summary>
        public string CropName { get; set; }
        /// <summary>
        /// LOV
        /// Plants per Acre (Nos.)
        /// </summary>
        public int PlansPerAcre { get; set; }
        /// <summary>
        /// LOV
        /// Dripper/Inline Drip/ Micro Sprinkler/Spray Jet/Bubbler/Rain Gun/Mini Sprinkler/ Center Pivot/Linear Move/ Reel
        /// </summary>
        public string SystemClassification { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        #endregion



    }
}
