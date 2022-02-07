$(document).ready(function () {
    $('.myCustomDate').datepicker(
        {
            dateFormat: "yy-mm-dd",
            minDate: new Date(),
            maxDate: addSubstractMonth(new Date(), 2)
        }
    );
});