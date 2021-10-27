﻿$(document).ready(function () {

	var notifElement = $("#companyNotifications");
	notifElement.kendoNotification({
	});

	var notification = notifElement.data("kendoNotification");

	// Insert

	$("body").on("click", "#companiesCloseInsertWindow", function () {
		$("#companiesInsertWindow").data("kendoDialog").close();
	});

	$("body").on("click", "#companiesCloseEditWindow", function () {
		$("#companyEditWindow").data("kendoDialog").close();
	});

	$("#companiesInsertWindow").kendoDialog({
		modal: true,
		width: "700px",
		closable: false,
		visible: false,
		title: false,
	});

	$("body").on("click", "#btnCompanyInsert", function () {
		let window = $("#companiesInsertWindow").data("kendoDialog")
		window.open();
	});

	var formInsert = $("#companiesInsertForm").kendoForm({
		visible: false,
		validatable: { validationSummary: false },
		items: [
			{
				type: "group",
				label: "Добавление новой организации",
				items: [
					{ field: "Name", placeholder: "Укажите название", label: "Название", validation: { required: true } },
					{
						field: "DateOfBegin", label: "Дата начала сотрудничества", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="insertDate" name="DateOfBegin" required="required" />');
							input.appendTo(container);
							input.kendoDatePicker({
								placeholder: "Укажите дату начала сотрудничества",
								format: 'dd/MM/yy'
							});
						}
					},
					{
						field: "BIN", label: "BIN", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input name="BIN" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								placeholder: "Укажите 12 чисел",
								mask: "000-000-000-000"
							});
						}
					},
					{
						field: "Phone", label: "Телефон", validation: { required: true }, editor: function (container, options) {
							var input = $('<input name="Phone" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								placeholder: "Укажите номер телефона",
								mask: "0-(000)-000-00-00"
							});
						}
					}
				]
			}
		],
		buttonsTemplate: "<div class='form-buttons'><button class='btn-success' type='submit'>Сохранить</button> <button class='btn-danger' id='companiesCloseInsertWindow' type='button'>Отмена</button></div>"
	});

	formInsert.bind("submit", function (e) {
		var data = formInsert.serializeArray();
		$.ajax({
			url: "/Company/AddCompany/",
			type: "POST",
			data: data,
			success: function (json) {
				if (json.IsSuccess == true) {
					var grid = $("#companiesGrid").data("kendoGrid");
					grid.dataSource.read();
					notification.success(json.Message);
					$('#companiesInsertForm')[0].reset();
					$("#companiesInsertWindow").data("kendoDialog").close();
				}
				else {
					notification.error(json.Error);
				}
			}
		});


		return false;
	});

	// Edit

	$("#companyEditWindow").kendoDialog({
		modal: true,
		width: "500px",
		closable: false,
		visible: false,
		title: false,
	});

	var formEdit = $("#companyEditForm").kendoForm({
		visible: true,
		items: [
			{
				type: "group",
				label: "Редактирование данных компании",
				items: [
					{
						field: "Id", label: "", editor: function (container, options) {
							var input = $('<input id="editCompaniesId" name="Id" type="hidden"/>');
							input.appendTo(container);
						}
					},
					{
						field: "Name", label: "Назваине", placeholder: "Укажите название",
						editor: function (container, options) {
							var input = $('<input id="editCompaniesName" name="Name"/>');
							input.appendTo(container);
							input.kendoTextBox();
						}
					},
					{
						field: "BIN", label: "БИН",
						editor: function (container, options) {
							var input = $('<input id="editCompaniesBin" name="BIN"/>');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								placeholder: "Укажите 12 чисел",
								mask: "000-000-000-000"
							});
						}
					},
					{
						field: "DateOfBegin", label: "Дата начала сотрудничества",
						editor: function (container, options) {
							var input = $('<input id="editCompaniesDateOfBegin" name="DateOfBegin"/>');
							input.appendTo(container);
							input.kendoDatePicker({
								placeholder: "Укажите дату начала сотрудничества",
								format: 'dd/MM/yy',
							});
						}
					},
					{
						field: "Phone", label: "Телефон",
						editor: function (container, options) {
							var input = $('<input id="editCompaniesPhone" name="Phone"/>');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								placeholder: "Укажите номер телефона",
								mask: "0-(000)-000-00-00"
							});
						}
					},
					{
						field: "Validator", label: "",
						editor: function (container, options) {
							var block = $('<div class="validateContainer"/>');
							block.appendTo(container);
						}
					},
				]
			}
		],
		buttonsTemplate: "<button class='btn-success' type='submit'>Сохранить</button> <button class='btn-danger' id='companiesCloseEditWindow' type='button' class=''>Отмена</button>"
	});

	function EditCompany(oldService) {
		var filteredBIN = EditBIN(oldService.BIN);
		var filteredDate = new Date(oldService.DateOfBegin).toLocaleDateString();
		var filteredPhone = EditNumber(oldService.Phone);

		$("#companyEditForm #editCompaniesId").val(oldService.Id);
		$("#companyEditForm #editCompaniesName").val(oldService.Name);
		$("#companyEditForm #editCompaniesBin").val(filteredBIN);
		$("#companyEditForm #editCompaniesDateOfBegin").val(filteredDate);
		$("#companyEditForm #editCompaniesPhone").val(filteredPhone);

		$("#companyEditWindow").data("kendoDialog").open();

		return false;
	}

	function ActivateCompany(id) {
		$.ajax({
			url: "/Company/ActivateCompany/",
			type: "POST",
			data: { id: id },
			success: function (json) {
				if (json.IsSuccess == true) {
					var grid = $("#companiesGrid").data("kendoGrid");
					grid.dataSource.read();
					notification.success(json.Message);
				}
				else {
					notification.error(json.Message);
				}
			}
		})
	}

	// Validator

	$("#companyEditForm").validate({
		rules: {
			Name: {
				required: true,
				minlength: 6
			},
			BIN: {
				required: true,
			}
		},
		messages: {
			Name: {
				required: "Необходимо указать название компании!",
				minlength: "Название компании должно содержать минимум 6 символов!"
			},
			BIN: {
				required: "Необходимо указать BIN!"
			}
		},
		focusInvalid: true,
		errorClass: "validationFormMessage",
		errorLabelContainer: ".validateContainer",
		submitHandler: function () {
			var data = formEdit.serializeArray();

			$.ajax({
				url: "/Company/EditCompany/",
				type: "POST",
				data: data,
				success: function (json) {
					if (json.IsSuccess == true) {
						var grid = $("#companiesGrid").data("kendoGrid");
						grid.dataSource.read();
						notification.success(json.Message);
						$("#companyEditWindow").data("kendoDialog").close();
					}
					else {
						notification.error(json.Error);
					}
				}
			});
		}
	});

	// Filters

	$("body").on("click", "#btnCompanySearch", function () {
		var grid = $("#companiesGrid").data("kendoGrid");dit
		grid.dataSource.read();
	});

	$("#companiesToolbar").kendoToolBar({
	});

	$("#nameFilter").kendoTextBox({});

	$("#statusesListCompanies").kendoDropDownList({
		dataTextField: "text",
		dataValueField: "value",
		dataSource: [
			{ text: "Активные", value: 1 },
			{ text: "Отключенные", value: 2 },
		]
	});

	$("#companiesSortingTypes").kendoDropDownList({
		dataTextField: "text",
		dataValueField: "value",
		dataSource: [
			{ text: "Названию", value: "Name" },
			{ text: "Дате начала", value: "DateOfBegin" }
		]
	});

	// Delete
	function DeleteCompany(id) {
		document.getElementById('companiesDeleteWindow').style.display = "block";
		$("#companiesDeleteWindow").kendoDialog({
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
							url: "/Company/DeleteCompany/",
							type: "POST",
							data: { id: id },
							success: function (json) {
								if (json.IsSuccess == true) {
									var grid = $("#companiesGrid").data("kendoGrid");
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
	// Grid

	var dataSource = new kendo.data.DataSource({
		pageSize: 20,
		transport: {
			read: {
				url: "/Company/Index",
				type: "POST",
				contentType: "application/json; charset=utf-8",
			},
			parameterMap: function (options) {
				let name = $("#nameFilter").val();
				var data = {
					CompanyName: name,
					Page: options.page,
					PageSize: options.pageSize,
					Status: $("#statusesListCompanies").val(),
					SortingType: $("#companiesSortingTypes").val()
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
					DateOfBegin: { type: "date" }
				}
			}
		},
		serverPaging: true,
		serverSorting: true,
	});

	$("#companiesGrid").kendoGrid({
		dataSource: dataSource,
		columns: [
			{ field: "Id", title: "Id", width: "5%", hidden: true },
			{ field: "Name", title: "Название компании", width: "30%" },
			{ field: "Phone", title: "Номер телефона", width: "10%" },
			{ field: "BIN", title: "БИН", width: "10%" },
			{ field: "DateOfBegin", title: "Дата добавления", width: "10%", format: "{0: dd-MM-yyyy}" },
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
							DeleteCompany(dataItem.Id);
						},
					},
					{
						name: "Activate",
						className: "btn-activate",
						text: "Востановить",
						visible: function (dataItem) { return dataItem.Status == 2 },
						click: function (e) {
							var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
							ActivateCompany(dataItem.Id);
						}
					},
					{
						name: "Edit",
						className: "btn-edit",
						text: "Редактирование",
						click: function (e) {
							var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
							EditCompany(dataItem);
						},
					},
				],
				width: "30%",
			},
		],
		height: 620,
		pageable: true,
		scrollable: true,
	});
});