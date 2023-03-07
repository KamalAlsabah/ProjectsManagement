(function ($) {
    var _HomeStatisticsService = abp.services.app.homeStatistics,
        l = abp.localization.getSource('ProjectsManagement');
    alert("gg");

    if ($('#flexSwitchCheckDefault').prop('checked')) {
        alert("gg");
    }
    $("#flexSwitchCheckDefault").on('change', function () {
        if ($('#flexSwitchCheckDefault').prop('checked')) {
            alert("gg");
        }
        
    });

})(jQuery);

