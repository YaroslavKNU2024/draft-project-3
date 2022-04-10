using System;
using System.Collections.Generic;

namespace CoronavirusBase
{
    public partial class Variant
    {
        public Variant()
        {
            CountriesVariants = new HashSet<CountriesVariant>();
            IdSymptoms = new HashSet<Symptom>();
        }

        public int IdVariant { get; set; }
        public string VariantName { get; set; } = null!;
        public string? VariantOrigin { get; set; }
        public int IdVirus { get; set; }
        public DateTime? DateDiscovered { get; set; }

        public virtual Virus IdVirusNavigation { get; set; } = null!;
        public virtual ICollection<CountriesVariant> CountriesVariants { get; set; }

        public virtual ICollection<Symptom> IdSymptoms { get; set; }
    }
}
