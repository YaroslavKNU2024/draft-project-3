using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoronavirusBase
{
    public partial class ClassVirus
    {
        public ClassVirus()
        {
            VirusGroups = new HashSet<VirusGroup>();
        }

        public int IdClass { get; set; }
        [Required(ErrorMessage ="Поле не може бути пустим")]
        [Display(Name="Класифікація вірусів")]
        public string InfoVirusClass { get; set; } = null!;
        [Display(Name="Інфо про абстрактний вірус")]
        public string TypeClass { get; set; } = null!;

        public virtual ICollection<VirusGroup> VirusGroups { get; set; }
    }
}
