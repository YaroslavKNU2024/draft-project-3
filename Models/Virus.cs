using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoronavirusBase
{
    public partial class Virus
    {
        public Virus()
        {
            Variants = new HashSet<Variant>();
        }

        public int IdVirus { get; set; }
        [Required(ErrorMessage ="Поле не може бути пустим")]
        [Display(Name="Вірус")]
        public string VirusName { get; set; } = null!;
        [Display(Name = "Інформація")]
        public DateTime? DateDiscovered { get; set; }
        public int IdVirusGroup { get; set; }

        public virtual VirusGroup IdVirusGroupNavigation { get; set; } = null!;
        public virtual ICollection<Variant> Variants { get; set; }
    }
}
