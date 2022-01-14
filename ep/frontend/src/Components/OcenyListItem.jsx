

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
                    <button>Szczegóły</button>
                    <button>Akceptuj</button>
                    <button>Reklamuj</button>
                </div>
            </div>
        </div>
    );
}

export default OcenyListItem;