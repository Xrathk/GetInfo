// Example function -- addition between numbers, result in alert box
function showAlert(num1, num2) {
    alert(num1 + num2);
}

// Adds popover capabilities and controls their behavior
function addPopovers() {
    $('[data-toggle="popover"]').popover();
    $('.popover-dismiss').popover({
        trigger: 'focus'
    });
}

// Adds tooltip capabilities and controls their behavior
function addTooltips() {
    $('[data-toggle="tooltip"]').tooltip({
        trigger: 'hover'
    });
    $('[data-toggle="tooltip"]').on('mouseleave', function () {
        $(this).tooltip('hide');
    });
}

