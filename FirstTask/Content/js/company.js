$(document).ready(function () {

	var notifElement = $("#allNotif");
	notifElement.kendoNotification({
	});
	var notification = notifElement.data("kendoNotification");

	$("#toolbar1").kendoToolBar({
	});
	$("#nameFilter1").kendoTextBox({
		change: function () {
			let grid = $("#grid").data("kendoGrid");
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

	var dataSource = new kendo.data.DataSource({
		pageSize: 20,
		transport: {
			read: {
				url: "/Company/Index",
				type: "POST",
				contentType: "application/json; charset=utf-8",
			},
			parameterMap: function (options) {
				let name = $("#nameFilter1").val();
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
			{ field: "Status", title: "Статус", width: "10%" },
			{ field: "DateOfBegin", title: "Дата добавления", width: "20%", format: "{0: dd-MM-yyyy}" },
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