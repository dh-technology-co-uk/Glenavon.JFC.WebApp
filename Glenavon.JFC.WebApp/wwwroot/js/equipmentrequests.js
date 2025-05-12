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


    const selectedType = document.getElementById("Type").value;
    const typeSelect = document.getElementById("Type");

    typeSelect.classList.remove("is-invalid");

    if (!selectedType) {
        typeSelect.classList.add("is-invalid");
        showFormMessage("Please select a request type before submitting.");
        return;
    }

    const additionalInfo = document.getElementById("additionalInfo").value.trim();

    const formData = new FormData();
    formData.append("TeamName", selectedTeam);
    formData.append("Status", "To Do");
    formData.append("ManagerName", managerName.value.trim());
    formData.append("ManagerMobile", managerMobile.value.trim());
    formData.append("ManagerEmail", managerEmail.value.trim());
    formData.append("AdditionalInfo", additionalInfo);
    formData.append("Type", selectedType);

    fetch('/EquipmentRequests/SubmitRequest', {
        method: 'POST',
        body: formData
    }).then(response => {
        if (response.ok) {
            response.json().then(data => {
                if (data.success && data.requestNumber) {
                    window.location.href = `/EquipmentRequests/Success/${data.requestNumber}`;
                } else {
                    showFormMessage("Unexpected server response.");
                }
            });
        } else {
            response.text().then(text => {
                showFormMessage("Error submitting equipment request: " + text);
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