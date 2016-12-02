var click;

document.addEventListener('DOMContentLoaded', function () {
    $(window).load(function () {
        $('body').addClass('ready');
    });

    click = Helpers.detectTouchDevice();

    Helpers.customScroll();
    FeaturesGroups.init();
    PaybackCalculator.init();
    Dashboard.init();
});

window.addEventListener('resize', function () {
    Helpers.updateContentHeight();
});

window.addEventListener('orientationchange', function () {
    Helpers.updateContentHeight();
});

var Helpers = {
    customScroll: function () {
        var height = window.innerHeight - $('header').innerHeight();

        $('.content-area-scroll').mCustomScrollbar({
            setHeight: height,
            scrollInertia: 200,
            autoHideScrollbar: false,
            contentTouchScroll: true,
            scrollbarPosition: 'outside',
            advanced: {
                updateOnContentResize: true
            },
            mouseWheel: {
                normalizeDelta: true,
                deltaFactor: 100
            },
            theme: 'minimal-dark'
        });
        $('.content-area-overflow').css('overflow', 'auto');

        $('.profile').mCustomScrollbar({
            setHeight: height,
            scrollInertia: 200,
            autoHideScrollbar: false,
            contentTouchScroll: true,
            scrollbarPosition: 'outside',
            advanced: {
                updateOnContentResize: true
            },
            mouseWheel: {
                normalizeDelta: true,
                deltaFactor: 100
            },
            theme: 'minimal-dark'
        });
        $('.side-container').css('overflow', 'auto');
    },
    updateContentHeight: function () {
        $('.content-area-scroll, .profile').height(window.innerHeight - $('header').innerHeight());
    },
    detectTouchDevice: function () {
        if (('ontouchstart' in window) || window.DocumentTouch && document instanceof DocumentTouch) {
            return 'touchend';
        }
        return 'click';
    }
}

var FeaturesGroups = {
    init: function () {
        var self = this;

        this.$features = $('.feature');
        this.$featureInputs = this.$features.find('input');

        this.$featureInputs.each(function () {
            if (this.checked) {
                $('.feature-parent[data-id=' + $(this).data('id') + ']').addClass('active');
            }
        });

        self.process();

        this.$featureInputs.on('click', function () {
            var input = this;
            self.toggle(input);
        });
    },
    toggle: function (elem) {
        var self = this,
            dataId = $(elem).data('id'),
            $parentFeature = $('.feature-parent[data-id=' + dataId + ']');

        if (elem.checked) {
            $parentFeature.addClass('active');
        }
        else {
            $parentFeature.removeClass('active');
            
            $parentFeature.parent().find('input').each(function () {
                if (this.checked) {
                    $(this).parent().click();
                    var parentDataId = $(this).parent().data('id');
                    $('.feature-parent[data-id="' + parentDataId + '"]').removeClass('active');
                }
            });
            
        }

        this.process();
    },
    process: function () {
        this.$features.each(function () {
            var $self = $(this),
                $features = $self.find('.feature-parent'),
                $featureParent = $features.closest('.feature'),
                increment = 0;

            $featureParent.addClass('depended');

            //start tooltips

            var tooltipText = '';

            $features.not('.active').find('span').each(function () {
                tooltipText = tooltipText + '<div class="tooltip-feature">- ' + $(this).text() + '</div>';
            });

            $featureParent.find('.tooltip').tooltip({
                items: "div",
                show: {
                    duration: 200
                },
                hide: {
                    duration: 200
                },
                content: "<div>This feature depends on:</div>" + tooltipText
            });

            //end tooltips

            for (var i = 0; i < $features.length; i++) {
                if ($features.eq(i).hasClass('active')) {
                    increment++;
                }

                if (increment == $features.length) {
                    $self.addClass('active');
                }
                else {
                    $self.removeClass('active');
                }
            }
        });
    }

}

var PaybackCalculator = {
    init: function () {
        if (!$('#PaybackCalculator').length)
            return;

        this.toggleRows();
        this.tooltip();
    },

    toggleRows: function () {
        var $rowHeaders = $('.table-row.semibold');

        $rowHeaders.on(Helpers.detectTouchDevice(), function () {
            $(this).toggleClass('collapse');
            $(this).closest('.table-row-group').find('.hidden-row').toggleClass('visible');
        })
    },

    tooltip: function () {
        $('#PaybackCalculator .tooltip').each(function () {
            var tooltipText = $(this).parent().find('.tooltip-text').text();

            $(this).tooltip({
                items: "div",
                show: {
                    duration: 200
                },
                hide: {
                    duration: 200
                },
                content: tooltipText
            });
        });
    }
}

var Dashboard = {
    init: function () {
        $('.view-weekly-report').on(Helpers.detectTouchDevice(), function () {
            var $elem = $('.report-list .shop[data-name="' + $(this).data('name') + '"]'),
                elemTop = $elem.offset().top,
                $scroller = $('#mCSB_1_container'),
                scrollerPos = parseInt($scroller.css('top'));

            $scroller.addClass('smooth');
            $scroller.css('top', -elemTop + scrollerPos + (window.innerHeight / 2) - ($elem.height() / 2));
            setTimeout(function () {
                $scroller.removeClass('smooth');
                $elem.addClass('highlight');
                setTimeout(function () {
                    $elem.removeClass('highlight');
                }, 1500);
            }, 200);
        });
    }
}