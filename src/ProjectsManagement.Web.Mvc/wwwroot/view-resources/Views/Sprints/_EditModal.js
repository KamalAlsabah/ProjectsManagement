(function ($) {
    var _sprintsService = abp.services.app.sprints,
        _projectHistoryService = abp.services.app.projectHistory,
        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#SprintsEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var sprints = _$form.serializeFormToObject();
        var projectId = $("#ProjectId").val();
        abp.ui.setBusy(_$form);
        _sprintsService.update(sprints).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('sprints.edited', sprints);
            //Add To History 
            let history = { ProjectId: projectId, ProjectHistoryActions: 1, ProjectHistoryColumns: 1, SprintId:sprints.Id };
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
