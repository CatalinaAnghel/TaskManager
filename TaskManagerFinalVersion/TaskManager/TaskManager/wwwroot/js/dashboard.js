const dashboard_events_handler = {
    init: function () {
        self = this;
        var close_button = $("#modal-close-button");
        if (close_button.length) {
            close_button.on("click", () => {
                self.add_modal_event();
            });
        }

        var tasks_button = $("#tasks-button");
        if (tasks_button.length) {
            tasks_button.on("click", () => {
                self.edit_visibility_tasks("info", tasks_button);
            });
        }
        self.display_login_modal();
    },
    edit_visibility_tasks: function (id, obj) {
        if (document.getElementById(id).style.display == "none") {
            document.getElementById(id).style.display = "flex";
            obj.innerHTML = "Hide tasks";
        } else {
            document.getElementById(id).style.display = "none";
            obj.innerHTML = "Show tasks";
        }
    },
    add_modal_event: function (modal) {
        $("#log_in_modal").hide();
    },
    display_login_modal: function () {
        // for the login popup
        if (document.getElementById("var_item") != null &&
            document.getElementById("var_item").innerText == 1) {
            document.getElementById("log_in_modal").style.display = "block";
        }
    }
}

$(document).ready(() => {
    dashboard_events_handler.init();
});