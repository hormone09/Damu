﻿
$(document).ready(function () {

	var notifElement = $("#schedulerNotifications");
	notifElement.kendoNotification();
	var notification = notifElement.data("kendoNotification")

	//REPORT

	$("#reportWindow").kendoDialog({
		modal: true,
		width: "500px",
		closable: false,
		visible: false,
		title: false,
	});

	var reportWindow = $("#reportWindow").data("kendoDialog");
	
	$("body").on("click", "#reportWindowButton", function () {
		reportWindow.open();
	});

	$("body").on("click", "#reportCloseWindow", function () {
		reportWindow.close();
	});

	var formReport = $("#reportForm").kendoForm({
		visible: true,
		items: [
			{
				type: "group",
				label: "Получение отчета оказанных услуг",
				items: [
					{
						field: "CompanyId", label: "Компания", validation: { required: false },
						editor: function (container, options) {
							var input = $('<input id="reportCompanyId" name="CompanyId"/>');
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
						field: "ServiceId", label: "Услуга", validation: { required: false },
						editor: function (container, options) {
							var input = $('<input id="reportServiceId" name="ServiceId"/>');
							input.appendTo(container);
							input.kendoComboBox({
								placeholder: "Введите название",
								dataTextField: "Name",
								dataValueField: "Id",
								filter: "contains",
								dataSource: services,
								optionLabel: "Все",
							});
						}
					},
					{
						field: "EmployeeId", label: "Работник", validation: { required: false },
						editor: function (container, options) {
							var input = $('<input id="reportEmployeeId" name="EmployeeId"/>');
							input.appendTo(container);
							input.kendoComboBox({
								placeholder: "Введите имя",
								dataTextField: "FullName",
								dataValueField: "Id",
								filter: "contains",
								dataSource: employee,
								optionLabel: "Все",
							});
						}
					},
					{
						field: "Date", label: "Отчет в период с", validation: { required: true },
						editor: function (container, options) {
							var flexContainer = $('<div style="display: flex; font-weight: bold; justify-content: space-around; margin: 10px 0px;"/></div>');
							var input1 = $('<input id="reportDateBegin" name="DateBegin"/>');
							var input2 = $('<input id="reportDateEnd" name="DateEnd"/>');
							var p1 = $('<p>с </p>');
							var p2 = $('<p>по </p>');
							input1.appendTo(flexContainer);
							input1.kendoDatePicker({
								placeholder: "Укажите точную дату",
								format: 'MM/dd/yy',
							});
							p2.appendTo(flexContainer);
							input2.appendTo(flexContainer);
							input2.kendoDatePicker({
								placeholder: "Укажите точную дату",
								format: 'MM/dd/yy',
							});
							flexContainer.appendTo(container);
						}
					},
					{
						field: "Type", label: "Тип конечного файла", validation: { required: false },
						editor: function (container, options) {
							var input = $('<input id="reportReportType" name="Type"/>');
							input.appendTo(container);
							input.kendoDropDownList({
								dataTextField: "text",
								dataValueField: "value",
								dataSource: [
									{ text: "Exel", value: 1 },
									{ text: "Word", value: 2 },
									{ text: "PDF", value: 3 },
									{ text: "XML", value: 4 },
									{ text: "PNG", value: 5 },
								]
							})
						}
					},
				]
			}
		],
		buttonsTemplate: "<button class='btn-primary' type='submit'>Получить</button> <button class='btn-default' id='reportCloseWindow' type='button'>Закрыть</button>"
	});

	formReport.bind("submit", function (e) {
		e.preventDefault();
		let url = window.location.href;

		var date1 = $("#reportDateBegin").val();
		var date2 = $("#reportDateEnd").val();
		var companyId = $("#reportCompanyId").val();
		var serviceId = $("#reportServiceId").val();
		var employeeId = $("#reportEmployeeId").val();
		var type = $("#reportReportType").val();

		window.open("GetReport/?DateBegin=" + date1 + "&DateEnd=" + date2 + "&CompanyId=" + companyId + "&ServiceId=" + serviceId + "&EmployeeId=" + employeeId + "&Type=" + type);

		$("#reportWindow").data("kendoDialog").close();
	});

	// SCHEDULER
	var dataSource = new kendo.data.SchedulerDataSource({
		sync: function () {
			this.read();
		},
		transport: {
			read: {
				url: "/ServicesHistory/Index",
				type: "POST",
				contentType: "application/json; charset=utf-8",
			},
			create: {
				url: "/ServicesHistory/Create",
				type: "POST",
				contentType: "application/json; charset=utf-8",
			},
			update: {
				url: "/ServicesHistory/Update",
				type: "POST",
				contentType: "application/json; charset=utf-8",
			},
			destroy: {
				url: "/ServicesHistory/Delete",
				type: "POST",
				contentType: "application/json; charset=utf-8",
			},
			parameterMap: function (options, type) {
				let scheduler = $("#scheduler").data("kendoScheduler");
				let timeZone = scheduler._model.formattedShortDate.split('- ').join('');
				let timeZoneArray = timeZone.split(' ');
				let dateBegin = new Date(timeZoneArray[0]).toLocaleDateString();
				let dateEnd = new Date(timeZoneArray[1]).toLocaleDateString();
				if (type == "read") {
					let json = {
						DateBegin: dateBegin,
						DateEnd: dateEnd
					};

					return kendo.stringify(json);
				}
				else if (type == "create") {
					let json = {
						DateOfCreate: $("#schedulerDateOfCreate").data("kendoDateTimePicker").value(),
						Employee: { Id: $("#schedulerEmployee").val() },
						Company: { Id: $("#schedulerCompany").val() },
						Service: { Id: $("#schedulerService").val() }
					};
					return kendo.stringify(json);
				}
				else if (type == "update") {
					let json = {
						Id: options.Id,
						DateOfCreate: $("#schedulerDateOfCreate").data("kendoDateTimePicker").value(),
						Employee: { Id: $("#schedulerEmployee").val() },
						Company: { Id: $("#schedulerCompany").val() },
						Service: { Id: $("#schedulerService").val() }
					}
					return kendo.stringify(json);
				}
				else if (type == "destroy") {
					return kendo.stringify({ id: options.Id });
				}
			}
		},
		requestEnd: function (e) {
			if (e.type == "update" || e.type == "create") {
				$("#scheduler").data("kendoScheduler").dataSource.read();
			}

			if (e.type == "update" || e.type == "create" || e.type == "destroy") {
				let result = e.response;
				if (result.IsSuccess) {
					notification.success(result.Message);
				}
				else {
					notification.error(result.Error);
				}
			}
		},
		batch: false,
		schema: { 
			model: {
				id: "Id",
				fields: {
					title: { from: "Title" },
					start: { type: "date", from: "DateOfCreate" },
					end: { type: "date", from: "DateOfFinish" }
				}
			}
		}
	});
	$("#scheduler").kendoScheduler({
		dataSource: dataSource,
		startTime: new Date("2021/01/01 07:00"),
		endTime: new Date("2021/12/01 18:00"),
		showWorkHours: "none",
		majorTimeHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'HH:mm')#</strong><sup></sup>"),
		height: "600px",
		databound: "",
		views: [
			{
				type: "workWeek",
				title: "Week",
				minorTickCount: 1,
				majorTick: 15,
				slotHeight: 10,
				dateHeaderTemplate: kendo.template("<span class='days-name'>#=kendo.toString(date, 'dd.MM.yyyy')#</span>"),
				allDaySlot: false,
				selectedDateFormat: "{0:dddd dd.MM.yyyy}"
			}],
		dateHeaderTemplate: kendo.template("<u>#=kendo.toString(date, 'dd/M')#</u> - (#=percentage(date)#%)"),
		editable: {
			template: $("#schedulerEditor").html(),
			window: {
				title: "Оказанная услуга",
				width: "650px",
				height: "400px",
				scrollable: false
			}
		},
		edit: function (e) {
			$("#schedulerForm").kendoForm({
				orientation: "vertical",
				buttonsTemplate: "",
				items: [
					{
						field: "DateOfCreate", label: "Начало", validation: { required: true },
						editor: function (container, options) {
							let input = $('<input id="schedulerDateOfCreate" type="datetime" name="DateOfCreate" required="required" />');
							input.appendTo(container);
							input.kendoDateTimePicker({
								format: "MM/dd/yy hh:mm tt"
							});
						}
					},
					{
						field: "ServiceId", label: "Услуга", validation: { required: true },
						editor: function (container, options) {
							let input = $('<input id="schedulerService" name="SchedulerServiceId" required="required" />');
							input.appendTo(container);
							input.kendoComboBox({
								dataSource: services,
								placeholder: "Название услуги",
								dataTextField: "Name",
								dataValueField: "Id",
								filter: "contains",
							});
						}
					},
					{
						field: "CompanyId", label: "Компания", validation: { required: true },
						editor: function (container, options) {
							let input = $('<input id="schedulerCompany" name="SchedulerCompanyId" required="required" />');
							input.appendTo(container);
							input.kendoComboBox({
								dataSource: companies,
								placeholder: "Название компании",
								dataTextField: "Name",
								dataValueField: "Id",
								filter: "contains",
							});
						}
					},
					{
						field: "EmployeeId", label: "Сотрудник", validation: { required: true },
						editor: function (container, options) {
							let input = $('<input id="schedulerEmployee" name="SchedulerEmployeeId" required="required" />');
							input.appendTo(container);
							input.kendoComboBox({
								placeholder: "Имя сотрудника",
								dataTextField: "FullName",
								dataValueField: "Id",
								filter: "contains",
								dataSource: employee,
								value: "4"
							});
						}
					},
				]
			})
			// Fill form fields
			if (e.event.Id > 0) {
				$("#schedulerDateOfCreate").data("kendoDateTimePicker").value(e.event.start);
				$("#schedulerCompany").data("kendoComboBox").value(e.event.Company.Id);
				$("#schedulerService").data("kendoComboBox").value(e.event.Service.Id);
				$("#schedulerEmployee").data("kendoComboBox").value(e.event.Employee.Id);
			}
			else {
				$("#schedulerDateOfCreate").data("kendoDateTimePicker").value(e.event.start);
			}

			let buttonsContainer = e.container.find(".k-edit-buttons");
			let cancelButton = buttonsContainer.find(".k-scheduler-cancel");
			let saveButton = buttonsContainer.find(".k-scheduler-update");
			cancelButton.text("Отмена");
			saveButton.text("Применить");
			saveButton.click(function () {
				if (e.event.Id > 0) {
					let sheduler = $("#scheduler").data("kendoScheduler");
					sheduler.dataSource.at(sheduler.dataSource.indexOf(e.event)).set();
				}
			});
		}
	});

	$(".k-nav-today").on("click", function () {
		$("#scheduler").data("kendoScheduler").dataSource.read();
		$("#scheduler").data("kendoScheduler").dataSource.read();
	});

	$(".k-nav-prev").on("click", function () {
		$("#scheduler").data("kendoScheduler").dataSource.read();
		$("#scheduler").data("kendoScheduler").dataSource.read();
	});

	$(".k-nav-next").on("click", function () {
		$("#scheduler").data("kendoScheduler").dataSource.read();
		$("#scheduler").data("kendoScheduler").dataSource.read();
	});
});