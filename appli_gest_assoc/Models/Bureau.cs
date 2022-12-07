using System;
using System.Collections.Generic;

#nullable disable

namespace appli_gest_assoc.Models
{
    public partial class Bureau
    {
        public Bureau()
        {
            Depenses = new HashSet<Depense>();
            MembreBureaus = new HashSet<MembreBureau>();
            Recettes = new HashSet<Recette>();
        }

        public int IdBureau { get; set; }
        public string NomBureau { get; set; }
        public DateTime DateCreation { get; set; }
        public int AssociationId { get; set; }
        public int ActiviteId { get; set; }

        public virtual Activite Activite { get; set; }
        public virtual Association Association { get; set; }
        public virtual ICollection<Depense> Depenses { get; set; }
        public virtual ICollection<MembreBureau> MembreBureaus { get; set; }
        public virtual ICollection<Recette> Recettes { get; set; }
    }
}
