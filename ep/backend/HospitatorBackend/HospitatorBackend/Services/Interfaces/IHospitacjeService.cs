using HospitatorBackend.Dtos;
using HospitatorBackend.Models;

namespace HospitatorBackend.Services.Interfaces
{
    public interface IHospitacjeService
    {
        Protokol? DodajRaportHospitacji(FormularzProtokoluInputDto protokol);
        ICollection<HospitacjaDto> GetHospitacjeZespolu(int id_przewodniczacego);
    }
}