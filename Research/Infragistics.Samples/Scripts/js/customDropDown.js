function createDropDown(menuId) {
    var source = $("#" + menuId);
    var selected = source.find("option[selected]");  // get selected <option>
    var options = $("option", source);  // get all <option> elements
    var customDropDown = $('<dl id="custom-' + menuId + '" class="custom-dropdown"></dl>');
    var customDropDownMenu = $('<dd><ul></ul></dd>');
    var customDropDownButton = $('<div class="cdd-button"><div class="cdd-arrow"></div></div>').css("cursor", "pointer");

    // create <dl> and <dt> with selected value inside it and apply default style.            
    if (!selected.length) {
        selected = source.find("option:first");
    }

    var customDropDownBox = $('<dt><a style="display: block;" href="javascript:void(0);">' + selected.text() + '<span style="display:none;" class="value">' + selected.val() + '</span></a></dt>');

    customDropDown.append(customDropDownBox);
    customDropDown.append(customDropDownButton);
    customDropDown.append(customDropDownMenu);

    // iterate through all the <option> elements and create UL
    options.each(function () {
        $("ul", customDropDownMenu).append('<li><a style="display: block;" href="javascript:void(0);">' + $(this).text() + '<span style="display:none;" class="value">' + $(this).val() + '</span></a></li>');
    });

    // Append the custom dropdown list to the DOM.
    source.parent().append(customDropDown);
    source.hide();

    var buttonWidth = customDropDownButton.outerWidth();
    var menuWidth = customDropDownMenu.outerWidth() + buttonWidth;

    customDropDownMenu.css({
        left: customDropDown.position().left
    }).width(menuWidth).hide();

    if (menuWidth < source.outerWidth()) {
        menuWidth = source.outerWidth();
        customDropDownMenu.width(menuWidth);
    }
    $("ul li", customDropDownMenu).css("list-style-type", "none");

    // Set the width of the customDropDown
    customDropDown.outerWidth(customDropDownMenu.outerWidth()).outerHeight(source.outerHeight());
    // Set the width of the customDropDownBox
    customDropDownBox.outerWidth(customDropDown.innerWidth() - buttonWidth);
    // Center the selected item text
    customDropDownBox.css({
        "line-height": customDropDown.innerHeight() + "px"
    });
    // Set the arrow in the middle of the button
    customDropDownButton.css({
        "line-height": customDropDownButton.innerHeight() + "px"
    });
    var buttonArrow = $(".cdd-arrow", customDropDownButton);
    buttonArrow.css({
        "margin-top": (customDropDownButton.innerHeight() - buttonArrow.outerHeight()) / 2
    });

    $("a", customDropDown).css({
        outline: 0
    });

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
        hideMenu();
    }).mouseenter(function () {
        customDropDownMenu.data('hideMenu', false);
    });

    customDropDownBox.mouseenter(function () {
        customDropDownMenu.data('hideMenu', false);
    }).mouseleave(function () {        
        hideMenu(); 
    });

    function hideMenu()
    {
       customDropDownMenu.data('hideMenu', true);
        setTimeout(function () {
            if (customDropDownMenu.data('hideMenu')) {
                customDropDownMenu.hide();
            }
        }, 500);
    }

    function boxClick() {
        var cdd = $("dl.custom-dropdown");
        var pTop = cdd.position().top + cdd.outerHeight();
        customDropDownMenu.css({
            top: pTop,
            left: cdd.position().left
        });
        customDropDownMenu.toggle();
    }
}