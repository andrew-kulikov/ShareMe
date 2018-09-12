var PhotoDetailsController = function (followingService) {
    var button;

    var done = function () {
        var text = button.text() === "Following" ? "Follow" : "Following";

        button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    };

    var fail = function () {
        alert("Something failed!");
    };

    var toggleFollowing = function (e) {
        button = $(e.target);

        var userId = button.attr("data-user-id");

        if (button.hasClass("btn-info"))
            followingService.follow(userId, done, fail);
        else
            followingService.unfollow(userId, done, fail);
    };

    var init = function () {
        $(".js-toggle-following").click(toggleFollowing);
    };

    return {
        init: init
    };
}(FollowingsService);