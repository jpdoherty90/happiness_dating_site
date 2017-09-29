$(document).ready(function(){

    $('select').material_select();
    $('.slider').slider();
    
    $("#viewSingles").click(function(e){
        e.preventDefault();
        $(".sexPerference").hide();
        $(".infoPerference").show();
    })

    $("#submitForm").click(function(){
        $("#userForm").submit();
    });

    $("#heightContainer").hide();
    $("#salaryContainer").hide();  
    $("#weedContainer").hide();      
    $("#bodyContainer").hide(); 
    $("#petsContainer").hide(); 
    $("#dietContainer").hide();    
    $("#kidsContainer").hide();
    $("#divorcedWidowedContainer").hide();             
    $("#ethnicityContainer").hide(); 
    $("#drinkingContainer").hide(); 
    $("#religionContainer").hide(); 
    $("#finishInterestContainer").hide(); 
                
    $("#begin").click(function(){
        $("#welcomeContainer").hide();
        $("#heightContainer").show();
    });
    $("#goToSalary").click(function(){
        $("#heightContainer").hide();
        $("#salaryContainer").show();   
        
        var sliderFormat = document.getElementById('slider-format');
        
        noUiSlider.create(sliderFormat, {
            start: [ 20000 ],
            step: 1000,
            range: {
                'min': [ 20000 ],
                'max': [ 1000000 ]
            },
            format: wNumb({
                decimals: 2,
                thousand: ',',
                prefix: '$',
            })
        });
        var inputFormat = document.getElementById('input-format');
        
        sliderFormat.noUiSlider.on('update', function( values, handle ) {
            inputFormat.value = values[handle];
        });
        
        inputFormat.addEventListener('change', function(){
            sliderFormat.noUiSlider.set(this.value);
        });
    });

    $("#goToWeed").click(function(){
        $("#salaryContainer").hide();        
        $("#weedContainer").show();      
    });
    $("#goToBody").click(function(){
        $("#weedContainer").hide();      
        $("#bodyContainer").show();          
    });
    $("#goToPets").click(function(){
        $("#bodyContainer").hide();          
        $("#petsContainer").show();          
    });
    $("#goToDiet").click(function(){
        $("#petsContainer").hide();          
        $("#dietContainer").show();          
    });
    $("#goToKids").click(function(){
        $("#dietContainer").hide();          
        $("#kidsContainer").show();          
    });
    $("#goTodivorcedWidowed").click(function(){
        $("#kidsContainer").hide();          
        $("#divorcedWidowedContainer").show();          
    });
    $("#goToEthnicity").click(function(){
        $("#divorcedWidowedContainer").hide();          
        $("#ethnicityContainer").show();          
    });
    $("#goToDrinking").click(function(){
        $("#ethnicityContainer").hide();          
        $("#drinkingContainer").show();          
    });
    $("#goToReligion").click(function(){
        $("#drinkingContainer").hide();          
        $("#religionContainer").show();          
    });
    $("#goToFinishedInterest").click(function(){
        $("#religionContainer").hide();          
        $("#finishInterestContainer").show();          
    });
    $("#submitInterestForm").click(function(){
        $("#interestForm").submit();        
    });





    $("#heightContainerP").hide();    
    $("#salaryContainerP").hide();
    $("#bodyContainerP").hide();
    $("#ethnicityContainerP").hide();
    $("#divorcedWidowedContainerP").hide();
    $("#kidsContainerP").hide();
    $("#heightContainerP").hide();
    $("#drinkingContainerP").hide();
    $("#weedContainerP").hide();
    $("#dietContainerP").hide();
    $("#petsContainerP").hide();
    $("#finishInterestContainerP").hide();
            
    
    $("#goToSalaryP").click(function(){
        $("#ageContainerP").hide();          
        $("#salaryContainerP").show(); 

        var sliderFormat = document.getElementById('slider-format');
        
        noUiSlider.create(sliderFormat, {
            start: [ 20000 ],
            step: 1000,
            range: {
                'min': [ 20000 ],
                'max': [ 1000000 ]
            },
            format: wNumb({
                decimals: 2,
                thousand: ',',
                prefix: '$',
            })
        });
        var inputFormat = document.getElementById('input-format');
        
        sliderFormat.noUiSlider.on('update', function( values, handle ) {
            inputFormat.value = values[handle];
        });
        
        inputFormat.addEventListener('change', function(){
            sliderFormat.noUiSlider.set(this.value);
        });         
    });

    $("#goToHeightP").click(function(){
        $("#salaryContainerP").hide(); 
        $("#heightContainerP").show();          
    });
    $("#goToBodyP").click(function(){
        $("#heightContainerP").hide();          
        $("#bodyContainerP").show();          
    });
    $("#goToEthnicityP").click(function(){
        $("#bodyContainerP").hide();          
        $("#ethnicityContainerP").show();          
    });
    $("#goToMaritalP").click(function(){
        $("#ethnicityContainerP").hide();          
        $("#divorcedWidowedContainerP").show();          
    });
    $("#goToKidsP").click(function(){
        $("#divorcedWidowedContainerP").hide();          
        $("#kidsContainerP").show();          
    });
    $("#goToDrinkP").click(function(){
        $("#kidsContainerP").hide();          
        $("#drinkingContainerP").show();          
    });
    $("#goToWeedP").click(function(){
        $("#drinkingContainerP").hide();          
        $("#weedContainerP").show();          
    });
    $("#goToDietP").click(function(){
        $("#weedContainerP").hide();          
        $("#dietContainerP").show();          
    });
    $("#goToPetsP").click(function(){
        $("#dietContainerP").hide();          
        $("#petsContainerP").show();          
    });
    $("#goToSubmitP").click(function(){
        $("#petsContainerP").hide();          
        $("#finishInterestContainerP").show();          
    });
    $("#submitInterestFormP").click(function(){
        $("#preferencesForm").submit();                
    });
    $("#update-btn").click(function(){
        $(this).delay(500).hide(0);
        $("#shh").delay(500).show(0);
    });

    $(".lover-cube").hover(function(){
        $(this).addClass("lover-hover");
        $(this).removeClass("lover-cube");
    }, function(){
        $(this).addClass("lover-cube");
        $(this).removeClass("lover-hover");
    });
})
