﻿(function ($) {
    var _jobsService = abp.services.app.jobs,
        _projectHistoryService = abp.services.app.projectHistory,
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
                data: 'description',
                sortable: true,

            },
            {
                targets: 3,
                data: 'sprintName',
                sortable: true,

            },
            {
                targets: 4,
                data: 'jobWorkers',
                sortable: true,
                render: (data, type, row, meta) => {
                    
                    let temp = ""

                    data.map(x => temp += `${x},`)
                    return temp;
                }
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
                data: 'wieghtOfHours',
                sortable: true,

            },
            {
                targets: 8,
                data: 'startDate',
                sortable: true,
                render: (data, type, row, meta) => {
                    return `${data.split("T")[0]}`
                }
            },
            {
                targets: 9,
                data: 'status',
                sortable: true,

            },
            {
                targets: 10,
                data: 'endDate',
                sortable: true,
                render: (data, type, row, meta) => {
                    return `${data.split("T")[0]}`
                }
            },
            {
                targets: 11,
                data: null,
                sortable: false,
                autoWidth: false,
                width: "200px",
                className:"text-center",
                defaultContent: '',
                render: (data, type, row, meta) => {
                    if (IsSupervisor) {
                        return `
                        <div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-toggle="dropdown" aria-expanded="false">
                                <i class="fa fa-cog"></i>
                          </button>
                          <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                             <li>
                                <a type="button" class="dropdown-item create-jobNote" data-project-id="${row.projectId}" data-jobs-id="${row.id}" data-toggle="modal" data-target="#SupervisorNotesCreateModal" title="Note">
                                    <i class="fas fa-pencil-alt"></i> Add Note
                                </a>
                            </li>
                            <li>
                                <a href="/JobTasks?JobsId=${row.id}&ProjectId=${row.projectId}" class="dropdown-item ">
                                    <i class="fas fa-list"></i> Sub Tasks
                                </a>
                            </li>
                          </ul>
                        </div>
                    `
                    } else {
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
                                <a href="/JobTasks?JobsId=${row.id}&ProjectId=${row.projectId}" class="dropdown-item ">
                                    <i class="fas fa-list"></i> Sub Tasks
                                </a>
                            </li>
                          </ul>
                        </div>
                    `
                    }
                }
            }
        ]
    });
    console.log(_$jobsTable);
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

    //To Create Supervisotr Note 
    $(document).on('click', '.create-jobNote', function (e) {
        e.preventDefault();
        var jobsId = $(this).attr("data-jobs-id");
        var ProjectId = $(this).attr("data-project-id");
        abp.ajax({
            url: abp.appPath + `Jobs/SupervisorNotesCreateModal?JobId=${jobsId}${ProjectId}`,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#SupervisorNotesCreateModal div.modal-content').html(content);

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
                        var projectId = $("#ProjectId").val();
                        //Add To History 
                        let history = { ProjectId: projectId, ProjectHistoryActions: 2, ProjectHistoryColumns: 2, JobId: jobsId };
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
        _$jobsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$jobsTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
