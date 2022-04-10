using System;
using System.Collections.Generic;

namespace CoronavirusBase
{
    public partial class Country
    {
        public Country()
        {
            CountriesVariants = new HashSet<CountriesVariant>();
        }

        public int IdCountry { get; set; }
        public string NameCountry { get; set; } = null!;

        public virtual ICollection<CountriesVariant> CountriesVariants { get; set; }
    }
}
