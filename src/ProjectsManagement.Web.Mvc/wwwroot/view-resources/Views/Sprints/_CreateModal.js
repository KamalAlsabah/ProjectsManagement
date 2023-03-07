(function ($) {
    var _sprintsService = abp.services.app.sprints,
        _projectHistoryService = abp.services.app.projectHistory,
        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#SprintsCreateModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }
        var sprints = _$form.serializeFormToObject();
        var projectId = $("#ProjectId").val();
        abp.ui.setBusy(_$form);
        _sprintsService.create(sprints).done(function (sprints) {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('sprints.created', sprints);
            //Add To History 
            let history = { ProjectId: projectId, ProjectHistoryActions: 0, ProjectHistoryColumns: 1, SprintId: sprints.id };
            _projectHistoryService.create(history).done(function () { });

        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }
    //$('#StartDate').on('change', function () { compareDates(); });
    //$('#ExpectedEndDate').on('change', function () { compareDates(); });
    //function compareDates() {
    //    //Get the text in the elements
    //    var from = document.getElementById('StartDate').textContent;
    //    var to = document.getElementById('ExpectedEndDate').textContent;

    //    //Generate an array where the first element is the year, second is month and third is day
    //    var splitFrom = from.split('/');
    //    var splitTo = to.split('/');

    //    //Create a date object from the arrays
    //    var fromDate = Date.parse(splitFrom[0], splitFrom[1] - 1, splitFrom[2]);
    //    var toDate = Date.parse(splitTo[0], splitTo[1] - 1, splitTo[2]);
    //    if (fromDate > toDate) {
    //        $('#Message').textContent('err');
    //    }
    //    //Return the result of the comparison
    //    return fromDate < toDate;
    //}
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
