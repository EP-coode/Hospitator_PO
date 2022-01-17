import { useState, createContext } from "react"

const ProwadzacyContext = createContext({
    idProwadzacego: -1,
    nazwaProwadzacego: "",
    setIdProwadzacego: () => { },
    setNazwaProwadzacego: () => { }
})

export const ProwadzacyProvider = ({ children }) => {
    const [idProwadzacego, setIdProwadzacego] = useState("-1")
    const [nazwaProwadzacego, setNazwaProwadzacego] = useState("")

    return (
        <ProwadzacyContext.Provider value={{
            idProwadzacego,
            nazwaProwadzacego,
            setIdProwadzacego,
            setNazwaProwadzacego
        }}>
            {children}
        </ProwadzacyContext.Provider>
    )
}

export default ProwadzacyContext