from models.entities import *
from models.db_utils import *


class HospitacjaModel:

    @staticmethod
    def getAll():
        return get_all(Hospitacja)

    @staticmethod
    def getById(hospitacja_id: int):
        session = make_session()
        return session.query(Hospitacja).get(hospitacja_id)

    @staticmethod
    def add(hospitacja: Hospitacja):
        session = make_session()
        session.add(hospitacja)
        session.commit()

    @staticmethod
    def update(hospitacja_id: int, termin=None, harmonogram_id=None, zespol_hospitujacy_id=None, prowadzacy_id=None,
               kurs_kod=None):

        session = make_session()
        hospitacja = session.query(Hospitacja).get(hospitacja_id)

        hospitacja.termin = termin if termin is not None else hospitacja.termin
        hospitacja.harmonogram_id = harmonogram_id if harmonogram_id is not None else hospitacja.harmonogram_id
        hospitacja.zespol_hospitujacy_id = zespol_hospitujacy_id if zespol_hospitujacy_id is not None else hospitacja.zespol_hospitujacy_id
        hospitacja.prowadzacy_id = prowadzacy_id if prowadzacy_id is not None else hospitacja.prowadzacy_id
        hospitacja.kurs_kod = kurs_kod if kurs_kod is not None else hospitacja.kurs_kod

        session.commit()

    @staticmethod
    def delete(hospitacja_id: int):
        session = make_session()
        hospitacja = session.query(Hospitacja).get(hospitacja_id)
        session.delete(hospitacja)
        session.commit()


class ProwadzacyModel:
    @staticmethod
    def getAll():
        return get_all(Prowadzacy)

    @staticmethod
    def getById(prowadzacy_id: int):
        session = make_session()
        return session.query(Prowadzacy).get(prowadzacy_id)

class KursModel:

    @staticmethod
    def getAll():
        return get_all(Kurs)

    @staticmethod
    def getById(kurs_kod):
        session = make_session()
        return session.query(Kurs).get(kurs_kod)


class ProwadzacyZespolHospitujacyModel:

    @staticmethod
    def getAll():
        return get_all(ProwadzacyZespolHospitujacy)

    @staticmethod
    def getById(prowadzacy_id: int, zespol_id):
        session = make_session()
        return session.query(ProwadzacyZespolHospitujacy).get(prowadzacy_id, zespol_id)

class ProtokolModel:
    @staticmethod
    def getAll():
        return get_all(Protokol)

    @staticmethod
    def getById(protokol_id: int):
        session = make_session()
        return session.query(Protokol).get(protokol_id)

class ZespolHospitujacyModel:

    @staticmethod
    def getAll():
        return get_all(ZespolHospitujacy)

    @staticmethod
    def getById(zespol_id: int):
        session = make_session()
        return session.query(ZespolHospitujacy).get(zespol_id)

class GrupaZajeciowaModel:

    @staticmethod
    def getAll():
        return get_all(GrupaZajeciowa)

    @staticmethod
    def getById(kod: str):
        session = make_session()
        return session.query(GrupaZajeciowa).get(kod)


