$(document).ready(function(){

    $('select').material_select();

    $("#viewSingles").click(function(e){
        e.preventDefault();
        $(".sexPerference").hide();
        $(".infoPerference").show();
    })

    $("#submitForm").click(function(){
        $("#userForm").submit();
    });
})