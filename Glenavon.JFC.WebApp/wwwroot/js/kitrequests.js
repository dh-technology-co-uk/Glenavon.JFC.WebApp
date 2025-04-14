let rowIndex = 1;

function addRow() {
    const lastRow = document.querySelectorAll(".team-item")[document.querySelectorAll(".team-item").length - 1];

    const topSize = lastRow.querySelector("select[name='TopSize']");
    const shortsSize = lastRow.querySelector("select[name='ShortsSize']");
    const socksSize = lastRow.querySelector("select[name='SocksSize']");
    const shirtNumber = lastRow.querySelector("input[name='ShirtNumber']");
    const kitType = lastRow.querySelector("select[name='KitType']");

    let isValid = true;

    [topSize, shortsSize, socksSize, shirtNumber, kitType].forEach(field => {
        field.classList.remove("is-invalid");
    });

    if (!topSize.value) { topSize.classList.add("is-invalid"); isValid = false; }
    if (!shortsSize.value) { shortsSize.classList.add("is-invalid"); isValid = false; }
    if (!socksSize.value) { socksSize.classList.add("is-invalid"); isValid = false; }
    if (shirtNumber.value === '' || Number(shirtNumber.value) < 1) {
        shirtNumber.classList.add("is-invalid"); isValid = false;
    }
    if (!kitType.value) { kitType.classList.add("is-invalid"); isValid = false; }

    if (!isValid) {
        alert("Please complete all fields and ensure shirt number is 1 or higher before adding another row.");
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
        alert("You must have at least one team member row.");
    }
}

function submitRequest() {
    const selectedTeam = document.getElementById("teamSelect").value;
    const teamSelect = document.getElementById("teamSelect");

    teamSelect.classList.remove("is-invalid");

    if (!selectedTeam) {
        teamSelect.classList.add("is-invalid");
        alert("Please select a team before submitting.");
        return;
    }

    const rows = document.querySelectorAll(".team-item");
    const data = [];
    let isValid = true;

    rows.forEach((row, i) => {
        const topSize = row.querySelector("select[name='TopSize']");
        const shortsSize = row.querySelector("select[name='ShortsSize']");
        const socksSize = row.querySelector("select[name='SocksSize']");
        const shirtNumber = row.querySelector("input[name='ShirtNumber']");
        const kitType = row.querySelector("select[name='KitType']");

        // Reset any previous errors
        [topSize, shortsSize, socksSize, shirtNumber, kitType].forEach(f => f.classList.remove("is-invalid"));

        if (!topSize.value) { topSize.classList.add("is-invalid"); isValid = false; }
        if (!shortsSize.value) { shortsSize.classList.add("is-invalid"); isValid = false; }
        if (!socksSize.value) { socksSize.classList.add("is-invalid"); isValid = false; }
        if (shirtNumber.value === '' || Number(shirtNumber.value) < 1) {
            shirtNumber.classList.add("is-invalid"); isValid = false;
        }
        if (!kitType.value) { kitType.classList.add("is-invalid"); isValid = false; }

        if (isValid) {
            data.push({
                topSize: topSize.value,
                shortsSize: shortsSize.value,
                socksSize: socksSize.value,
                shirtNumber: Number(shirtNumber.value),
                kitType: kitType.value
            });
        }
    });

    if (!isValid) {
        alert("Please fix the highlighted fields.");
        return;
    }

    const payload = {
        teamName: selectedTeam,
        players: data,
        status: 'To Do'
    };

    console.log("Submitting JSON payload:", JSON.stringify(payload));

    fetch('/KitRequests/SubmitTeam', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(payload)
    }).then(response => {
        if (response.ok) {
            alert("Team submitted successfully!");
        } else {
            alert(response.body + " Error submitting team.");
        }
    }).catch(error => {
        console.error("Error submitting form:", error);
        alert("Something went wrong submitting the form.");
    });

}
