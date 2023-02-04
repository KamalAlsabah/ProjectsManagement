﻿(function ($) {
    var _jobsService = abp.services.app.jobs,
        _projectHistoryService = abp.services.app.projectHistory,

        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#JobsCreateModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }
        var jobs = _$form.serializeFormToObject();
        abp.ui.setBusy(_$form);
        _jobsService.create(jobs).done(function (jobs) {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('jobs.created', jobs);

            var projectId = $("#ProjectId").val();
            //Add To History 
            let history = { ProjectId: projectId, ProjectHistoryActions: 0, ProjectHistoryColumns: 2 ,JobId: jobs.id };
            _projectHistoryService.create(history).done(function () { });

        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

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
