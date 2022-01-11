using System.ComponentModel.DataAnnotations.Schema;

namespace HospitatorBackend.Models
{
    [Table("Prowadzacy_ZespolHospitujacy")]
    public class Prowadzacy_ZespolHospitujacy
    {
        [Column("prowadzacy_id")]
        public int ProwadzacyID { get; set; }
        [Column("zespol_id")]
        public string ZespolID { get; set; }

        public Zespolhospitujacy Zespolhospitujacy { get; set; }
    }
}
