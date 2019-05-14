function validateModel() {
    var model = {
        User: {
            Name: $('#userName').val(),
            Surname: $('#userSurname').val(),
            Age: $('#userAge').val()
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
            var getValidateFunc = new Function('return ' + data.ValidationFunction + ';');
            var validate = getValidateFunc();
            var isValid = validate(model.User);
            $('#pageBody').append(`<div>Client side IsValid result: ${isValid}</div>`);
        }
    });
}