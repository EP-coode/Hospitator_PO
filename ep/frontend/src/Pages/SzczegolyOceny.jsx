import { useState, useContext, useEffect } from "react"
import { useParams } from "react-router-dom"
import ProwadzacyContext from "../context/ProwadzacyContext"

import settigns from '../settings'

function SzeegolyOceny() {
    const { idProtokolu } = useParams()
    const [szczegolyOceny, setSzczegolyOceny] = useState({
        kurs: {
            kod: "",
            nazwa: ""
        },
        formulazprotokolus: {
            id: 1,
            ocenaKoncowa: 1,
            punktualnie: false,
            opuznienie: false,
            sprawdzonoObecnosc: false,
            liczbaObecnych: 1,
            salaPrzystosowana: false,
            powodyNieprzystosowania: "",
            trescKursuZgodna: false
        }
    })
    const { idProwadzacego } = useContext(ProwadzacyContext)

    useEffect(() => {
        async function GetSzczegoly() {
            const url = `${settigns.api_url}/Oceny/${idProwadzacego}/${idProtokolu}`
            debugger
            const result = await fetch(url)
            const data = await result.json()
            setSzczegolyOceny(data)
            debugger
        }
        GetSzczegoly()
        return () => {
            // todo
        }
    }, [idProwadzacego, idProtokolu])

    return (
        <div className="szczegoly-oceny">
            <div className="szczegoly-oceny__informacje-ogolne">
                <h3>Ogólne informacje: </h3>
                <div>
                    Kurs: {`${szczegolyOceny.kurs.kod} - ${szczegolyOceny.kurs.nazwa}`}
                </div>
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