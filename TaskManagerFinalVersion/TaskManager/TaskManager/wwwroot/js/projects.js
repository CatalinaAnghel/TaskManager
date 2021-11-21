const projects_handler = {
    init: function () {
        self = this;
        var selected_project = $("#selected-project");
        if (selected_project.length) {
            selected_project.on("change", () => {
                self.get_project_details();
            })
        }
    },
    get_project_details: function () {
        // get the project's id from the form
        var project = { id: $("#selected-project").val() };
        // make the AJAX request
        $.ajax({
            url: "/Projects/GetProjectDetails",
            type: "GET",
            data: project,
            success: function (data) {
                console.log(data);
                $('#name-field').val(data.name);
                var start_date = data.startDate.split("T")[0];
                $('#start-date-field').val(start_date);
                var end_date = data.endDate.split("T")[0];
                $('#end-date-field').val(end_date);
                $('#description-field').val(data.description);
                $('#worked-hours-field').val(data.workedHours);
                $('#link-field').val(data.link);
                $('#importance-field').val(data.importance);
                $('#difficulty-field').val(data.difficulty);
            },
            error: function () {
                console.log("Something went wrong");
            }
        });
    }
}

$(document).ready(() => {
    projects_handler.init();
});