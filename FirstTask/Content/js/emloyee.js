
$(document).ready(function () {

	var notifElement = $("#employeeNotifications");
	notifElement.kendoNotification();
	var notification = notifElement.data("kendoNotification");

	// Insert
	$("body").on("click", "#employeeInsertButton", function () {
		let window = $("#employeeInsertWindow").data("kendoDialog");
		window.open();
	});

	$("#employeeInsertWindow").kendoDialog({
		modal: true,
		width: "400px",
		closable: true,
		visible: false,
		title: false,
	});

	$("body").on("click", "#employeeCloseInsertWindow", function () {
		$("#employeeInsertWindow").data("kendoDialog").close();
	});

	var formInsert = $("#employeeInsertForm").kendoForm({
		visible: false,
		formData: {
		},
		items: [
			{
				type: "group",
				label: "Добавление нового сотрудника",
				items: [
					{ field: "FullName", label: "Полное имя", validation: { required: true } },
					{
						field: "Phone", label: "Телефон", validation: { required: true }, editor: function (container, options) {
							var input = $('<input name="Phone" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								mask: "0-(000)-000-00-00"
							});
						}
					},
					{ field: "BirthdayDate", label: "Дата рождения", editor: "DatePicker", validation: { required: true } },
					{ field: "DateOfBegin", label: "Дата начала действия ", editor: "DatePicker", validation: { required: true } },
					{
						field: "PersonalNumber", label: "ИИН", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input name="PersonalNumber" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								mask: "000-000-000-000"
							});
						}
					},
					{
						field: "CompanyId", label: "Компания", validation: { required: false },
						editor: function (container, options) {
							var input = $('<input id="CompanyId" name="CompanyId"  />');
							input.appendTo(container);
							input.kendoDropDownList();
						}
					},
				]
			},
		],
		buttonsTemplate: "<button class='btn-success' type='submit'>Сохранить</button> <button class='btn-danger' id='employeeCloseInsertWindow' type='button' class=''>Отмена</button>"
	});

	formInsert.bind("submit", function (e) {
		var data = formInsert.serializeArray();

		$.ajax({
			url: "/Employee/AddEmployee/",
			type: "POST",
			data: data,
			success: function (json) {
				if (json.IsSuccess == true)
					notification.success(json.Message);
				else
					return notification.error(json.Error);
			}
		});

		$("#employeeInsertWindow").data("kendoDialog").close();

		return false;
	});

	// Edit
	$("body").on("click", "#employeeCloseEditWindow", function () {
		$("#editEmployeeWindow").data("kendoDialog").close();
	});

	$("#editEmployeeWindow").kendoDialog({
		modal: true,
		width: "500px",
		closable: true,
		visible: false,
		title: false,
	});

	var formEdit = $("#editEmployeeForm").kendoForm({
		visible: true,
		items: [
			{
				type: "group",
				label: "Редактирование параметров сотрудника",
				items: [
					{
						field: "Id", label: "", editor: function (container, options) {
							var input = $('<input id="editId" name="Id" type="hidden"/>');
							input.appendTo(container);
						}
					},
					{
						field: "FullName", label: "Полное имя", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editFullName" name="FullName" required="required" />');
							input.appendTo(container);
							input.kendoTextBox();
						}
					},
					{
						field: "Phone", label: "Телефон", validation: { required: true }, editor: function (container, options) {
							var input = $('<input id="editPhone" name="Phone" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								mask: "0-(000)-000-00-00"
							});
						}
					},
					{
						field: "PersonalNumber", label: "ИИН", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editPesonalNumber" name="PersonalNumber" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								mask: "000-000-000-000"
							});
						}
					},
					{
						field: "BirthdayDate", label: "Дата рождения", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editBirthdayDate" name="BirthdayDate" required="required" />');
							input.appendTo(container);
							input.kendoDatePicker({
								format: 'dd/MM/yy',
							});
						}
					},
					{
						field: "CompanyId", label: "Компания", validation: { required: false },
						editor: function (container, options) {
							var input = $('<input id="editCompanyId" name="CompanyId"  />');
							input.appendTo(container);
							input.kendoDropDownList();
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
		buttonsTemplate: "<button class='btn-success' type='submit'>Сохранить</button> <button class='btn-danger' id='employeeCloseEditWindow' type='button' class=''>Отмена</button>"
	});

	formEdit.bind("submit", function (e) {
		var data = formEdit.serializeArray();

		$.ajax({
			url: "/Employee/EditEmployee/",
			type: "POST",
			data: data,
			success: function (json) {
				if (json.IsSuccess == true) {
					var grid = $("#emloyeeGrid").data("kendoGrid");
					grid.dataSource.read();
					notification.success(json.Message);
				}
				else {
					notification.error(json.Error);
				}
			}
		});

		$("#editEmployeeWindow").data("kendoDialog").close();

		return false;
	});

	function EditEmployee(oldService) {
		$("#editEmployeeForm #editId").val(oldService.Id);
		$("#editEmployeeForm #editFullName").val(oldService.FullName);
		$("#editEmployeeForm #editPhone").val(oldService.Phone);
		$("#editEmployeeForm #editCompanyId").val(oldService.CompanyId);
		$("#editEmployeeForm #editPersonalNumber").val(oldService.PersonalNumber);

		$("#editEmployeeWindow").data("kendoDialog").open();

		return false;
	}

	// Delete
	document.getElementById('employeeDeleteWindow').style.display = "none";
	function DeleteEmployee(id) {
		document.getElementById('employeeDeleteWindow').style.display = "block";
		$("#employeeDeleteWindow").kendoDialog({
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
							url: "/Employee/DeleteEmployee/",
							type: "POST",
							data: { Id: id },
							success: function (json) {
								if (json.IsSuccess == true) {
									var grid = $("#emloyeeGrid").data("kendoGrid");
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
	$("#employeeToolbar").kendoToolBar({
	});
	$("#fullNameFilter").kendoTextBox({
		change: function () {
			let grid = $("#emloyeeGrid").data("kendoGrid");
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
			let grid = $("#emloyeeGrid").data("kendoGrid");
			grid.dataSource.page(1);
			setTimeout(() => grid.dataSource.read(), 1000);
		}
	});

	$("#companyFilter").kendoComboBox({
		dataSource: {
			transport: {
				read: {
					url: "/Company/Index",
					type: "POST",
					contentType: "application/json; charset=utf-8",
				},
				parameterMap: function (options) {
					var data = { PageSize: 50, CompanyName: this.dataValueField, Status: 1 };

					return kendo.stringify(data);
				}
			}
		},
		change: function () {
			$("#companyFilter").data("kendoComboBox").dataSource.read();
		},
		serverFiltering: true
	});

	function changeComboBox() {
		alert("Wordking!");
		$("#companyFilter").data("kendoComboBox").dataSource.read();
	}

	$("#employeeSortingTypes").kendoDropDownList({
		dataTextField: "text",
		dataValueField: "value",
		dataSource: [
			{ text: "Имени", value: "FullName" },
			{ text: "Дата рождения", value: "BirthdayDate" }
		],
		select: function () {
			let grid = $("#emloyeeGrid").data("kendoGrid");
			grid.dataSource.page(1);
			setTimeout(() => grid.dataSource.read(), 1000);
		}
	});

	// GRID
	var dataSource = new kendo.data.DataSource({
		pageSize: 20,
		transport: {
			read: {
				url: "/Employee/Index",
				type: "POST",
				contentType: "application/json; charset=utf-8",
			},
			parameterMap: function (options) {
				let name = $("#fullNameFilter").val();
				var data = {
					FullName: name,
					Page: options.page,
					PageSize: options.pageSize,
					Status: $("#statusesList").val(),
					SortingType: $("#employeeSortingTypes").val()
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
		page: "Page",
		serverPaging: true,
		serverSorting: true,
	});

	$("#emloyeeGrid").kendoGrid({
		dataSource: dataSource,
		columns: [
			{ field: "Id", title: "Id", width: "5%", hidden: true },
			{ field: "FullName", title: "Полное имя", width: "20%" },
			{ field: "PersonalNumber", title: "ИИН", width: "15%" },
			{ field: "Phone", title: "Номер", width: "15%" },
			{ field: "Company.Name", title: "Название компании", width: "15%" },
			{ field: "BirthdayDate", title: "Дата рождения", width: "20%", format: "{0: dd-MM-yyyy}" },
			{ field: "DateOfBegin", title: "Дата образования", width: "20%", format: "{0: dd-MM-yyyy}" },
			{
				command: [{
					name: "Delete",
					className: "btn-destroy",
					text: "Удаление",
					click: function (e) {
						var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
						DeleteEmployee(dataItem.Id);
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
						EditEmployee(dataItem);
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