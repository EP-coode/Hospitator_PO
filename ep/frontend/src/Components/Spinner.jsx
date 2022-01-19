import "./Spinner.css"

function Spinner({ className }) {
    return (<div className={`lds-dual-ring ${className}`}></div>);
}

export default Spinner;