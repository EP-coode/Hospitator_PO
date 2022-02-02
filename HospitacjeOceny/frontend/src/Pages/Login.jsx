
import { useState, useContext, useEffect } from 'react'

import { useNavigate } from "react-router-dom"

import ProwadzacyContext from "../context/ProwadzacyContext"

import settings from '../settings'
import './Login.css'

function Login() {
    const navigate = useNavigate()
    const [prowadzacy, setProwadzacy] = useState([])
    const prowadzacyCtx = useContext(ProwadzacyContext)

    useEffect(() => {
        async function fetchProwadzacy() {
            try {
                const result = await fetch(`${settings.api_url}/Prowadzacy`)
                const data = await result.json()
                setProwadzacy(data)
            }
            catch (e) {
                console.log(e);
            }
        }
        fetchProwadzacy()
        return () => {
            // todo
        }
    }, [])


    const onProwadzacyChange = e => {
        const prowadzacyId = e.target.value
        const p = prowadzacy.find(p => p.id == prowadzacyId)
        prowadzacyCtx.setIdProwadzacego(prowadzacyId)
        prowadzacyCtx.setNazwaProwadzacego(`${p.imie} ${p.nazwisko}`)
    }

    const onZalogujClick = e => {
        if (parseInt(prowadzacyCtx.idProwadzacego) > 0)
            navigate('/')
    }

    const prowadzacy_options = prowadzacy.map(p => (
        <option value={p.id} key={p.id}>{`${p.imie} ${p.nazwisko} - ${p.id}`}</option>
    ))


    return (
        <div className='login'>
            <h3 className='login__header'>Zaloguj jako:</h3>
            <select onChange={onProwadzacyChange}>
                {prowadzacy_options}
            </select>
            <div className='login_selected'>
                {prowadzacyCtx.idProwadzacego > 0
                    ? `Wybrano Prowadzacego: ${prowadzacyCtx.nazwaProwadzacego} - ${prowadzacyCtx.idProwadzacego}`
                    : "Nie wybrano prowadzacego"
                }
            </div>
            <button onClick={onZalogujClick}>zaloguj</button>
        </div>
    );
}

export default Login;