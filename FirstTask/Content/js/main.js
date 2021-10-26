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

function Loading() {

	let loading = document.getElementById('loading');
	loading.style.display = "none"
}
setTimeout(Loading, 800);

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

	var nameArray = [];
	var count = ajax.length;
	for (var i = 0; i < count; i++) {
		var temp = {
			Name: ajax[i].Name,
			Id: ajax[i].Id
		};
		nameArray[i] = temp;
	}

	return nameArray;
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

	var nameArray = [];
	var count = ajax.length;
	for (var i = 0; i < count; i++) {
		var temp = {
			Name: ajax[i].Name,
			Id: ajax[i].Id
		};
		nameArray[i] = temp;
	}

	return nameArray;
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

	var nameArray = [];
	var count = ajax.length;
	for (var i = 0; i < count; i++) {
		var temp = {
			FullName: ajax[i].FullName,
			Id: ajax[i].Id
		};
		nameArray[i] = temp;
	}

	return nameArray;
}

var employee = getEmployeeList();
var services = getServiceList();
var companies = getCompanyList();
