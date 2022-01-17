
import { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import ProwadzacyContext from "../context/ProwadzacyContext";

import settings from "../settings";

function HospitacjaItem({ imie, nazwisko, nazwa_kursu, kod, id }) {
    const navigate = useNavigate()

    const onWypelnijProtokol = e => {
        navigate(`/Hospitacje/${id}`)
    }

    return (
        <li>
            <div>
                <div>
                    Imie i nazwisko: {`${imie} ${nazwisko}`}
                </div>
                <div>
                    Nazwa kursu - kod: {`${nazwa_kursu} - ${kod}`}
                </div>
            </div>
            <div>
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
        kod={h.kursKod}
        nazwa_kursu={`${h.kursKodNavigation.nazwa}`}
        key={h.id}
    />)

    return (
        <div>
            <ul>
                {hospitacje_items}
            </ul>
        </div>
    );
}

export default Hospituj;