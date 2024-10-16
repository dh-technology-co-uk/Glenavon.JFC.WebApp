// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    // Attach click event to all external links
    $('a.external-link').on('click', function (e) {
        e.preventDefault();  // Prevent the default link behavior

        var externalUrl = $(this).attr('href');  // Get the URL of the external link

        // Set the href attribute of the 'Proceed' button in the modal
        $('#proceedExternal').attr('href', externalUrl);

        // Show the modal
        $('#externalLinkModal').modal('show');
    });
});
