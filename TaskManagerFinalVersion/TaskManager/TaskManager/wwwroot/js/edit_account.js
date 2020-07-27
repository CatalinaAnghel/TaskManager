function show_email_field(){
    if(document.getElementById('email_field').style.display == 'none'){
        document.getElementById('button_show_email').className = "fa fa-close btn btn-primary";
        document.getElementById('email_field').style.display="flex";
    }else{
        document.getElementById('button_show_email').className = "fa fa-edit btn btn-primary";
        document.getElementById('email_field').style.display="none";
    }
    
}

function show_last_name_field(){
    if(document.getElementById('last_name_field').style.display == "none"){
        document.getElementById('button_show_last_name').className = "fa fa-close btn btn-primary";
        document.getElementById('last_name_field').style.display="flex";
    }else{
        document.getElementById('button_show_last_name').className = "fa fa-edit btn btn-primary";
        document.getElementById('last_name_field').style.display="none";
    }
    
}

function show_username_field() {
    if (document.getElementById('username_field').style.display == "none") {
        document.getElementById('button_show_username').className = "fa fa-close btn btn-primary";
        document.getElementById('username_field').style.display = "flex";
    } else {
        document.getElementById('button_show_username').className = "fa fa-edit btn btn-primary";
        document.getElementById('username_field').style.display = "none";
    }

}

function show_first_name_field(){
    if(document.getElementById('first_name_field').style.display == 'none'){
        document.getElementById('button_show_first_name').className = "fa fa-close btn btn-primary";
        document.getElementById('first_name_field').style.display="flex";
    }else{
        document.getElementById('button_show_first_name').className = "fa fa-edit btn btn-primary";
        document.getElementById('first_name_field').style.display="none";
    }
    
}

function show_password_field(){
    if(document.getElementById('password_field').style.display == "none" ){
        document.getElementById('button_show_password').className = "fa fa-close btn btn-primary";
        document.getElementById('password_field').style.display="flex";
    }else{
        document.getElementById('button_show_password').className = "fa fa-edit btn btn-primary";
        document.getElementById('password_field').style.display="none";
        
    }
    
}

function show_phone_number_field() {
    if (document.getElementById('phone_number_field').style.display == "none") {
        document.getElementById('button_show_phone_number').className = "fa fa-close btn btn-primary";
        document.getElementById('phone_number_field').style.display = "flex";
    } else {
        document.getElementById('button_show_phone_number').className = "fa fa-edit btn btn-primary";
        document.getElementById('phone_number_field').style.display = "none";

    }

}
