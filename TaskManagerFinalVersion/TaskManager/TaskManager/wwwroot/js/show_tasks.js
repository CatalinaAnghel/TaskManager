function edit_visibility_tasks(id, obj) {
    if (document.getElementById(id).style.display == "none") {
        document.getElementById(id).style.display = "flex";
        obj.innerHTML = "Hide tasks";
    } else {
        document.getElementById(id).style.display = "none";
        obj.innerHTML = "Show tasks";
    }
}
