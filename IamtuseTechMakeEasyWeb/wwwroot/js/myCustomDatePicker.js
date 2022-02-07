$(document).ready(function () {
    $('.myCustomDate').datepicker(
        {
            dateFormat: "yy-mm-dd",
            minDate: new Date(),
            maxDate: addSubstractMonth(new Date(), 2)
        }
    );


    function addSubstractMonth(date, numMonths) {
        var month = date.getMonth();

        var milliSeconds = new Date(date).setMonth(month + numMonths);
        return new Date(milliSeconds);


    }
});