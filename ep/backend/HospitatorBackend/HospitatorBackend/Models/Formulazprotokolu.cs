using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitatorBackend.Models
{
    [Table("formulazprotokolu")]
    [Index(nameof(ProtokolId), Name = "protokol_id")]
    public partial class Formulazprotokolu
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("protokol_id", TypeName = "int(11)")]
        public int? ProtokolId { get; set; }
        [Column("ocena_koncowa", TypeName = "int(11)")]
        public int? OcenaKoncowa { get; set; }
        [Column("punktualnie")]
        public bool? Punktualnie { get; set; }
        [Column("opuznienie", TypeName = "int(11)")]
        public int? Opuznienie { get; set; }
        [Column("sprawdzono_obecnosc")]
        public bool? SprawdzonoObecnosc { get; set; }
        [Column("liczba_obecnych", TypeName = "int(11)")]
        public int? LiczbaObecnych { get; set; }
        [Column("sala_przystosowana")]
        public bool? SalaPrzystosowana { get; set; }
        [Column("powody_nieprzystosowania")]
        [StringLength(255)]
        public string? PowodyNieprzystosowania { get; set; }
        [Column("tresc_kursu_zgodna")]
        public bool? TrescKursuZgodna { get; set; }

        [ForeignKey(nameof(ProtokolId))]
        [InverseProperty("Formulazprotokolus")]
        public virtual Protokol? Protokol { get; set; }
    }
}
