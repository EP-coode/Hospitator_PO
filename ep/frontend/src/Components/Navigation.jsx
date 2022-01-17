import { useContext } from "react";

import { Link } from "react-router-dom"

import ProwadzacyContext from "../context/ProwadzacyContext"

function Navigation() {
    const prowadzacyCtx = useContext(ProwadzacyContext)

    return (
        <nav>
            <h3>Zalogowano jako: {`${prowadzacyCtx.nazwaProwadzacego} - ${prowadzacyCtx.idProwadzacego}`}</h3>
            <Link to="/MojeOceny">Moje oceny</Link><br />
            <Link to="/Hospitacje">Hospituj</Link>
        </nav>
    );
}

export default Navigation;