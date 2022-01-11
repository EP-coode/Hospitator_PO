using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitatorBackend.Models
{
    [Table("zespolhospitujacy")]
    [Index(nameof(ProwadzacyId), Name = "zesp_hosp_fk_p")]
    public partial class Zespolhospitujacy
    {
        public Zespolhospitujacy()
        {
            Hospitacjas = new HashSet<Hospitacja>();
            Prowadzacies = new HashSet<Prowadzacy>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("prowadzacy_id", TypeName = "int(11)")]
        public int? ProwadzacyId { get; set; }

        [ForeignKey(nameof(ProwadzacyId))]
        [InverseProperty("Zespolhospitujacies")]
        public virtual Prowadzacy? Prowadzacy { get; set; }
        [InverseProperty(nameof(Hospitacja.ZespolHospitujacy))]
        public virtual ICollection<Hospitacja> Hospitacjas { get; set; }

        [ForeignKey("ZespolId")]
        [InverseProperty("Zespols")]
        public virtual ICollection<Prowadzacy> Prowadzacies { get; set; }

        //public virtual ICollection<Prowadzacy_ZespolHospitujacy> Skald { get; set ;}
    }
}
