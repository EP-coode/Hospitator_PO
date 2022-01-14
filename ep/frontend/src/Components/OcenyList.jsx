
import OcenyListITem from "./OcenyListItem";

function OcenyList({ oceny }) {
    const items = oceny.map(p => <OcenyListITem nazwaKursu={p.kodKursu + p.nazwaKursu} ocena={p.ocena} termin={p.termin} key={p.id} />)

    return (
        <ul className="oceny-list">
            {items}
        </ul>
    );
}

export default OcenyList;