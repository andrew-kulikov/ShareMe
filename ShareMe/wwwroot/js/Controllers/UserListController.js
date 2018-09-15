var UserListController = function (followingService) {
    var button;

    var doneFollowing = function () {
        var text = button.text().trim() === "Following" ? "Follow" : "Following";

        button.toggleClass("btn-default").toggleClass("btn-info").text(text);
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

    var init = function () {
        $(".js-toggle-following").click(toggleFollowing);
    };

    return {
        init: init
    };
}(FollowingsService);