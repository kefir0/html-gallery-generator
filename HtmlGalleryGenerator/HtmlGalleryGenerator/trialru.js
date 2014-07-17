$(document).ready(function () {
    AdjustAllPictures();
    $("img.photo3").click(function () {
        var maxHeightCss = $(this).css("max-height");
        if (maxHeightCss != "none") {
            ClearSize(this);
        }
        else {
            AdjustSize(this);
        }
    });
});

$(window).resize(function () {
    AdjustAllPictures();
});

function AdjustAllPictures() {
    $("img.photo3").each(function (index, element) {
        AdjustSize(element);
    });
}

function AdjustSize(element) {
    $(element).css("max-height", $(window).height() - 60);
    $(element).css("max-width", $(window).width() - 20);
}

function ClearSize(element) {
    $(element).css("max-height", "");
    $(element).css("max-width", "");
}
