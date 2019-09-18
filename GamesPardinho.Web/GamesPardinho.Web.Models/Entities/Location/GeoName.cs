using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GamesPardinho.Web.Models.Entities.Location
{
    public class GeoNameObject
    {
        public IList<GeoName> GeoNames { get; set; }
    }

    public class GeoName
    {
        public class GNTimeZone
        {
            public double GmtOffset { get; set; }
            public double DstOffset { get; set; }
            public string TimeZoneId { get; set; }
        }

        public class GNAlternateName
        {
            public string Name { get; set; }
            public string Lang { get; set; }
        }

        public int GeoNameId { get; set; }
        public string Name { get; set; }
        public string ToponymName { get; set; }
        public string AsciiName { get; set; }
        public GNTimeZone TimeZone { get; set; }
        public IList<GNAlternateName> AlternateNames { get; set; }
        
        public GNAlternateName GetName(string lang)
        {
            return AlternateNames.SingleOrDefault(x => x.Lang == lang);
        }

        public string ContinentCode { get; set; }
        public string CountryCode { get; set; }
        public IDictionary<string, string> AdminCodes1 { get; set; }

        public string Code
        {
            get
            {
                if (AdminCodes1 != null)
                {
                    return AdminCodes1.Count == 1 ? AdminCodes1[AdminCodes1.Keys.FirstOrDefault()] : string.Join("; ", AdminCodes1.Values);
                } 
                else if (CountryCode != null)
                {
                    return CountryCode;
                }
                else
                {
                    return ContinentCode;
                }
            }
        }
    }
}
