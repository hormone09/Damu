$(document).ready(function () {


	var notifElement = $("#sProvidedNotifications");
	notifElement.kendoNotification();
	var notification = notifElement.data("kendoNotification");

	// Insert
	$("body").on("click", "#sProvidedInsertButton", function () {
		let window = $("#sProvidedInsertWindow").data("kendoDialog");
		window.open();
	});

	$("body").on("click", "#sProvidedCloseInsertWindow", function () {
		$("#sProvidedInsertWindow").data("kendoDialog").close();
	});

	$("body").on("click", "#sProvidedCloseEditWindow", function () {
		$("#editSProvidedWindow").data("kendoDialog").close();
	});

	$("#sProvidedInsertWindow").kendoDialog({
		modal: true,
		width: "400px",
		closable: false,
		visible: false,
		title: false,
	});

	var formInsert = $("#sProvidedInsertForm").kendoForm({
		visible: false,
		formData: {
		},
		items: [
			{
				type: "group",
				label: "Добавление оказываемых услуг",
				items: [
					{
						field: "Company.Id", label: "Компания", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="insertCompanyId" name="Company.Id"  />');
							input.appendTo(container);
							input.kendoComboBox({
								placeholder: "Введите название",
								dataTextField: "Name",
								dataValueField: "Id",
								filter: "contains",
								dataSource: getCompanyList()
							});
						}
					},
					{
						field: "Service.Id", label: "Услуга", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="insertServiceId" name="Service.Id"  />');
							input.appendTo(container);
							input.kendoComboBox({
								placeholder: "Введите название",
								dataTextField: "Name",
								dataValueField: "Id",
								filter: "contains",
								dataSource: getServiceList()
							});
						}
					},
					{
						field: "ServicePrice", label: "Price", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="insertServicePrice" name="ServicePrice" required="required" />');
							input.appendTo(container);
							input.kendoNumericTextBox({
								placeholder: "Введите стоимость",
								culture: "de-DE"
							});
						}
					},
					{
						field: "DateOfBegin", label: "Дата начала работы", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editDateOfBegin" name="DateOfBegin" required="required" />');
							input.appendTo(container);
							input.kendoDatePicker({
								placeholder: "Укажите точную дату",
								format: 'dd/MM/yy',
							});
						}
					},
				]
			}
		],
		buttonsTemplate: "<button class='btn-success' type='submit'>Сохранить</button> <button class='btn-danger' id='sProvidedCloseInsertWindow' type='button'>Отмена</button>"
	});

	formInsert.bind("submit", function (e) {
		var data = formInsert.serializeArray();
		$.ajax({
			url: "/ServiceProvided/AddProvidedService/",
			type: "POST",
			data: data,
			success: function (json) {
				if (json.IsSuccess == true) {
					var grid = $("#providedGrid").data("kendoGrid");
					grid.dataSource.read();
					notification.success(json.Message);
				}
				else
					return notification.error(json.Error);
			}
		});

		$("#sProvidedInsertWindow").data("kendoDialog").close();

		return false;
	});


	// Edit
	$("#editSProvidedWindow").kendoDialog({
		modal: true,
		width: "500px",
		closable: false,
		visible: false,
		title: false,
	});

	var formEdit = $("#editSProvidedForm").kendoForm({
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
						field: "Company.Id", label: "Компания", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="insertCompanyId" name="Company.Id"  required="required" />');
							input.appendTo(container);
							input.kendoComboBox({
								placeholder: "Введите название",
								dataTextField: "Name",
								dataValueField: "Id",
								filter: "contains",
								dataSource: getCompanyList()
							});
						}
					},
					{
						field: "Service.Id", label: "Услуга", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="insertServiceId" name="Service.Id"  required="required" />');
							input.appendTo(container);
							input.kendoComboBox({
								placeholder: "Введите название",
								dataTextField: "Name",
								dataValueField: "Id",
								filter: "contains",
								dataSource: getServiceList()
							});
						}
					},
					{
						field: "ServicePrice", label: "Price", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editServicePrice" name="ServicePrice" required="required" />');
							input.appendTo(container);
							input.kendoNumericTextBox({
								placeholder: "Введите стоимость услуги",
								culture: "de-DE"
							});
						}
					},
					{
						field: "DateOfBegin", label: "Дата начала работы", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editDateOfBegin" name="DateOfBegin" required="required" />');
							input.appendTo(container);
							input.kendoDatePicker({
								placeholder: "Укажите точную дату",
								format: 'dd/MM/yy',
							});
						}
					},
				]
			}
		],
		buttonsTemplate: "<button class='btn-success' type='submit'>Сохранить</button> <button class='btn-danger' id='sProvidedCloseEditWindow' type='button'>Отмена</button>"
	});

	formEdit.bind("submit", function (e) {
		var data = formEdit.serializeArray();

		$.ajax({
			url: "/ServiceProvided/EditProvidedService/",
			type: "POST",
			data: data,
			success: function (json) {
				if (json.IsSuccess == true) {
					var grid = $("#providedGrid").data("kendoGrid");
					grid.dataSource.read();
					notification.success(json.Message);
				}
				else {
					notification.error(json.Error);
				}
			}
		});

		$("#editSProvidedWindow").data("kendoDialog").close();

		return false;
	});

	function EditSProvided(oldService) {
		$("#editSProvidedForm #editId").val(oldService.Id);
		$("#editSProvidedWindow").data("kendoDialog").open();

		return false;
	}

	// Delete
	document.getElementById('sProdivdedDeleteWindow').style.display = "none";
	function DeleteSProvided(id) {
		document.getElementById('sProdivdedDeleteWindow').style.display = "block";
		$("#sProdivdedDeleteWindow").kendoDialog({
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
							url: "/ServiceProvided/DeleteProvidedService",
							type: "POST",
							data: { Id: id },
							success: function (json) {
								if (json.IsSuccess == true) {
									var grid = $("#providedGrid").data("kendoGrid");
									grid.dataSource.read();
									notification.success(json.Message);
								}
								else {
									notification.error(json.Error);
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
	$("#sProvidedToolbar").kendoToolBar({});

	$("#sCompany").kendoComboBox({
		placeholder: "Введите название",
		dataTextField: "Name",
		dataValueField: "Id",
		filter: "contains",
		dataSource: getCompanyList(),
		change: function () {
			let grid = $("#providedGrid").data("kendoGrid");
			grid.dataSource.page(1);
		}
	});

	$("#sService").kendoComboBox({
		placeholder: "Введите название",
		dataTextField: "Name",
		dataValueField: "Id",
		filter: "contains",
		dataSource: getServiceList(),
		change: function () {
			let grid = $("#providedGrid").data("kendoGrid");
			grid.dataSource.page(1);
		}
	});

	$("#sProvidedStatusesList").kendoDropDownList({
		dataTextField: "text",
		dataValueField: "value",
		dataSource: [
			{ text: "Активные", value: 1 },
			{ text: "Отключенные", value: 2 },
		],
		change: function () {
			let grid = $("#providedGrid").data("kendoGrid");
			grid.dataSource.page(1);
		}
	});

	$("#sProvidedSortingTypes").kendoDropDownList({
		dataTextField: "text",
		dataValueField: "value",
		dataSource: [
			{ text: "Дата начала работы", value: "DateOfBegin" }
		],
		change: function () {
			let grid = $("#providedGrid").data("kendoGrid");
			grid.dataSource.page(1);
		}
	});

	// GRID
	var dataSource = new kendo.data.DataSource({
		pageSize: 20,
		transport: {
			read: {
				url: "/ServiceProvided/Index",
				type: "POST",
				contentType: "application/json; charset=utf-8",
			},
			parameterMap: function (options) {
				var data = {
					Page: options.page,
					PageSize: options.pageSize,
					Status: $("#sProvidedStatusesList").val(),
					CompanyId: $("#sCompany").val(),
					ServiceId: $("#sService").val()
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
					BirthdayDate: { type: "date" },
					DateOfBegin: { type: "date" }
				}
			}
		},
		serverPaging: true,
		serverSorting: true,
	});

	$("#providedGrid").kendoGrid({
		dataSource: dataSource,
		columns: [
			{ field: "Id", title: "Id", width: "5%", hidden: true },
			{ field: "Service.Name", title: "Название услуги", width: "20%" },
			{ field: "Service.Price", title: "Стоимость услуги", width: "15%" },
			{ field: "Company.Name", title: "Название компании", width: "15%" },
			{ field: "DateOfBegin", title: "Дата начала действия", width: "10%", format: "{0: dd-MM-yyyy}" },
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
						visible: function (dataItem) { return dataItem.Status == 1 },
						click: function (e) {
							var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
							DeleteSProvided(dataItem.Id);
						},
					},
					{
						name: "Edit",
						className: "btn-edit",
						text: "Редактирование",
						click: function (e) {
							var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
							EditSProvided(dataItem);
						},
					},
				]
			},
		],
		pageable: true,
		scrollable: false,
	});
});