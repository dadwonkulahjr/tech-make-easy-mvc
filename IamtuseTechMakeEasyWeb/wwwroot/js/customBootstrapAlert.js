function presentClosableBootstrapAlert(placeholderElemId, alertType, alertHeading, alertMessage) {

    if (alertType == '') {
        alertType = 'info';
    }

    var htmlToDisplay = `<div class="alert alert-` + alertType + ` alert-dismissible fade show" role="alert">
                          <strong>`+ alertHeading + `</strong><br/>` + alertMessage + `
                          <button type="button" class="btn-close" data-dismiss="alert" aria-label="Close"></button>
                        </div>`;

    $(placeholderElemId).html(htmlToDisplay);
}

function closeAlert(placeholderElemId) {
    $(placeholderElemId).html(htmlToDisplay);
}