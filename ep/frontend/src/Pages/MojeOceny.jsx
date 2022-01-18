
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
    const [argumentacjaReklamacj, setArgumentacjaReklamacji] = useState("")

    const [nowe, setNowe] = useState([])
    const [zareklamowane, setZareklamowane] = useState([])
    const [zakceptowane, setZakceptowane] = useState([])
    const { idProwadzacego } = useContext(ProwadzacyContext)

    useEffect(() => {
        async function fetchOcenyProwadzacego() {
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
        fetchOcenyProwadzacego()
    }, [idProwadzacego])

    const akceptujOcene = async () => {
        if (wybrany == null)
            return;

        const idProtokolu = wybrany.id
        const url = `http://localhost:5091/api/Oceny/Akceptuj/${idProwadzacego}/${idProtokolu}`
        try {
            const result = await fetch(url)
            if (result.status === 200) {
                setZakceptowane([...zakceptowane, wybrany])
                setNowe(prev => prev.filter(o => o.id != wybrany.id))
                setWybranyProtokol(null)
            }
        }
        catch (e) {
            console.log(e);
        }
    }

    const reklamujOcene = async () => {
        if (wybrany == null)
            return;

        const idProtokolu = wybrany.id
        const url = `http://localhost:5091/api/Oceny/Reklamuj`
        const body = JSON.stringify({
            prowadzacyId: parseInt(idProwadzacego),
            protokolId: parseInt(idProtokolu),
            uzasadnienie: argumentacjaReklamacj
        })
        try {
            const result = await fetch(url, {
                method: "POST",
                headers: {
                    'Content-Type': 'application/json',
                },
                body: body
            })
            if (result.ok) {
                setZareklamowane([...zareklamowane, wybrany])
                setNowe(prev => prev.filter(o => o.id != wybrany.id))
                setWybranyProtokol(null)
            }
        }
        catch (e) {
            console.log(e);
        }
    }

    const onTextareaType = e => {
        if (e.target.value.length < 255)
            setArgumentacjaReklamacji(e.target.value)
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
                    <OcenyList oceny={nowe} onDetailsClick={onDetailsClick} onAkceptujClick={onAcceptClick} onReklamujClick={onReklamujClick} />
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
                            haderText={"Uzasadnij Reklamację"}
                            onClose={onModalClose}
                            closeText="Powrót"
                            onConfirm={reklamujOcene}
                            confirmText="Zatwierdź"
                        >
                            <textarea onChange={onTextareaType} value={argumentacjaReklamacj}>
                            </textarea>
                            <span>{argumentacjaReklamacj.length}/255</span>
                        </Modal>
                        <Backdrop />
                    </Fragment>
                }
            </div>
        </div>
    )
}

export default MojeOceny