﻿using HospitatorBackend.Models;

namespace HospitatorBackend.Dtos
{
    public static class ExtensionsDto
    {
        public static ProwadzacyDto ToDto(this Prowadzacy prowadzacy)
        {
            return new ProwadzacyDto()
            {
                Id = prowadzacy.Id,
                Imie = prowadzacy.Imie,
                Nazwisko = prowadzacy.Nazwisko,
                Tytol = prowadzacy.Tytol
            };
        }

        //public static ProtokolDto ToDto(this Protokol p)
        //{
        //    return new ProtokolDto()
        //    {
        //         Id = p.Id,
        //        HospitacjaId = p.HospitacjaId,
        //        Zakceptowane = p.Zakceptowane,
        //        DataWystawienia = p.DataWystawienia,
        //        DataZapoznania = p.DataZapoznania,
        //        Formulazprotokolus = p.Formulazprotokolus,
        //        Odwolanie = p.Odwolanie,
        //        NazwaKursu = p.Hospitacja.KursKodNavigation.Nazwa,
        //        KodKursu = p.Hospitacja.KursKodNavigation.Kod
        //    };
        //}

        //public static ProtokolDto ToDto(this Protokol p)
        //{
        //    return new ProtokolDto()
        //    {
        //        Zakceptowane = p.Zakceptowane,
        //        DataWystawienia = p.DataWystawienia,
        //        DataZapoznania = p.DataZapoznania,
        //        Formulazprotokolus = p.Formulazprotokolus,
        //        Odwolanie = p.Odwolanie
        //    };
        //}
    }
}
