(function ($) {
    var _projectDetailsService = abp.services.app.projectDetails,
        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#ProjectDetailsEditModal'),
        _$form = _$modal.find('form');

    $('.summernote').summernote();
    function save() {
        if (!_$form.valid()) {
            return;
        }

        var projectDetails = _$form.serializeFormToObject();

        abp.ui.setBusy(_$form);
        _projectDetailsService.update(projectDetails).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('projectDetails.edited', projectDetails);
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
