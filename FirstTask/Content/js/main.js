// Change on user-form format
function EditNumber(phone) {
	var arr = phone.split('');
	var result = "";
	result += arr[0] + "-(" + arr[1] + arr[2] + arr[3] + ")-" + arr[4] + arr[5] + arr[6] + "-" + arr[7] + arr[8] + "-" + arr[9] + arr[10];

	return result;
}

function EditBIN(bin) {
	var arr = bin.split('');
	var result = "";
	var temp = 0;
	for (var i = 0; i < arr.length; i++) {
		if (temp < 3 && i != arr.length) {
			result += arr[i];
			temp++;
		}
		else {
			result += "-" + arr[i];
			temp = 1;
		}
	}

	return result;
}

// Page Animation for Grid
function Loading() {

	let loading = document.getElementById('loading');
	loading.style.display = "none"
}
setTimeout(Loading, 800);

// Loading ComboBox

function getCompanyList() {
	var data = {
		PageSize: 100000,
		Status: 1
	};

	var ajax = null;

	$.ajax({
		url: "/Company/Index",
		type: "POST",
		contentType: "application/json; charset=utf-8",
		data: JSON.stringify(data),
		async: false,
		success: function (response) {
			ajax = response;
		}
	});

	for (var i = 0; i < ajax.length; i++) {
		ajax[i].BIN = EditBIN(ajax[i].BIN);
		ajax[i].Phone = EditNumber(ajax[i].Phone)
		ajax[i].DateOfBegin = new Date();
	}

	return ajax;
}

function getServiceList() {
	var data = {
		PageSize: 100000,
		Status: 1
	};


	var ajax = null;

	$.ajax({
		url: "/Service/Index",
		type: "POST",
		contentType: "application/json; charset=utf-8",
		data: JSON.stringify(data),
		async: false,
		success: function (response) {
			ajax = response;
		}
	});

	for (var i = 0; i < ajax.length; i++) {
		ajax[i].DateOfBegin = new Date();
	}

	return ajax;
}

function getEmployeeList() {
	var data = {
		PageSize: 100000,
		Status: 1
	};

	var ajax = null;

	$.ajax({
		url: "/Employee/Index",
		type: "POST",
		contentType: "application/json; charset=utf-8",
		data: JSON.stringify(data),
		async: false,
		success: function (response) {
			ajax = response;
		}
	});

	for (var i = 0; i < ajax.length; i++) {
		ajax[i].PersonalNumber = EditBIN(ajax[i].PersonalNumber);
		ajax[i].Phone = EditNumber(ajax[i].Phone)
		ajax[i].DateOfBegin = new Date();
	}

	return ajax;
}

// Validation

function ComboBoxsValidation(boxesIdes) {

	let result = true;
	
	boxesIdes.forEach((id) => {
		let comboBoxSelect = $("#" + id).data("kendoComboBox").selectedIndex;
		
		if (parseInt(comboBoxSelect) < 1)
			result = false;
	})

	return result;
}

$(document).ready(function () {

	jQuery.validator.addMethod("lettersonly", function (value, element) {
		return this.optional(element) || /^[А-я\s]*$/.test(value);
	});

	$.validator.addMethod(
		"pattern",
		function (value, element, regexp) {
			var re = new RegExp(regexp);
			return this.optional(element) || re.test(value);
		}
	);

	$.validator.addMethod("dateFilter", function (value, element) {
		let variant1 = value.replace(".", "").replace(".", "");
		let variant2 = value.replace("/", "").replace("/", "");
		let regex = new RegExp("^[0-9]{8}$");

		if (regex.test(variant1) || regex.test(variant2)) {
			return true;
		}
		else {
			return false;
		}
	});

	$.validator.addMethod("pastDate", function (value, element) {
		let today = new Date();
		let userDateInArray = value.split('/');

		if (userDateInArray[0] == value) {
			userDateInArray = value.split('.');
		}

		let userDate = new Date(userDateInArray[2], (parseInt(userDateInArray[1]) - 1), userDateInArray[0]);

		if (userDate > today)
			return false;
		else
			return true;
	});

	$.validator.addMethod("comboBoxField", function (value, element) {
		var boxId = element.id;
		alert(boxId);
		return false;
	});
})
