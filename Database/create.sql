
-- @BLOCK
CREATE TABLE Kurs(
    kod VARCHAR(255),
    forma INTEGER,
    nazwa VARCHAR(255),
    Semestr INTEGER,
    CONSTRAINT kurs_pk PRIMARY KEY(kod)
);
-- @BLOCK
CREATE TABLE Prowadzacy(
    id INTEGER,
    data_ost_hospitacji DATE,
    habilitowany BOOLEAN,
    uznany BOOLEAN,
    doswiadczony BOOLEAN,
    imie VARCHAR(255),
    nazwisko VARCHAR(255),
    tytol VARCHAR(255),
    stopien_naukowy VARCHAR(255),
    stanowisko VARCHAR(255),
    jednostka_organizacyjna VARCHAR(255),
    CONSTRAINT prowadzacy_pk PRIMARY KEY(id)
);

-- @BLOCK
CREATE TABLE GrupaZajeciowa(
    kod VARCHAR(255),
    dzien INTEGER,
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
    id INTEGER,
    zatwierdzony_wkozjk BOOLEAN,
    zatwierdzony_dyrektor BOOLEAN,

    CONSTRAINT harmonogram_pk PRIMARY KEY (id)
);

-- @BLOCK
CREATE TABLE ZespolHospitujacy(
    id INTEGER PRIMARY KEY,
    prowadzacy_id INTEGER,
    CONSTRAINT zesp_hosp_fk_p FOREIGN KEY (prowadzacy_id) REFERENCES Prowadzacy(id)
);

-- @BLOCK
CREATE TABLE Prowadzacy_ZespolHospitujacy(
    prowadzacy_id INTEGER,
    zespol_id INTEGER,

    FOREIGN KEY (prowadzacy_id) REFERENCES Prowadzacy(id),
    FOREIGN KEY (zespol_id) REFERENCES ZespolHospitujacy(id)
);

-- @BLOCK
CREATE TABLE Hospitacja(
    id INTEGER,
    termin DATE, -- tutaj date czy datetime lepiej ?
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
    id INTEGER PRIMARY KEY,
    hospitacja_id INTEGER NOT NULL UNIQUE,
    data_wystawienia DATE,
    zakceptowane BOOLEAN,
    data_zapoznania DATE
);

-- @BLOCK
CREATE TABLE FormulazProtokolu(
    id INTEGER PRIMARY KEY,
    protokol_id INTEGER,
    ocena_koncowa INTEGER,
    punktualnie BOOLEAN,
    opuznienie INTEGER,
    sprawdzono_obecnosc BOOLEAN,
    liczba_obecnych INTEGER,
    sala_przystosowana BOOLEAN,
    powody_nieprzystosowania VARCHAR(255),
    tresc_kursu_zgodna BOOLEAN,

    FOREIGN KEY (protokol_id) REFERENCES Protokol(id)
);

-- @BLOCK
CREATE TABLE Odwolanie(
    id INTEGER PRIMARY KEY,
    protokol_id INTEGER NOT NULL UNIQUE,
    prowadzacy_id INTEGER,
    data_odwolania DATE,
    uzasadnienie VARCHAR(255),
    _status INTEGER,

    FOREIGN KEY (protokol_id) REFERENCES Protokol(id),
    FOREIGN KEY (prowadzacy_id) REFERENCES Prowadzacy(id)
);