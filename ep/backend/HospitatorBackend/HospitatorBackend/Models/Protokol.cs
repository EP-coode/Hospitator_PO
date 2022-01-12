using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitatorBackend.Models
{
    [Table("protokol")]
    [Index(nameof(HospitacjaId), Name = "hospitacja_id", IsUnique = true)]
    public partial class Protokol
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; } = 0;
        [Column("hospitacja_id", TypeName = "int(11)")]
        public int HospitacjaId { get; set; }
        [Column("data_wystawienia")]
        public DateOnly? DataWystawienia { get; set; }
        [Column("zakceptowane")]
        public bool? Zakceptowane { get; set; } = false;
        [Column("data_zapoznania")]
        public DateOnly? DataZapoznania { get; set; }

        [InverseProperty("Protokol")]
        public virtual Odwolanie Odwolanie { get; set; } = null!;
        [InverseProperty(nameof(Formulazprotokolu.Protokol))]
        public virtual Formulazprotokolu? Formulazprotokolus { get; set; }
        public virtual Hospitacja? Hospitacja { get; set; }
    }
}
