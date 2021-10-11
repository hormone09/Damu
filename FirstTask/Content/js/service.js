
$(document).ready(function () {

	var notifElement = $("#allNotif");
	notifElement.kendoNotification({
	});
	var notification = notifElement.data("kendoNotification");

	$("body").on("click", "#btn-insert", function () {
		let window = $("#insert-window").data("kendoDialog");
		window.open();
	});

	$("#insert-window").kendoDialog({
		modal: true,
		width: "400px",
		closable: true,
		visible: false,
		title: false,
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
			success: function (json) {
				if (json.IsSuccess == true)
					notification.success(json.Message);
				else
					return notification.error(json.Error);
			}
		});

		$("#insert-window").data("kendoDialog").close();

		return false;
	});

	$("#edit-window").kendoDialog({
		modal: true,
		width: "500px",
		closable: true,
		visible: false,
		title: false,
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
			success: function (json) {
				if (json.IsSuccess == true) {
					var grid = $("#grid").data("kendoGrid");
					grid.dataSource.read();
					notification.success(json.Message);
				}
				else {
					notification.error(json.Message);
				}
			}
		});

		$("#edit-window").data("kendoDialog").close();

		return false;
	});

	function EditService(oldService) {
		$("#edit-form #edit-id").val(oldService.Id);
		$("#edit-form #edit-name").val(oldService.Name);
		$("#edit-form #edit-price").val(oldService.Price);
		$("#edit-form #edit-code").val(oldService.Code);

		$("#edit-window").data("kendoDialog").open();

		return false;
	}

	// TODO: заменить чистый JS
	document.getElementById('delete-window').style.display = "none";
	function DeleteService(id) {
		document.getElementById('delete-window').style.display = "block";
		$("#delete-window").kendoDialog({
			title: false,
			modal: true,
			width: "400px",
			closable: false,
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
							success: function (json) {
								if (json.IsSuccess == true) {
									var grid = $("#grid").data("kendoGrid");
									grid.dataSource.read();
									notification.success(json.Message);
								}
								else {
									notification.error(json.Message);
								}
							}
						})
					}
				},
				{ text: "Нет" }
			]
		});
	};

	$("#toolbar").kendoToolBar({
	});
	$("#nameFilter").kendoTextBox({
		change: function () {
			let grid = $("#grid").data("kendoGrid");
			grid.dataSource.page(1);
			grid.dataSource.read();
		}
	});

	$("#statusesList").kendoDropDownList({
		dataTextField: "text",
		dataValueField: "value",
		dataSource: [
			{ text: "Активные", value: 1 },
			{ text: "Отключенные", value: 2 },
		],
		select: function () {
			let grid = $("#grid").data("kendoGrid");
			grid.dataSource.page(1);
			setTimeout(() => grid.dataSource.read(), 1000);
		}
	});

	$("#orderByTypes").kendoDropDownList({
		dataTextField: "text",
		dataValueField: "value",
		dataSource: [
			{ text: "Названию", value: "Name" },
			{ text: "Возрастанию цены", value: "Price" },
			{ text: "Убыванию цены", value: "Price DESC" }
		],
		select: function () {
			let grid = $("#grid").data("kendoGrid");
			grid.dataSource.page(1);
			setTimeout(() => grid.dataSource.read(), 1000);
		}
	});

	var dataSource = new kendo.data.DataSource({
		pageSize: 20,
		transport: {
			read: {
				url: "/Service/Index",
				type: "POST",
				contentType: "application/json; charset=utf-8",
			},
			parameterMap: function (options) {
				let name = $("#nameFilter").val();
				var data = {
					ServiceName: name,
					Page: options.page,
					PageSize: options.pageSize,
					Status: $("#statusesList").val(),
					SortingType: $("#orderByTypes").val()
				}
				return kendo.stringify(data);
			}
		},
		schema: {
			data: "Items",
			total: "RowNumber",
			model: {
				fields: {
					DateOfBegin: { type: "date" }
				}
			}
		},
		page: "Page",
		serverPaging: true,
		serverSorting: true,
	});

	$("#grid").kendoGrid({
		dataSource: dataSource,
		columns: [
			{ field: "Id", title: "Id", width: "5%", hidden: true },
			{ field: "Name", title: "Название услуги", width: "20%" },
			{ field: "Price", title: "Цена", width: "15%" },
			{ field: "Status", title: "Статус", width: "10%" },
			{ field: "Code", title: "Код ", width: "15%" },
			{ field: "DateOfBegin", title: "Дата образования", width: "20%", format: "{0: dd-MM-yyyy}" },
			{
				command: [{
					name: "Delete",
					className: "btn-destroy",
					text: "Удаление",
					click: function (e) {
						var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
						DeleteService(dataItem.Id);
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
		width: "1000px"
	});
});