(function ($) {
    var _workersHistoryService = abp.services.app.workersHistory,
        l = abp.localization.getSource('ProjectsManagement');

    _workersHistoryService.isUserOnline().done(function (result) {
        if (result == true)
            $(".form-check-input").attr('checked', true);

        else
            $(".form-check-input").attr('checked', false);
    });
    $("#flexSwitchCheckDefault").on('change', function () {
        if ($('#flexSwitchCheckDefault').prop('checked')) {
            _workersHistoryService.createHistory(true).done(function () { });

        } else {
            _workersHistoryService.createHistory(false).done(function () { });
        }
    });

})(jQuery);

