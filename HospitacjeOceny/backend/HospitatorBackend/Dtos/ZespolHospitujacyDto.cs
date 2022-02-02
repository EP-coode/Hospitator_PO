using HospitatorBackend.Models;

namespace HospitatorBackend.Dtos;

public record ZespolHospitujacyDto
{
    public int Id { get; init; }
    public virtual ProwadzacyDto Przewodniczacy { get; init; }
    public virtual ICollection<HospitacjaDto> Hospitacje { get; init; }
    public virtual ICollection<Prowadzacy> Sklad { get; init; }
}


