jQuery(document).ready(function () {

    jQuery("#country").on("change", function () {
        jQuery("#state").empty();
        var id = jQuery(this).val();
        //jQuery("#state").append("<option value=" + "Select State" + ">" + "Select State" + "</option>");
        jQuery.ajax({
            type: "Get",
            url: "/Account/GetStates",
            dataType: "json",
            data: { CountryId: id },
            success: function (states) {
               jQuery("#state").append("<option value=" + -1 + ">" + "Select State" + "</option>");
                jQuery.each(states, function (key, state) {
                    jQuery("#state").append("<option value=" + state.value + ">" + state.text + "</option>");
                });
            }
        });
    });

    jQuery("#state").on("change", function () {
        jQuery("#city").empty();
        var id = jQuery(this).val();
        //jQuery("#state").append("<option value=" + "Select State" + ">" + "Select State" + "</option>");
        jQuery.ajax({
            type: "Get",
            url: "/Account/GetCities",
            dataType: "json",
            data: { StateId: id },
            success: function (cities) {
                jQuery("#city").append("<option value=" + "" + ">" + "Select City" + "</option>");
                jQuery.each(cities, function (key, city) {
                    jQuery("#city").append("<option value=" + city.value + ">" + city.text + "</option>");
                });
            }
        });
    });

    jQuery("#city").on("change", function () {
        jQuery("#pcode").empty();
        var id = jQuery(this).val();

        jQuery.ajax({
            type: "Get",
            url: "/Account/GetPostalcodes",
            dataType: "json",
            data: { CityId: id },
            success: function (pcodes) {
                jQuery("#pcode").append("<option value=" + ""  + ">" + "Select Postalcode" + "</option>");
                jQuery.each(pcodes, function (key, pcode) {
                    jQuery("#pcode").append("<option value=" + pcode.value + ">" + pcode.text + "</option>");
                });
            }
        });
    });

    jQuery("#btnSubmit").click(function () {

        var register = {};
        register.UserId = 1,
        register.Username = jQuery("#username").val();
        register.Gender = jQuery("#gender").val();

        register.CountryId = jQuery("#country option:selected").val();
        jQuery("#countryId").val(register.CountryId);


        register.StateId = jQuery("#state option:selected").val();
        jQuery("#stateId").val(register.StateId);

        register.CityId = jQuery("#city option:selected").val();
        jQuery("#cityId").val(register.CityId);

        register.PostalId = jQuery("#pcode option:selected").val();
        jQuery("#postalId").val(register.PostalId);

        register.Address = jQuery("#address").val();
        register.Email = jQuery("#email").val();
        register.Password = jQuery("#password").val();
        register.ConfirmPassword = jQuery("#confirmPassword").val();
       

        if (register != null) {
            jQuery.ajax({
                type: "Post",
                url: "/Account/Register",
                data: { register: JSON.stringify(register) },
                dataType: "json",

                contentType: "application/json; charset=utf-8",
                success: function () {
                    alert("Data Added!");
                }
            });
        }


    });


    jQuery("#submit").click(function () {
        var vm = {};
        vm.Id = 1;
        vm.Username = jQuery("#username option:selected").val();
        jQuery("#uid").val(vm.Username);
        vm.Rolename = jQuery("#roles option:selected").val();
        jQuery("#rid").val(vm.Rolename );

        jQuery.ajax({
            type: "Post",
            url: "/Account/CreateRole",
            data: vm,
            dataType: "json",

            contentType: "application/json; charset=utf-8",
            success: function () {
                alert("Data Added!");
            }
        });
    })

 

});
    

   

