// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



    

function loadPage() {
    $.ajax({
        url: "/Home/_listPersona",
        method: "GET",
        success: function (response) {
            $("#partialPage").html(response)
        }
    })
}

function loadForm() {
    $.ajax({
        url: "/Home/_formPersona",
        method: "GET",
        success: function (response) {
            $("#partialForm").html(response)
        }
    })
}





//$('#btn').click(function () {
//    console.log("qua");
//    $('#modelWindow').modal('show');
//});

//function openModal() {
//    $('#modelWindow').modal('show');
//}

