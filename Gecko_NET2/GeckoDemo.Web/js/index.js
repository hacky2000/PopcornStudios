// JavaScript Document
$(document).ready(function () {
	

	$(".gosearch").bind("click", function () {
	 
	  var thesearch = $(".gosearch").val();
	  
	  if(thesearch == '搜索应用') {
		  
		  $(".gosearch").val("");
		  }
		  
	  
   
    });
	
	$(".migubang_zm a").bind("click", function () {
	  
	  var thean_i =	$(this).attr("id").substr(3, 2);
	  
	  $(".migubang_list_i").hide();
	  for(i=thean_i; i<=26;i++){
	  $("#list_"+i).show();
	  }
	  
      $(".main_right").mCustomScrollbar("update");
   
    });
	
	$(".migubang_title a").bind("click", function () {
	 
	   var theniu = $(this).attr("id");
	  
	 $(".migubang .migubang_list").hide();
	 $("#"+theniu+"_list").show();
	 $(".migubang_title a").removeClass("on");
	 $(this).addClass("on");
	  
   
    });
	

    $(".theover").hover(function () {
      $(this).css("background-position","bottom");
	  $(this).next().css("display","block");
	},
	function (){
	  $(this).css("background-position","top");
	  $(this).next().hide();
	}
	);
	
	
	$(".migubang_list_real ul li").hover(function () {
	  $(this).find(".migubang_list_name").css("display","none");
	  $(this).find(".migubang_list_install").css("display","block");
	},
	function (){
	  $(this).find(".migubang_list_name").css("display","block");
	  $(this).find(".migubang_list_install").css("display","none");
	}
	);
	
	
	
	$(".migubang_list_i_l_tu").hover(function () {
	  $(this).next().show();
	},
	function (){
	  $(this).next().hide();
	}
	);
	
	$(".migubang_list_i_l_info").hover(function () {
	  $(this).show();
	},
	function (){
	  $(this).hide();
	}
	);
	

   $(".migubang_list_i_gou").bind("click", function () {
	   var thebps = $(this).css("backgroundPosition");
	   
      if(thebps == "50% 0%")
	  {
           $(this).css("background-position","bottom");
	  }
	  else {
		   $(this).css("background-position","top");
      }
      $(".migu_dall_gou").css("background-position","top");
   
	});
	
	$(".migu_dall_gou").bind("click", function () {
	   var thebps = $(this).css("backgroundPosition");
	   
      if(thebps == "50% 0%")
	  {
           $(this).css("background-position","bottom");
		   $(".migubang_list_i_gou").css("background-position","bottom");
	  }
	  else {
		   $(this).css("background-position","top");
		   $(".migubang_list_i_gou").css("background-position","top");
      }

   
	});
	
	$(".migu_dall_gogo").bind("click", function () {

	   
      $(".migu_dall_gou").css("background-position","bottom");
	  $(".migubang_list_i_gou").css("background-position","bottom");
	 
   
	});
   
	

	
})

	