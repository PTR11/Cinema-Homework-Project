// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('label.reservation').click(function () {
    var inputElements = [].slice.call(document.querySelectorAll('.asd'));
    var checkedValue = inputElements.filter(chk => chk.checked).length;
    var checked = $('input', this).is(':checked');
   
    console.log(checkedValue);
    if (checkedValue <= 6) {
        $('img', this).attr('src', (checked ? "/reservedPlace.png" : "/emptyPlace.png"));
    } else {
        alert("6nál több helyet nem lehet foglalni");
        event.preventDefault();
    }
});

