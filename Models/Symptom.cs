using System;
using System.Collections.Generic;

namespace CoronavirusBase
{
    public partial class Symptom
    {
        public Symptom()
        {
            IdVariants = new HashSet<Variant>();
        }

        public int IdSymptom { get; set; }
        public string NameSymptom { get; set; } = null!;

        public virtual ICollection<Variant> IdVariants { get; set; }
    }
}
