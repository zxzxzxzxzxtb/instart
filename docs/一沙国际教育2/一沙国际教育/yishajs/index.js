// JavaScript Document

var index={
    init:function(){
		this.shadow();
	},
	//鼠标移动到图片上
	shadow:function(){
		 var timg=$(".com-cont .top,.com-cont .recimg")
	     timg.hover(function(){
			 var tip=$(this).find("p");
		     $(this).find("i").stop().animate({opacity:".4"},200);
			 if(tip.length!=0){
			      tip.fadeIn(200);
			 }
		 },function(){
			 var tip=$(this).find("p");
		     $(this).find("i").stop().animate({opacity:"0"},200);
			 if(tip.length!=0){
			      tip.fadeOut(200);
			 }
		 })
	}
}
index.init();


//页面滚动
$(window).scroll(function(){
   var tophere=$(".toprel")
   var scrT=$(window).scrollTop();
   console.log(scrT)
   if(scrT>0){
       tophere.addClass("topfix")
   }
   else{
       tophere.removeClass("topfix")
   }
})

