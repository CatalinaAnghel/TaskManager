// JS function used for the logo photo on the home page => the team photo will appear
// this function will make a photo dissapear and other photo will replace it
function get_image(){
    document.getElementById("logo_image").style.display = "none";
    document.getElementById("team_image").style.display = "block";
}

// JS function used for the logo photo on the home page => the logo photo will be restored
function restore_picture(){
    document.getElementById("logo_image").style.display = "block";
    document.getElementById("team_image").style.display = "none";
}

// function used to show the quote
function show_text(){
    document.getElementById("principal_text").innerHTML= "Don't limit your challenges... Challenge your limits. :)";
}

// function used to restore the text
function hide_text(){
    document.getElementById("principal_text").innerHTML= "This is an WEB Application used to make your work easier by keeping things organized.";
}

// the function used to change a picture. This is just other version for get_image(), but here, the image source is changed
function change_picture(obj, source){
    obj.src = source;
}