using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitatorBackend.Models
{
    [Table("odwolanie")]
    [Index(nameof(ProtokolId), Name = "protokol_id", IsUnique = true)]
    [Index(nameof(ProwadzacyId), Name = "prowadzacy_id")]
    public partial class Odwolanie
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("protokol_id", TypeName = "int(11)")]
        public int ProtokolId { get; set; }
        [Column("prowadzacy_id", TypeName = "int(11)")]
        public int? ProwadzacyId { get; set; }
        [Column("data_odwolania")]
        public DateOnly? DataOdwolania { get; set; }
        [Column("uzasadnienie")]
        [StringLength(255)]
        public string? Uzasadnienie { get; set; }
        [Column("_status", TypeName = "int(11)")]
        public int? Status { get; set; }

        [ForeignKey(nameof(ProtokolId))]
        [InverseProperty("Odwolanie")]
        public virtual Protokol Protokol { get; set; } = null!;
        [ForeignKey(nameof(ProwadzacyId))]
        [InverseProperty("Odwolanies")]
        public virtual Prowadzacy? Prowadzacy { get; set; }
    }
}
