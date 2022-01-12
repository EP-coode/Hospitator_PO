﻿using HospitatorBackend.Models;

namespace HospitatorBackend.Dtos
{
    public class ProtokolDto
    {
        public int Id { get; set; } = 0;
        public int HospitacjaId { get; set; }
        public DateOnly? DataWystawienia { get; set; }
        public bool? Zakceptowane { get; set; }
        public DateOnly? DataZapoznania { get; set; }
        public virtual Odwolanie Odwolanie { get; set; } = null!;
        public virtual Formulazprotokolu? Formulazprotokolus { get; set; }
    }
}
