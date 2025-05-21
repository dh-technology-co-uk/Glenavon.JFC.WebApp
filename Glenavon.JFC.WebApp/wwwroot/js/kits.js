function setupKitModalHandler(modalId) {
    const kitModal = document.getElementById(modalId);
    if (!kitModal) return;

    kitModal.addEventListener('show.bs.modal', function (event) {
        const trigger = event.relatedTarget;
        const imgSrc = trigger.getAttribute('data-img');
        const title = trigger.getAttribute('data-title');
        const modalImage = kitModal.querySelector('#kitModalImage');
        const modalTitle = kitModal.querySelector('.modal-title');

        if (modalImage && imgSrc) {
            modalImage.src = imgSrc;
        }

        if (modalTitle && title) {
            modalTitle.textContent = title;
        }
    });
}

document.addEventListener('DOMContentLoaded', function () {
    setupKitModalHandler('kitModal');
});