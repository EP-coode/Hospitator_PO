import { Link } from "react-router-dom"


function OcenyListItem({ nazwaKursu, termin, ocena, id, onSczegulyClick, onAkceptujClick, onReklamujClick }) {
    return (
        <div>
            <div>
                <div className="ocena-list-item__details">
                    <div>Kurs: {nazwaKursu}</div>
                    <div>Termin: {termin}</div>
                    <div>Ocena końcowa: {ocena}</div>
                </div>
                <div>
                    <Link to={`/MojeOceny/${id}`}>Szczegóły</Link>
                    <button>Akceptuj</button>
                    <button>Reklamuj</button>
                </div>
            </div>
        </div>
    );
}


function OcenyList({ oceny }) {
    // debugger

    const items = oceny.map(p => <OcenyListItem
        nazwaKursu={`${p.kurs.kod} ${p.kurs.nazwa}`}
        ocena={p.formulazprotokolus.ocenaKoncowa}
        termin={p.dataWystawienia}
        key={p.id}
        id={p.id} />)

    return (
        <ul className="oceny-list">
            {items}
        </ul>
    );
}

export default OcenyList;