document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".change-status-form button").forEach(button => {
        button.addEventListener("click", function (e) {
            const form = button.closest("form");
            const id = form.dataset.id;
            const type = form.dataset.type;
            const newStatus = form.dataset.status;

            fetch("/EquipmentKitManager/UpdateItem", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    id: parseInt(id),
                    type: type,
                    status: newStatus
                })
            })
                .then(res => {
                    if (!res.ok) throw new Error("Status update failed");
                    return res.text();
                })
                .then(() => location.reload()) // reload to reflect new status
                .catch(err => {
                    console.error("Error updating item status:", err);
                    alert("Failed to move item. Please try again.");
                });
        });
    });
});
