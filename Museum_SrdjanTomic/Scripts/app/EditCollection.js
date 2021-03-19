$("#save").click(function () {
    var data = {
        name: $("#name").val(),
        id: $(this).attr("data-id"),
        url: $("#picture").val(),
        description: $("#description").val()
    };
    debugger;
    $.ajax({
        url: "/Museum/SaveEditArt",
        type: "post",
        datatype: "json",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            alert("Collection saved.");
            console.log(response);
            window.location = "/Museum/GetTree";
        },
        error: function (xhr, status) {
            console.log(status);
        }
    });
});


$("#preview").click(function () {
    var data = {
        name: $("#name").val(),
        url: $("#picture").val(),
        description: $("#description").val()
    };

    $("#all-data").css("display", "block");
    $("#name-preview").empty().append(data.name);
    $("#picture-preview").attr('src', data.url);
    $("#description-preview").empty().append(data.description);
});