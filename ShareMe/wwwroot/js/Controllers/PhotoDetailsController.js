var PhotoDetailsController = function (followingService, ratingService) {
    var button;
    var likeImg;
    var photoId;

    var doneFollowing = function () {
        var text = button.text().trim() === "Following" ? "Follow" : "Following";

        button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    };

    var doneLike = function() {
        var count = parseInt($("#likes").html());
        $("#likes").html(count + 1);
        $(likeImg).attr("liked", "1");
        $(likeImg).css("color", "red");
    };

    var doneDislike = function() {
        var count = parseInt($("#likes").html());
        $("#likes").html(count - 1);
        $(likeImg).removeAttr("liked");
        $(likeImg).css("color", "black");
    }

    var showLikers = function (response) {
        $("#modal-content").html(response);
        $("#myModal").modal("show");
    };

    var fail = function () {
        alert("Something failed!");
    };

    var toggleFollowing = function (e) {
        button = $(e.target);

        var userId = button.attr("data-user-id");

        if (button.hasClass("btn-info"))
            followingService.follow(userId, doneFollowing, fail);
        else
            followingService.unfollow(userId, doneFollowing, fail);
    };

    var toggleLike = function () {
        likeImg = $("#like-img");
        if ($(likeImg)[0].hasAttribute("liked")) 
            ratingService.dislike(photoId, doneDislike, fail);
        else 
            ratingService.like(photoId, doneLike, fail);
    };

    var getLikes = function () {
        ratingService.getLikers(photoId, showLikers, fail);
    };

    var init = function () {
        photoId = $("#data").attr("data-photo-id");
        $(".js-toggle-following").click(toggleFollowing);
        $("#like").click(toggleLike);
        $("#likes").click(getLikes);
    };

    return {
        init: init
    };
}(FollowingsService, RatingsService);