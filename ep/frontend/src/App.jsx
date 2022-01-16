import { useState, useContext, useEffect } from 'react'
import MojeOceny from "./Pages/MojeOceny"
import { Link, Route, Routes } from "react-router-dom"
import ProwadzacyContext from "./context/ProwadzacyContext"
import SzczegolyOceny from "./Pages/SzczegolyOceny"
import settings from './settings'
import './App.css'

function App() {
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
    debugger
    const prowadzacyId = e.target.value
    const p = prowadzacy.find(p => p.id == prowadzacyId)
    prowadzacyCtx.setIdProwadzacego(prowadzacyId)
    prowadzacyCtx.setNazwaProwadzacego(`${p.imie} ${p.nazwisko}`)
    debugger
  }

  const prowadzacy_options = prowadzacy.map(p => (
    <option value={p.id} key={p.id}>{`${p.imie} ${p.nazwisko} - ${p.id}`}</option>
  ))

  return (
    <div className="App">
      My app
      <nav>
        <select onChange={onProwadzacyChange}>
          {prowadzacy_options}
        </select>
        <div>
          {prowadzacyCtx.idProwadzacego > 0
            ? `Wybrano Prowadzacego: ${prowadzacyCtx.nazwaProwadzacego} - ${prowadzacyCtx.idProwadzacego}`
            : "Nie wybrano prowadzacego"
          }
        </div>
        <Link to="/MojeOceny">Moje oceny</Link>
        {/* <Link to="/OtrzymaneOceny"> przegladaj otrzymane oceny</Link> */}
      </nav>
      <Routes>
        <Route path="/MojeOceny" element={<MojeOceny />} />
        <Route path="/MojeOceny/:idProtokolu" element={<SzczegolyOceny />} />
        {/* <Route path="/OtrzymaneOceny" element={<OtrzymaneOceny />} /> */}
      </Routes>
    </div>
  )
}

export default App
