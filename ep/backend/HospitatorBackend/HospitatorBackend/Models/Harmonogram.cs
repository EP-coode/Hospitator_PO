using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitatorBackend.Models
{
    [Table("harmonogram")]
    public partial class Harmonogram
    {
        public Harmonogram()
        {
            Hospitacjas = new HashSet<Hospitacja>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("zatwierdzony_wkozjk")]
        public bool? ZatwierdzonyWkozjk { get; set; }
        [Column("zatwierdzony_dyrektor")]
        public bool? ZatwierdzonyDyrektor { get; set; }

        [InverseProperty(nameof(Hospitacja.Harmonogram))]
        public virtual ICollection<Hospitacja> Hospitacjas { get; set; }
    }
}
