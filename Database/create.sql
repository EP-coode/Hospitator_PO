ALTER DATABASE hospitator DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci;

-- @BLOCK
CREATE TABLE Kurs(
    kod VARCHAR(255),
    forma ENUM(
        'wyklad',
        'labolatorium',
        'cwiczenia',
        'projekt',
        'seminarium',
        'praktyki',
        'praca_dyplomowa'
    ),
    nazwa VARCHAR(255),
    Semestr INTEGER,
    CONSTRAINT kurs_pk PRIMARY KEY(kod)
);
-- @BLOCK
CREATE TABLE Prowadzacy(
    id INTEGER AUTO_INCREMENT,
    data_ost_hospitacji DATE,
    habilitowany BOOLEAN,
    uznany BOOLEAN,
    doswiadczony BOOLEAN,
    imie VARCHAR(255),
    nazwisko VARCHAR(255),
    tytul VARCHAR(255),
    stopien_naukowy VARCHAR(255),
    stanowisko VARCHAR(255),
    jednostka_organizacyjna VARCHAR(255),
    CONSTRAINT prowadzacy_pk PRIMARY KEY(id)
);
-- @BLOCK
CREATE TABLE GrupaZajeciowa(
    kod VARCHAR(255),
    dzien ENUM('pn', 'wt', 'sr', 'czw', 'pt', 'sob', 'nd'),
    godzina INTEGER,
    minuta INTEGER,
    nazwa VARCHAR(255),
    miejsce VARCHAR(255),
    liczba_zapisanych INTEGER,
    kurs_kod VARCHAR(255),
    prowadzacy_id INTEGER,
    CONSTRAINT grupa_zaj_pk PRIMARY KEY(kod),
    CONSTRAINT grupa_zaj_fk_k FOREIGN KEY (kurs_kod) REFERENCES Kurs(kod),
    CONSTRAINT grupa_zaj_fk_p FOREIGN KEY (prowadzacy_id) REFERENCES Prowadzacy(id)
);
-- @BLOCK 
CREATE TABLE Harmonogram(
    id INTEGER AUTO_INCREMENT,
    zatwierdzony_wkozjk BOOLEAN,
    zatwierdzony_dyrektor BOOLEAN,
    CONSTRAINT harmonogram_pk PRIMARY KEY (id)
);
-- @BLOCK
CREATE TABLE ZespolHospitujacy(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    prowadzacy_id INTEGER,
    CONSTRAINT zesp_hosp_fk_p FOREIGN KEY (prowadzacy_id) REFERENCES Prowadzacy(id)
);
-- @BLOCK
CREATE TABLE Prowadzacy_ZespolHospitujacy(
    prowadzacy_id INTEGER,
    zespol_id INTEGER,
    FOREIGN KEY (prowadzacy_id) REFERENCES Prowadzacy(id),
    FOREIGN KEY (zespol_id) REFERENCES ZespolHospitujacy(id),
     PRIMARY KEY(prowadzacy_id, zespol_id)
);
-- @BLOCK
CREATE TABLE Hospitacja(
    id INTEGER AUTO_INCREMENT,
    termin DATE,
    harmonogram_id INTEGER,
    zespol_hospitujacy_id INTEGER,
    prowadzacy_id INTEGER,
    kurs_kod VARCHAR(255),
    PRIMARY KEY (id),
    FOREIGN KEY (harmonogram_id) REFERENCES Harmonogram(id),
    FOREIGN KEY (zespol_hospitujacy_id) REFERENCES ZespolHospitujacy(id),
    FOREIGN KEY (prowadzacy_id) REFERENCES Prowadzacy(id),
    FOREIGN KEY (kurs_kod) REFERENCES Kurs(kod)
);
-- @BLOCK
CREATE TABLE Protokol(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    hospitacja_id INTEGER NOT NULL UNIQUE,
    data_wystawienia DATE,
    zakceptowane BOOLEAN,
    data_zapoznania DATE,

    FOREIGN KEY (hospitacja_id) REFERENCES Hospitacja(id)
);
-- @BLOCK
CREATE TABLE FormulazProtokolu(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    protokol_id INTEGER,
    ocena_koncowa ENUM('2', '3', '3.5', '4', '4.5', '5', '5.5'),
    punktualnie BOOLEAN,
    opuznienie INTEGER,
    sprawdzono_obecnosc BOOLEAN,
    liczba_obecnych INTEGER,
    sala_przystosowana BOOLEAN,
    powody_nieprzystosowania VARCHAR(255),
    tresc_kursu_zgodna BOOLEAN,
    FOREIGN KEY (protokol_id) REFERENCES Protokol(id) ON DELETE CASCADE
);
-- @BLOCK
CREATE TABLE Odwolanie(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    protokol_id INTEGER NOT NULL UNIQUE,
    prowadzacy_id INTEGER,
    data_odwolania DATE,
    uzasadnienie VARCHAR(255),
    _status ENUM('oczekujaca', 'odrzucona', 'akceptowana'),
    FOREIGN KEY (protokol_id) REFERENCES Protokol(id),
    FOREIGN KEY (prowadzacy_id) REFERENCES Prowadzacy(id)
);
