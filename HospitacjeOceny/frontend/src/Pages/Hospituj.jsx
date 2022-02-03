
import { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import ProwadzacyContext from "../context/ProwadzacyContext";
import Navigation from '../Components/Navigation'

import settings from "../settings";
import './Hospituj.css'

function HospitacjaItem({ imie, nazwisko, nazwa_kursu, kod, id }) {
    const navigate = useNavigate()

    const onWypelnijProtokol = e => {
        navigate(`/Hospitacje/${id}`)
    }

    return (
        <li className="hospitacja">
            <div className="hospitacja__details">
                <div>
                    <strong>Imie i nazwisko:</strong> {`${imie} ${nazwisko}`}
                </div>
                <div>
                    <strong>Nazwa kursu - kod:</strong> {`${nazwa_kursu} - ${kod}`}
                </div>
            </div>
            <div className="hospitacja__actions">
                <button onClick={onWypelnijProtokol}>Wypełnij protokół</button>
            </div>
        </li>
    )
}


function Hospituj() {
    const prowadzacyCtx = useContext(ProwadzacyContext)
    const [hospitacje, setHospitacje] = useState([])

    useEffect(() => {
        async function fetchHospitacjeProwadzacego() {
            try {
                const url = `${settings.api_url}/Hospitacja/${prowadzacyCtx.idProwadzacego}`
                const result = await fetch(url)
                const data = await result.json()
                setHospitacje(data)
            }
            catch (e) {
                console.log(e);
            }
        }

        fetchHospitacjeProwadzacego()
    }, [prowadzacyCtx.idProwadzacego])



    const hospitacje_items = hospitacje.map(h => <HospitacjaItem
        id={h.id}
        imie={h.prowadzacy.imie}
        nazwisko={h.prowadzacy.nazwisko}
        kod={h.kursKod || "Nie ustalono"}
        nazwa_kursu={`${h.kursKodNavigation ? h.kursKodNavigation.nazwa : ""}`}
        key={h.id}
    />)

    return (
        <div>
            <Navigation />
            <ul className="hospitacje-list">
                {hospitacje_items}
            </ul>
        </div>
    );
}

export default Hospituj;