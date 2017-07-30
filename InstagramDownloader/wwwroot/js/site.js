$(function () {
    $('#downloadCarouselButton').on('click',
        function () {
            var carousel = $(this).data('carousel');
            var requestData = JSON.stringify({
                MediaFiles: carousel
            });

            $.ajax({
                method: "POST",
                url: "/Media/SaveCarousel",
                data: requestData,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    window.location = "/Media/DownloadCarousel?name=" + data.name;
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });
});
