document.addEventListener("DOMContentLoaded", function () {
    const tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
    const csrfToken = tokenElement ? tokenElement.value : "";

    if (!window.Sortable) {
        console.error("SortableJS not loaded.");
        return;
    }

    document.querySelectorAll(".kit-column").forEach(column => {
        new Sortable(column, {
            group: "kits",
            animation: 150,
            onEnd: function (evt) {
                const item = evt.item;
                const itemId = item?.dataset?.id;
                const itemType = item?.dataset?.type;
                const newStatus = evt.to?.dataset?.status;

                if (!itemId || !itemType || !newStatus) {
                    console.warn("Missing drag data.");
                    return;
                }

                fetch("/EquipmentKitManager/UpdateItem", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        id: parseInt(itemId),
                        status: newStatus,
                        type: itemType
                    })
                })
                    .then(res => {
                        if (!res.ok) throw new Error("Status update failed");
                        return res.text();
                    })
                    .catch(err => {
                        console.error("Error updating item status:", err);
                        alert("Failed to update item. Please try again.");
                    });
            }
        });
    });
});
