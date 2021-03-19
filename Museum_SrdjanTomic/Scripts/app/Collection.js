////$(document).ready(function () {
////    $("#filter").on("change paste keyup", function () {

////        var filter = $("#filter").val();
////        $.ajax({
////            url: "/Museum/Filter",
////            type: "get",
////            datatype: "json",
////            data: { filter },
////            success: function (response) {

////            },
////            error: function (xhr, status) {
////                console.log(status);
////            }
////        });
////    })
////});

$(".Send").click(function () {
    var id = $(this).attr("data-id");
    $.ajax({
        url: "/Museum/GetCollection",
        type: "get",
        datatype: "json",
        data: { id: id },
        success: function (response) {
            console.log(response);
            $("#all-data").css("display", "block");
            $("#name").empty().append(response.Name);
            $("#picture").attr('src', response.Url);
            $("#description").empty().append(response.Description);
            $("#edit").attr("data-id", response.Id);
        },
        error: function (xhr, status) {
            console.log(status);
        }
    });
});

$("#edit").click(function () {
    var id = $(this).attr("data-id");
    window.location = "/Museum/EditArt/?id=" + id;
});

var groupTimeoutId = 0;
$("#filter").keyup(function () {

    clearTimeout(groupTimeoutId);
    groupTimeoutId = setTimeout(FindName, 500);

});

function FindName() {
    var name = $("#filter").val();

    $.ajax({
        url: "/Museum/Filter",
        type: "get",
        datatype: "json",
        data: { name: name },
        success: function (response) {
            
            if (response.length > 0) {
                var deb = $("ul[data-id=10]");
                
                $("ul[data-id=10]").empty();
                $("ul[data-id=20]").empty();
                for (var i = 0; i < response.length; i++) {
                    if (response[i].Id > 100 && response[i].Id < 200) {
                        $("ul[data-id=10]").append(`<li><a class='Send' data-id='${response[i].Id}"'>${response[i].Name}</a></li>`);
                    }
                    else {
                        $("ul[data-id=20]").append(`<li><a class='Send' data-id='${response[i].Id}"'>${response[i].Name}</a></li>`);
                    }
                }
            }
        },
        error: function (xhr, status) {
            console.log(status);
        }
    });
}