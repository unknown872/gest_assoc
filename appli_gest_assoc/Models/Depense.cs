using System;
using System.Collections.Generic;

#nullable disable

namespace appli_gest_assoc.Models
{
    public partial class Depense
    {
        public int IdDepense { get; set; }
        public string Libelle { get; set; }
        public int TypeDepenseId { get; set; }
        public decimal Montant { get; set; }
        public int BureauId { get; set; }

        public virtual Bureau Bureau { get; set; }
        public virtual TypeDepense TypeDepense { get; set; }
    }
}
