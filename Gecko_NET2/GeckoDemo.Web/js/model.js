// JavaScript Document
     (function($){
      $(".main_left dd").hide()
      $(window).load(function(){
        $(".content").mCustomScrollbar({
          scrollButtons:{
            enable:true
          }
        });
        $(".main_left dt:not(.dt1)").click(function(e){
          $("dt:not(.dt1)").find("a").css("background","url(images/list-more-down.png) center left no-repeat")
          $(this).find("a").css("background","url(images/list-more-up.png) center left no-repeat")
          if($(this).next().css("display") == "block"){
            $(this).find("a").css("background","url(images/list-more-down.png) center left no-repeat")
            $(this).next().hide()
          }
          else {
            $(".main_left dt").next().hide()
            $(this).next().show()
          }
          if($(".mCSB_container").height() > 590){
            e.preventDefault();
            $(".main_left .content_2").mCustomScrollbar("update");
          }
          else{
            e.preventDefault();
            $(".main_left .content_2").mCustomScrollbar("disable",true);
          }; 
           $(".mCSB_container").css("top","0px");
        });
      });

	    //ËÑË÷¿ò
	$('.gosearch').focus(function(){
		$(this).val('');
		$('.searchlist').show();
	});
	$('.searchlist li .spanm').click(function(){
		$('.gosearch').val($(this).html());
		$('.searchlist').hide();
	});
	


    })(jQuery);