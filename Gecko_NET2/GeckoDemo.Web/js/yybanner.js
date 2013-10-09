// JavaScript Document
$(document).ready(function () {
	
	timerr = setTimeout(autoClick, 6000);
	
	var lth = $(".banner_l_r a").length;
	var defaultZ = 10;
	var str = '';
	for(var i=lth;i>0;i--){
	  str += '<a id="adbot_' + (i) + '" href="###"></a>';
	}
	var $controlA = $(".bannerbot").append($('<div class="control">' + str + '</div>').css('zIndex', defaultZ + 10000)).find('.control a');

    $("#adbot_1:eq(0)").addClass("on");
	
	$(".banner_l_r").css("width",(558*lth)+"px");
	
	$(".control a").bind("click", function () {
	  $(".banner_l_r").stop(true,true);
      clearTimeout(timerr);
	  var pxml = parseInt( $(".banner_l_r").css("marginLeft") );
	  /*alert(pxml);*/
	  var themj = $(this).attr("id");
/*	   alert($(this).attr("id").indexOf("_"));*/
	  var adboti =	$(this).attr("id").substr(6, 2);
	/*  alert(adboti);*/
	  $(".bannerr a").removeClass("on");
	  $("#banner_"+themj).addClass("on");
	 

      $(".banner_l_r").stop(true,true).animate({marginLeft:-(558*(adboti-1))+"px"},1000);
	 
	
	  $(".control a").removeClass("on")
	  $(this).addClass("on")
	
	  
   
    });
	
    function autoClick(){
       clearTimeout(timerr);
       
	   var thadnum = $(".control .on").attr("id").substr(6, 2);
	   
	   if(thadnum != lth) {
		   $("#adbot_"+(parseInt(thadnum)+1)).click();
	   }
	   else{
		   $("#adbot_1").click();
	   }
	   
	  /* alert("#adbot_"+(parseInt(thadnum)+1));*/
      timerr = setTimeout(autoClick, 6000);
	 }

	
	

	
})

	