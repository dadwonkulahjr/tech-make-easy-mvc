$(function () {
    $('button[name = "saveSelectedUsers"]').prop('disabled', true);

    var errorText = "An error has occured. An Administrator has been notified. Please try again later.";

    $('select').on('change', function () {
        var url = '/Admin/UsersToCategory/GetUsersForCategory?categoryId=' + this.value;

        if (this.value != 0) {
            $.ajax({
                type: 'GET',
                url: url,
                success: function (data) {
                    $('#usersCheckList').html(data);
                    $('button[name = "saveSelectedUsers"]').prop('disabled', false);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    presentClosableBootstrapAlert('#alert_placeholder', 'danger', 'An error has occured!', errorText);

                    console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);
                }
            })
        }
        else {
            $('button[name = "saveSelectedUsers"]').prop('disabled', true);
            $('input[type="checkbox"]').prop('checked', false);
            $('input[type="checkbox"]').prop('disabled', true);
        }
    });

    $('#saveSelectedUsers').click(function () {

        var url = "/Admin/UsersToCategory/SaveSelectedUsers";
        var categoryId = $('#categoryId').val();



        var antiForgeryToken = $('input[name = "__RequestVerificationToken"]').val();
        var userSelected = [];

        //$('input[type = "checkbox"]').prop('disabled', true);
        //$('#saveSelectedUsers').prop('disabled', true);
        //$('select').prop('disabled', true);
        disabledControls(true);


        $('.progress').show("fade");


        $('input[type="checkbox"]:checked').each(function () {
            var userModel = {
                Id: $(this).attr('value')
            };
            userSelected.push(userModel);

        });

        var userSelectedForCategory = {
            __RequestVerificationToken: antiForgeryToken,
            CategoryId: categoryId,
            UsersSelected: userSelected
        };

        $.ajax({
            type: 'POST',
            url: url,
            data: userSelectedForCategory,
            success: function (data) {

                $('.progress').hide("fade", function () {
                    $('.alert-success').fadeTo(2000, 500).slideUp(500, function () {

                        disabledControls(false);
                        //$('input[type="checkbox"]').prop('disabled', false);
                        //$('#saveSelectedUsers').prop('disabled', false);
                        //$('select').prop('disabled', false);
                    });
                });



                $('#usersCheckList').html(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {

                $('.progress').hide("fade", function () {

                    $('.alert-success').fadeTo(2000, 500).slideUp(500, function () {

                        presentClosableBootstrapAlert('#alert_placeholder', 'danger', 'An error has occured!', errorText);

                        console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);

                        disabledControls(false);
                    });
                });
                
                //$('input[type="checkbox"]').prop('disabled', false);
                //$('#saveSelectedUsers').prop('disabled', false);
                //$('select').prop('disabled', false);
            }
        })

    });

    function disabledControls(disabled) {
        $('input[type = "checkbox"]').prop('disabled', disabled);
        $('#saveSelectedUsers').prop('disabled', disabled);
        $('select').prop('disabled', disabled);
    }



});