(function ($) {

	var methods = {
		init: function (options) {
			return this.each(function () {
				CreateDropDown($(this));
			});
		}
	};

	$.fn.igDropDown = function (method) {

		// Method calling logic
		if (methods[method]) {
			return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
		} else if (typeof method === 'object' || !method) {
			return methods.init.apply(this, arguments);
		} else {
			$.error('Method ' + method + ' does not exist on jQuery.tooltip');
		}

	};

	// plugin specific methods
	function CreateDropDown(source) {
		//var source = this;
		var selected = source.find("option:selected");  // get selected <option>
		var items = $("option", source);  // get all <option> elements

		if ($('dl[id=custom-' + source.attr('id') + ']')) { $('dl[id=custom-' + source.attr('id') + ']').remove(); }

		var customDropDown = $('<dl />')
					.attr('id', 'custom-' + source.attr('id'))
					.addClass('custom-dropdown');

		var customDropDownMenu = $('<dd />').append('<ul />')
					.css({
						'max-height': '300px',
						'overflow': 'hidden'
					});

		var customDropDownButton = $('<div />')
					.addClass('cdd-button')
					.append($('<div />').addClass('cdd-arrow'))
					.css('cursor', 'pointer');


		// create <dl> and <dt> with selected value inside it and apply default style.            
		if (!selected.length) {
			selected = source.find("option:first");
		}

		var customDropDownBox = $('<dt />')
				.append($('<a />')
							.css('display', 'block')
							.attr('href', 'javascript:void(0);')
							.html(selected.text() + '<span style="display:none;" class="value">' + selected.val() + '</span>')
						);

		customDropDown.append(customDropDownBox)
				.append(customDropDownButton)
				.append(customDropDownMenu);

		// iterate through all the <option> elements and create UL
		items.each(function () {
			$("ul", customDropDownMenu).append(
											$('<li />').append($('<a />')
																	.css('display', 'block')
																	.attr('href', 'javascript:void(0);')
																	.html($(this).text() + '<span style="display:none;" class="value">' + $(this).val() + '</span>')
																		));
		});

		// Append the custom dropdown list to the DOM and hide the original dropdown.			
		customDropDown.insertAfter(source);
		source.hide();

		var buttonWidth = customDropDownButton.outerWidth();
		var menuWidth = customDropDownMenu.outerWidth() + buttonWidth;

		customDropDownMenu
				.css('left', customDropDown.position().left)
				.width(menuWidth)
				.hide();

		if (menuWidth < source.outerWidth()) {
			menuWidth = source.outerWidth();
			customDropDownMenu.width(menuWidth);
		}
		$('ul', customDropDown).addClass('list_style_none');

		// Set the width of the customDropDown
		customDropDown.outerWidth(customDropDownMenu.outerWidth())
				.outerHeight(source.outerHeight());
		// Set the width of the customDropDownBox
		customDropDownBox.outerWidth(customDropDown.innerWidth() - buttonWidth);
		// Center the selected item text
		customDropDownBox.css('line-height', customDropDown.innerHeight() + 'px');
		// Set the arrow in the middle of the button
		customDropDownButton.css('line-height', customDropDownButton.innerHeight() + 'px');

		var buttonArrow = $(".cdd-arrow", customDropDownButton);
		buttonArrow.css('margin-top', (customDropDownButton.innerHeight() - buttonArrow.outerHeight()) / 2);

		$("a", customDropDown).css('outline', 0);

		//Atach event handlers
		$("dt a", customDropDown).click(boxClick);
		customDropDownButton.click(boxClick);

		$("dd a", customDropDown).click(function () {
			var text = $(this).html();
			$("dt a", customDropDown).html(text);
			source.val($(this).find("span.value").html());
			source.trigger("change");
			customDropDownMenu.toggle();
		});

		customDropDownMenu.mouseleave(function () {
			$(this).hide();
		})

		function boxClick() {
			//debugger;
			var cdd = $(this).parents("dl.custom-dropdown");
			var pTop = cdd.position().top + cdd.outerHeight() + 1;
			customDropDownMenu.css({
				top: pTop,
				left: cdd.position().left
			});
			customDropDownMenu.toggle();
		}

		//		$('custom-dropdown ul > li > a').mouseenter(function () {
		//			$(this).css({ 'color': '#3fc3fa !important', backgroundColor: '#fff' });
		//		}).mouseleave(function () {
		//		});

		return customDropDown;
	}

})(jQuery);