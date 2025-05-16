let rowIndex = 1;

function addRow() {
    const lastRow = document.querySelectorAll(".team-item")[document.querySelectorAll(".team-item").length - 1];

    const topSize = lastRow.querySelector("select[name='TopSize']");
    const shortsSize = lastRow.querySelector("select[name='ShortsSize']");
    const socksSize = lastRow.querySelector("select[name='SocksSize']");
    const shirtNumber = lastRow.querySelector("input[name='ShirtNumber']");
    const kitType = lastRow.querySelector("select[name='KitType']");
    const quarterZip = lastRow.querySelector("select[name='QuarterZip']");

    let isValid = true;

    [topSize, shortsSize, socksSize, shirtNumber, kitType, quarterZip].forEach(field => {
        field.classList.remove("is-invalid");
    });

    const kitTypeValue = kitType.value?.toLowerCase() || "";
    const isAwayKit = kitTypeValue.includes("away");

    if (!topSize.value) { topSize.classList.add("is-invalid"); isValid = false; }
    if (!isAwayKit && !shortsSize.value) { shortsSize.classList.add("is-invalid"); isValid = false; }
    if (!isAwayKit && !socksSize.value) { socksSize.classList.add("is-invalid"); isValid = false; }
    if (shirtNumber.value === '' || Number(shirtNumber.value) < 1) {
        shirtNumber.classList.add("is-invalid"); isValid = false;
    }
    if (!kitType.value) { kitType.classList.add("is-invalid"); isValid = false; }
    if (!quarterZip.value) { quarterZip.classList.add("is-invalid"); isValid = false; }

    if (!isValid) {
        showFormMessage("Please complete all required fields before adding another row.");
        return;
    }

    const container = document.getElementById("itemRows");
    const newRow = lastRow.cloneNode(true);

    newRow.querySelectorAll("select, input[type='number']").forEach(el => {
        el.value = '';
        el.classList.remove("is-invalid");
    });

    container.appendChild(newRow);
    rowIndex++;
}

function removeRow(button) {
    const row = button.closest(".team-item");
    const allRows = document.querySelectorAll(".team-item");

    if (allRows.length > 1) {
        row.remove();
    } else {
        showFormMessage("You must have at least one team member row.");
    }
}

function submitRequest() {
    const selectedTeam = document.getElementById("teamSelect").value;
    const teamSelect = document.getElementById("teamSelect");

    teamSelect.classList.remove("is-invalid");

    if (!selectedTeam) {
        teamSelect.classList.add("is-invalid");
        showFormMessage("Please select a team before submitting.");
        return;
    }

    const managerName = document.getElementById("managerName");
    const managerMobile = document.getElementById("managerMobile");
    const managerEmail = document.getElementById("managerEmail");

    [managerName, managerMobile, managerEmail].forEach(field => field.classList.remove("is-invalid"));

    let isValidManager = true;

    if (!managerName.value.trim()) {
        managerName.classList.add("is-invalid");
        isValidManager = false;
    }

    if (!managerMobile.value.trim()) {
        managerMobile.classList.add("is-invalid");
        isValidManager = false;
    } else {
        // Basic mobile number validation: exactly 11 digits
        const mobilePattern = /^[0-9]{11}$/;
        if (!mobilePattern.test(managerMobile.value.trim())) {
            managerMobile.classList.add("is-invalid");
            isValidManager = false;
        }
    }

    if (!managerEmail.value.trim()) {
        managerEmail.classList.add("is-invalid");
        isValidManager = false;
    } else {
        // Basic email validation
        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailPattern.test(managerEmail.value.trim())) {
            managerEmail.classList.add("is-invalid");
            isValidManager = false;
        }
    }

    if (!isValidManager) {
        showFormMessage("Please complete all Manager fields correctly before submitting.");
        return;
    }

    const rows = document.querySelectorAll(".team-item");
    const playersData = [];
    let isValidPlayers = true;

    rows.forEach((row) => {

        const topSize = row.querySelector("select[name='TopSize']");
        const shortsSize = row.querySelector("select[name='ShortsSize']");
        const socksSize = row.querySelector("select[name='SocksSize']");
        const shirtNumber = row.querySelector("input[name='ShirtNumber']");
        const kitType = row.querySelector("select[name='KitType']");
        const quarterZip = row.querySelector("select[name='QuarterZip']");

        [topSize, shortsSize, socksSize, shirtNumber, kitType].forEach(f => f.classList.remove("is-invalid"));

        const kitTypeValue = kitType.value?.toLowerCase() || "";
        const isAwayKit = kitTypeValue.includes("away");

        if (!topSize.value) { topSize.classList.add("is-invalid"); isValidPlayers = false; }
        if (!isAwayKit && !shortsSize.value) { shortsSize.classList.add("is-invalid"); isValidPlayers = false; }
        if (!isAwayKit && !socksSize.value) { socksSize.classList.add("is-invalid"); isValidPlayers = false; }
        if (shirtNumber.value === '' || Number(shirtNumber.value) < 1) {
            shirtNumber.classList.add("is-invalid"); isValidPlayers = false;
        }
        if (!kitType.value) { kitType.classList.add("is-invalid"); isValidPlayers = false; }
        if (!quarterZip.value) { quarterZip.classList.add("is-invalid"); isValid = false; }

        if (isValidPlayers) {
            playersData.push({
                topSize: topSize.value,
                shortsSize: shortsSize.value,
                socksSize: socksSize.value,
                shirtNumber: Number(shirtNumber.value),
                kitType: kitType.value,
                quarterZip: quarterZip.value
            });
        }
    });


    if (!isValidPlayers) {
        showFormMessage("Please add at least 1 kit request and fix the highlighted player fields before submitting.");
        return;
    }

    const additionalInfo = document.getElementById("additionalInfo").value.trim();
    const sponsorLogo = document.getElementById("sponsorLogo").files[0];

    const formData = new FormData();
    formData.append("TeamName", selectedTeam);
    formData.append("Status", "To Do");
    formData.append("ManagerName", managerName.value.trim());
    formData.append("ManagerMobile", managerMobile.value.trim());
    formData.append("ManagerEmail", managerEmail.value.trim());
    formData.append("AdditionalInfo", additionalInfo);

    if (sponsorLogo) {
        formData.append("SponsorLogo", sponsorLogo);
    }

    formData.append("Players", JSON.stringify(playersData));

    console.log("Submitting FormData payload...");

    fetch('/KitRequests/SubmitTeam', {
        method: 'POST',
        body: formData
    }).then(response => {
        if (response.ok) {
            response.json().then(data => {
                if (data.success && data.requestNumber) {
                    window.location.href = `/KitRequests/Success/${data.requestNumber}`;
                } else {
                    showFormMessage("Unexpected server response.");
                }
            });
        } else {
            response.text().then(text => {
                showFormMessage("Error submitting team: " + text);
            });
        }
    }).catch(error => {
        console.error("Error submitting form:", error);
        showFormMessage("Something went wrong submitting the form.");
    });
}

function showFormMessage(message) {
    document.getElementById("formMessageModalBody").innerText = message;
    var modal = new bootstrap.Modal(document.getElementById('formMessageModal'));
    modal.show();
}

function previewSponsorLogo() {
    const fileInput = document.getElementById('sponsorLogo');
    const previewContainer = document.getElementById('sponsorLogoPreviewContainer');
    const previewImage = document.getElementById('sponsorLogoPreview');

    const file = fileInput.files[0];

    if (file) {
        const reader = new FileReader();

        reader.onload = function (e) {
            previewImage.src = e.target.result;
            previewContainer.style.display = 'block';
        };

        reader.readAsDataURL(file);
    } else {
        previewContainer.style.display = 'none';
        previewImage.src = '#';
    }
}

function removeSponsorLogo() {
    const fileInput = document.getElementById('sponsorLogo');
    const previewContainer = document.getElementById('sponsorLogoPreviewContainer');
    const previewImage = document.getElementById('sponsorLogoPreview');

    // Clear the file input
    fileInput.value = '';
    // Hide the preview
    previewContainer.style.display = 'none';
    previewImage.src = '#';
}

function replaceSponsorLogo() {
    const fileInput = document.getElementById('sponsorLogo');
    fileInput.click(); // Re-open the file picker
}

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