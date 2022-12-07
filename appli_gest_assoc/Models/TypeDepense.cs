using System;
using System.Collections.Generic;

#nullable disable

namespace appli_gest_assoc.Models
{
    public partial class TypeDepense
    {
        public TypeDepense()
        {
            Depenses = new HashSet<Depense>();
        }

        public int IdTypeDepense { get; set; }
        public string Libelle { get; set; }

        public virtual ICollection<Depense> Depenses { get; set; }
    }
}
