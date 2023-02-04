(function ($) {
    var _projectWorkersService = abp.services.app.projectWorkers,
        _projectHistoryService = abp.services.app.projectHistory,

        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#ProjectWorkersEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var projectWorkers = _$form.serializeFormToObject();
        

        abp.ui.setBusy(_$form);
        _projectWorkersService.update(projectWorkers).done(function (projectWorkers) {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('projectWorkers.edited', projectWorkers);
            var projectId = $("#ProjectId").val();
            //Add To History 
            let history = { ProjectId: projectId, ProjectHistoryActions: 1, ProjectHistoryColumns: 4, ProjectWorkersId: projectWorkers.id };
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
