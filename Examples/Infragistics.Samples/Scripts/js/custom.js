//Global Nav Script Start
function ShowSubmenu(elem) {
    $('.menu_sub').hide();
    $(elem).addClass('bg');
    $('a', elem).addClass('mainnav_link_over');
    $('.menu_sub', elem).slideDown(350);
}
$('#main_nav .top_nav_li:not(.last)').mouseenter(function (e) {
    ShowSubmenu(this)
}).mouseleave(function (e) {
    if ($(this).hasClass('bg')) {
        $(this).removeClass('bg');
        $('a', this).removeClass('mainnav_link_over');
        $('.menu_sub').stop(true, true);
        $('.menu_sub').hide();
    }
});
//Global Nav Script End

// Text selection
function select(element) {
	var body = document.body, range, sel;
	if (document.createRange && window.getSelection) { // FF, Safari, Opera
		range = document.createRange();
		range.selectNodeContents(element);
		sel = window.getSelection();
		sel.removeAllRanges();
		sel.addRange(range);
	} else if (body.createTextRange) { // IE
		document.selection.empty();
		range = body.createTextRange();
		range.moveToElementText(element);
		range.select();
	}
}
// End of text selection

//ipad scroll in samples browser.
var pref_x;
var scrolling = false;
var scrollLeft = false;
function featuresListScroll(event) {
	scrolling = true;
	var _this = $(this);
	event.preventDefault();
	var e = event.originalEvent;
	if (e.touches.length == 1) {
		var touch = e.touches[0]
		var scontent = $('.scroll-content', _this);
		var pageSize = 7; //(scontent.height() / _this.height());
		var x = touch.pageY - _this.offset().top;
		if (pref_x > x) {
			if (x < 0) { return; }
		}
		var sliderVertical = $('.slider-vertical', _this);
		var sliderValue = Math.round(100 * (Math.abs(x) / scontent.height()));
		var currentSliderValue = sliderVertical.slider('value');
		//console.log('currentValue: ' + currentSliderValue + ', sliderValue: ' + sliderValue);
		if (pref_x > x) {
			sliderValue = currentSliderValue - (sliderValue / pageSize);
		}
		else {
			sliderValue = currentSliderValue + (sliderValue / pageSize);
		}
		//console.log('sliderValue: ' + sliderValue);
		sliderVertical.slider('value', sliderValue);

		pref_x = x;
	}
}
function startTopNavigationScroll(event) {
	event.preventDefault();
	var e = event.originalEvent;
	if (e.touches.length == 1) {
		var touch = e.touches[0];
		var x = touch.pageX;
		if (pref_x > x) {
			scrollLeft = false;
		}
		else if (pref_x < x) {
			scrollLeft = true;
		}
		scrolling = true;
		pref_x = x;
	}

}
function endTopNavigationScroll(event) {
	if (scrolling) {
		if (!scrollLeft) {
			//console.log('scroll left');
			$("#nextNavButton").trigger('click');
		}
		else {
			//console.log('scroll right');
			$("#prevNavButton").trigger('click');
		}
	}
	scrolling = false;
}
function startMainNavigationMaximize(event) {
	event.preventDefault();
	var e = event.originalEvent;
	if (e.touches.length == 1) {
		var y = e.touches[0].pageY;
		if (pref_x < y) {
			scrolling = true;
		}
		pref_x = y;
	}
}
function endMainNavigationMaximize(event) {
	if (scrolling) {
		//console.log('nav maximize');
		$('a#minimize').trigger('click');
	}
}
$(document).ready(function () {
	$('body').delegate('.topNavMainContainer', 'touchmove', startTopNavigationScroll)
				.delegate('.topNavMainContainer', 'touchend', endTopNavigationScroll)
				.delegate('#_topNavTitle', 'touchmove', startMainNavigationMaximize)
				.delegate('#_topNavTitle', 'touchend', endMainNavigationMaximize)
				.delegate('.scroll-pane', 'touchmove', featuresListScroll)
				.delegate('.scroll-pane', 'touchend', function (event) {
					if (scrolling) {
						event.preventDefault();
						scrolling = false;
					}
				});
});
