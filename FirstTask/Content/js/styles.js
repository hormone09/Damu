$(document).ready(function () {
	$('.k-dialog ').width(500);

	// GRID Translater
	setTimeout(() => {
		let pageNumbers = Array.from($('.k-pager-numbers li').children());
		let pageButtons = Array.from($('.k-pager-nav'));

		if (Array.isArray(pageButtons)) {
			pageButtons.forEach((el) => {
				el.title = "";
			})
		}

		if (Array.isArray(pageNumbers)) {
			pageNumbers.forEach((el) => {
				el.title = "";
			})
		}
	}, 1000);


});
