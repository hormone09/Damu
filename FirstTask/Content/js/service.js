
$(document).ready(function () {
	
	var notifElement = $("#allNotif");
	notifElement.kendoNotification({
	});
	var notification = notifElement.data("kendoNotification");

	// Insert
	$("body").on("click", "#btn-insert", function () {
		let window = $("#insert-window").data("kendoDialog");
		window.open();
	});

	$("body").on("click", "#serviceCloseInsertWindow", function () {
		$("#insert-window").data("kendoDialog").close();
	});

	$("body").on("click", "#serviceCloseEditWindow", function () {
		$("#edit-window").data("kendoDialog").close();
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
		items: [
			{
				type: "group",
				label: "Добавление новой услуги",
				items: [
					{
						field: "Name", label: "Назваине", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="insertName" name="Name" required="required" />');
							input.appendTo(container);
							input.kendoTextBox({
								placeholder: "Укажите наименование услуги",
							});
						}
					},
					{
						field: "Price", label: "Price", placeholder: "Укажите стоимость", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="insertPrice" name="Price" required="required" />');
							input.appendTo(container);
							input.kendoNumericTextBox({
								placeholder: "Введите стоимость ",
								culture: "de-DE"
							});
						}
					},
					{
						field: "DateOfBegin", label: "Дата начала работы", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="insertDate" name="DateOfBegin" required="required" />');
							input.appendTo(container);
							input.kendoDatePicker({
								format: 'dd/MM/yy',
							});
						}
					},
					{
						field: "Code", label: "Код услуги", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editCode" name="Code" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								placeholder: "Укажите код в формате 'Z00.000.000'",
								mask: "L00.000.000"
							});
						}
					}
				]
			}
		],
		buttonsTemplate: "<button class='btn-success' type='submit'>Сохранить</button> <button class='btn-danger' id='serviceCloseInsertWindow' type='button'>Отмена</button>"
	});

	formInsert.bind("submit", function (e) {
		var data = formInsert.serializeArray();

		$.ajax({
			url: "/Service/AddService/",
			type: "POST",
			data: data,
			success: function (json) {
				if (json.IsSuccess == true) {
					var grid = $("#servicesGrid").data("kendoGrid");
					grid.dataSource.read();
					notification.success(json.Message);
				}
				else
					return notification.error(json.Error);
			}
		});

		$("#insert-window").data("kendoDialog").close();

		return false;
	});

	// Edit
	$("#edit-window").kendoDialog({
		modal: true,
		width: "500px",
		closable: true,
		visible: false,
		title: false,
	});

	var formEdit = $("#editServiceForm").kendoForm({
		visible: true,
		items: [
			{
				type: "group",
				label: "Редактирование параметров услуги",
				items: [
					{
						field: "Id", label: "", editor: function (container, options) {
							var input = $('<input id="editId" name="Id" type="hidden"/>');
							input.appendTo(container);
						}
					},
					{
						field: "Name", label: "Назваине",  validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editName" name="Name" required="required" />');
							input.appendTo(container);
							input.kendoTextBox({
								placeholder: "Укажите наименование услуги",
							});
						}
					},
					{
						field: "Price", label: "Price", placeholder: "Укажите стоимость", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editPrice" name="Price" required="required" />');
							input.appendTo(container);
							input.kendoNumericTextBox({
								placeholder: "Введите стоимость ",
								culture: "de-DE"
							});
						}
					},
					{
						field: "DateOfBegin", label: "Дата начала работы", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editDate" name="DateOfBegin" required="required" />');
							input.appendTo(container);
							input.kendoDatePicker({
								placeholder: "Введите точную дату",
								format: 'dd/MM/yy',
							});
						}
					},
					{
						field: "Code", label: "Код услуги", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editCode" name="Code" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								placeholder: "Укажите код в формате 'Z00.000.000'",
								mask: "L00.000.000"
							});
						}
					}
				]
			}
		],
		buttonsTemplate: "<button class='btn-success' type='submit'>Сохранить</button> <button class='btn-danger' id='serviceCloseEditWindow' type='button'>Отмена</button>"
	});

	formEdit.bind("submit", function (e) {
		var data = formEdit.serializeArray();

		$.ajax({
			url: "/Service/EditService/",
			type: "POST",
			data: data,
			success: function (json) {
				if (json.IsSuccess == true) {
					var grid = $("#servicesGrid").data("kendoGrid");
					grid.dataSource.read();
					notification.success(json.Message);
				}
				else {
					notification.error(json.Error);
				}
			}
		});

		$("#edit-window").data("kendoDialog").close();

		return false;
	});

	function EditService(oldService) {
		$("#editServiceForm #editId").val(oldService.Id);

		$("#edit-window").data("kendoDialog").open();

		return false;
	}


	// Delete
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
									var grid = $("#servicesGrid").data("kendoGrid");
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

	// Filters
	$("#toolbar").kendoToolBar({
	});
	$("#serviceNameFilter").kendoTextBox({
		change: function () {
			let grid = $("#servicesGrid").data("kendoGrid");
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
		change: function () {
			let grid = $("#servicesGrid").data("kendoGrid");
			grid.dataSource.page(1);
			grid.dataSource.read();
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
		change: function () {
			let grid = $("#servicesGrid").data("kendoGrid");
			grid.dataSource.page(1);
		}
	});

	// GRID

	var dataSource = new kendo.data.DataSource({
		pageSize: 20,
		transport: {
			read: {
				url: "/Service/Index",
				type: "POST",
				contentType: "application/json; charset=utf-8",
			},
			parameterMap: function (options) {
				let name = $("#serviceNameFilter").val();
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
			total: function (response) {
				if (response.length > 0)
					return response[0].TotalRows;
				else 
					return 1;
			},
			model: {
				fields: {
					DateOfBegin: { type: "date" },
				}
			},
		},

		serverPaging: true,
		serverSorting: true,
	}); 

	$("#servicesGrid").kendoGrid({
		dataSource: dataSource,
		columns: [
			{ field: "Id", title: "Id", width: "5%", hidden: true },
			{ field: "Name", title: "Название услуги", width: "30%" },
			{ field: "Price", title: "Цена", width: "10%" },
			{ field: "Code", title: "Код ", width: "10%" },
			{ field: "DateOfBegin", title: "Дата образования", width: "10%", format: "{0: dd-MM-yyyy}" },
			{
				field: "Status", title: "Статус", width: "5%", values: [
					{ text: "Активно", value: 1 },
					{ text: "Удалено", value: 2 }
				]
			},
			{
				command: [
					{
						name: "Delete",
						className: "btn-destroy",
						text: "Удаление",
						visible: function(dataItem) { return dataItem.Status == 1 },
						click: function (e) {
							var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
							DeleteService(dataItem.Id);
						}
					},
					{
						name: "Edit",
						className: "btn-edit",
						text: "Редактирование",
						click: function (e) {
							var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
							EditService(dataItem);
						}
					},
				],
			},
		],
		pageable: true,
		scrollable: false,
	});
});