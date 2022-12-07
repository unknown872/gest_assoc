using System;
using System.Collections.Generic;

#nullable disable

namespace appli_gest_assoc.Models
{
    public partial class Association
    {
        public Association()
        {
            Bureaus = new HashSet<Bureau>();
        }

        public int IdAssociation { get; set; }
        public string NomAssociation { get; set; }
        public DateTime AnneeCreation { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }

        public virtual ICollection<Bureau> Bureaus { get; set; }
    }
}
