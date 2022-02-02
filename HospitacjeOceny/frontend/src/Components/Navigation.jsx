import { useContext } from "react";

import { NavLink } from "react-router-dom"

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
                <NavLink to="/Login" className={active => active.isActive ? "navigation__option --active" : "navigation__option"}>Wyloguj</NavLink>
                <NavLink to="/MojeOceny" className={active => active.isActive ? "navigation__option --active" : "navigation__option"}>Moje oceny</NavLink>
                <NavLink to="/Hospitacje" className={active => active.isActive ? "navigation__option --active" : "navigation__option"}>Hospituj</NavLink>
            </div>
        </nav>
    );
}

export default Navigation;