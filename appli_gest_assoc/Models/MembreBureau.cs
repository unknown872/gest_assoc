using System;
using System.Collections.Generic;

#nullable disable

namespace appli_gest_assoc.Models
{
    public partial class MembreBureau
    {
        public int IdMembreBureau { get; set; }
        public int MembreId { get; set; }
        public DateTime DateCreation { get; set; }
        public string Poste { get; set; }
        public int BureauId { get; set; }

        public virtual Bureau Bureau { get; set; }
        public virtual Membre Membre { get; set; }
    }
}
