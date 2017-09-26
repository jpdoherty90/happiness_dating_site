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

    $('.datepicker').pickadate({
        selectMonths: true, // Creates a dropdown to control month
        selectYears: 100, // Creates a dropdown of 15 years to control year,
        today: 'Today',
        clear: 'Clear',
        close: 'Ok',
        closeOnSelect: false // Close upon selecting a date,
    });
})