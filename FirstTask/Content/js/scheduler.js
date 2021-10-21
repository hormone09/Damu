﻿
$(document).ready(function () {

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
						field: "Date", label: "Выберите день для отчета", validation: { required: true },
						editor: function (container, options) {
							var input = $('<input id="reportDate" name="Date"/>');
							input.appendTo(container);
							input.kendoDatePicker({
								placeholder: "Укажите точную дату",
								format: 'MM/dd/yy',
							});
						}
					},
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
								dataSource: employee,
								optionLabel: "Все",
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

		var date = $("#reportDate").val();
		var companyId = $("#reportCompanyId").val();
		var serviceId = $("#reportServiceId").val();
		var employeeId = $("#reportEmployeeId").val();
		var type = $("#reportReportType").val();

		window.open("GetReport/?Date=" + date + "&CompanyId=" + companyId + "&ServiceId=" + serviceId + "&EmployeeId=" + employeeId + "&Type=" + type);

		$("#reportWindow").data("kendoDialog").close();
	});

	// SCHEDULER
	var dataSource = new kendo.data.SchedulerDataSource({
		transport: {
			read: {
				url: "/ServicesHistory/Index",
				type: "POST",
			},
			create: function (e) {
				alert(12312312);
				let _form = $("#schedulerForm").data("kendoFrom");
				let data = _form.serializeArray();

				alert(12312312);
				$.ajax({
					url: "/ServicesHistory/Create",
					type: "POST",
					data: data,
					success: function (json) {
						alert(json);
					}
				});
				alert(12312312);
			}
		},
		batch: false,
		schema: { 
			model: {
				id: "Id",
				fields: {
					id: { from: "Id" },
					title: { from: "Title" },
					start: { type: "date", from: "DateOfCreate" },
					end: { type: "date", from: "DateOfFinish" }
				}
			}
		}
	});

	$("#scheduler").kendoScheduler({
		dataSource: dataSource,
		startTime: new Date("2021/10/18 07:00 AM"),
		endTime: new Date("2021/10/18 06:00 PM"),
		height: "600px",
		views: [
			{
				type: "workWeek",
				minorTickCount: 1, // display one time slot per major tick
				majorTick: 15,
				slotHeight: 10
			},
		],
		editable: {
			template: $("#schedulerEditor").html(),
			window: {
				title: "Оказанная услуга",
				width: "630px",
				height: "350px",
				scrollable: false
			}
		},
		edit: function (e) {
			$("#schedulerForm").kendoForm({
				orientation: "horizontal",
				buttonsTemplate: ""
			})
			$("#schedulerDatePicker").kendoDateTimePicker({
				format: "dd/MM/yy"
			});
			$("#schedulerCompany").kendoComboBox({
				placeholder: "Название компании",
				dataTextField: "Name",
				dataValueField: "Id",
				filter: "contains",
				dataSource: companies
			});
			$("#schedulerService").kendoComboBox({
				placeholder: "Название услуги",
				dataTextField: "Name",
				dataValueField: "Id",
				filter: "contains",
				dataSource: services,
			});
			$("#schedulerEmployee").kendoComboBox({
				placeholder: "Имя сотрудника",
				dataTextField: "FullName",
				dataValueField: "Id",
				filter: "contains",
				dataSource: employee
			});
			let buttonsContainer = e.container.find(".k-edit-buttons");
			let cancelButton = buttonsContainer.find(".k-scheduler-cancel");
			let saveButton = buttonsContainer.find(".k-scheduler-update");
			cancelButton.text("Отмена");
			saveButton.text("Применить");
		}
	});
});