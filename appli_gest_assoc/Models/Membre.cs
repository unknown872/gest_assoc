using System;
using System.Collections.Generic;

#nullable disable

namespace appli_gest_assoc.Models
{
    public partial class Membre
    {
        public Membre()
        {
            MembreBureaus = new HashSet<MembreBureau>();
        }

        public int IdMembre { get; set; }
        public string PrenomMembre { get; set; }
        public string NomMembre { get; set; }
        public string Sexe { get; set; }
        public DateTime DateNaiss { get; set; }
        public string LieuNaiss { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }

        public virtual ICollection<MembreBureau> MembreBureaus { get; set; }
    }
}
