import { useState, useContext, useEffect } from "react"
import { useParams } from "react-router-dom"
import ProwadzacyContext from "../context/ProwadzacyContext"

import settigns from '../settings'

function SzeegolyOceny({ szczegolyOceny }) {

    return (
        <div className="szczegoly-oceny">
            <div className="szczegoly-oceny__informacje-ogolne">
                <h3>Ogólne informacje: </h3>
                {
                    szczegolyOceny.kurs ?
                        <div>Kurs : {`${szczegolyOceny.kurs.kod} - ${szczegolyOceny.kurs.nazwa}`}</div>
                        : <div>Kurs: Nie ustalono</div>

                }
                <div>
                    Termin: {`${szczegolyOceny.dataWystawienia}`}
                </div>
            </div>
            <div className="szczegoly-oceny__informacje">
                <h3>Szczegóły oceny:</h3>
                <div>
                    Opóźnienie:
                    {
                        szczegolyOceny.formulazprotokolus.opuznienie
                            ? <span style={{ color: "red" }}>{szczegolyOceny.formulazprotokolus.opuznienie} min</span>
                            : <span style={{ color: "green" }}> Brak</span>
                    }
                </div>
                <div>
                    Liczba obecnych: {`${szczegolyOceny.formulazprotokolus.liczbaObecnych}`}
                </div>
                <div>
                    Przygotowanie sali:
                    {
                        szczegolyOceny.formulazprotokolus.salaPrzystosowana
                            ? <span style={{ color: "green" }}> Przystosowana</span>
                            : <span>{`${szczegolyOceny.formulazprotokolus.powodyNieprzystosowania}`}</span>
                    }
                </div>
                <div>
                    Zgodność terści z programem kursu: {szczegolyOceny.formulazprotokolus.trescKursuZgodna
                        ? <span style={{ color: "green" }}>TAK</span>
                        : <span style={{ color: "red" }}>NIE</span>}
                </div>
            </div>
        </div>
    )
}

export default SzeegolyOceny