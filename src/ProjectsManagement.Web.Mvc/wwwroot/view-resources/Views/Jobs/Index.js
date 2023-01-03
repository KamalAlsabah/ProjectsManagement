(function ($) {
    var _jobsService = abp.services.app.jobs,
        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#JobsCreateModal'),
        _$form = _$modal.find('form'),
        _$searchForm = $('#JobsSearchForm'),
        _$table = $('#JobsTable');

    var _$jobsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _jobsService.getAll,
            inputFilter: function () {
                return _$searchForm.serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$jobsTable.draw(false)
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
                data: 'workerFullName',
                sortable: true,

            },
            {
                targets: 3,
                data: 'description',
                sortable: true,

            },
            {
                targets: 4,
                data: 'sprintName',
                sortable: true,

            },
            {
                targets: 5,
                data: 'expectedNoOfHours',
                sortable: true,

            },
            {
                targets: 6,
                data: 'actualNumberOfHours',
                sortable: true,

            },
            {
                targets: 7,
                data: 'startDate',
                sortable: true,
                render: (data, type, row, meta) => {
                    return `${data.split("T")[0]}`
                }
            },
            {
                targets: 8,
                data: 'status',
                sortable: true,

            },
            {
                targets: 9,
                data: 'endDate',
                sortable: true,
                render: (data, type, row, meta) => {
                    return `${data.split("T")[0]}`
                }
            },
            {
                targets: 10,
                data: null,
                sortable: false,
                autoWidth: false,
                width: "200px",
                className:"text-center",
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return `
                        <div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-toggle="dropdown" aria-expanded="false">
                                <i class="fa fa-cog"></i>
                          </button>
                          <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                            <li>
                                <a type="button" class="dropdown-item edit-jobs" data-jobs-id="${row.id}" data-toggle="modal" data-target="#JobsEditModal" title="Edit">
                                    <i class="fas fa-pencil-alt"></i> Edit
                                </a>
                            </li>
                             <li>
                                <a type="button" class="dropdown-item delete-jobs" data-jobs-id="${row.id}" data-jobs-name="${row.name}">
                                    <i class="fas fa-trash"></i> Delete
                                </a>
                            </li>
                            <li>
                                <a href="/JobTasks?JobsId=${row.id}" class="dropdown-item ">
                                    <i class="fas fa-list"></i> Sub Tasks
                                </a>
                            </li>
                          </ul>
                        </div>
                    `

                }
            }
        ]
    });

    $(document).on('click', '.delete-jobs', function () {
        var jobsId = $(this).attr("data-jobs-id");
        var jobsName = $(this).attr('data-jobs-name');
        deleteJobs(jobsId, jobsName);
    });

    $(document).on('click', '.edit-jobs', function (e) {
        var jobsId = $(this).attr("data-jobs-id");
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Jobs/EditModal?jobsId=' + jobsId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#JobsEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });
    $(document).on('click', '.create-jobs', function (e) {
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Jobs/CreateModal?ProjectId=' + _$searchForm.find("input[name='ProjectId']").val(),
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#JobsCreateModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });

    abp.event.on('jobs.edited', (data) => {
        _$jobsTable.ajax.reload();
    });
    abp.event.on('jobs.created', (data) => {
        _$jobsTable.ajax.reload();
    });

    function deleteJobs(jobsId, jobsName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                jobsName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _jobsService.delete({
                        id: jobsId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$jobsTable.ajax.reload();
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
        _$jobsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$jobsTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
