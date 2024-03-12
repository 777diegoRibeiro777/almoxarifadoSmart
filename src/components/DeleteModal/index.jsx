import React from 'react'

export const Modal = () => {
  return (
    <div>
      <Modal
        isOpen={modalIsOpen}
        onRequestClose={closeModal}
        contentLabel="Exemple Modal"
        overlayClassName="modal-overlay"
        className="modal-content" 
      >

      </Modal>
    </div>
  )
}
