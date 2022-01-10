using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitatorBackend.Models
{
    [Keyless]
    [Table("prowadzacy_zespolhospitujacy")]
    [Index(nameof(ProwadzacyId), Name = "prowadzacy_id")]
    [Index(nameof(ZespolId), Name = "zespol_id")]
    public partial class ProwadzacyZespolhospitujacy
    {
        [Column("prowadzacy_id", TypeName = "int(11)")]
        public int? ProwadzacyId { get; set; }
        [Column("zespol_id", TypeName = "int(11)")]
        public int? ZespolId { get; set; }

        [ForeignKey(nameof(ProwadzacyId))]
        public virtual Prowadzacy? Prowadzacy { get; set; }
        [ForeignKey(nameof(ZespolId))]
        public virtual Zespolhospitujacy? Zespol { get; set; }
    }
}
