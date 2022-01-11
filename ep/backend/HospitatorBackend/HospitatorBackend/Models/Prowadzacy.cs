using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitatorBackend.Models
{
    [Table("prowadzacy")]
    public partial class Prowadzacy
    {
        public Prowadzacy()
        {
            Grupazajeciowas = new HashSet<Grupazajeciowa>();
            Hospitacjas = new HashSet<Hospitacja>();
            Odwolanies = new HashSet<Odwolanie>();
            Zespolhospitujacies = new HashSet<Zespolhospitujacy>();
            Zespols = new HashSet<Zespolhospitujacy>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("data_ost_hospitacji")]
        public DateOnly? DataOstHospitacji { get; set; }
        [Column("habilitowany")]
        public bool? Habilitowany { get; set; }
        [Column("uznany")]
        public bool? Uznany { get; set; }
        [Column("doswiadczony")]
        public bool? Doswiadczony { get; set; }
        [Column("imie")]
        [StringLength(255)]
        public string? Imie { get; set; }
        [Column("nazwisko")]
        [StringLength(255)]
        public string? Nazwisko { get; set; }
        [Column("tytol")]
        [StringLength(255)]
        public string? Tytol { get; set; }
        [Column("stopien_naukowy")]
        [StringLength(255)]
        public string? StopienNaukowy { get; set; }
        [Column("stanowisko")]
        [StringLength(255)]
        public string? Stanowisko { get; set; }
        [Column("jednostka_organizacyjna")]
        [StringLength(255)]
        public string? JednostkaOrganizacyjna { get; set; }

        [InverseProperty(nameof(Grupazajeciowa.Prowadzacy))]
        public virtual ICollection<Grupazajeciowa> Grupazajeciowas { get; set; }
        [InverseProperty(nameof(Hospitacja.Prowadzacy))]
        public virtual ICollection<Hospitacja> Hospitacjas { get; set; }
        [InverseProperty(nameof(Odwolanie.Prowadzacy))]
        public virtual ICollection<Odwolanie> Odwolanies { get; set; }
        [InverseProperty(nameof(Zespolhospitujacy.Prowadzacy))]
        public virtual ICollection<Zespolhospitujacy> Zespolhospitujacies { get; set; }

        [ForeignKey("ProwadzacyId")]
        [InverseProperty(nameof(Zespolhospitujacy.Prowadzacies))]
        public virtual ICollection<Zespolhospitujacy> Zespols { get; set; }
    }
}
