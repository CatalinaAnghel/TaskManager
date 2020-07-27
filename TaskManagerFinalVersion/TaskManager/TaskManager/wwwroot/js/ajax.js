function get_tasks() {
    // get the project's id from the form
    var project = { id: $("#select-project").val() };
    console.log(project);
    // make the AJAX request
    $.ajax({
        url: "../ProjectTasks/GetTasks",
        type: "GET",
        data: project,
        success: function (data) {
            $("#select_task").empty();
            data.forEach(function (element) {
                $("#select_task").append("<option>" + element.name + "</option>");
            });
            console.log(data);
        },
        error: function () {
            $("#select_task").empty();
            console.log("Something went wrong");
        }
    });

}

// function called when the user tries to edit a project
function get_project_details() {
    // get the project's id from the form
    var project = { id: $("#selected-project").val() };
    console.log(project);
    // make the AJAX request
    $.ajax({
        url: "/Projects/GetProjectDetails",
        type: "GET",
        data: project,
        success: function (data) {
            $('#name-field').val(data.name);
            var start_date = data.startDate.split("T")[0];
            $('#start-date-field').val(start_date);
            var end_date = data.endDate.split("T")[0];
            $('#end-date-field').val(end_date);
            $('#description-field').val(data.description);
            $('#worked-hours-field').val(data.workedHours);
            $('#link-field').val(data.link);
            $('#importance-field').val(data.importance);
            console.log(data);
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

function get_task_details() {
    // get the project's id from the form
    var task = { id: $("#selected-task").val() };
    console.log(task);
    // make the AJAX request
    $.ajax({
        url: "/ProjectTasks/GetTaskDetails",
        type: "GET",
        data: task,
        success: function (data) {
            $('#name-field').val(data.name);
            var due_date = data.dueDate.split("T")[0];
            $('#due-date-field').val(due_date);
            $('#description-field').val(data.description);
            $('#status-field').val(data.status);
            $('#points-field').val(data.points);
            $('#importance-field').val(data.importance);
            $('#user-field').val(data.userId);
            $('#project-field').val(data.projectId);

            console.log(data);
        },
        error: function () {
            console.log("Something went wrong");
        }
    });

}

function get_image() {
    // make the AJAX request
    $(document).ready(function () {
        $.ajax({
            url: "/Home/RetrieveImage",
            type: "GET",
            success: function (data) {
                console.log(data);
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
        
        // for the login popup
        if (document.getElementById("var_item")!= null && document.getElementById("var_item").innerText == 1) {
            document.getElementById("log_in_modal").style.display = "block";
        }

    });

}

// when the user clicks on the close button
function span_click() {
    document.getElementById("log_in_modal").style.display = "none";
}

// function called when the user tries to edit a badge
function get_badge_details() {
    // get the badge's id from the form
    var badge = { id: $("#selected-badge").val() };
    console.log(badge);
    // make the AJAX request
    $.ajax({
        url: "/Badges/GetBadgeDetails",
        type: "GET",
        data: badge,
        success: function (data) {
            $('#name_field').val(data.name);
            $('#necessary_score_field').val(data.necessaryScore);
            console.log(data);
        },
        error: function () {
            console.log("Something went wrong");
        }
    });
}

function get_colleagues(){
    // get the badge's id from the form
    var id = { id: $("#team_field").val() };
    console.log(id);
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
            console.log(data);
        },
        error: function () {
            $("#user_field").empty();
            console.log("Something went wrong");
        }
    });
}

function get_profile_image() {
    document.querySelector('input[type="file"]').addEventListener('change', function () {
        if (this.files && this.files[0]) {
            console.log(URL.createObjectURL(this.files[0]));
            document.getElementById("current_image_field").src = URL.createObjectURL(this.files[0]); // set src to blob url
        }
    });
}