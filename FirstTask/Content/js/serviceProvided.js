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
		closable: true,
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
						field: "Company", label: "Компания", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editCompany" name="FullName" required="required" />');
							input.appendTo(container);
							input.kendoComboBox();
						}
					},
					{
						field: "Service", label: "Услуга", validation: { required: true }, editor: function (container, options) {
							var input = $('<input id="editPhone" name="Service" required="required" />');
							input.appendTo(container);
							input.kendoComboBox();
						}
					},
					{
						field: "DateOfBegin", label: "Дата начала работы", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editDateOfBegin" name="DateOfBegin" required="required" />');
							input.appendTo(container);
							input.kendoDatePicker({
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
				if (json.IsSuccess == true)
					notification.success(json.Message);
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
		closable: true,
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
						field: "Company", label: "Компания", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editCompany" name="FullName" required="required" />');
							input.appendTo(container);
							input.kendoComboBox();
						}
					},
					{
						field: "Service", label: "Услуга", validation: { required: true }, editor: function (container, options) {
							var input = $('<input id="editPhone" name="Service" required="required" />');
							input.appendTo(container);
							input.kendoComboBox();
						}
					},
					{
						field: "DateOfBegin", label: "Дата начала работы", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editDateOfBegin" name="DateOfBegin" required="required" />');
							input.appendTo(container);
							input.kendoDatePicker({
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
		$("#editSProvidedForm #editFullName").val(oldService.FullName);
		$("#editSProvidedForm #editPhone").val(oldService.Phone);
		$("#editSProvidedForm #editCompanyId").val(oldService.CompanyId);
		$("#editSProvidedForm #editPersonalNumber").val(oldService.PersonalNumber);

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
	});

	$("#sService").kendoComboBox({
		placeholder: "Введите название",
	});

	$("#sProvidedStatusesList").kendoDropDownList({
		dataTextField: "text",
		dataValueField: "value",
		dataSource: [
			{ text: "Активные", value: 1 },
			{ text: "Отключенные", value: 2 },
		],
		select: function () {
			let grid = $("#providedGrid").data("kendoGrid");
			grid.dataSource.page(1);
			setTimeout(() => grid.dataSource.read(), 1000);
		}
	});

	$("#sProvidedSortingTypes").kendoDropDownList({
		dataTextField: "text",
		dataValueField: "value",
		dataSource: [
			{ text: "Имени", value: "FullName" },
			{ text: "Дата рождения", value: "BirthdayDate" }
		],
		select: function () {
			let grid = $("#providedGrid").data("kendoGrid");
			grid.dataSource.page(1);
			setTimeout(() => grid.dataSource.read(), 1000);
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
					Status: $("#sProvidedStatusesList").val()
				}
				return kendo.stringify(data);
			}
		},
		schema: {
			total: function (response) {
				return response[0].TotalRows;
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
			{ field: "DateOfBegin", title: "Дата начала действия", width: "20%", format: "{0: dd-MM-yyyy}" },
			{
				command: [{
					name: "Delete",
					className: "btn-destroy",
					text: "Удаление",
					click: function (e) {
						var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
						DeleteSProvided(dataItem.Id);
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
						EditSProvided(dataItem);
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