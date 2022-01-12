
namespace HospitatorBackend.Dtos
{
    public record PrzegladOcenDto
    {
        public ICollection<ProtokolDto> Nowe { get;init;}
        public ICollection<ProtokolDto> Zakceptowane { get; init; }
        public ICollection<ProtokolDto> Zareklamowane { get; init; }
    }
}
