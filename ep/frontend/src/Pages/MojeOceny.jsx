import { useContext, useEffect, useState } from "react"
import OcenyList from "../components/OcenyList"
import ProwadzacyContext from "../context/ProwadzacyContext"
import settings from "../settings"

function MojeOceny() {
    const [nowe, setNowe] = useState([])
    const [zareklamowane, setZareklamowane] = useState([])
    const [zakceptowane, setZakceptowane] = useState([])
    const { idProwadzacego } = useContext(ProwadzacyContext)

    useEffect(() => {
        async function fetchApi() {
            try {
                const url = `${settings.api_url}/Oceny/${idProwadzacego}`
                const response = await fetch(url)
                const result = await response.json()
                setNowe(result.nowe)
                setZakceptowane(result.zakceptowane)
                setZareklamowane(result.zareklamowane)
            }
            catch (e) {
                console.warn(e)
            }

        }
        fetchApi()
    }, [idProwadzacego])

    const onSzczegulClick = (id) => {

    }

    return (
        <div className="wybor-hospitacji">
            Hospitacja Prowadzacego:
            <div>
                Nowe:
                <OcenyList oceny={nowe} />
                Zakceptowane:
                <OcenyList oceny={zakceptowane} />
                Zareklamowane:
                <OcenyList oceny={zareklamowane} />
            </div>
        </div>
    )
}

export default MojeOceny