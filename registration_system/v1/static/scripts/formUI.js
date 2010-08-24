var isStudent= false;

function formOnLoad() {
    validateForm('memberRegistration');
    setInterval( function() { validateForm('memberRegistration') }, 500);
}

function formConfirmOnLoad() {
    validateForm('memberRegistrationConfirm')
    setInterval( function() { validateForm('memberRegistrationConfirm') }, 800);
}

function formRowHighlight(inputId) {
    obj = document.getElementById(inputId + "_tr");
    obj.bgColor = '#E0E0E0';
    obj.style.border = '1px solid #C7C7C7';
}

function formRowUnhighlight(inputId) {
    obj = document.getElementById(inputId + "_tr");
    obj.bgColor = '#ffffff';
    obj.style.border = '0px';
}

function checkUbcAffliation() {
    ubcAffliation = document.memberRegistration.ubcAffliation;
    for ( i=0; i<ubcAffliation.length; i++ ) {
        if( ubcAffliation[i].checked == true ) {
            // Student Number 
            if( ubcAffliation[i].value == 'Student' ) {
                showTr('studentNo_tr');
                isStudent = true;
            } else {
                hideTr('studentNo_tr'); 
                isStudent = false;
            }

            // Other (Non-AMS)
            if ( ubcAffliation[i].value == 'Other (Non-AMS)' ) {
                hideTr('memberType_tr')
            } else {
                showTr('memberType_tr')
            }
            break;
        }
    }
}

function hideTr(id) {
    row = document.getElementById(id);
    if (row)
        row.style.display = 'none';
}

function showTr(id) {
    row = document.getElementById(id);
    if (row)
        row.style.display = '';
}
