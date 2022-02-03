import './OcenyList.css'


function OcenyListItem({ kod, nazwa, termin, ocena, id, onDetailsClick, onAkceptujClick, onReklamujClick }) {
    return (
        <li className="ocena">
            <div className="ocena__details">
                <div><strong>Kod: </strong> {kod}</div>
                <div><strong>Nazwa: </strong> {nazwa}</div>
                <div><strong>Termin: </strong> {termin}</div>
                <div><strong>Ocena końcowa: </strong> {ocena}</div>
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


function OcenyList({ oceny, onDetailsClick, onAkceptujClick, onReklamujClick }) {
    const items = oceny.map(p => <OcenyListItem
        kod={p.kurs ? p.kurs.kod : "Nie ustalono"}
        nazwa={p.kurs ? p.kurs.nazwa : "Nie ustalono"}
        ocena={p.formulazprotokolus.ocenaKoncowa}
        termin={p.dataWystawienia}
        key={p.id}
        id={p.id}
        onDetailsClick={onDetailsClick}
        onAkceptujClick={onAkceptujClick}
        onReklamujClick={onReklamujClick} />)

    return (
        <ul className="oceny-list">
            {items}
        </ul>
    );
}

export default OcenyList;