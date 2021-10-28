﻿
$(document).ready(function () {
	
	var notifElement = $("#allNotif");
	notifElement.kendoNotification({
	});
	var notification = notifElement.data("kendoNotification");

	// Insert
	$("body").on("click", "#btn-insert", function () {
		let window = $("#servicesInsertWindow").data("kendoDialog");
		window.open();
	});

	$("body").on("click", "#serviceCloseInsertWindow", function () {
		$("#servicesInsertWindow").data("kendoDialog").close();
	});

	$("body").on("click", "#serviceCloseEditWindow", function () {
		$("#edit-window").data("kendoDialog").close();
	});

	$("#servicesInsertWindow").kendoDialog({
		modal: true,
		width: "400px",
		closable: true,
		visible: false,
		title: false,
	});

	var formInsert = $("#servicesInsertForm").kendoForm({
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
						field: "Price", label: "Стоимость", placeholder: "Укажите стоимость", validation: { required: true },
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
								format: 'dd/MM/yyyy',
							});
						}
					},
					{
						field: "Code", label: "Код услуги", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editCode" name="Code" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								mask: "L00.000.000"
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
		buttonsTemplate: "<button class='btn-success' type='submit'>Сохранить</button> <button class='btn-danger' id='serviceCloseInsertWindow' type='button'>Отмена</button>"
	});

	$("#servicesInsertForm").validate({
		rules: {
			Name: {
				required: true,
				minlength: 6,
			},
			Price: {
				required: true,
				maxlength: 20
			},
			DateOfBegin: {
				required: true,
				dateFilter: true,
				pastDate: true
			},
			Code: {
				required: true,
				pattern: "^[A-z]{1}[0-9]{2}.[0-9]{3}.[0-9]{3}$"
			}
		},
		messages: {
			Name: {
				required: "Необходимо указать название услуги!",
				minlength: "Название услуги должно содержать минимум 6 символов!",
			},
			Price: {
				required: "Необходимо указать стоимость!",
				maxlength: "Стоимость услуги должна ограничеваться 20-ю символами!"
			},
			DateOfBegin: {
				required: "Необходимо указать дату!",
				dateFilter: "Укажите дату в формате!",
				pastDate: "Дата не может быть больше действующей!"
			},
			Code: {
				required: "Необходимо указать код услуги!",
				pattern: "Укажите код услуги в формате Z11.111.111"
			}
		},
		focusInvalid: true,
		errorClass: "validationFormMessage",
		errorLabelContainer: ".validateContainer",
		submitHandler: function () {
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
						$('#servicesInsertForm')[0].reset();
						$("#servicesInsertWindow").data("kendoDialog").close();
					}
					else
						return notification.error(json.Error);
				}
			});
		}
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
							var input = $('<input id="editServicesId" name="Id" type="hidden"/>');
							input.appendTo(container);
						}
					},
					{
						field: "Name", label: "Название",
						editor: function (container, options) {
							var input = $('<input id="editServicesName" name="Name"/>');
							input.appendTo(container);
							input.kendoTextBox({
								placeholder: "Укажите наименование услуги",
							});
						}
					},
					{
						field: "Price", label: "Стоимость", placeholder: "Укажите стоимость",
						editor: function (container, options) {
							var input = $('<input id="editServicesPrice" name="Price" />');
							input.appendTo(container);
							input.kendoNumericTextBox({
								placeholder: "Введите стоимость ",
								culture: "de-DE"
							});
						}
					},
					{
						field: "DateOfBegin", label: "Дата начала работы",
						editor: function (container, options) {
							var input = $('<input id="editServicesDateOfBegin" name="DateOfBegin" />');
							input.appendTo(container);
							input.kendoDatePicker({
								placeholder: "Введите точную дату",
								format: 'dd/MM/yyyy',
							});
						}
					},
					{
						field: "Code", label: "Код услуги",
						editor: function (container, options) {
							var input = $('<input id="editServicesCode" name="Code" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								placeholder: "Укажите код в формате 'Z00.000.000'",
								mask: "L00.000.000"
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
		buttonsTemplate: "<button class='btn-success' type='submit'>Сохранить</button> <button class='btn-danger' id='serviceCloseEditWindow' type='button'>Отмена</button>"
	});

	// Edit Validation and Submit

	$("#editServiceForm").validate({
		rules: {
			Name: {
				required: true,
				minlength: 6,
			},
			Price: {
				required: true,
				maxlength: 20
			},
			DateOfBegin: {
				required: true,
				dateFilter: true,
				pastDate: true
			},
			Code: {
				required: true,
				pattern: "^[A-z]{1}[0-9]{2}.[0-9]{3}.[0-9]{3}$"
			}
		},
		messages: {
			Name: {
				required: "Необходимо указать название услуги!",
				minlength: "Название услуги должно содержать минимум 6 символов!",
			},
			Price: {
				required: "Необходимо указать стоимость!",
				maxlength: "Стоимость услуги должна ограничеваться 20-ю символами!"
			},
			DateOfBegin: {
				required: "Необходимо указать дату!",
				dateFilter: "Укажите дату в формате!",
				pastDate: "Дата не может быть больше действующей!"
			},
			Code: {
				required: "Необходимо указать код услуги!",
				pattern: "Укажите код услуги в формате Z11.111.111"
			}
		},
		debug: true,
		focusInvalid: true,
		errorClass: "validationFormMessage",
		errorLabelContainer: ".validateContainer",
		submitHandler: function () {
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
						$("#edit-window").data("kendoDialog").close();
					}
					else {
						notification.error(json.Error);
					}
				}
			});
		}
	});

	function EditService(oldService) {
		$("#editServiceForm #editServicesId").val(oldService.Id);
		$("#editServiceForm #editServicesName").val(oldService.Name);
		$("#editServiceForm #editServicesPrice").data("kendoNumericTextBox").value(oldService.Price);
		$("#editServiceForm #editServicesDateOfBegin").val(new Date().toLocaleDateString());
		$("#editServiceForm #editServicesCode").val(oldService.Code);

		$("#edit-window").data("kendoDialog").open();

		return false;
	}

	function ActivateService(id) {
		$.ajax({
			url: "/Service/ActivateService/",
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

	$("body").on("click", "#btn-search", function () {
		let grid = $("#servicesGrid").data("kendoGrid");
		grid.dataSource.read();
	});

	$("#toolbar").kendoToolBar({
	});
	$("#serviceNameFilter").kendoTextBox({
	});

	$("#serviceStatusesList").kendoDropDownList({
		dataTextField: "text",
		dataValueField: "value",
		dataSource: [
			{ text: "Активные", value: 1 },
			{ text: "Отключенные", value: 2 },
		]
	});

	$("#orderByTypes").kendoDropDownList({
		dataTextField: "text",
		dataValueField: "value",
		dataSource: [
			{ text: "Названию", value: "Name" },
			{ text: "Возрастанию цены", value: "Price" },
			{ text: "Убыванию цены", value: "Price DESC" }
		]
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
					Status: $("#serviceStatusesList").val(),
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
						name: "Activate",
						className: "btn-activate",
						text: "Востановить",
						visible: function (dataItem) { return dataItem.Status == 2 },
						click: function (e) {
							var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
							ActivateService(dataItem.Id);
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
		height: 620,
		pageable: {
			messages: {
				display: '<button type="button" id="btn-insert">Добавить</button>'
			}
		},
		scrollable: true,
	});
});