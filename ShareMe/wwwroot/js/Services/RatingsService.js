var RatingsService = function () {
    var like = function (photoId, done, fail) {
        $.post("/api/photos/like/" + photoId)
            .done(done)
            .fail(fail);
    };

    var dislike = function (photoId, done, fail) {
        $.ajax({
                url: "/api/photos/dislike/" + photoId,
                method: "DELETE"
            })
            .done(done)
            .fail(fail);
    };

    return {
        like: like,
        dislike: dislike
    };
}();