using HospitatorBackend.Models;

namespace HospitatorBackend.Dtos
{
    public record HospitacjaDto
    {
        public int Id { get; init; }
        public DateOnly? Termin { get; init; }
        public string? KursKod { get; init; }
        public virtual Kurs KursKodNavigation { get; init; }

        public virtual Prowadzacy Prowadzacy { get; init; }

    }
}
