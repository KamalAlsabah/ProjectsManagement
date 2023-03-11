(function ($) {
    var _workersDashboardService = abp.services.app.workersDashboard,
        _projectHistoryService = abp.services.app.projectHistory,

        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#WorkersDashboardsCreateModal'),
        _$form = _$modal.find('form'),
        _$searchForm = $('#WorkersDashboardsSearchForm'),
        _$table = $('#WorkersDashboardsTable');

    var _$workersDashboardTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _workersDashboardService.getAll,
            inputFilter: function () {
                return _$searchForm.serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$workersDashboardTable.draw(false)
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
                data: 'workerJobsCount',
                sortable: true,

            },
            {
                targets: 3,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return `
                        <div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-toggle="dropdown" aria-expanded="false">
                                <i class="fa fa-cog"></i>
                          </button>
                          <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                            <li>
                                <a type="button" class="dropdown-item edit-workersDashboard"  data-workersDashboard-id="${row.id}" data-toggle="modal" data-target="#WorkersDashboardsEditModal" title="Edit">
                                    <i class="fas fa-pencil-alt"></i> Edit
                                </a>
                            </li>
                             <li>
                                <a type="button" class="dropdown-item delete-workersDashboard" data-workersDashboard-id="${row.id}" data-workersDashboard-name="${row.name}" title="Delete">
                                    <i class="fas fa-trash"></i> Delete
                                </a>
                            </li>
                          </ul>
                        </div>
                  `
                }
            }
        ]
    });

    $(document).on('click', '.delete-workersDashboard', function () {
        var workersDashboardId = $(this).attr("data-workersDashboard-id");
        var workersDashboardName = $(this).attr('data-workersDashboard-name');
        deleteWorkersDashboards(workersDashboardId, workersDashboardName);
    });

    $(document).on('click', '.edit-workersDashboard', function (e) {
        var workersDashboardId = $(this).attr("data-workersDashboard-id");
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'WorkersDashboards/EditModal?workersDashboardId=' + workersDashboardId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#WorkersDashboardsEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });
    $(document).on('click', '.create-workersDashboard', function (e) {
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'WorkersDashboards/CreateModal?ProjectId=' + _$searchForm.find("input[name='ProjectId']").val(),
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#WorkersDashboardsCreateModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });

    abp.event.on('workersDashboard.edited', (data) => {
        _$workersDashboardTable.ajax.reload();
    });
    abp.event.on('workersDashboard.created', (data) => {
        _$workersDashboardTable.ajax.reload();
    });

    function deleteWorkersDashboards(workersDashboardId, workersDashboardName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                workersDashboardName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _workersDashboardService.delete({
                        id: workersDashboardId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$workersDashboardTable.ajax.reload();
                        //Add To History 
                        var projectId = $("#ProjectId").val();
                        let history = { ProjectId: projectId, ProjectHistoryActions: 2, ProjectHistoryColumns: 1, WorkersDashboardId: workersDashboardId };
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
        _$workersDashboardTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$workersDashboardTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
