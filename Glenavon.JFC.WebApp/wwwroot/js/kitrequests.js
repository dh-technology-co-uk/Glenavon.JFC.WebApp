let rowIndex = 1;

function addRow() {
    const lastRow = document.querySelectorAll(".team-item")[document.querySelectorAll(".team-item").length - 1];

    const topSize = lastRow.querySelector("select[name='TopSize']").value;
    const shortsSize = lastRow.querySelector("select[name='ShortsSize']").value;
    const socksSize = lastRow.querySelector("select[name='SocksSize']").value;
    const shirtNumber = lastRow.querySelector("input[name='ShirtNumber']").value;
    const kitType = lastRow.querySelector("select[name='KitType']").value;

    if (!topSize || !shortsSize || !socksSize || shirtNumber === '' || Number(shirtNumber) < 1 || !kitType) {
        alert("Please complete all fields and ensure shirt number is 1 or higher before adding another row.");
        return;
    }

    const container = document.getElementById("itemRows");
    const newRow = lastRow.cloneNode(true);
    newRow.querySelectorAll("select, input[type='number']").forEach(el => el.value = '');

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

function submitTeam() {
    const selectedTeam = document.getElementById("teamSelect").value;
    if (!selectedTeam) {
        alert("Please select a team before submitting.");
        return;
    }

    const rows = document.querySelectorAll(".team-item");
    const data = [];

    for (let i = 0; i < rows.length; i++) {
        const row = rows[i];
        const topSize = row.querySelector("select[name='TopSize']").value;
        const shortsSize = row.querySelector("select[name='ShortsSize']").value;
        const socksSize = row.querySelector("select[name='SocksSize']").value;
        const shirtNumber = row.querySelector("input[name='ShirtNumber']").value;
        const kitType = row.querySelector("select[name='KitType']").value;

        if (!topSize || !shortsSize || !socksSize || shirtNumber === '' || Number(shirtNumber) < 1 || !kitType) {
            alert(`Please complete all fields in row ${i + 1}.`);
            return;
        }

        data.push({
            topSize,
            shortsSize,
            socksSize,
            shirtNumber: Number(shirtNumber),
            kitType
        });
    }

    const payload = {
        teamName: selectedTeam,
        players: data
    };

    console.log("Submitting JSON payload:", JSON.stringify(payload));

    fetch('/YourController/SubmitTeam', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(payload)
    }).then(response => {
        if (response.ok) {
            alert("Team submitted successfully!");
        } else {
            alert("Error submitting team.");
        }
    });
}
