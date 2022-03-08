$(function () {
    var userLoginButton = $('#UserRegistrationModal button[name="register"]').click(onClickLoginUsers);

    $('#UserRegistrationModal input[name="AcceptUserAgreement"]').click(onAcceptUserAgreementClick);


    $('#UserRegistrationModal button[name="register"]').prop('disabled', true);


    function onAcceptUserAgreementClick() {
        if ($(this).is(':checked')) {
            $('#UserRegistrationModal button[name="register"]').prop('disabled', false);
        }
        else {
            $('#UserRegistrationModal button[name="register"]').prop('disabled', true);
        }

        //alert('Programming with jquery is awesome!');
    }

    $('#UserRegistrationModal input[name="Email"]').blur(function () {
        var email = $('#UserRegistrationModal input[name="Email"]').val();

        var url = "/userAuth/CheckIfEmailExists?userName=" + email;

        $.ajax({
            type: 'GET',
            url: url,
            success: function (data) {
                if (data == true) {
                    presentClosableBootstrapAlert('#placeholder_email_exists', 'warning', 'Invalid email!', 'This email address has already being registered.');
                    //var htmlToDisplay = `<div class="alert alert-warning alert-dismissible fade show" role="alert">
                    //      <strong>Invalid email!</strong><br/>This email address has already being registered.
                    //      <button type="button" class="btn-close" data-dismiss="alert" aria-label="Close"></button>
                    //    </div>`;

                    //$('#placeholder_email_exists').html(htmlToDisplay);
                }
                else {
                    closeAlert('#placeholder_email_exists');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var errorText = 'Status: ' + xhr.status + ' - ' + xhr.statusText;
                presentClosableBootstrapAlert('#placeholder_email_exists', 'danger', 'Error!', errorText);

                console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);
            }
        })
    });

    function onClickLoginUsers() {

        var url = "/userAuth/RegisterUser";

        var retrievedToken = $('#UserRegistrationModal input[name="__RequestVerificationToken"]').val();

        var email = $('#UserRegistrationModal input[name="Email"]').val();
        var password = $('#UserRegistrationModal input[name="Password"]').val();
        var confirmPassword = $('#UserRegistrationModal input[name="ConfirmPassword"]').val();
        var firstName = $('#UserRegistrationModal input[name="FirstName"]').val();
        var lastName = $('#UserRegistrationModal input[name="LastName"]').val();
        var address1 = $('#UserRegistrationModal input[name="Address1"]').val();
        var address2 = $('#UserRegistrationModal input[name="Address2"]').val();
        var postCode = $('#UserRegistrationModal input[name="PostCode"]').val();
        var phoneNumber = $('#UserRegistrationModal input[name="PhoneNumber"]').val();


        //var confirmPassword = $('#UserRegistrationModal input[name="ConfirmPassword"]').prop("checked");

        var userInput = {
            __RequestVerificationToken: retrievedToken,
            Email: email,
            Password: password,
            ConfirmPassword: confirmPassword,
            FirstName: firstName,
            LastName: lastName,
            Address1: address1,
            Address2: address2,
            PostCode: postCode,
            PhoneNumber: phoneNumber,
            AcceptUserAgreement: true
        };


        $.ajax({
            type: "POST",
            url: url,
            data: userInput,
            success: function (data) {
                var parsed = $.parseHTML(data);

                var findErrors = $(parsed).find('input[name="RegistrationInvalid"]').val() == "true";

                if (findErrors) {

                    $('#UserRegistrationModal').html(data);

                    userLoginButton = $('#UserRegistrationModal button[name="register"]').click(onClickLoginUsers);

                    $('#UserRegistrationForm').removeData("validator");
                    $('#UserRegistrationForm').removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse('#UserRegistrationForm');
                }
                else {

                    location.href = "/Home/Index";

                }
            },
            error: function (xhr, ajaxOptions, thrownError) {

                var errorText = 'Status: ' + xhr.status + ' - ' + xhr.statusText;
                presentClosableBootstrapAlert('#placeholder_email_exists', 'danger', 'Error!', errorText);

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

