namespace HospitatorBackend.Dtos
{
    public record ReklamacjaDto
    {
        public int ProwadzacyId { get; init; }
        public int ProtokolId { get; init; }
        public string Uzasadnienie { get; init; }
    }
}
