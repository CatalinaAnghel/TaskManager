const tasks_handler = {
    init: function(){
        var self = this;
        var selected_task = $("#selected-task");
        if (selected_task.length) {
            selected_task.on("change", () => {
                self.get_task_details();
            });
        }
        var selected_project = $("#select-project"); console.log(selected_project.length);
        if (selected_project.length) {
            selected_project.on("change", () => {
                self.get_tasks();
            });
        }
    },
    get_task_details: function(){
        // get the project's id from the form
        var task = { id: $("#selected-task").val() };
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
    },
    get_tasks: function() {
        // get the project's id from the form
        var project = { id: $("#select-project").val() };
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
}

$(document).ready(() => {
    tasks_handler.init();
});