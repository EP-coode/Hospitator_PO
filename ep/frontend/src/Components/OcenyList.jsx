import './OcenyList.css'


function OcenyListItem({ nazwaKursu, termin, ocena, id, onDetailsClick, onAkceptujClick, onReklamujClick }) {
    return (
        <li className="ocena">
            <div className="ocena__details">
                <div><strong>Kurs:</strong> {nazwaKursu}</div>
                <div><strong>Termin:</strong> {termin}</div>
                <div><strong>Ocena końcowa:</strong> {ocena}</div>
            </div>
            <div className="ocena__actions">
                <button onClick={() => onDetailsClick(id)}>Sczegóły</button>
                {
                    onAkceptujClick && <button onClick={() => onAkceptujClick(id)}>Akceptuj</button>
                }
                {
                    onReklamujClick && <button onClick={() => onReklamujClick(id)}>Reklamuj</button>
                }
            </div>
        </li>
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