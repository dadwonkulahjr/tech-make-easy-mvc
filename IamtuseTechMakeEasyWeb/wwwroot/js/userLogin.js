$(function () {
    var userLoginButton = $('#UserLoginModal button[name="login"]').click(onClickLoginUsers);

    function onClickLoginUsers() {

        var url = "/userAuth/Login";

        var retrievedToken = $('#UserLoginModal input[name="__RequestVerificationToken"]').val();

        var email = $('#UserLoginModal input[name="Email"]').val();
        var password = $('#UserLoginModal input[name="Password"]').val();
        var rememberMe = $('#UserLoginModal input[name="RememberMe"]').prop("checked");

        var userInput = {
            __RequestVerificationToken: retrievedToken,
            Email: email,
            Password: password,
            RememberMe: rememberMe
        };


        $.ajax({
            type: "POST",
            url: url,
            data: userInput,
            success: function (data) {
                var parsed = $.parseHTML(data);

                var findErrors = $(parsed).find('input[name="LoginInvalid"]').val() == "true";

                if (findErrors == true) {

                    $('#UserLoginModal').html(data);

                    userLoginButton = $('#UserLoginModal button[name="login"]').click(onClickLoginUsers);

                    var form = $('#userLoginForm');

                    $(form).removeData("validator");
                    $(form).removeData("unobtrusiveValidation");

                    $.validator.unobtrusive.parse(form);
                }
                else {
                  
                    location.href = "/Home/Index";
                   
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {

                var errorText = 'Status: ' + xhr.status + ' - ' + xhr.statusText;
                presentClosableBootstrapAlert('#user_login', 'danger', 'Error!', errorText);

                console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);

                console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);
            }
        });

        //alert('Dad S Wonkulah Jr');
    }
});


//$(function () {
//    var userLoginButton = $('#UserLoginModal button[class="btn btn-success"]').click(onUserLoginClick());

//    function onUserLoginClick() {
//        alert('hello World!');
//    }
//});

