using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace HospitatorBackend.Models
{
    [Table("hospitacja")]
    [Index(nameof(HarmonogramId), Name = "harmonogram_id")]
    [Index(nameof(KursKod), Name = "kurs_kod")]
    [Index(nameof(ProwadzacyId), Name = "prowadzacy_id")]
    [Index(nameof(ZespolHospitujacyId), Name = "zespol_hospitujacy_id")]
    public partial class Hospitacja
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("termin")]
        public DateOnly? Termin { get; set; }
        [Column("harmonogram_id", TypeName = "int(11)")]
        public int? HarmonogramId { get; set; }
        [Column("zespol_hospitujacy_id", TypeName = "int(11)")]
        public int? ZespolHospitujacyId { get; set; }
        [Column("prowadzacy_id", TypeName = "int(11)")]
        public int? ProwadzacyId { get; set; }
        [Column("kurs_kod")]
        public string? KursKod { get; set; }

        [ForeignKey(nameof(HarmonogramId))]
        [InverseProperty("Hospitacjas")]
        [JsonIgnore]
        public virtual Harmonogram? Harmonogram { get; set; }

        [ForeignKey(nameof(KursKod))]
        [InverseProperty(nameof(Kurs.Hospitacjas))]
        [JsonIgnore]
        public virtual Kurs? KursKodNavigation { get; set; }

        [ForeignKey(nameof(ProwadzacyId))]
        [InverseProperty("Hospitacjas")]
        [JsonIgnore]
        public virtual Prowadzacy? Prowadzacy { get; set; }

        [ForeignKey(nameof(ZespolHospitujacyId))]
        [InverseProperty(nameof(Zespolhospitujacy.Hospitacjas))]
        [JsonIgnore]
        public virtual Zespolhospitujacy? ZespolHospitujacy { get; set; }

        [JsonIgnore]
        public virtual Protokol? Protokol { get; set; } 
    }
}
