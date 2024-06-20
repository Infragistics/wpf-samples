(function ($) {

	var methods = {
		init: function (options) {
			return this.each(function () {
				CreateScrollBar($(this));
			});
		}
	};

	$.fn.igScroll = function (method) {

		// Method calling logic
		if (methods[method]) {
			return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
		} else if (typeof method === 'object' || !method) {
			return methods.init.apply(this, arguments);
		} else {
			$.error('Method ' + method + ' does not exist on jQuery.igScroll');
		}
	};

	// plugin specific methods
	function CreateScrollBar(sp) {
		if (sp.data('loaded')) { return; }

		//change the main div to overflow-hidden as we can use the slider now
		var maxHeight = sp.css('max-height').replace('px', '').replace('em', '');
		if (sp.height() < maxHeight) { return; }

		sp.css({ 'overflow': 'hidden', 'height': maxHeight });

		//compare the height of the scroll content to the scroll pane to see if we need a scrollbar
		var scrollContent = $('ul.leftNavLinks', sp);
		var origScrollContentWidth = scrollContent.width();
		scrollContent.addClass('scroll-content')
        .width('209px');

		var difference = scrollContent.outerHeight() - sp.innerHeight(); //eg it's 200px longer

		if (difference > 0)//if the scrollbar is needed, set it up...
		{
			var proportion = difference / scrollContent.outerHeight(); //eg 200px/500px
			var handleHeight = Math.round((1 - proportion) * sp.innerHeight()); //set the proportional height - round it to make sure everything adds up correctly later on
			handleHeight -= handleHeight % 2;

			var sliderWrap = $('.slider-wrap', sp);
			if (sliderWrap.length > 0) { sliderWrap.remove(); }

			sliderWrap = $('<\div class="slider-wrap"><\div class="slider-vertical"><\/div><\/div>').appendTo(sp); //append the necessary divs so they're only there if needed
			sliderWrap.outerHeight(sp.innerHeight()); //set the height of the slider bar to that of the scroll pane             

			//set up the slider 
			var sliderVertical = $('.slider-vertical', sliderWrap);
			sliderVertical.slider({
				orientation: 'vertical',
				min: 0,
				max: 100,
				value: 100,
				//animate: true,
				slide: function (event, ui) {//used so the content scrolls when the slider is dragged
					var topValue = -((100 - ui.value) * difference / 100);
					scrollContent.css({ top: topValue }); //move the top up (negative value) by the percentage the slider has been moved times the difference in height
				},
				change: function (event, ui) {//used so the content scrolls when the slider is changed by a click outside the handle or by the mousewheel
					var topValue = -((100 - ui.value) * difference / 100);
					scrollContent.css({ top: topValue }); //move the top up (negative value) by the percentage the slider has been moved times the difference in height
					//scrollContent.animate({ top: topValue }, 1500);
				}
			});

			//set the handle height and bottom margin so the middle of the handle is in line with the slider
			$(".ui-slider-handle", sliderVertical)
            .css({
            	height: handleHeight,
            	'margin-bottom': -0.5 * handleHeight
            });

			var origSliderHeight = sliderVertical.height(); //read the original slider height
			var sliderHeight = origSliderHeight - handleHeight; //the height through which the handle can move needs to be the original height minus the handle height
			var sliderMargin = (origSliderHeight - sliderHeight) * 0.5; //so the slider needs to have both top and bottom margins equal to half the difference        
			sliderVertical.css({ height: sliderHeight, 'margin-top': sliderMargin }); //set the slider height and margins

			//code to handle clicks outside the slider handle        
			sliderVertical.click(function (event) {//stop any clicks on the slider propagating through to the code below
				event.stopPropagation();
			});

			sliderWrap.click(function (event) {//clicks on the wrap outside the slider range
				var offsetTop = $(this).offset().top; //read the offset of the scroll pane
				var clickValue = (event.pageY - offsetTop) * 100 / $(this).height(); //find the click point, subtract the offset, and calculate percentage of the slider clicked
				sliderVertical.slider("value", 100 - clickValue); //set the new value of the slider
			});

			var selectedItem = $('.selected', scrollContent);
			if (selectedItem.length > 0) {
				var value = 100 - Math.round(100 * ((selectedItem.position().top + (selectedItem.is('li:last-child') || selectedItem.next().is('li:last-child') ? selectedItem.height() : 0)) / scrollContent.height()));
				sliderVertical.slider('value', value);
			}
			else {
				scrollContent.css('top', '0px');
			}

			sp.data('loaded', true);
		}
		else {
			scrollContent.width(origScrollContentWidth);
		}  //end if

		//additional code for mousewheel    
		sp.mousewheel(function (event, delta) {
			var speed = 7;
			var sliderVal = sliderVertical.slider("value"); //read current value of the slider

			sliderVal += (delta * speed); //increment the current value

			sliderVertical.slider("value", sliderVal); //and set the new value of the slider

			event.preventDefault(); //stop any default behaviour
		});
	}

})(jQuery);