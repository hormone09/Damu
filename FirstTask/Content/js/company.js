$(document).ready(function () {

	var notifElement = $("#companyNotifications");
	notifElement.kendoNotification({
	});

	var notification = notifElement.data("kendoNotification");

	// Insert
	$("#companiesInsertWindow").kendoDialog({
		modal: true,
		width: "400px",
		closable: true,
		visible: false,
		title: false,
	});

	$("body").on("click", "#btnCompanyInsert", function () {
		let window = $("#companiesInsertWindow").data("kendoDialog")
		window.open();
	});

	var formInsert = $("#companiesInsertForm").kendoForm({
		visible: false,
		items: [
			{
				type: "group",
				label: "Добавление новой компании",
				items: [
					{ field: "Name", label: "Назваине", validation: { required: true } },
					{ field: "DateOfBegin", label: "Дата начала работы", editor: "DatePicker", validation: { required: true } },
					{
						field: "BIN", label: "BIN", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input name="BIN" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								mask: "000-000-000-000"
							});
						}
					},
					{
						field: "Phone", label: "Телефон", validation: { required: true }, editor: function (container, options) {
							var input = $('<input name="Phone" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								mask: "0-(000)-000-00-00"
							});
						}
					}
				]
			}
		]
	});

	formInsert.bind("submit", function (e) {
		var data = formInsert.serializeArray();
		$.ajax({
			url: "/Company/AddCompany/",
			type: "GET",
			data: data,
			success: function (json) {
				if (json.IsSuccess == true)
					notification.success(json.Message);
				else
					notification.error(json.Error);
			}
		});

		$("#companiesInsertWindow").data("kendoDialog").close();

		return false;
	});

	// Edit
	$("#companyEditWindow").kendoDialog({
		modal: true,
		width: "500px",
		closable: true,
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
						field: "Id", editor: function (container, options) {
							var input = $('<input id="editId" name="Id" type="hidden"/>');
							input.appendTo(container);
						}
					},
					{
						field: "Name", label: "Назваине", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editName" name="Name" required="required" />');
							input.appendTo(container);
							input.kendoTextBox();
						}
					},
					{
						field: "BIN", label: "BIN", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editBin" name="BIN" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								mask: "000-000-000-000"
							});
						}
					},
					{
						field: "DateOfBegin", label: "Дата начала работы", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editDate" name="DateOfBegin" required="required" />');
							input.appendTo(container);
							input.kendoDatePicker();
						}
					},
					{
						field: "Phone", label: "Телефон", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="editPhone" name="Phone" required="required" />');
							input.appendTo(container);
							input.kendoMaskedTextBox({
								mask: "0-(000)-000-00-00"
							});
						}
					}
				]
			}
		]
	});

	formEdit.bind("submit", function (e) {
		var data = formEdit.serializeArray();

		$.ajax({
			url: "/Company/EditCompany/",
			type: "GET",
			data: data,
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
		});

		$("#companyEditWindow").data("kendoDialog").close();

		return false;
	});

	function EditCompany(oldService) {
		$("#companyEditForm #editId").val(oldService.Id);
		$("#companyEditForm #editName").val(oldService.Name);
		$("#companyEditForm #editPhone").val(oldService.Phone);

		$("#companyEditWindow").data("kendoDialog").open();

		return false;
	}

	// Filters
	$("#companiesToolbar").kendoToolBar({
	});

	$("#nameFilter").kendoTextBox({
		change: function () {
			let grid = $("#companiesGrid").data("kendoGrid");
			grid.dataSource.page(1);
			grid.dataSource.read();
		}
	});

	$("#statusesListCompanies").kendoDropDownList({
		dataTextField: "text",
		dataValueField: "value",
		dataSource: [
			{ text: "Активные", value: 1 },
			{ text: "Отключенные", value: 2 },
		],
		select: function () {
			let grid = $("#companiesGrid").data("kendoGrid");
			grid.dataSource.page(1);
			setTimeout(() => grid.dataSource.read(), 1000);
		}
	});

	$("#companiesSortingTypes").kendoDropDownList({
		dataTextField: "text",
		dataValueField: "value",
		dataSource: [
			{ text: "Названию", value: "Name" }
		],
		select: function () {
			let grid = $("#grid").data("kendoGrid");
			grid.dataSource.page(1);
			setTimeout(() => grid.dataSource.read(), 1000);
		}
	});

	// Delete
		document.getElementById('companiesDeleteWindow').style.display = "none";
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

	$("#companiesGrid").kendoGrid({
		dataSource: dataSource,
		columns: [
			{ field: "Id", title: "Id", width: "5%", hidden: true },
			{ field: "Name", title: "Название компании", width: "20%" },
			{ field: "Phone", title: "Номер телефона", width: "20%" },
			{ field: "Status", title: "Статус", width: "10%" },
			{ field: "DateOfBegin", title: "Дата добавления", width: "20%", format: "{0: dd-MM-yyyy}" },
			{
				command: [{ 
					name: "Delete",
					className: "btn-destroy",
					text: "Удаление",
					click: function (e) {
						var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
						DeleteCompany(dataItem.Id);
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
						EditCompany(dataItem);
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