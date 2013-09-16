$(function() {
    $('a[rel*=lightbox]').lightBox({
        overlayBgColor: '#000',
        overlayOpacity: 0.7,
        imageLoading: '/images/lightbox/lightbox-ico-loading.gif',
        imageBtnClose: '/images/lightbox/lightbox-btn-close.gif',
        imageBtnPrev: '/images/lightbox/lightbox-btn-prev.gif',
        imageBtnNext: '/images/lightbox/lightbox-btn-next.gif',
        imageBlank: '/images/lightbox/lightbox-blank.gif',
        containerResizeSpeed: 350
    });


    $(".tab").click(function() {
        var TabName = $(this).attr("id");
        LoadRendering("Ecommerce/Examples/ProductTab" + TabName, $(".tabContent"));
        SetCurrentTab(TabName);
        $("#li_Specifications,#li_Accessories,#li_Resources,#li_Reviews").removeClass("current");
        $("#li_" + TabName).addClass("current");
        return false;
    });
});

function rating(id) {
    rate = parseInt(id.charAt(4));
    document.getElementById("review_rate").value = rate;
    for (i = 1; i <= rate; i++) {
        document.getElementById("rate" + i).className = "scoreSelected";
    }
    for (i = rate + 1; i <= 5; i++) {
        document.getElementById("rate" + i).className = "score";
    }
}