var fieldErrorColor = '#f578a1';
var fieldNormalColor = '#ffffff';
var confirmationPassword = "paid"

function validateForm(formId) {
	var msg = "";

    formObj = document.getElementById(formId);

	if (formId == "memberRegistration" ) {
        var firstName = formObj.firstName;
        var lastName = formObj.lastName;
        var studentNo = formObj.studentNo;
        var phoneNumber = formObj.phoneNumber;
        var email = formObj.email;

        msg += print(firstName.id, validateName(firstName));
        msg += print(lastName.id, validateName(lastName));
        msg += print(studentNo.id, validateStudentNo(studentNo));
        msg += print(phoneNumber.id, validatePhone(phoneNumber));
        msg += print(email.id, validateEmail(email));
	} else if (formId == "memberRegistrationConfirm" ) {
        var confirmation = formObj.execConfirm;
       
        msg += print(confirmation.id, validateExecConfirmation(confirmation)); 
    }
	
	if (msg != "") {
		return false;
	} else {
	    return true;
    }
}

function print(labelId, errorMsg) {
    document.getElementById(labelId + "_lbl").innerHTML = errorMsg;
    return errorMsg;
}

function validateName(fld) {
	var error = "";
	
    if (fld.value == "") {
        fldBackground(fld, fieldErrorColor);
	    error = fld.id + " required"
	} else if (!isAlpha(fld.value)) {
        fldBackground(fld, fieldErrorColor);
	    error = fld.id + " should contain alpha letters only";
	} else {
        fldBackground(fld, fieldNormalColor);
	}
    
	return error;
}

function validateEmail(fld) {
    var error = "";
    var tfld = trim(fld.value);                        // value of field with whitespace trimmed off
    var emailFilter = /^[^@]+@[^@.]+\.[^@]*\w\w$/ ;
    var illegalChars= /[\(\)\<\>\,\;\:\\\"\[\]]/ ;
   
    if (fld.value == "") {
        fldBackground(fld, fieldErrorColor);
	    error = fld.id + " required"
    } else if (!emailFilter.test(tfld)) {              // test email for illegal characters
        fldBackground(fld, fieldErrorColor);
        error = "Invalid " + fld.id;
    } else if (fld.value.match(illegalChars)) {
        fldBackground(fld, fieldErrorColor);
        error = fld.id + " contains illegal characters";
    } else {
        fldBackground(fld, fieldNormalColor);
    }
    return error;
}

function validatePhone(fld) {
    var error = "";
    var stripped = fld.value.replace(/[\(\)\.\-\ ]/g, '');    

    if (fld.value == "") {
        fldBackground(fld, fieldErrorColor);
	    error = fld.id + " required"
    } else if (isNaN(stripped)) {
        error = fld.id + " should contain numeric characters only";
        fldBackground(fld, fieldErrorColor);
    } else if (stripped.length != 10) {
        error = fld.id + " should contain 10 digits (e.g. 604-123-1234)";
        fldBackground(fld, fieldErrorColor);
    } else {
        fldBackground(fld, fieldNormalColor);
    }
    return error;
}

function validateStudentNo(fld) {
    var error = "";
    var stripped = trim(fld.value);

    checkUbcAffliation();
    if (isStudent == false) {
        fldBackground(fld, fieldNormalColor);
    } else if (fld.value == "") {
        if (isStudent == true) {
            error = fld.id + " required"
            fldBackground(fld, fieldErrorColor);
        } else {
            fldBackground(fld, fieldNormalColor);
        }
    } else if (isNaN(stripped)) {
        error = fld.id + " should contain only numeric characters";
        fldBackground(fld, fieldErrorColor);
    } else if (stripped.length != 8) {
        error = fld.id + " should contain 8 numerals"; 
        fldBackground(fld, fieldErrorColor);
    } else {
        fldBackground(fld, fieldNormalColor);
    }
    return error;
}

function validateExecConfirmation(fld) {
    var error = "";
    var stripped = trim(fld.value);

    if (fld.value == "") {
        fldBackground(fld, fieldErrorColor)
        error = "Please speak to a club executives to proceed"
    } else if (fld.value != confirmationPassword) {
        fldBackground(fld, fieldErrorColor);
    } else {
        fldBackground(fld, fieldNormalColor);
    }
    return error;
}

function isAlpha(str){
	var re = /[^a-zA-Z ]/g
	if (re.test(str)) return false;
	return true;
}

function fldBackground(fld, color) {
    fld.style.background = color;
}

function trim(s)
{
	return s.replace(/^\s+|\s+$/, '');
}
