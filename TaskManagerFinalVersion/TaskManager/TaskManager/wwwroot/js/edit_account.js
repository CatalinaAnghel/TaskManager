const account_image_handler = {
    init: function () {
        this.get_profile_image();
    },
    get_profile_image: function () {
        $("#image_field").on("change", function () {
            if (this.files && this.files[0]) {
                document.getElementById("current_image_field").src = URL.createObjectURL(this.files[0]); // set src to blob url
            }
        });
    }
}

$(document).ready(() => {
    account_image_handler.init();
});