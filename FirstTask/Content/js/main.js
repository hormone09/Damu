
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