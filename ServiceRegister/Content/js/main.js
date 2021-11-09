// Page Animation for Grid
function Loading() {

	let loading = document.getElementById('loading');
	loading.style.display = "none"
}
setTimeout(Loading, 800);

// Custom Validation
$(document).ready(function () {

	jQuery.validator.addMethod("lettersonly", function (value, element) {
		return this.optional(element) || /^[А-я\s]*$/.test(value);
	});

	$.validator.addMethod(
		"cmbIsRequired",
		function (value, element) {
			let comboBoxSelect = $("#" + element.id).data("kendoComboBox").selectedIndex;

			if (parseInt(comboBoxSelect) < 0)
				return false;
			else
				return true;
		}
	);

	$.validator.addMethod(
		"textMask",
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

	$.validator.addMethod("dateTimeFilter", function (value, element) {
		let regex = new RegExp("^[0-9]{2}.[0-9]{2}.[0-9]{2} [0-9]{2}:[0-9]{2}$");

		return regex.test(value);
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
})
