using System;
using System.Collections.Generic;

#nullable disable

namespace appli_gest_assoc.Models
{
    public partial class TypeRecette
    {
        public TypeRecette()
        {
            Recettes = new HashSet<Recette>();
        }

        public int IdTypeRecette { get; set; }
        public string Libelle { get; set; }

        public virtual ICollection<Recette> Recettes { get; set; }
    }
}
