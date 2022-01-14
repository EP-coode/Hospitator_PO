import { useEffect, useState } from "react"
import OcenyList from "../components/OcenyList"
import settings from "../settings"

function MojeOceny() {
    const [nowe, setNowe] = useState([])
    const [zareklamowane, setZareklamowane] = useState([])
    const [zakceptowane, setZakceptowane] = useState([])

    useEffect(() => {
        async function fetchApi(idProwadzacego = 3) {
            try {
                const url = `${settings.api_url}/${idProwadzacego}`
                const response = await fetch(url)
                const result = await response.json()
                //debugger;

                function mapObejcts(e) {
                    return e.map((e) => ({
                        id: e.id,
                        nazwaKursu: e.nazwaKursu,
                        kodKursu: e.kodKursu,
                        termin: e.dataWystawienia,
                        ocena: e.formulazprotokolus.ocenaKoncowa
                    }))
                }

                setZareklamowane(mapObejcts(result.zareklamowane))
                setZareklamowane(mapObejcts(result.zareklamowane))
                setZareklamowane(mapObejcts(result.zareklamowane))
            }
            catch (e) {
                console.warn(e)
            }

        }
        fetchApi()
    }, [])

    const onSzczegulClick = (id)=>{
        
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