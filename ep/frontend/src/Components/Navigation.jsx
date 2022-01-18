import { useContext } from "react";

import { Link } from "react-router-dom"

import ProwadzacyContext from "../context/ProwadzacyContext"

import "./Navigation.css"

function Navigation() {
    const prowadzacyCtx = useContext(ProwadzacyContext)

    return (
        <nav className="navigation">
            <h3 className="navigation__header">
                Zalogowano jako: {`${prowadzacyCtx.nazwaProwadzacego} - ${prowadzacyCtx.idProwadzacego}`}
            </h3>
            <div className="navigation__options">
                <Link to="/Login"><button className="navigation__option">Wyloguj</button></Link>
                <Link to="/MojeOceny"><button className="navigation__option">Moje oceny</button></Link>
                <Link to="/Hospitacje"><button className="navigation__option">Hospituj</button></Link>
            </div>
        </nav>
    );
}

export default Navigation;