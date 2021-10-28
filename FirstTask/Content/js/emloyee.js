﻿
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

	$("#employeeInsertForm").kendoForm({
		visible: false,
		formData: {
		},
		items: [
			{
				type: "group",
				label: "Добавление нового сотрудника",
				items: [
					{
						field: "FullName", label: "Полное имя", placeholder: "Укажите полное имя: ",
						editor: function (container, options) {
							var input = $('<input id="empInsertFullName" name="FullName" />');
							input.appendTo(container);
							input.kendoTextBox();
						}
					},
					{
						field: "Phone", label: "Телефон", editor: function (container, options) {
							var input = $('<input id="empInsertPhone" name="Phone" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								placeholder: "Укажите номер телефона",
								mask: "0-(000)-000-00-00"
							});
						}
					},
					{
						field: "BirthdayDate", label: "Дата рождения",
						editor: function (container, options) {
							var input = $('<input id="empInsertBirthdayDate" name="BirthdayDate" />');
							input.appendTo(container);
							input.kendoDatePicker({
								placeholder: "Укажите дату рождения",
								format: 'dd/MM/yyyy',
							});
						}
					},
					{
						field: "DateOfBegin", label: "Дата начала работы",
						editor: function (container, options) {
							var input = $('<input id="empInsertDateOfBegin" name="DateOfBegin" />');
							input.appendTo(container);
							input.kendoDatePicker({
								placeholder: "Укажите дату начала работы",
								format: 'dd/MM/yyyy',
							});
						}
					},
					{
						field: "PersonalNumber", label: "ИИН",
						editor: function (container, options) {
							var input = $('<input id="empInsertPesonalNumber" name="PersonalNumber" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								placeholder: "Укажите 12 чисел",
								mask: "000-000-000-000"
							});
						}
					},
					{
						field: "CompanyId", label: "Компания",
						editor: function (container, options) {
							var input = $('<input id="empInsertCompanyId" name="CompanyId" />');
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
						field: "Validator", label: "",
						editor: function (container, options) {
							var block = $('<div class="validateContainer"/>');
							block.appendTo(container);
						}
					}
				]
			},
		],
		buttonsTemplate: "<button class='btn-success' type='submit'>Сохранить</button> <button class='btn-danger' id='employeeCloseInsertWindow' type='button' class=''>Отмена</button>"
	});
	$("#employeeInsertForm").validate({
		rules: {
			FullName: {
				required: true,
				minlength: 10,
				lettersonly: true
			},
			PersonalNumber: {
				required: true,
				pattern: "^[0-9]{3}-[0-9]{3}-[0-9]{3}-[0-9]{3}$"
			},
			BirthdayDate: {
				required: true,
				dateFilter: true,
				pastDate: true
			},
			DateOfBegin: {
				required: true,
				dateFilter: true,
				pastDate: true
			},
			Phone: {
				required: true,
				pattern: "^[0-9]-[(]?[0-9]{3}[)]?-[0-9]{3}-[0-9]{2}-[0-9]{2}$"
			}
		},
		messages: {
			FullName: {
				required: "Необходимо указать полное имя сотрудника!",
				minlength: "Длина имени должна быть больше 10 символов!",
				lettersonly: "Имя сотрудника должно включать в себя только буквы кириллицы!"
			},
			PersonalNumber: {
				required: "Необходимо указать ИИН!",
				pattern: "Укажите ИИН в формате 'XXX-XXX-XXX-XXX'"
			},
			BirthdayDate: {
				required: "Необходимо указать дату!",
				dateFilter: "Укажите дату в формате ДД/ММ/ГГГГ!",
				pastDate: "Дата не может быть больше действующей!"
			},
			DateOfBegin: {
				required: "Необходимо указать дату!",
				dateFilter: "Укажите дату в формате ДД/ММ/ГГГГ!",
				pastDate: "Дата не может быть больше действующей!"
			},
			Phone: {
				required: true,
				pattern: "^[0-9]-[(]?[0-9]{3}[)]?-[0-9]{3}-[0-9]{2}-[0-9]{2}$"
			}
		},
		focusInvalid: true,
		errorClass: "validationFormMessage",
		errorLabelContainer: ".validateContainer",
		submitHandler: function (e) {
			var comboBoxesIsValid = ComboBoxsValidation(["empInsertCompanyId"]);
			if (comboBoxesIsValid) {
				let data = {
					DateOfBegin: $("#empInsertDateOfBegin").data("kendoDatePicker").value(),
					Company: companies.find(x => x.Id == $("#empInsertCompanyId").val()),
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
							$('#employeeInsertForm')[0].reset();
							$("#employeeInsertWindow").data("kendoDialog").close();
						}
						else
							return notification.error(json.Error);
					}
				});
			}
			else {
				notification.error("Необходимо указать услугу и компанию!");
			}
		}
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

	$("#editEmployeeForm").kendoForm({
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
						field: "FullName", label: "Полное имя", placeholder: "Укажите полное имя: ",
						editor: function (container, options) {
							var input = $('<input id="empEditFullName" name="FullName" />');
							input.appendTo(container);
							input.kendoTextBox();
						}
					},
					{
						field: "Phone", label: "Телефон", editor: function (container, options) {
							var input = $('<input id="empEditPhone" name="Phone" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								placeholder: "Укажите номер телефона",
								mask: "0-(000)-000-00-00"
							});
						}
					},
					{
						field: "PersonalNumber", label: "ИИН",
						editor: function (container, options) {
							var input = $('<input id="empEditPesonalNumber" name="PersonalNumber" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								placeholder: "Укажите 12 чисел",
								mask: "000-000-000-000"
							});
						}
					},
					{
						field: "BirthdayDate", label: "Дата рождения",
						editor: function (container, options) {
							var input = $('<input id="empEditBirthdayDate" name="BirthdayDate" />');
							input.appendTo(container);
							input.kendoDatePicker({
								placeholder: "Укажите дату рождения",
								format: 'dd/MM/yyyy',
							});
						}
					},
					{
						field: "CompanyId", label: "Компания",
						editor: function (container, options) {
							var input = $('<input id="empEditCompanyId" name="CompanyId" />');
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
						field: "DateOfBegin", label: "Дата начала работы",
						editor: function (container, options) {
							var input = $('<input id="empEditDateOfBegin" name="DateOfBegin" />');
							input.appendTo(container);
							input.kendoDatePicker({
								placeholder: "Укажите дату начала работы",
								format: 'dd/MM/yyyy',
							});
						}
					},
					{
						field: "Validator", label: "",
						editor: function (container, options) {
							var block = $('<div class="validateContainer"/>');
							block.appendTo(container);
						}
					}
				]
			}
		],
		buttonsTemplate: "<button class='btn-success' type='submit'>Сохранить</button> <button class='btn-danger' id='employeeCloseEditWindow' type='button' class=''>Отмена</button>"
	});

	$("#editEmployeeForm").validate({
		rules: {
			FullName: {
				required: true,
				minlength: 10,
				lettersonly: true
			},
			PersonalNumber: {
				required: true,
				pattern: "^[0-9]{3}-[0-9]{3}-[0-9]{3}-[0-9]{3}$"
			},
			BirthdayDate: {
				required: true,
				dateFilter: true,
				pastDate: true
			},
			DateOfBegin: {
				required: true,
				dateFilter: true,
				pastDate: true
			},
			Phone: {
				required: true,
				pattern: "^[0-9]-[(]?[0-9]{3}[)]?-[0-9]{3}-[0-9]{2}-[0-9]{2}$"
			}
		},
		messages: {
			FullName: {
				required: "Необходимо указать полное имя сотрудника!",
				minlength: "Длина имени должна быть больше 10 символов!",
				lettersonly: "Имя сотрудника должно включать в себя только буквы кириллицы!"
			},
			PersonalNumber: {
				required: "Необходимо указать ИИН!",
				pattern: "Укажите ИИН в формате 'XXX-XXX-XXX-XXX'"
			},
			BirthdayDate: {
				required: "Необходимо указать дату!",
				dateFilter: "Укажите дату в формате ДД/ММ/ГГГГ!",
				pastDate: "Дата не может быть больше действующей!"
			},
			DateOfBegin: {
				required: "Необходимо указать дату!",
				dateFilter: "Укажите дату в формате ДД/ММ/ГГГГ!",
				pastDate: "Дата не может быть больше действующей!"
			},
			Phone: {
				required: true,
				pattern: "^[0-9]-[(]?[0-9]{3}[)]?-[0-9]{3}-[0-9]{2}-[0-9]{2}$"
			}
		},
		focusInvalid: true,
		errorClass: "validationFormMessage",
		errorLabelContainer: ".validateContainer",
		submitHandler: function (e) {
			var comboBoxesIsValid = ComboBoxsValidation(["empEditCompanyId"]);
			if (comboBoxesIsValid) {

				let companyId = $("#empEditCompanyId").val();
				let data = {
					Id: $("#empEditId").val(),
					DateOfBegin: new Date($("#empEditDateOfBegin").data("kendoDatePicker").value()).toLocaleDateString(),
					Company: companies.find(x => x.Id == companyId),
					BirthdayDate: new Date($("#empEditBirthdayDate").data("kendoDatePicker").value()).toLocaleDateString(),
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
							$("#editEmployeeWindow").data("kendoDialog").close();
						}
						else {
							notification.error(json.Error);
						}
					}
				});
			}
			else {
				notification.error("Необходимо указать услугу и компанию!");
			}
		}
	});

	function EditEmployee(oldService) {
		var filteredBirthdayDate = new Date(oldService.BirthdayDate).toLocaleDateString();
		var filteredDateOfBegin = new Date(oldService.DateOfBegin).toLocaleDateString();
		var filteredPersonalNumber = EditBIN(oldService.PersonalNumber);
		var filteredPhone = EditNumber(oldService.Phone);

		$("#editEmployeeForm #empEditId").val(oldService.Id);
		$("#editEmployeeForm #empEditFullName").val(oldService.FullName);
		$("#editEmployeeForm #empEditPhone").val(filteredPhone);
		$("#editEmployeeForm #empEditPesonalNumber").val(filteredPersonalNumber);
		$("#editEmployeeForm #empEditBirthdayDate").val(filteredBirthdayDate);
		$("#editEmployeeForm #empEditCompanyId").data("kendoComboBox").value(oldService.Company.Id);
		$("#editEmployeeForm #empEditDateOfBegin").val(filteredDateOfBegin);

		$("#editEmployeeWindow").data("kendoDialog").open();

		return false;
	}

	function ActivateEmployee(id) {
		$.ajax({
			url: "/Employee/ActivateEmployee/",
			type: "POST",
			data: { id: id },
			success: function (json) {
				if (json.IsSuccess == true) {
					var grid = $("#emloyeeGrid").data("kendoGrid");
					grid.dataSource.read();
					notification.success(json.Message);
				}
				else {
					notification.error(json.Message);
				}
			}
		})
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

	$("body").on("click", "#employeeSearchButton", function () {
		var grid = $("#emloyeeGrid").data("kendoGrid");
		grid.dataSource.read();
	});
	$("#employeeToolbar").kendoToolBar({});

	$("#fullNameFilter").kendoTextBox({});

	$("#statusesList").kendoDropDownList({
		dataTextField: "text",
		dataValueField: "value",
		dataSource: [
			{ text: "Активные", value: 1 },
			{ text: "Отключенные", value: 2 },
		]
	});

	$("#companyFilter").kendoComboBox({
		placeholder: "Введите название",
		dataTextField: "Name",
		dataValueField: "Id",
		filter: "contains",
		dataSource: companies,
	});

	$("#employeeSortingTypes").kendoDropDownList({
		dataTextField: "text",
		dataValueField: "value",
		dataSource: [
			{ text: "Имени", value: "FullName" },
			{ text: "Дата рождения", value: "BirthdayDate" }
		],
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
			{ field: "PersonalNumber", title: "ИИН", width: "10%" },
			{ field: "Phone", title: "Номер", width: "10%" },
			{ field: "Company.Name", title: "Название компании", width: "10%" },
			{ field: "BirthdayDate", title: "Дата рождения", width: "15%", format: "{0: dd-MM-yyyy}" },
			{ field: "DateOfBegin", title: "Дата начала работы", width: "15%", format: "{0: dd-MM-yyyy}" },
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
						name: "Activate",
						className: "btn-activate",
						text: "Востановить",
						visible: function (dataItem) { return dataItem.Status == 2 },
						click: function (e) {
							var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
							ActivateEmployee(dataItem.Id);
						}
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
		height: 620,
		pageable: {
			messages: {
				display: '<button type="button" id="employeeSearchButton">Добавить</button>'
			}
		},
		scrollable: true,
	});
});