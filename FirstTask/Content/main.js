
$(document).ready(function () {

	$("body").on("click", "#btn-insert", function () {
		let window = $("#insert-window").data("kendoDialog");
		window.open();
	});

	$("#insert-window").kendoDialog({
		modal: true,
		width: "400px",
		closable: true,
		visible: false,
	});

	var formInsert = $("#insert-form").kendoForm({
		visible: false,
		formData: {
			Name: "Название услуги",
			Code: "Укажите номер услуги",
			DateOfBegin: new Date(),
			Price: 7000.00,
		},
		items: [
			{
				type: "group",
				label: "Добавление новой услуги",
				items: [
					{ field: "Name", label: "Назваине", validation: { required: true } },
					{ field: "Code", label: "Номер услуги", validation: { required: true } },
					{ field: "DateOfBegin", label: "Дата начала действия услуги", editor: "DatePicker", validation: { required: true } },
					{ field: "Price", label: "Цена", validation: { required: true } }
				]
			}
		]
	});

	formInsert.bind("submit", function (e) {
		var data = formInsert.serializeArray();

		$.ajax({
			url: "/Service/AddService/",
			type: "GET",
			data: data,
			success: function (data) {
				let json = JSON.parse(data);
				if (json.IsSuccess == true)
					return alert(json.Message);
				else
					return alert(json.Error);
			}
		});

		$("#insert-window").data("kendoDialog").close();

		return false;
	});

	var formEdit = $("#edit-form").kendoForm({
		visible: true
	});

	formEdit.bind("submit", function (e) {
		var data = formEdit.serializeArray();

		$.ajax({
			url: "/Service/EditService/",
			type: "GET",
			data: data,
			success: function (data) {
				let json = JSON.parse(data);
				if (json.IsSuccess == true) {
					var grid = $("#grid").data("kendoGrid");
					grid.dataSource.read();

					alert(json.Message);
				}
				else {
					alert(json.Error);
				}
			}
		});

		$("#edit-window").data("kendoDialog").close();

		return false;
	});

	$("#edit-window").kendoDialog({
		modal: true,
		width: "500px",
		closable: false,
		visible: false,
		titile: "Редактирование"
	});

	function EditService(oldService) {
		/*var date = $("#edit-form #edit-date").kendoDatePicker();
		date.closest("span.k-datepicker").width(200);
		var numeric = $("#edit-form #edit-price").kendoNumericTextBox();
		numeric.closest("span.k-numerictextbox").width(200);*/
		
		$("#edit-form #edit-id").val(oldService.Id);
		$("#edit-form #edit-name").val(oldService.Name);
		$("#edit-form #edit-price").val(oldService.Price);
		$("#edit-form #edit-code").val(oldService.Code);

		$("#edit-window").data("kendoDialog").open();

		return false;
	}

	// TODO: заменить чистый JS
	document.getElementById('delete-window').style.display = "none";
	function DeleteService(id, str) {
		document.getElementById('delete-window').style.display = "block";
		var window = $("#delete-window").kendoDialog({
			modal: true,
			width: "400px",
			closable: true,
			visible: true,
			actions: [
				{
					text: "Да",
					primary: true,
					action: function () {
						$.ajax({
							url: "/Service/DeleteService/",
							type: "POST",
							data: { id: id },
							success: function (data) {
								let json = JSON.parse(data);
								if (json.IsSuccess == true) {
									var grid = $("#grid").data("kendoGrid");
									grid.dataSource.read();

									alert(json.Message);
								}
								else {
									alert(json.Error);
								}
							}
						})
					}
				},
				{ text: "Нет" }
			]
		});
	};

	var dataSource = new kendo.data.DataSource({
		transport: {
			read: {
				url: "/Service/Index",
				type: "POST",
				contentType: "application/json; charset=utf-8",
			},
			parameterMap: function (data) {
				return data.models;
			}
		},
		schema: {
			data: "Items",
			total: "RowNumber",
		},
		pageSize: 20,
		page: "Page",
		serverPaging: true,
		serverSorting: true,
	});

	$("#toolbar").kendoToolBar({
});

	$("#grid").kendoGrid({
		dataSource: dataSource,
		columns: [
			{ field: "Id", title: "Id", width: "5%", hidden: true },
			{ field: "Name", title: "Название услуги", width: "20%" },
			{ field: "DateOfBegin", title: "Дата образования", width: "20%" },
			{ field: "Price", title: "Цена", width: "15%" },
			{ field: "Status", title: "Статус", width: "10%" },
			{ field: "Code", title: "Код ", width: "15%" },
			{
				command: [{
					name: "Delete",
					className: "btn-destroy",
					text: "Удаление",
					click: function (e) {
						var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
						//var str = $(this).closest("tr");
						var str = $(this).closest("tr");
						DeleteService(dataItem.Id, str);
					}
				}],
				width: "15%",
			},
			{
				command: [{
					name: "Edit",
					className: "btn-edit",
					text: "Редактирование",
					click: function (e) {
						var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
						EditService(dataItem);
					}
				}],
				width: "15%",
			},
		],
		pageable: true,
		scrollable: false,
		width: "600px"
	});
});