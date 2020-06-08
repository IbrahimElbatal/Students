function CustomizeDropDownList(url) {

    var governorateDDL = $('.GovernorateDropDown');
    var neighborhoodDDL = $('.NeighborhoodDropDown');
    var ddlOptions = '';
    governorateDDL.on('change',
        function() {
            var value = Number($(this).val());
            ddlOptions = '<option value>Select Neighborhood</option>';
            if (value === 1) {
                neighborhoodDDL.empty();
                $.getJSON(url,
                    function(data) {
                        $.each(data,
                            function(index, item) {
                                ddlOptions += '<option value="' + item.ID + '">' + item.Name + '</option>';
                            });
                        neighborhoodDDL.append(ddlOptions);
                    });
            } else {
                console.log("jjj");
                neighborhoodDDL.empty();
                neighborhoodDDL.append(ddlOptions);

            }
        });
}