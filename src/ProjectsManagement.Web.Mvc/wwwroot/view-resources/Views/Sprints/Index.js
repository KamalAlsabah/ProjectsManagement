(function ($) {
    var _sprintsService = abp.services.app.sprints,
        _projectHistoryService = abp.services.app.projectHistory,

        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#SprintsCreateModal'),
        _$form = _$modal.find('form'),
        _$searchForm = $('#SprintsSearchForm'),
        _$table = $('#SprintsTable');

    var _$sprintsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _sprintsService.getAll,
            inputFilter: function () {
                return _$searchForm.serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$sprintsTable.draw(false)
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
                data: 'name',
                sortable: true,
                
            },
            {
                targets: 2,
                data: 'projectName',
                sortable: true,
                
            },
            {
                targets: 3,
                data: 'description',
                sortable: true,
                
            },
            {
                targets: 4,
                data: 'status',
                sortable: true,
                
            },
            {
                targets: 5,
                data: 'startDate',
                sortable: true,
                render: (data, type, row, meta) => {
                    return `${data.split("T")[0]}`
                }
                
            },
            {
                targets: 6,
                data: 'expectedEndDate',
                sortable: true,
                render: (data, type, row, meta) => {
                    return `${data.split("T")[0]}`
                }
                
            },
            {
                targets:7 ,
                data: 'endDate',
                sortable: true,
                render: (data, type, row, meta) => {
                    return `${data.split("T")[0]}`
                }
                
            },
            {
                targets:8 ,
                data: 'wieghtOfHours',
                sortable: true,
            },
            {
                targets:9,
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
                                <a type="button" class="dropdown-item edit-sprints"  data-sprints-id="${row.id}" data-toggle="modal" data-target="#SprintsEditModal" title="Edit">
                                    <i class="fas fa-pencil-alt"></i> Edit
                                </a>
                            </li>
                             <li>
                                <a type="button" class="dropdown-item delete-sprints" data-sprints-id="${row.id}" data-sprints-name="${row.name}" title="Delete">
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

    $(document).on('click', '.delete-sprints', function () {
        var sprintsId = $(this).attr("data-sprints-id");
        var sprintsName = $(this).attr('data-sprints-name');
        deleteSprints(sprintsId, sprintsName);
    });

    $(document).on('click', '.edit-sprints', function (e) {
        var sprintsId = $(this).attr("data-sprints-id");
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Sprints/EditModal?sprintsId=' + sprintsId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#SprintsEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });
    $(document).on('click', '.create-sprints', function (e) {
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Sprints/CreateModal?ProjectId=' + _$searchForm.find("input[name='ProjectId']").val(),
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#SprintsCreateModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });

    abp.event.on('sprints.edited', (data) => {
        _$sprintsTable.ajax.reload();
    });
    abp.event.on('sprints.created', (data) => {
        _$sprintsTable.ajax.reload();
    });

    function deleteSprints(sprintsId, sprintsName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                sprintsName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _sprintsService.delete({
                        id: sprintsId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$sprintsTable.ajax.reload();
                        //Add To History 
                        var projectId = $("#ProjectId").val();
                        let history = { ProjectId: projectId, ProjectHistoryActions: 2, ProjectHistoryColumns: 1, SprintId: sprintsId};
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
        _$sprintsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$sprintsTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
