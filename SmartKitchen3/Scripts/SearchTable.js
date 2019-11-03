$(document).ready(function () {
    $("#searchable").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#search-table tr").filter(function () {
            $(this).toggle(
                value.split(' ').every(val => $(this).text().toLowerCase().indexOf(val) > -1)
            )
        });
    });
});