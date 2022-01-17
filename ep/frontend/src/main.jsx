import React from 'react'
import ReactDOM from 'react-dom'
import { BrowserRouter } from "react-router-dom";
import { ProwadzacyProvider } from './context/ProwadzacyContext'
import './index.css'
import App from './App'

ReactDOM.render(
  <React.StrictMode>
    <ProwadzacyProvider>
      <BrowserRouter>
        <App />
      </BrowserRouter>
    </ProwadzacyProvider>
  </React.StrictMode>,
  document.getElementById('root')
)
