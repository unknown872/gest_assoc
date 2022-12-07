using System;
using System.Collections.Generic;

#nullable disable

namespace appli_gest_assoc.Models
{
    public partial class Activite
    {
        public Activite()
        {
            Bureaus = new HashSet<Bureau>();
        }

        public int IdActivite { get; set; }
        public string Libelle { get; set; }

        public virtual ICollection<Bureau> Bureaus { get; set; }
    }
}
