function validateModel() {
    var model = {
        User: {
            Name: $('#userName').val(),
            Surname: $('#userSurname').val(),
            Age: $('#userAge').val(),
            Adress: {
                Street: $('#userStreet').val(),
                HouseNumber: $('#userHouseNumber').val(),
                PostIndex: $('#userPostIndex').val()
            }
        }
    };
    $.ajax({
        url: '/api/validation',
        type: 'POST',
        dataType: 'json',
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#pageBody').append(`<div>Server IsValid result: ${data.IsValid}</div>`);
            var validationFunction = new Function('model', data.ValidationFunction);
            var isValid = validationFunction(model.User);
            $('#pageBody').append(`<div>Client side IsValid result: ${isValid}</div>`);
        }
    });
}