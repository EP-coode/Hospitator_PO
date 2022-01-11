namespace HospitatorBackend.Dtos;

public record ProwadzacyDto
{
    public int Id { get; init; }
    //public DateOnly? DataOstHospitacji { get; init; }
    //public bool? Habilitowany { get; init; }
    //public bool? Uznany { get; init; }
    //public bool? Doswiadczony { get; init; }
    public string? Imie { get; init; }
    public string? Nazwisko { get; init; }
    public string? Tytol { get; init; }
    //public string? StopienNaukowy { get; init; }
    //public string? Stanowisko { get; init; }
    //public string? JednostkaOrganizacyjna { get; init; }
}

