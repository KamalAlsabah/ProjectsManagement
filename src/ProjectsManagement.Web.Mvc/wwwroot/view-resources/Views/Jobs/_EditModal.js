(function ($) {
    var _jobsService = abp.services.app.jobs,
        _projectHistoryService = abp.services.app.projectHistory,
        _workeraJobService = abp.services.app.workersJobs,

        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#JobsEditModal'),
        _$form = _$modal.find('form');
    var intArray = [];

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var jobs = _$form.serializeFormToObject();
        
        abp.ui.setBusy(_$form);
        _jobsService.update(jobs).done(function (jobs) {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('jobs.edited', jobs);
            SaveWorkerJob();
            _workeraJobService.createJobWorkers(jobs.id, intArray);

            var projectId = $("#ProjectId").val();
            //Add To History 
            let history = { ProjectId: projectId, ProjectHistoryActions: 1, ProjectHistoryColumns: 2, JobId: jobs.id  };
            _projectHistoryService.create(history).done(function () { });

        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }
    // select2
    $('.multipleSelectExample').select2({
        placeholder: '--Select User--',
        dropdownParent: $("#JobsEditModal")
    });
    // Create  workerjob
    function SaveWorkerJob() {
        var options = new Array();
        $('.multipleSelectExample > option:selected').each(
            function (i) {
                options[i] = $(this).val();
            });
        intArray = options;
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
