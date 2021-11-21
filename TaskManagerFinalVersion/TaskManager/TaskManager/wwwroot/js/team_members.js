const members_handler = {
    init: function () {
        self = this;
        var selector = $("#team_field");
        if (selector.length) {
            selector.on("change", () => {
                self.get_colleagues(selector);
            });
        }
    },
    get_colleagues: function (element) {
        // get the badge's id from the form
        var id = { id: element.val() };
        // make the AJAX request
        $.ajax({
            url: "/UserTeams/FindColleagues",
            type: "GET",
            data: id,
            success: function (data) {
                $("#user_field").empty();
                data.forEach(function (element) {
                    $("#user_field").append("<option>" + element.userName + "</option>");
                });
            },
            error: function () {
                $("#user_field").empty();
                console.log("Something went wrong");
            }
        });
    }
}

$(document).ready(() => {
    members_handler.init();
});