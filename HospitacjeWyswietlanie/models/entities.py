import datetime
import enum

from sqlalchemy.ext import declarative
from sqlalchemy.orm import relationship
from sqlalchemy import Column, Integer, Date, ForeignKey, VARCHAR, Boolean, Enum

Base = declarative.declarative_base()


class Harmonogram(Base):
    __tablename__ = "harmonogram"

    id = Column(Integer, primary_key=True)
    # booleany
    zatwierdzony_wkozjk = Column(Integer)
    zatwierdzony_dyrektor = Column(Integer)
    hospitacje = relationship("Hospitacja")


class Hospitacja(Base):
    __tablename__ = "hospitacja"

    id = Column(Integer, primary_key=True)
    termin = Column(Date)
    harmonogram_id = Column(Integer, ForeignKey("harmonogram.id"))
    zespol_hospitujacy_id = Column(Integer, ForeignKey("zespolhospitujacy.id"))
    prowadzacy_id = Column(Integer, ForeignKey("prowadzacy.id"))
    kurs_kod = Column(VARCHAR, ForeignKey("kurs.kod"))

    protokol = relationship("Protokol")

    def toDict(self):
        return {"id": self.id, "termin": self.termin, "harmonogram_id": self.harmonogram_id,
                "zespolhospitujacy_id": self.zespol_hospitujacy_id,
                "prowadzacy_id": self.prowadzacy_id, "kurs_kod": self.kurs_kod, "isDone":self.isDone}

    @property
    def isDone(self):
        if self.termin is None:
            return False
        else:
            return self.termin < datetime.date.today()


class Prowadzacy(Base):
    __tablename__ = "prowadzacy"

    id = Column(Integer, primary_key=True)
    data_ost_hospitacji = Column(Date)
    habilitowany = Column(Boolean)
    uznany = Column(Boolean)
    doswiadczony = Column(Boolean)
    imie = Column(VARCHAR)
    nazwisko = Column(VARCHAR)
    tytul = Column(VARCHAR)
    stopien_naukowy = Column(VARCHAR)
    stanowisko = Column(VARCHAR)
    jednostka_organizacyjna = Column(VARCHAR)

    prowadzacy_zespolhospitujacy = relationship("ProwadzacyZespolHospitujacy")
    przewodniczacy = relationship("ZespolHospitujacy")

    def __str__(self):
        return (self.tytul if self.tytul else "") + " " + (self.imie if self.imie else "") + " " + (
            self.nazwisko if self.nazwisko else "")

    @property
    def fullName(self):
        return (self.tytul if self.tytul else "") + " " + (self.imie if self.imie else "") + " " + (
            self.nazwisko if self.nazwisko else "")


class ProwadzacyZespolHospitujacy(Base):
    __tablename__ = "prowadzacy_zespolhospitujacy"

    prowadzacy_id = Column(Integer, ForeignKey("prowadzacy.id"), primary_key=True)
    zespol_id = Column(Integer, ForeignKey("zespolhospitujacy.id"), primary_key=True)


class Kurs(Base):
    __tablename__ = "kurs"

    kod = Column(VARCHAR, primary_key=True)
    forma = Column(Integer)
    nazwa = Column(VARCHAR)
    Semestr = Column(Integer)


class Protokol(Base):
    __tablename__ = "protokol"

    id = Column(Integer, primary_key=True)
    hospitacja_id = Column(Integer, ForeignKey("hospitacja.id"))
    data_wystawienia = Column(Date)
    zaakceptowane = Column(Integer)
    data_zapoznania = Column(Date)


class ZespolHospitujacy(Base):
    __tablename__ = "zespolhospitujacy"

    id = Column(Integer, primary_key=True)
    prowadzacy_id = Column(Integer, ForeignKey("prowadzacy.id"))


class DzienTygodnia(enum.Enum):
    pn = 0
    wt = 1
    sr = 2
    czw = 3
    pt = 4
    sob = 5
    nd = 6


class GrupaZajeciowa(Base):
    __tablename__ = "grupazajeciowa"

    kod = Column(VARCHAR, primary_key=True)
    dzien = Column(Enum(DzienTygodnia))
    godzina = Column(Integer)
    minuta = Column(Integer)
    nazwa = Column(VARCHAR)
    miejsce = Column(VARCHAR)
    liczba_zapisanych = Column(Integer)
    kurs_kod = Column(ForeignKey("kurs.kod"))
    prowadzacy_id = Column(ForeignKey("prowadzacy.id"))