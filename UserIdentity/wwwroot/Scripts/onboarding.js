jQuery(document).ready(function () {

    var rowCount = $("#secondtable tr").length;
    $("#totalrow").val(rowCount);



    jQuery(function () {
        jQuery('.datepicker').datepicker();
    });

    jQuery("#dept").on("change", function () {

        jQuery("#sdept").empty();
        var id = jQuery(this).val();
        jQuery.ajax({
            type: "Get",
            url: "/OnBoarding/GetSubDepartments",
            dataType: "json",
            data: { DeptID: id },
            success: function (subdepts) {
                jQuery("#sdept").append("<option value=" + -1 + ">" + "Select SubDepartment" + "</option>");
                jQuery.each(subdepts, function (key, subdept) {
                    jQuery("#sdept").append("<option value=" + subdept.value + ">" + subdept.text + "</option>");
                });
            }
        });
    });

    jQuery("#onboardsubmit").click(function () {


        var onboardingRequest = {};
        onboardingRequest.EmpId = 1,
        onboardingRequest.Firstname = jQuery("#firstname").val();
        onboardingRequest.Lastname = jQuery("#lastname").val();
        onboardingRequest.Email = jQuery("#email").val();
        onboardingRequest.Mobile = jQuery("#mobile").val();
        onboardingRequest.Doj = jQuery("#doj").val();
        onboardingRequest.Designation = jQuery("#designation").val();



        onboardingRequest.BUnitId = jQuery("#bunit option:selected").val();
        jQuery("#bunitId").val(onboardingRequest.BUnitId);


        onboardingRequest.DeptID = jQuery("#dept option:selected").val();
        jQuery("#deptId").val(onboardingRequest.DeptID);

        onboardingRequest.SDeptID = jQuery("#sdept option:selected").val();
        jQuery("#sdeptId").val(onboardingRequest.DeptID);

        onboardingRequest.ReportID = jQuery("#rm option:selected").val();
        jQuery("#rmId").val(onboardingRequest.ReportID);



        if (onboardingRequest != null) {
            jQuery.ajax({
                type: "Post",
                url: "/OnBoarding/Dashboard",
                data: { onboardingRequest: JSON.stringify(onboardingRequest) },
                dataType: "json",

                contentType: "application/json; charset=utf-8",
                success: function () {
                    alert("Data Added!");
                }
            });
        }


    });

    jQuery("#onboardChecksubmit").click(function () {

       
        var onBoardCheckpoint = {};
        onBoardCheckpoint.CheckPointId = 1;

        onBoardCheckpoint.CheckPointName = jQuery("#CheckPointName").val();

        onBoardCheckpoint.BUnitId = jQuery("#bunit option:selected").val();
        jQuery("#bunitId").val(onBoardCheckpoint.BUnitId);


        onBoardCheckpoint.DeptID = jQuery("#dept option:selected").val();
        jQuery("#deptId").val(onBoardCheckpoint.DeptID);

        onBoardCheckpoint.AssigneeId = jQuery("#assignee option:selected").val();
        jQuery("#assigneeId").val(onBoardCheckpoint.AssigneeId);

        onBoardCheckpoint.Description = jQuery("#description").val();

        //  onBoardCheckpoint.IsRequired = jQuery("#required").val();


        if (onboardcheck != null) {
            jQuery.ajax({
                type: "Post",
                url: "/Admin/OnBoardingCheckPoint",
                data: { onBoardCheckpoint: JSON.stringify(onBoardCheckpoint) },
                dataType: "json",

                contentType: "application/json; charset=utf-8",
                success: function () {
                   
                    alert("Data Added!");
                }
            });
        }

    });


    //jQuery("#editsubmit").click(function () {
    //    var onBoardCheckpoint = {};
    //    onBoardCheckpoint.CheckPointId = 1;

    //    onBoardCheckpoint.CheckPointName = jQuery("#CheckPointName").val();

    //    onBoardCheckpoint.BUnitId = jQuery("#bunit option:selected").val();
    //    jQuery("#bunitId").val(onBoardCheckpoint.BUnitId);

     


    //    onBoardCheckpoint.DeptID = jQuery("#dept option:selected").val();
    //    jQuery("#deptId").val(onBoardCheckpoint.DeptID);

    //    onBoardCheckpoint.AssigneeId = jQuery("#assignee option:selected").val();
    //    jQuery("#assigneeId").val(onBoardCheckpoint.AssigneeId);

    //    onBoardCheckpoint.Description = jQuery("#description").val();

      


    //    if (onboardcheck != null) {
    //        jQuery.ajax({
    //            type: "Post",
    //            url: "/OnBoarding/AddOrEdit",
    //            data: onBoardCheckpoint,
    //            dataType: "json",

    //            contentType: "application/json; charset=utf-8",
    //            success: function () {
    //                alert("Data Added!");
    //            }
    //        });
    //    }

       
       

    //});
    jQuery("#offboardChecksubmit").click(function () {

       
        var offBoardCheckpoint = {};
        offBoardCheckpoint.CheckPointId = 1;

        offBoardCheckpoint.offCheckPointName = jQuery("#CheckPointName").val();

        offBoardCheckpoint.BUnitId = jQuery("#bunit option:selected").val();
        jQuery("#bunitId").val(offBoardCheckpoint.BUnitId);


        offBoardCheckpoint.DeptID = jQuery("#dept option:selected").val();
        jQuery("#deptId").val(offBoardCheckpoint.DeptID);

        offBoardCheckpoint.AssigneeId = jQuery("#assignee option:selected").val();
        jQuery("#assigneeId").val(offBoardCheckpoint.AssigneeId);

        offBoardCheckpoint.Description = jQuery("#description").val();

        //  onBoardCheckpoint.IsRequired = jQuery("#required").val();


        if (offBoardCheckpoint != null) {
            jQuery.ajax({
                type: "Post",
                url: "/Admin/OffBoardingCheckPoint",
                data: { offBoardCheckpoint: JSON.stringify(offBoardCheckpoint) },
                dataType: "json",

                contentType: "application/json; charset=utf-8",
                success: function () {
                    alert("Data Added!");
                }
            });
        }

    });

    jQuery("#copy").click(function () {



       //  onBoardCheckpoint.IsRequired = jQuery("#required").val();


       
            jQuery.ajax({
                type: "Post",
                url: "/OffBoarding/Copy",
              
                success: function () {
                    alert("Data Added!");
                }
            });
        

    });

})