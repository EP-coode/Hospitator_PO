import { Link } from "react-router-dom"


function OcenyListItem({ nazwaKursu, termin, ocena, id, onDetailsClick, onAkceptujClick, onReklamujClick }) {
    return (
        <div>
            <div>
                <div className="ocena-list-item__details">
                    <div>Kurs: {nazwaKursu}</div>
                    <div>Termin: {termin}</div>
                    <div>Ocena końcowa: {ocena}</div>
                </div>
                <div>
                    <button onClick={() => onDetailsClick(id)}>Sczegóły</button>
                    {
                        onAkceptujClick && <button onClick={() => onAkceptujClick(id)}>Akceptuj</button>
                    }
                    {
                        onReklamujClick && <button onClick={() => onReklamujClick(id)}>Reklamuj</button>
                    }
                </div>
            </div>
        </div>
    );
}


function OcenyList({ oceny, onDetailsClick, onAkceptujClick }) {

    const items = oceny.map(p => <OcenyListItem
        nazwaKursu={`${p.kurs.kod} ${p.kurs.nazwa}`}
        ocena={p.formulazprotokolus.ocenaKoncowa}
        termin={p.dataWystawienia}
        key={p.id}
        id={p.id}
        onDetailsClick={onDetailsClick}
        onAkceptujClick={onAkceptujClick} />)

    return (
        <ul className="oceny-list">
            {items}
        </ul>
    );
}

export default OcenyList;