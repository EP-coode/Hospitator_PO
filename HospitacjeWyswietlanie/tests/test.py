import datetime
import unittest
from models.utils import *
import models.model as model


class TestModels(unittest.TestCase):

    def test_getProcessedHospitalisations(self):
        model.url.setURL('mysql+mysqlconnector://root:root@localhost:3306/po_test')
        processedHosp1 =[{'id': 1, 'termin': datetime.date(2022, 1, 19), 'harmonogram_id': 1, 'zespolhospitujacy_id': 1,
                          'prowadzacy_id': 3, 'kurs_kod': 'SCZ001114S', 'isDone': True, 'tytul_imie_nazwisko': ' Arnold Boczek'},
                         {'id': 2, 'termin': datetime.date(2022, 1, 31), 'harmonogram_id': 1, 'zespolhospitujacy_id': 1,
                          'prowadzacy_id': 2, 'kurs_kod': 'INZ004358W', 'isDone': True, 'tytul_imie_nazwisko': 'dr. Marian Paździoch'},
                         {'id': 3, 'termin': datetime.date(2022, 1, 3), 'harmonogram_id': 1, 'zespolhospitujacy_id': 1,
                          'prowadzacy_id': 2, 'kurs_kod': 'INZ002013L', 'isDone': True, 'tytul_imie_nazwisko': 'dr. Marian Paździoch'},
                         {'id': 4, 'termin': datetime.date(2022, 1, 1), 'harmonogram_id': 1, 'zespolhospitujacy_id': 1,
                          'prowadzacy_id': 3, 'kurs_kod': 'INZ002014L', 'isDone': True, 'tytul_imie_nazwisko': ' Arnold Boczek'},
                         {'id': 5, 'termin': datetime.date(2022, 1, 2), 'harmonogram_id': 1, 'zespolhospitujacy_id': 2,
                          'prowadzacy_id': 3, 'kurs_kod': 'INZ002015L', 'isDone': True, 'tytul_imie_nazwisko': ' Arnold Boczek'},
                         {'id': 17, 'termin': None, 'harmonogram_id': None, 'zespolhospitujacy_id': 1, 'prowadzacy_id': 2,
                          'kurs_kod': 'INZ004358W', 'isDone': False, 'tytul_imie_nazwisko': 'dr. Marian Paździoch'}]
        self.assertEqual(getProcessedHospitalisations(), processedHosp1)
        self.assertNotEqual(getProcessedHospitalisations(), [{}])
        self.assertEqual(len(getProcessedHospitalisations()), len(list(model.HospitacjaModel.getAll())))

    def test_isDone(self):
        """ isDone - checks if hospitalisations is already done """

        model.url.setURL('mysql+mysqlconnector://root:root@localhost:3306/po_test')
        hospitalisationSample1 = model.Hospitacja(termin=datetime.date(2022,1,29))
        hospitalisationSample2 = model.HospitacjaModel.getById(1)
        self.assertFalse(isDone(hospitalisationSample1.id))
        self.assertTrue(isDone(hospitalisationSample2.id))

    def test_getDictTeacherCourses(self):
        model.url.setURL('mysql+mysqlconnector://root:root@localhost:3306/po_test')
        testData1 = {1: {'INZ002012L'}, 2: {'INZ004358W'}, 3: {'SCZ001114S'}}
        testData2 = {1: {'INZ002012L'}, 2: {'INZ004358W'}}
        self.assertEqual(getDictTeacherCourses(), testData1)
        self.assertNotEqual(getDictTeacherCourses(), testData2)

    def test_getTeachersWithCourses(self):
        model.url.setURL('mysql+mysqlconnector://root:root@localhost:3306/po_test')
        self.assertEqual([x.id for x in getTeachersWithCourse()], [1,2,3])
        self.assertNotEqual([x.id for x in getTeachersWithCourse()], [1,2,4])
        self.assertNotEqual([x.id for x in getTeachersWithCourse()], [])


    def test_getHospTeamById(self):
        model.url.setURL('mysql+mysqlconnector://root:root@localhost:3306/po_test')
        self.assertEqual(getHospTeamByHospId(40), None)
        self.assertEqual(getHospTeamByHospId(1).id, 1)
        self.assertNotEqual(getHospTeamByHospId(1), 2)

    def test_teamMembers(self):
        model.url.setURL('mysql+mysqlconnector://root:root@localhost:3306/po_test')
        self.assertEqual([x.id for x in teamMembers(1)], [1])
        self.assertEqual([x.id for x in teamMembers(2)], [1,5])
        self.assertNotEqual([x.id for x in teamMembers(2)], [1,2, 3])
        self.assertEqual([x.id for x in teamMembers(3)], [])

    def test_getTeachersTeamsIds(self):
        model.url.setURL('mysql+mysqlconnector://root:root@localhost:3306/po_test')
        self.assertEqual(getTeacherTeamsIds(-1), [])
        self.assertEqual(getTeacherTeamsIds(1), [1, 2])
        self.assertNotEqual(1, [])

    def test_getTeamsWithMembers(self):
        model.url.setURL('mysql+mysqlconnector://root:root@localhost:3306/po_test')
        self.assertEqual(list(getTeamsWithMembers(1).keys()), [1,2])
        self.assertEqual({y:[x.id for x in getTeamsWithMembers(1)[y]] for y in getTeamsWithMembers(1)}, {1:[1], 2:[1,5]})
        self.assertNotEqual({y:[x.id for x in getTeamsWithMembers(1)[y]] for y in getTeamsWithMembers(1)}, {1:[1]})

    def test_getTeamHospitalisations(self):
        model.url.setURL('mysql+mysqlconnector://root:root@localhost:3306/po_test')
        self.assertEqual(len(getTeamHospitalisations(1)),5)
        self.assertEqual(len(getTeamHospitalisations(9)),0)
        self.assertEqual(len(getTeamHospitalisations(-1)),0)
        self.assertEqual(getTeamHospitalisations(2),
                         [{'teacher': ' Arnold Boczek',
                           'isDone': 'Zakończona',
                           'date': datetime.date(2022, 1, 2)}])
