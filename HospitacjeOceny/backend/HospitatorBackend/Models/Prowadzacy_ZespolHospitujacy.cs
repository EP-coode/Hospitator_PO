using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitatorBackend.Models
{
    [Table("Prowadzacy_ZespolHospitujacy")]
    public class Prowadzacy_ZespolHospitujacy
    {
        [Column("prowadzacy_id")]
        public int ProwadzacyID { get; set; }

        [Column("zespol_id")]
        public int ZespolID { get; set; }
    }
}