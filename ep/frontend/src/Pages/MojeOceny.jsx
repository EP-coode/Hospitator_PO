
import { useContext, useEffect, useState, Fragment } from "react"

import Modal from "../Components/Modal"
import Backdrop from "../Components/Backdrop"
import OcenyList from "../components/OcenyList"
import ProwadzacyContext from "../context/ProwadzacyContext"
import settings from "../settings"
import SzeegolyOceny from "../Components/SzczegolyOceny"
import Navigation from '../Components/Navigation'

import './MojeOceny.css'

const modal_type = Object.freeze({
    SZCZEGOLY: 1,
    AKCEPTUJ: 2,
    REKLAMUJ: 3
})

function MojeOceny() {
    const [wybrany, setWybranyProtokol] = useState(null)
    const [modalType, setModalType] = useState(modal_type.SZCZEGOLY)

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

    const akceptujOcene = () => {
        if (wybrany == null)
            return;

        console.log("akceptuje" + wybrany.kurs.nazwa);
        setNowe(prev => prev.filter(o => o.id != wybrany.id))
        setZakceptowane(prev => [...prev, wybrany])

        setWybranyProtokol(null)
    }

    const onDetailsClick = idProtokolu => {
        const wybrany = [...nowe, ...zakceptowane, ...zareklamowane].find(p => p.id == idProtokolu)
        setWybranyProtokol(wybrany)
        setModalType(modal_type.SZCZEGOLY)
    }

    const onAcceptClick = idProtokolu => {
        const wybrany = [...nowe, ...zakceptowane, ...zareklamowane].find(p => p.id == idProtokolu)
        setWybranyProtokol(wybrany)
        setModalType(modal_type.AKCEPTUJ)
    }

    const onReklamujClick = idProtokolu => {
        const wybrany = [...nowe, ...zakceptowane, ...zareklamowane].find(p => p.id == idProtokolu)
        setWybranyProtokol(wybrany)
        setModalType(modal_type.REKLAMUJ)
    }


    const onModalClose = () => {
        setWybranyProtokol(null)
    }

    return (
        <div>
            <Navigation />
            <div className="oceny">
                <div className="oceny__group">
                    <h3 className="oceny__type-header">Nowe:</h3>
                    <OcenyList oceny={nowe} onDetailsClick={onDetailsClick} onAkceptujClick={onAcceptClick} />
                </div>
                <div className="oceny__group">
                    <h3 className="oceny__type-header">Zakceptowane:</h3>
                    <OcenyList oceny={zakceptowane} onDetailsClick={onDetailsClick} />
                </div>
                <div className="oceny__group">
                    <h3 className="oceny__type-header">Zareklamowane:</h3>
                    <OcenyList oceny={zareklamowane} onDetailsClick={onDetailsClick} />
                </div>
                {wybrany != null && modalType == modal_type.SZCZEGOLY &&
                    <Fragment>
                        <Modal
                            haderText={"Sczegóły oceny"}
                            onClose={onModalClose}
                            closeText="Powrót"
                        >
                            <SzeegolyOceny szczegolyOceny={wybrany} />
                        </Modal>
                        <Backdrop />
                    </Fragment>
                }
                {wybrany != null && modalType == modal_type.AKCEPTUJ &&
                    <Fragment>
                        <Modal
                            haderText={`Czy chcesz kaceptować ocenę z kursu ${wybrany.kurs.nazwa}?`}
                            onClose={onModalClose}
                            closeText="Powrót"
                            confirmText="Potwierdź"
                            onConfirm={akceptujOcene}
                        >
                        </Modal>
                        <Backdrop />
                    </Fragment>
                }
                {wybrany != null && modalType == modal_type.REKLAMUJ &&
                    <Fragment>
                        <Modal
                            haderText={"Sczegóły oceny"}
                            onClose={onModalClose}
                            closeText="Powrót"
                        >
                            <SzeegolyOceny szczegolyOceny={wybrany} />
                        </Modal>
                        <Backdrop />
                    </Fragment>
                }
            </div>
        </div>
    )
}

export default MojeOceny