using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitatorBackend.Models
{
    [Table("kurs")]
    public partial class Kur
    {
        public Kur()
        {
            Grupazajeciowas = new HashSet<Grupazajeciowa>();
            Hospitacjas = new HashSet<Hospitacja>();
        }

        [Key]
        [Column("kod")]
        public string Kod { get; set; } = null!;
        [Column("forma", TypeName = "enum('wyklad','labolatorium','cwiczenia','projekt','seminarium','praktyki','praca_dyplomowa')")]
        public string? Forma { get; set; }
        [Column("nazwa")]
        [StringLength(255)]
        public string? Nazwa { get; set; }
        [Column(TypeName = "int(11)")]
        public int? Semestr { get; set; }

        [InverseProperty(nameof(Grupazajeciowa.KursKodNavigation))]
        public virtual ICollection<Grupazajeciowa> Grupazajeciowas { get; set; }
        [InverseProperty(nameof(Hospitacja.KursKodNavigation))]
        public virtual ICollection<Hospitacja> Hospitacjas { get; set; }
    }
}
