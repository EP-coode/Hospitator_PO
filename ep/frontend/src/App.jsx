import { useState } from 'react'
import MojeOceny from "./Pages/MojeOceny"
import { Link, Route, Routes } from "react-router-dom"
import './App.css'

function App() {
  const [count, setCount] = useState(0)

  return (
    <div className="App">
      My app
      <nav>
        <Link to="/MojeOceny">hospitacje prowadzacych</Link>
        {/* <Link to="/OtrzymaneOceny"> przegladaj otrzymane oceny</Link> */}
      </nav>
      <Routes>
        <Route path="/MojeOceny" element={<MojeOceny />} />
        {/* <Route path="/OtrzymaneOceny" element={<OtrzymaneOceny />} /> */}
      </Routes>
    </div>
  )
}

export default App
