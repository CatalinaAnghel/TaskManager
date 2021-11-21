const badges_handler = {
    init: function () {
        this.get_badge_details();
    },
    get_badge_details: function () {
        var badge_element = $("#selected-badge");
        if (badge_element.length) {
            // get the badge's id from the form
            badge_element.on("change", () => {
                var badge = { id: $("#selected-badge").val() };
                // make the AJAX request
                $.ajax({
                    url: "/Badges/GetBadgeDetails",
                    type: "GET",
                    data: badge,
                    success: function (data) {
                        $('#name_field').val(data.name);
                        $('#necessary_score_field').val(data.necessaryScore);
                    },
                    error: function () {
                        console.log("Something went wrong");
                    }
                });
            });
        }
    }
}

$(document).ready(() => {
    badges_handler.init();
});