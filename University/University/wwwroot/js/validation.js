
function validateFormCourses() {
    var fieldNames = ["Name", "Description"];
    return validateForm("myForm", fieldNames);
}

function validateFormStudents() {
    var fieldNames = ["First_Name", "Last_Name"];
    return validateForm("myForm", fieldNames);
}

function validateFormGroup() {
    var fieldNames = ["Name"];
    return validateForm("myForm", fieldNames);
}


function validateForm(formName, fieldNames) {
    var isValid = true;
    fieldNames.forEach(function (fieldName) {
        var fieldValue = document.forms[formName][fieldName].value;
        if (fieldValue.trim() == null || fieldValue.trim() == "" || fieldValue === " ") {
            alert("Заполните все поля");
            isValid = false;
            return false;
        }
    });
    return isValid;
}
