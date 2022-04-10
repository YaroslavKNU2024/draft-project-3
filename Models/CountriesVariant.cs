using System;
using System.Collections.Generic;

namespace CoronavirusBase
{
    public partial class CountriesVariant
    {
        public int IdCountry { get; set; }
        public int IdVariant { get; set; }
        public int? Cases { get; set; }
        public int? Dead { get; set; }

        public virtual Country IdCountryNavigation { get; set; } = null!;
        public virtual Variant IdVariantNavigation { get; set; } = null!;
    }
}
