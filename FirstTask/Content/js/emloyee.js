
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
		closable: false,
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
					{
						field: "FullName", label: "Полное имя", placeholder: "Укажите полное имя: ", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="empInsertFullName" name="FullName" required="required" />');
							input.appendTo(container);
							input.kendoTextBox();
						}
					},
					{
						field: "Phone", label: "Телефон", validation: { required: true }, editor: function (container, options) {
							var input = $('<input id="empInsertPhone" name="Phone" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								placeholder: "Укажите номер телефона",
								mask: "0-(000)-000-00-00"
							});
						}
					},
					{
						field: "BirthdayDate", label: "Дата рождения", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="empInsertBirthdayDate" name="BirthdayDate" required="required" />');
							input.appendTo(container);
							input.kendoDatePicker({
								placeholder: "Укажите дату рождения",
								format: 'dd/MM/yy',
							});
						}
					},
					{
						field: "DateOfBegin", label: "Дата начала работы", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="empInsertDateOfBegin" name="DateOfBegin" required="required" />');
							input.appendTo(container);
							input.kendoDatePicker({
								placeholder: "Укажите дату начала работы",
								format: 'dd/MM/yy',
							});
						}
					},
					{
						field: "PersonalNumber", label: "ИИН", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="empInsertPesonalNumber" name="PersonalNumber" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								placeholder: "Укажите 12 чисел",
								mask: "000-000-000-000"
							});
						}
					},
					{
						field: "CompanyId", label: "Компания", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="empInsertCompanyId" name="CompanyId" required="required" />');
							input.appendTo(container);
							input.kendoComboBox({
								placeholder: "Введите название",
								dataTextField: "Name",
								dataValueField: "Id",
								filter: "contains",
								dataSource: companies
							});
						}
					},
				]
			},
		],
		buttonsTemplate: "<button class='btn-success' type='submit'>Сохранить</button> <button class='btn-danger' id='employeeCloseInsertWindow' type='button' class=''>Отмена</button>"
	});

	formInsert.bind("submit", function (e) {
		let data = {
			DateOfBegin: $("#empInsertDateOfBegin").data("kendoDatePicker").value(),
			Company: { Id: $("#empInsertCompanyId").val() },
			BirthdayDate: $("#empInsertBirthdayDate").data("kendoDatePicker").value(),
			FullName: $("#empInsertFullName").val(),
			PersonalNumber: $("#empInsertPesonalNumber").val(),
			Phone: $("#empInsertPhone").val()
		}

		$.ajax({
			url: "/Employee/AddEmployee/",
			type: "POST",
			contentType: "application/json; charset=utf-8",
			data: JSON.stringify(data),
			success: function (json) {
				if (json.IsSuccess == true) {
					var grid = $("#emloyeeGrid").data("kendoGrid");
					grid.dataSource.read();
					notification.success(json.Message);
				}
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
		closable: false,
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
							var input = $('<input id="empEditId" name="Id" type="hidden"/>');
							input.appendTo(container);
						}
					},
					{
						field: "FullName", label: "Полное имя", placeholder: "Укажите полное имя: ", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="empEditFullName" name="FullName" required="required" />');
							input.appendTo(container);
							input.kendoTextBox();
						}
					},
					{
						field: "Phone", label: "Телефон", validation: { required: true }, editor: function (container, options) {
							var input = $('<input id="empEditPhone" name="Phone" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								placeholder: "Укажите номер телефона",
								mask: "0-(000)-000-00-00"
							});
						}
					},
					{
						field: "PersonalNumber", label: "ИИН", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="empEditPesonalNumber" name="PersonalNumber" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								placeholder: "Укажите 12 чисел",
								mask: "000-000-000-000"
							});
						}
					},
					{
						field: "BirthdayDate", label: "Дата рождения", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="empEditBirthdayDate" name="BirthdayDate" required="required" />');
							input.appendTo(container);
							input.kendoDatePicker({
								placeholder: "Укажите дату рождения",
								format: 'dd/MM/yy',
							});
						}
					},
					{
						field: "CompanyId", label: "Компания", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="empEditCompanyId" name="CompanyId"  required="required" />');
							input.appendTo(container);
							input.kendoComboBox({
								placeholder: "Введите название",
								dataTextField: "Name",
								dataValueField: "Id",
								filter: "contains",
								dataSource: companies
							});
						}
					},
					{
						field: "DateOfBegin", label: "Дата начала работы", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="empEditDateOfBegin" name="DateOfBegin" required="required" />');
							input.appendTo(container);
							input.kendoDatePicker({
								placeholder: "Укажите дату начала работы",
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
		let data = {
			Id: $("#empEditId").val(),
			DateOfBegin: $("#empEditDateOfBegin").data("kendoDatePicker").value(),
			Company: { Id: $("#empEditCompanyId").val() },
			BirthdayDate: $("#empEditBirthdayDate").data("kendoDatePicker").value(),
			FullName: $("#empEditFullName").val(),
			PersonalNumber: $("#empEditPesonalNumber").val(),
			Phone: $("#empEditPhone").val()
		}

		$.ajax({
			url: "/Employee/EditEmployee/",
			type: "POST",
			contentType: "application/json; charset=utf-8",
			data: JSON.stringify(data),
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

		$("#editEmployeeWindow").data("kendoDialog").open();

		return false;
	}

	// Delete
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
		change: function () {
			let grid = $("#emloyeeGrid").data("kendoGrid");
			grid.dataSource.page(1);
		}
	});

	$("#companyFilter").kendoComboBox({
		placeholder: "Введите название",
		dataTextField: "Name",
		dataValueField: "Id",
		filter: "contains",
		dataSource: companies,
		change: function () {
			let grid = $("#emloyeeGrid").data("kendoGrid");
			grid.dataSource.page(1);
		}
	});

	$("#employeeSortingTypes").kendoDropDownList({
		dataTextField: "text",
		dataValueField: "value",
		dataSource: [
			{ text: "Имени", value: "FullName" },
			{ text: "Дата рождения", value: "BirthdayDate" }
		],
		change: function () {
			let grid = $("#emloyeeGrid").data("kendoGrid");
			grid.dataSource.page(1);
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
					SortingType: $("#employeeSortingTypes").val(),
					CompanyId: $("#companyFilter").val()
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
					DateOfBegin: { type: "date" },
					DateOfFinish: { type: "date" }
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
			{ field: "FullName", title: "Полное имя", width: "10%" },
			{ field: "PersonalNumber", title: "ИИН", width: "5%" },
			{ field: "Phone", title: "Номер", width: "15%" },
			{ field: "Company.Name", title: "Название компании", width: "5%" },
			{ field: "BirthdayDate", title: "Дата рождения", width: "8%", format: "{0: dd-MM-yyyy}" },
			{ field: "DateOfBegin", title: "Дата начала работы", width: "7%", format: "{0: dd-MM-yyyy}" },
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
							DeleteEmployee(dataItem.Id);
						},
					},
					{
						name: "Edit",
						className: "btn-edit",
						text: "Редактирование",
						click: function (e) {
							var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
							EditEmployee(dataItem);
						},
					},
				]
			},
		],
		pageable: true,
		scrollable: false,
	});
});