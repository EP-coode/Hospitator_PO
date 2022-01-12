namespace HospitatorBackend.Dtos
{
    public record FormularzProtokoluInputDto
    {
        public int HospitacjaId { get; set; }
        public int OcenaKoncowa { get; set; }
        public bool Punktualnie { get; set; }
        public int? Opuznienie { get; set; }
        public bool SprawdzonoObecnosc { get; set; }
        public int? LiczbaObecnych { get; set; }
        public bool SalaPrzystosowana { get; set; }
        public string? PowodyNieprzystosowania { get; set; }
        public bool TrescKursuZgodna { get; set; }
    }
}
