using HospitatorBackend.Dtos;
using HospitatorBackend.Models;

namespace HospitatorBackend.Services.Interfaces
{
    public interface IOcenyService
    {
        ICollection<ProtokolDto> GetNoweOcenyProwadzacego(int id_prowadzacego);
        ICollection<ProtokolDto> GetZakceptowaneOcenyProwadzacego(int id_prowadzacego);
        ICollection<ProtokolDto> GetZareklamowaneOcenyProwadzacego(int id_prowadzacego);
        Protokol? ZakceptujOcene(int id_nauczyciela, int id_protokolu);
        Odwolanie? ZareklamujOcene(ReklamacjaDto r);
    }
}