using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitatorBackend.Models
{
    [Table("grupazajeciowa")]
    [Index(nameof(KursKod), Name = "grupa_zaj_fk_k")]
    [Index(nameof(ProwadzacyId), Name = "grupa_zaj_fk_p")]
    public partial class Grupazajeciowa
    {
        [Key]
        [Column("kod")]
        public string Kod { get; set; } = null!;
        [Column("dzien", TypeName = "int(11)")]
        public int? Dzien { get; set; }
        [Column("godzina", TypeName = "int(11)")]
        public int? Godzina { get; set; }
        [Column("minuta", TypeName = "int(11)")]
        public int? Minuta { get; set; }
        [Column("nazwa")]
        [StringLength(255)]
        public string? Nazwa { get; set; }
        [Column("miejsce")]
        [StringLength(255)]
        public string? Miejsce { get; set; }
        [Column("liczba_zapisanych", TypeName = "int(11)")]
        public int? LiczbaZapisanych { get; set; }
        [Column("kurs_kod")]
        public string? KursKod { get; set; }
        [Column("prowadzacy_id", TypeName = "int(11)")]
        public int? ProwadzacyId { get; set; }

        [ForeignKey(nameof(KursKod))]
        [InverseProperty(nameof(Kurs.Grupazajeciowas))]
        public virtual Kurs? KursKodNavigation { get; set; }
        [ForeignKey(nameof(ProwadzacyId))]
        [InverseProperty("Grupazajeciowas")]
        public virtual Prowadzacy? Prowadzacy { get; set; }
    }
}
