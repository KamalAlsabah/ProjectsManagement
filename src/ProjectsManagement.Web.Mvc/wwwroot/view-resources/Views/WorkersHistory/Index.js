(function ($) {
    var _workersHistorysService = abp.services.app.workersHistory,
        _projectHistoryService = abp.services.app.projectHistory,
        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#WorkersHistoryCreateModal'),
        _$form = _$modal.find('form'),
        _$searchForm = $('#WorkersHistorySearchForm'),
        _$table = $('#WorkersHistoryTable');

    var _$workersHistorysTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _workersHistorysService.getAll,
            inputFilter: function () {
                return _$searchForm.serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$workersHistorysTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [
            {
                targets: 0,
                className: 'control',
                defaultContent: '',
            },
            {
                targets: 1,
                data: 'workerName',
                sortable: true,

            },
            {
                targets: 2,
                data: 'totalHours',
                sortable: true,

            },
            {
                targets: 3,
                data: null,
                sortable: false,
                autoWidth: false,
                width: "200px",
                className: "text-center",
                defaultContent: '',
                render: (data, type, row, meta) => {
                        return `
                        <div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-toggle="dropdown" aria-expanded="false">
                                <i class="fa fa-cog"></i>
                          </button>
                          <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                           
                          
                          </ul>
                        </div>
                    `
                    }
                
            }
        ]
    });

    $(document).on('click', '.delete-workersHistorys', function () {
        var workersHistorysId = $(this).attr("data-workersHistorys-id");
        var workersHistorysName = $(this).attr('data-workersHistorys-name');
        deleteWorkersHistory(workersHistorysId, workersHistorysName);
    });

    $(document).on('click', '.edit-workersHistorys', function (e) {
        var workersHistorysId = $(this).attr("data-workersHistorys-id");
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'WorkersHistory/EditModal?workersHistorysId=' + workersHistorysId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#WorkersHistoryEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });
    $(document).on('click', '.create-workersHistorys', function (e) {
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'WorkersHistory/CreateModal?ProjectId=' + _$searchForm.find("input[name='ProjectId']").val(),
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#WorkersHistoryCreateModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });

  

    abp.event.on('workersHistorys.edited', (data) => {
        _$workersHistorysTable.ajax.reload();
    });
    abp.event.on('workersHistorys.created', (data) => {
        _$workersHistorysTable.ajax.reload();
    });

    function deleteWorkersHistory(workersHistorysId, workersHistorysName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                workersHistorysName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _workersHistorysService.delete({
                        id: workersHistorysId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$workersHistorysTable.ajax.reload();
                        var projectId = $("#ProjectId").val();
                        //Add To History 
                        let history = { ProjectId: projectId, ProjectHistoryActions: 2, ProjectHistoryColumns: 2, WorkersHistoryId: workersHistorysId };
                        _projectHistoryService.create(history).done(function () { });

                    });
                }
            }
        );
    }

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$workersHistorysTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$workersHistorysTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
