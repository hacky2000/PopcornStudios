// JavaScript Document
$(document).ready(function () {
	

	$(".gosearch").bind("click", function () {
	 
	  var thesearch = $(".gosearch").val();
	  
	  if(thesearch == '搜索应用') {
		  
		  $(".gosearch").val("");
		  }
		  
	  
   
    });
	
	$(".xxx").bind("click", function () {
	 
	$(".bfts_mj_cc").hide();
   
    });
	
	$(".migubang_list ul li").hover(function () {
	  $(this).find(".yichu").show();
	  $(this).find(".sjgl_list_i_title a span").css("backgroundPosition","bottom left");
	},
	function (){
	  $(this).find(".yichu").hide();
	  $("#sjgl .sjgl_list_i_title a span").css("backgroundPosition","top left");
	  $("#sjgl .oushu .sjgl_list_i_title a span").css("backgroundPosition","center left");
	}
	);
	
	
	$(".migubang_list_i_gou").bind("click", function () {
	   var thebps = $(this).css("backgroundPosition");
	   
      if(thebps == "50% 0%")
	  {
           $(this).css("background-position","bottom");
		   $(this).find(".sjgl_list_i_title a span").css("backgroundPosition","bottom left");
	  }
	  else {
		   $(this).css("background-position","top");


      }
      $(".migu_dall_gou").css("background-position","top");
	  $("#sjgl .migubang_list ul li").removeClass("onall");
	   $(".zhezhao").show();
	   $(".anzhuang").removeClass("anzhuangon");
		$(".yichu").removeClass("yichuon");
		$(".daoru").removeClass("daoruon");
		$(".shengji").removeClass("shengjion");
		$(".hulue").removeClass("hulueon");
		$(".ydsdk").removeClass("ydsdkon");
		$(".ydsjnc").removeClass("ydsjncon");
   
	});
	
	$(".migu_dall_gou").bind("click", function () {
	   var thebps = $(this).css("backgroundPosition");
	  
	   
      if(thebps == "50% 0%")
	  {
           $(this).css("background-position","bottom");
		   $(".migubang_list_i_gou").css("background-position","bottom");
		   $("#sjgl .migubang_list ul li").addClass("onall");
		    $(".zhezhao").hide();
			
			$(".anzhuang").addClass("anzhuangon");
			$(".yichu").addClass("yichuon");
			$(".daoru").addClass("daoruon");
			$(".shengji").addClass("shengjion");
			$(".hulue").addClass("hulueon");
			$(".ydsdk").addClass("ydsdkon");
			$(".ydsjnc").addClass("ydsjncon");
			
			$(".sjgl_list_i_title a span").css("backgroundPosition","bottom left");
	  }
	  else {
		   $(this).css("background-position","top");
		   $(".migubang_list_i_gou").css("background-position","top");
		   $("#sjgl .migubang_list ul li").removeClass("onall");
		    $(".zhezhao").show();
			
			$(".anzhuang").removeClass("anzhuangon");
			$(".yichu").removeClass("yichuon");
			$(".daoru").removeClass("daoruon");
			$(".shengji").removeClass("shengjion");
			$(".hulue").removeClass("hulueon");
			$(".ydsdk").removeClass("ydsdkon");
			$(".ydsjnc").removeClass("ydsjncon");
			
			 $("#sjgl .sjgl_list_i_title a span").css("backgroundPosition","top left");
	        $("#sjgl .oushu .sjgl_list_i_title a span").css("backgroundPosition","center left");
      }
      
   
	});
	
	$(".tjwj").bind("click", function () {
	   $(".tankuang").show();
	});
	
	$(".xxx").bind("click", function () {
	   $(".tankuang").hide();
	});
	

   
	
	
	
	

	
})

	