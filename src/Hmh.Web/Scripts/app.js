
    
var App = {
    //bs初始化
    handleBootstrap: function () {           
        $('.carousel').carousel({
            interval: 1500,
            pause: 'hover'
        });
        $('.tooltips').tooltip();        
        $('[data-toggle="tooltip"]').tooltip();
        //$('.input-daterange').datepicker({});
        $('.popovers').popover();
        $('[data-toggle="popover"]').popover();

        $('[data-spy="scroll"]').each(function () {
            var $spy = $(this).scrollspy('refresh')
        })            
        $('body').on('click', ".dropdown-menu.hold-on-click", function (e) {
            e.stopPropagation();
        });
    },
    handleSpinners: function () {
        if (!$().spinner)
        {
            return;
        }
        $('.spinner').spinner();
    },
    handlePin: function () {
        if (!$().pin)
        {
            return;
        }
        $('.pinned').pin();
    },
        

       

    handleUniform: function () {
        if (!$().uniform) {
            return;
        }
        var test = $("input[type=checkbox]:not(.toggle, .make-switch), input[type=radio]:not(.toggle, .star, .make-switch)");
        if (test.size() > 0) {
            test.each(function () {
                if ($(this).parents(".checker").size() == 0) {
                    $(this).show();
                    $(this).uniform();
                }
            });
        }
    },
    //回到页面顶部
    handleMisc: function () {
        $('body').on('click', 'a.gotop', function () {
            $('html,body').animate({
                scrollTop: $('body').offset().top
            }, 'slow');
        });           
    },

    countDown: function () {
        $('[data-countdown]').each(function () {
            var the = $(this), finalDate = new Date($(this).data('countdown'));
            if (!$().countdown)
            {
                return;
            }
            the.countdown({ until: finalDate });
        });
    },

    //初始化Slider
    initSlick: function () {
        if (!$().slick)
        {
            return;
        }
        $('.bxslider').slick({
            infinite: true,
            slidesToShow: 5,
            slidesToScroll: 3,
            prevArrow: "<a  class='slick-prev'><i class='fa fa-2x fa-angle-left'></i></a>",
            nextArrow: "<a  class='slick-next'><i class='fa fa-2x fa-angle-right'></i></a>"
        });
    },

    //图片懒加载
    initImgLazyLoad: function () {
        if (!$().lazyload)
        {
            return;
        }
        $("img.lazy").lazyload();
    },

    init: function () {
        this.handleBootstrap();            
        this.handleMisc();
        this.handlePin();
        //this.handleSpinners();
        //this.handleUniform();

        this.initSlick();          
        //this.initImgLazyLoad();
        //this.countDown();

    }

};

   






