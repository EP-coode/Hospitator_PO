import "./Modal.css"

function Modal({ onClose, onConfirm, closeText = "Powrót", confirmText = "Zatwierdz", haderText, children }) {
    return (
        <div className="modal">
            <div className="modal__header">
                <h1>{haderText}</h1>
            </div>
            <div className="modal__content">
                {children}
            </div>
            <div className="modal__actions">
                {
                    onClose &&
                    <button className="modal__close-btn" onClick={onClose}>
                        {closeText}
                    </button>
                }
                {
                    onConfirm &&
                    <button className="modal__confirm-btn" onClick={onConfirm}>
                        {confirmText}
                    </button>
                }
            </div>
        </div>
    );
}

export default Modal