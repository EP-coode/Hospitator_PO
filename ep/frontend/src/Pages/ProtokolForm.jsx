import { useState } from "react";
import { useParams, useNavigate } from "react-router-dom"

function ProtokolFrom() {
    const navigate = useNavigate()
    const { idHospitacji } = useParams()
    const [punktualnie, setPunktualnie] = useState(false)
    const [opoznienie, setOpoznienie] = useState(0)
    const [obecnosc, setObecnosc] = useState(false)
    const [liczbaObecnych, setLiczbaObecnych] = useState(0)
    const [przystosowanie, setPrzystosowanie] = useState(false)
    const [powodyNieprzystosowania, setPowodyNieprzystosowania] = useState("")
    const [trescZgodna, settrescZgodna] = useState(false)

    const handleDataChange = ({ target }) => {
        switch (target.id) {
            case "punktualnie":
                setPunktualnie(prev => !prev)
                break;
            case "obecnosc":
                setObecnosc(prev => !prev)
                break;
            case "przystosowanie":
                setPrzystosowanie(prev => !prev)
                break;
            case "tresc-zgodna":
                settrescZgodna(prev => !prev)
                break;
            case "opoznienie":
                setOpoznienie(target.value)
                break;
            case "liczba-obecnych":
                setLiczbaObecnych(target.value)
                break;
            case "powody-nieprzystosowania":
                setPowodyNieprzystosowania(target.value)
                break;
        }
    }
    const handlePowrotClick = e => {
        e.preventDefault()
        navigate(-1)
    }

    const handleZatwierdzOcene = e => {
        e.preventDefault()
        console.log("zatwierdzam");
    }

    return (
        <div>
            Protokol {idHospitacji}
            <form>
                <div>
                    <input type="checkbox" id="punktualnie"
                        checked={punktualnie}
                        onChange={handleDataChange} />
                    <label htmlFor="punktualnie"> Czy zajęcia odbyły się punktualnie? </label>
                    <label htmlFor="opoznienie"> Opóżnienie w minutach: </label>
                    <input type="number" id="opoznienie" value={opoznienie} disabled={punktualnie} onChange={handleDataChange} min={0} />
                </div>
                <div>
                    <input type="checkbox" id="obecnosc"
                        checked={obecnosc}
                        onChange={handleDataChange} />
                    <label htmlFor="obecnosc"> Czy sprawdzono obecność studentów? </label>
                    <label htmlFor="liczba-obecnych"> Liczba obecnych studentów: </label>
                    <input type="number" id="liczba-obecnych" value={liczbaObecnych} onChange={handleDataChange} />
                </div>
                <div>
                    <input type="checkbox" id="przystosowanie"
                        checked={przystosowanie}
                        onChange={handleDataChange} />
                    <label htmlFor="przystosowanie"> Czy sala i jej wyposarzenie są przystosowane do formy prowadzania zajęć? </label>
                    <label htmlFor="powody-nieprzystosowania">Podaj powody nieprzystsowania: </label>
                    <input type="text" id="powody-nieprzystosowania" value={powodyNieprzystosowania} disabled={przystosowanie} onChange={handleDataChange} />
                </div>
                <div>
                    <input type="checkbox" id="tresc-zgodna"
                        checked={trescZgodna}
                        onChange={handleDataChange} />
                    <label htmlFor="tresc-zgodna"> Treść zajęć jest zgodna z programem kursu i umażliwia osiągnięcie założonych efektów kształecenia</label>
                </div>
                <div>
                    <button onClick={handlePowrotClick}>Powrót</button>
                    <button onClick={handleZatwierdzOcene}>Zatwierz ocenę</button>
                </div>
            </form >
        </div >
    );
}

export default ProtokolFrom;