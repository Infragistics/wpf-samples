$(document).ready(function () {
    $("#ddl-codeview1").igDropDown().change(function () {
        $('pre', $(this).parents('.toolbar').next('.body')).css({
            'font-size': $(this).val() + 'px'
        });
    });
    $('.select-all').click(function (e) {
        e.preventDefault();
        select($(this).parents('.toolbar').next('.body')[0]);
    });
    $('#showFancyCodeViewerBox')
        .click(function () {
            var fancyCodeViewer = $("#fancyCodeViewer");
            var fancyCodeViewerBody = $('.body', fancyCodeViewer);
            var maxWidth = $(window).width() - 100;
            if (maxWidth > 1024) { fancyCodeViewerBody.css('min-width', 1024); } else { fancyCodeViewerBody.css('min-width', 0); }
            fancyCodeViewerBody.empty().css({
                'max-height': $(window).height() - 160,
                'min-height': 300,
                'max-width': maxWidth,
                'overflow': 'auto'
            });
            $('#codeViewerContainer > .body pre').clone().css({ 'font-size': '12px' }).appendTo(fancyCodeViewerBody);
        })
        .fancybox({
            'padding': '0px',
            'centerOnScroll': true,
            'titleShow': false,
            'showNavArrows': false,
            onStart: function () {
                $('#sampleContainer object').hide();
                setTimeout(function () {
                    $("#ddl-codeview2").igDropDown().change(function () {
                        $('pre', $(this).parents('.toolbar').next('.body')).css({
                            'font-size': $(this).val() + 'px'
                        });
                    })
                }, 50);
            },
            onCleanup: function () {
                $("#fancyCodeViewer .body").empty();
            },
            onClosed: function () {
                $('#sampleContainer object').show();
            }
        });
});