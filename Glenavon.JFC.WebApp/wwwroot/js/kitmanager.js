document.addEventListener("DOMContentLoaded", function () {
    let items = document.querySelectorAll(".kit-item");
    let columns = document.querySelectorAll(".kit-column");

    items.forEach(item => {
        item.addEventListener("dragstart", function (event) {
            event.dataTransfer.setData("text", event.target.getAttribute("data-id"));
        });
    });

    columns.forEach(column => {
        column.addEventListener("dragover", function (event) {
            event.preventDefault();
        });

        column.addEventListener("drop", function (event) {
            event.preventDefault();
            let itemId = event.dataTransfer.getData("text");
            let newStatus = column.getAttribute("data-status");

            fetch(`/KitManager/UpdateItem?id=${itemId}&status=${newStatus}`, {
                method: "POST"
            }).then(() => location.reload());
        });
    });
});
