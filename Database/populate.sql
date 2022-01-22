INSERT INTO `harmonogram` (`id`, `zatwierdzony_wkozjk`, `zatwierdzony_dyrektor`) VALUES
(1, 1, 1);

INSERT INTO `kurs` (`kod`, `forma`, `nazwa`, `Semestr`) VALUES
('INZ002012L', 'labolatorium', 'Podstawy internetu rzeczy', 5),
('INZ002013L', 'labolatorium', 'Podstawy internetu rzeczy', 5),
('INZ002014L', 'labolatorium', 'Podstawy internetu rzeczy', 5),
('INZ002015L', 'labolatorium', 'Podstawy internetu rzeczy', 5),
('INZ004358W', 'wyklad', 'Cyberbezpieczeństwo', 5),
('SCZ001114S', 'seminarium', 'Techniki prezentacji', 5);

INSERT INTO `prowadzacy` (`id`, `data_ost_hospitacji`, `habilitowany`, `uznany`, `doswiadczony`, `imie`, `nazwisko`, `tytul`, `stopien_naukowy`, `stanowisko`, `jednostka_organizacyjna`) VALUES
(1, '2020-01-01', 1, 1, 1, 'Ferdynand', 'Kiepski', NULL, NULL, NULL, NULL),
(2, '2022-01-08', 0, 0, 0, 'Marian', 'Paździoch', 'dr.', NULL, NULL, NULL),
(3, '2022-01-07', 1, 1, 1, 'Arnold', 'Boczek', NULL, 'dr. hab.', NULL, NULL),
(4, '2022-01-19', 1, 1, 1, 'Helena', 'Kiepska', 'dr.', NULL, NULL, NULL),
(5, '2017-01-11', 1, 1, 1, 'Andrzej', 'Kozłowski', 'prezes', NULL, NULL, NULL);

INSERT INTO `grupazajeciowa` (`kod`, `dzien`, `godzina`, `minuta`, `nazwa`, `miejsce`, `liczba_zapisanych`, `kurs_kod`, `prowadzacy_id`) VALUES
('K01-95a', NULL, 7, 30, 'Podstawy internetu rzeczy', 'Sala wirtualna', 15, 'INZ002012L', 1),
('K01-98a', 'pt', 13, 15, 'Cyberbezpieczeństwo', 'D-2, s. 107a', 132, 'INZ004358W', 2),
('K02-11b', 'pt', 7, 30, 'Techniki prezentacji', 'C-3, s. 118', 17, 'SCZ001114S', 3);

INSERT INTO `zespolhospitujacy` (`id`, `prowadzacy_id`) VALUES
(1, 4),
(2, 5);

INSERT INTO `prowadzacy_zespolhospitujacy` (`prowadzacy_id`, `zespol_id`) VALUES
(1, 1),
(1, 2),
(5, 2);

INSERT INTO `hospitacja` (`id`, `termin`, `harmonogram_id`, `zespol_hospitujacy_id`, `prowadzacy_id`, `kurs_kod`) VALUES
(1, '2022-01-19', 1, 1, 3, 'SCZ001114S'),
(2, '2022-01-31', 1, 1, 2, 'INZ004358W'),
(3, '2022-01-3', 1, 1, 2,'INZ002013L'),
(4, '2022-01-1', 1, 1, 3, 'INZ002014L'),
(5, '2022-01-2', 1, 2, 3, 'INZ002015L');

INSERT INTO `protokol` (`id`, `hospitacja_id`, `data_wystawienia`, `zaakceptowane`, `data_zapoznania`) VALUES 
(1, 1, '2022-01-04', 0, NULL),
(2, 4, '2022-01-12', 0, NULL);

INSERT INTO `formulazprotokolu` (`id`, `protokol_id`, `ocena_koncowa`, `punktualnie`, `opuznienie`, `sprawdzono_obecnosc`, `liczba_obecnych`, `sala_przystosowana`, `powody_nieprzystosowania`, `tresc_kursu_zgodna`) VALUES 
(1, 1, 5, 1, NULL, 1, 13, 0, 'Sprzęt z 19 wieku', NULL),
(2, 2, 3.5, 0, 20, 0, 7, 1, NULL, NULL);
