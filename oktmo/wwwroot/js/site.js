// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    const showHiddenCheck = document.getElementById('showHiddenCheck');
    const table = document.getElementById('h');

    // Function to update sessionStorage and table visibility
    function updateVisibility() {
        sessionStorage.setItem('showHidden', showHiddenCheck.checked);
        table.style.display = showHiddenCheck.checked ? 'table' : 'none';
    }

    // Load saved state from sessionStorage on page load
    if (sessionStorage.getItem('showHidden') === 'true') {
        showHiddenCheck.checked = true;
        table.style.display = 'table';
    }

    // Add event listener to checkbox
    showHiddenCheck.addEventListener('change', updateVisibility);

    // Call updateVisibility initially to set initial state
    updateVisibility();
});




