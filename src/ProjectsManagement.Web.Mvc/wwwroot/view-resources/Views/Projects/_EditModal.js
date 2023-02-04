(function ($) {
    var _projectsService = abp.services.app.projects,
        _projectHistoryService = abp.services.app.projectHistory,
        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#ProjectsEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var projects = _$form.serializeFormToObject();
        abp.ui.setBusy(_$form);
        _projectsService.update(projects).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('projects.edited', projects);
            //Add To History 
            let history = { ProjectId: projects.Id, ProjectHistoryActions: 1, ProjectHistoryColumns:0 };
            _projectHistoryService.create(history).done(function () {});

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
