// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Get the Toastr type and message from TempData
//var toastrType = '@TempData["ToastrType"]';

// Check if a message exists
function displayToastr() {
    var toastrMessage = '@TempData["ToastrMessages"]';
    if (toastrMessage) {
        toastr.success(toastrMessage);
        // Display the Toastr notification based on the type
        /*switch (toastrType.toLowerCase()) {
            case 'success':
                toastr.success(toastrMessage);
                break;
            case 'info':
                toastr.info(toastrMessage);
                break;
            case 'warning':
                toastr.warning(toastrMessage);
                break;
            case 'error':
                toastr.error(toastrMessage);
                break;
            default:
                toastr.info(toastrMessage); // Fallback to info if type is unknown
                break;*/
    }
}

