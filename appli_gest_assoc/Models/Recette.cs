using System;
using System.Collections.Generic;

#nullable disable

namespace appli_gest_assoc.Models
{
    public partial class Recette
    {
        public int IdRecette { get; set; }
        public string Libelle { get; set; }
        public int TypeRecetteId { get; set; }
        public decimal Montant { get; set; }
        public int BureauId { get; set; }

        public virtual Bureau Bureau { get; set; }
        public virtual TypeRecette TypeRecette { get; set; }
    }
}
