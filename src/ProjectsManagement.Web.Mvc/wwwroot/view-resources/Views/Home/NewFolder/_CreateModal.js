﻿(function ($) {

    var _HomeStatisticsService = abp.services.app.homeStatistics,
        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#HomeStatisticsCreateModal'),
        _$form = _$modal.find('form');


    function save() {
        if (!_$form.valid()) {
            return;
        }
        var homeStatistics = _$form.serializeFormToObject();
        abp.ui.setBusy(_$form);
        _HomeStatisticsService.create(homeStatistics).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
             abp.event.trigger('homeStatistics.created', homeStatistics);
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    // select2
    $('.multipleSelectExample').select2({
        placeholder: '--Select UserType--',
        dropdownParent: $("#HomeStatisticsCreateModal")
    });

    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });
})(jQuery);
