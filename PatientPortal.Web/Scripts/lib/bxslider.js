$(document).ready(function(){
var $j = jQuery.noConflict();

var realSlider= $j("ul#bxslider").bxSlider({
      speed:1000,
      pager:false,
      nextText:'',
      prevText:'',
      infiniteLoop:false,
      hideControlOnEnd:true,
      onSlideBefore:function($slideElement, oldIndex, newIndex){
        changeRealThumb(realThumbSlider,newIndex);
        
      }
      
    });
    
    var realThumbSlider=$j("ul#bxslider-pager").bxSlider({
      minSlides: 4,
      maxSlides: 4,
      slideWidth: 156,
      slideMargin: 12,
      moveSlides: 1,
      pager:false,
      speed:1000,
      infiniteLoop:false,
      hideControlOnEnd:true,
      nextText:'<span></span>',
      prevText:'<span></span>',
      onSlideBefore:function($slideElement, oldIndex, newIndex){
        /*$j("#sliderThumbReal ul .active").removeClass("active");
        $slideElement.addClass("active"); */

      }
    });
    
    linkRealSliders(realSlider,realThumbSlider);
    
    if($j("#bxslider-pager li").length<5){
      $j("#bxslider-pager .bx-next").hide();
    }

// sincronizza sliders realizzazioni
function linkRealSliders(bigS,thumbS){
  
  $j("ul#bxslider-pager").on("click","a",function(event){
    event.preventDefault();
    var newIndex=$j(this).parent().attr("data-slideIndex");
        bigS.goToSlide(newIndex);
  });
}

//slider!=$thumbSlider. slider is the realslider
function changeRealThumb(slider,newIndex){
  
  var $thumbS=$j("#bxslider-pager");
  $thumbS.find('.active').removeClass("active");
  $thumbS.find('li[data-slideIndex="'+newIndex+'"]').addClass("active");
  
  if(slider.getSlideCount()-newIndex>=4)slider.goToSlide(newIndex);
  else slider.goToSlide(slider.getSlideCount()-4);

}
        
});