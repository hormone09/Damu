
$(document).ready(function () {

	var dataSource1 = new kendo.data.SchedulerDataSource({
		transport: {
			read: {
				url: "/ServicesHistory/LIST",
				type: "GET",
				contentType: "application/json; charset=utf-8",
			}
		},
		batch: false,
		schema: {
			model: {
				id: "Id",
				fields: {
					start: { type: "date", from: "Start", },
					end: { type: "date", from: "End" },
				}
			}
		}
	});

	$("#scheduler").kendoScheduler({
		startTime: new Date("2021/10/18 07:00 AM"),
		endTime: new Date("2021/10/18 06:00 PM"),
		height: "600px",
		views: [
			{
				type: "workWeek",
				minorTickCount: 4, // display one time slot per major tick
				majorTick: 60,
				slotHeight: 10
			},
		],
		editable: {
			template: $("#schedulerEditor").html(),
			window: {
				title: "Оказанная услуга",
				width: "500px",
				height: "310px",
				scrollable: false
			}
		}
			//{ field: "Service", dataSource: getServiceList() }
		/*
		}*/
	});
	$("#schedulerEditor").html(template({}));
	//Editor Tamplate

	$("#schedulerCompany").kendoComboBox({
		placeholder: "Введите название",
		dataTextField: "Name",
		dataValueField: "Id",
		filter: "contains",
		dataSource: getCompanyList()
	});
	/*$("#schedulerEmployee").kendoComboBox({
		placeholder: "Введите название",
		dataTextField: "Name",
		dataValueField: "Id",
		filter: "contains",
		dataSource: getEmployeeList()
	});*/
});
