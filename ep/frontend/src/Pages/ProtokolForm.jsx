import { useState } from "react";
import { useParams, useNavigate } from "react-router-dom"
import { Fragment } from "react/cjs/react.production.min";

import Modal from '../Components/Modal'
import Backdrop from '../Components/Backdrop'
import Spinner from '../Components/Spinner'

import settigns from '../settings'

import styles from "./ProtokolForm.css"

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
    const [ocenaKoncowa, setOcenaHoncowa] = useState(3)
    const [sending, setSending] = useState(false)
    const [success, setSucces] = useState(false)

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
                if (target.value.length < 255)
                    setPowodyNieprzystosowania(target.value)
                break;
            case "ocena-koncowa":
                setOcenaHoncowa(target.value)
                break;
        }
    }
    const handlePowrotClick = e => {
        e.preventDefault()
        navigate(-1)
    }

    const handleZatwierdzOcene = e => {
        e.preventDefault()
        wyslijFormularz()
    }

    const validate = () => {
        return true
    }

    const wyslijFormularz = async () => {
        if (!validate() || sending)
            return;

        setSending(true)

        const body = {
            "hospitacjaId": parseInt(idHospitacji),
            "ocenaKoncowa": ocenaKoncowa,
            "punktualnie": punktualnie,
            "opuznienie": punktualnie ? 0 : opoznienie,
            "sprawdzonoObecnosc": obecnosc,
            "liczbaObecnych": parseInt(liczbaObecnych),
            "salaPrzystosowana": przystosowanie,
            "powodyNieprzystosowania": powodyNieprzystosowania,
            "trescKursuZgodna": trescZgodna
        }
        const url = `${settigns.api_url}/Protokoly`
        try {
            const result = await fetch(url,
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(body)
                });
            if (result.status == 201) {
                setSucces(true)
            }
        }
        catch (e) {
            console.log(e);
        }
        finally {
            setSending(false)
        }
    }

    return (
        <form className="protokol">
            {
                success &&
                <Fragment>
                    <Modal
                        haderText={"Pomyślnie przesłano formularz"}
                        confirmText="Ok"
                        onConfirm={() => navigate('/Hospitacje')}
                    />
                    <Backdrop />
                </Fragment>

            }
            <h2 className="protokol__header">Protokol {idHospitacji}</h2>
            <div className="protokol__input-group">
                <label htmlFor="punktualnie">
                    <input type="checkbox" id="punktualnie"
                        checked={punktualnie}
                        onChange={handleDataChange} />Czy zajęcia odbyły się punktualnie? </label>
                <label htmlFor="opoznienie"> Opóżnienie w minutach:
                    <input type="number" id="opoznienie" value={opoznienie} disabled={punktualnie} onChange={handleDataChange} min={0} /> </label>
            </div>
            <div className="protokol__input-group">
                <label htmlFor="obecnosc">
                    <input type="checkbox" id="obecnosc"
                        checked={obecnosc}
                        onChange={handleDataChange} />
                    Czy sprawdzono obecność studentów? </label>
                <label htmlFor="liczba-obecnych"> Liczba obecnych studentów:
                    <input type="number" id="liczba-obecnych" value={liczbaObecnych} onChange={handleDataChange} />
                </label>
            </div>
            <div className="protokol__input-group --column">
                <label htmlFor="przystosowanie">
                    <input type="checkbox" id="przystosowanie"
                        checked={przystosowanie}
                        onChange={handleDataChange} />
                    Czy sala i jej wyposarzenie są przystosowane do formy prowadzania zajęć? </label>
                <label htmlFor="powody-nieprzystosowania">
                    Podaj powody nieprzystsowania:
                </label>
                <textarea
                    id="powody-nieprzystosowania"
                    value={powodyNieprzystosowania}
                    disabled={przystosowanie}
                    onChange={handleDataChange}
                    rows={4}
                    cols={50} />
                <span>{powodyNieprzystosowania.length}/255</span>
            </div>
            <div className="protokol__input-group">
                <label htmlFor="tresc-zgodna">
                    <input type="checkbox" id="tresc-zgodna"
                        checked={trescZgodna}
                        onChange={handleDataChange} />
                    Treść zajęć jest zgodna z programem kursu i umażliwia osiągnięcie założonych efektów kształecenia</label>
            </div>
            <div className="protokol__input-group">
                <label htmlFor="ocena-koncowa">
                    Ocena koncowa:
                    <select id="ocena-koncowa"
                        value={ocenaKoncowa}
                        onChange={handleDataChange} >
                        <option value={2}>2</option>
                        <option value={3}>3</option>
                        <option value={3.5}>3.5</option>
                        <option value={4}>4</option>
                        <option value={4.5}>4.5</option>
                        <option value={5}>5</option>
                        <option value={5.5}>5.5</option>
                    </select>
                </label>
            </div>
            <div className="protokol__controls">
                <button onClick={handlePowrotClick}>Powrót</button>
                <button onClick={handleZatwierdzOcene}>
                    Zatwierz ocenę
                    {sending && <Spinner />}
                </button>
            </div>
        </form >
    );
}

export default ProtokolFrom;