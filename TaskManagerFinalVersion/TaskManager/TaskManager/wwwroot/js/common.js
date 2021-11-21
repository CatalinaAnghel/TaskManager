const image_handler = {
    init: function () {
        this.get_image();
    },
    get_image: function () {
        // make the AJAX request
        $.ajax({
            url: "/Home/RetrieveImage",
            type: "GET",
            success: function (data) {
                if (data != null) {
                    document.getElementById("user_image_field").src = data;
                    if (document.getElementById("current_image_field") != null) {
                        document.getElementById("current_image_field").src = data;

                    }
                }

            },
            error: function () {
                console.log("Something went wrong");
            }
        });
    }
}
$(document).ready(() => {
    image_handler.init();
});