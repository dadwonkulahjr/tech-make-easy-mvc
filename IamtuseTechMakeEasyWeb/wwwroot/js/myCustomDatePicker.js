$(document).ready(function () {
    function wireUpDatePicker() {
        const addMonths = 2;
        var currentDate = new Date();
        $('.myCustomDate').datepicker(
            {
                dateFormat: "yy-mm-dd",
                minDate: currentDate,
                maxDate: addSubstractMonth(currentDate, addMonths)
            }
        );
    }

    wireUpDatePicker();
});