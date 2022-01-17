import { useState, useContext, useEffect } from 'react'

import { Link, Route, Routes, useNavigate } from "react-router-dom"

import MojeOceny from "./Pages/MojeOceny"
import Login from './Pages/Login'
import UserPanel from "./Pages/UserPanel"
import ProwadzacyContext from "./context/ProwadzacyContext"

import './App.css'
import Hospituj from './Pages/Hospituj'
import ProtokolForm from './Pages/ProtokolForm'

function App() {
  const prowadzacyCtx = useContext(ProwadzacyContext)
  const navigate = useNavigate()

  useEffect(() => {
    if (parseInt(prowadzacyCtx.idProwadzacego) < 0) {
      navigate("/Login")
    }
  }, [])

  return (
    <div className="App">
      My app
      <Routes>
        <Route path="/" element={<UserPanel />} />
        <Route path="/Login" element={<Login />} />
        <Route path="/MojeOceny" element={<MojeOceny />} />
        <Route path="/Hospitacje/:idHospitacji" element={<ProtokolForm />} />
        <Route path="/Hospitacje" element={<Hospituj />} />
      </Routes>
    </div>
  )
}

export default App
