

    
    var userCenter = {

        initSlick: function () {
            if (!$().slick)
            {
                return;
            }
            $('.zhuserlist-slick').slick({
                infinite: true,
                autoplay: true,
                slidesToShow: 10,
                slidesToScroll: 5,
                prevArrow: "<a  class='slick-prev'><i class='fa fa-2x fa-angle-left'></i></a>",
                nextArrow: "<a  class='slick-next'><i class='fa fa-2x fa-angle-right'></i></a>"
            });
        },
        handleDatePickers: function () {
            if ($().datepicker) {
                $('.date-picker').datepicker({
                    autoclose: true
                });
                $('body').removeClass("modal-open"); // fix bug when inline picker is used in modal
            }
        },
       
        
        init: function () {            
           
            this.initSlick();
            this.handleDatePickers(); 
        }
    };

   


