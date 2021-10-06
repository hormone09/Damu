
$(document).ready(function () {
	$("#delete-window").kendoDialog({
		modal: true,
		width: "400px",
		closable: true,
		visible: false,
		actions: [
			{
				text: "Да",
				primary: true,
				action: function () {
					$.ajax({
						url: "/Service/DeleteService/",
						type: "POST",
						data: { id: 2 },
						success: function (data) {
							let json = JSON.parse(data);
							if (json.IsSuccess == true)
								alert(json.Message);
							else
								alert(json.Error);
						}
					})
				}
			},
			{ text: "Нет" }
		]
	});

	$("body").on("click", "#btn", function () {
		var dialog = $("#delete-window").data("kendoDialog");
		dialog.open();
	});

	

	var data = [
		{
			FirstName: "Vladimir",
			LastName: "Korobov"
		},
		{
			FirstName: "Some",
			LastName: "People"
		}]

	$("#grid").kendoGrid({
		columns: [{
			field: "Name",
			title: "Title",
		},
		{
			field: "Price",
			title: "Title",
		},
		{
			field: "Date",
			title: "Title",
		},
		{
			field: "Id",
			title: "Title",
		},
		{
			field: "Button",
			title: "Title",
		}],
	});
});