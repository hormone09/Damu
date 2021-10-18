
$(document).ready(function () {

	var dataSource1 = new kendo.data.SchedulerDataSource({
		transport: {
			read: {
				url: "/ServicesHistory/LIST",
				type: "GET",
				dataType: "jsonp"
			}
		},
		batch: false,
		schema: {
			model: {
				id: "Id",
				fields: {
					serviceName: { from: "Service.Name", type: "string" },
					companyName: { from: "Company.Name", type: "string" },
					employeeName: { from: "Eployee.Name", type: "string" },
					start: { type: "date", from: "Start", },
					end: { type: "date", from: "End" },
				}
			}
		}
	});

	$("#scheduler").kendoScheduler({
		dataSource: dataSource1,
		startTime: new Date("2013/6/13 07:00 AM"),
		endTime: new Date("2013/6/13 06:00 PM"),
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
			window: {
				title: "My Custom Title",
				animation: false,
				open: myOpenEventHandler
			}
		}
	});
});