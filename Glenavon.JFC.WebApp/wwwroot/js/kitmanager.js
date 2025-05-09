document.addEventListener("DOMContentLoaded", function () {
    let items = document.querySelectorAll(".kit-item");
    let columns = document.querySelectorAll(".kit-column");

    items.forEach(item => {
        item.addEventListener("dragstart", function (event) {
            event.dataTransfer.setData("text", event.target.getAttribute("data-id"));
            event.dataTransfer.setData("type", event.target.getAttribute("data-type"));
        });
    });

    columns.forEach(column => {
        column.addEventListener("dragover", function (event) {
            event.preventDefault();
        });

        column.addEventListener("drop", function (event) {
            event.preventDefault();
            let itemId = event.dataTransfer.getData("text");
            let itemType = event.dataTransfer.getData("type");
            let newStatus = column.getAttribute("data-status");

            fetch(`/EquipmentKitManager/UpdateItem?id=${itemId}&status=${newStatus}&type=${itemType}`, {
                method: "POST"
            }).then(() => location.reload());
        });
    });
});
