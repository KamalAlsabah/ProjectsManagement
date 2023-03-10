(function ($) {
    var _jobTasksService = abp.services.app.jobTasks,
        _projectHistoryService = abp.services.app.projectHistory,

        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#JobTasksCreateModal'),
        _$form = _$modal.find('form'),
        _$searchForm = $('#JobTasksSearchForm'),
        _$table = $('#JobTasksTable');

    var _$jobTasksTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _jobTasksService.getAll,
            inputFilter: function () {
                return _$searchForm.serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$jobTasksTable.draw(false)
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
                data: 'jobName',
                sortable: true,
                
            },
            {
                targets: 4,
                data: 'jobTaskStatus',
                sortable: true,
                
            },
            {
                targets: 5,
                data: 'creationTime',
                sortable: true,
                render: (data, type, row, meta) => {
                    return `${data.split("T")[0]}`
                }
            },
            {
                targets:6,
                data: null,
                sortable: false,
                autoWidth: false,
                width: "200px",
                className: "text-center",
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
                                <a type="button" class="dropdown-item create-jobtaskNote" data-jobTasks-id="${row.id}"data-jobs-id="${row.jobId}"  data-toggle="modal" data-target="#SupervisorNotesCreateModal" title="Note">
                                    <i class="fas fa-pencil-alt"></i> Add Note
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
                                <a type="button" class="dropdown-item edit-jobTasks" data-jobTasks-id="${row.id}" data-toggle="modal" data-target="#JobTasksEditModal" title="Edit">
                                    <i class="fas fa-pencil-alt"></i> Edit
                                </a>
                            </li>
                             <li>
                                <a type="button" class="dropdown-item delete-jobTasks" data-jobTasks-id="${row.id}" data-jobTasks-name="${row.name}">
                                    <i class="fas fa-trash"></i> Delete
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

    $(document).on('click', '.delete-jobTasks', function () {
        var jobTasksId = $(this).attr("data-jobTasks-id");
        var jobTasksName = $(this).attr('data-jobTasks-name');
        deleteJobs(jobTasksId, jobTasksName);
    });

    $(document).on('click', '.edit-jobTasks', function (e) {
        var jobTasksId = $(this).attr("data-jobTasks-id");
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'JobTasks/EditModal?jobTasksId=' + jobTasksId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#JobTasksEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });
    $(document).on('click', '.create-jobTasks', function (e) {
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'JobTasks/CreateModal?JobsId=' + _$searchForm.find("input[name='JobId']").val(),
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#JobTasksCreateModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });

    $(document).on('click', '.create-jobtaskNote', function (e) {
        e.preventDefault();
        var jobTasksId = $(this).attr("data-jobTasks-id");
        var jobId = $(this).attr("data-jobs-id");
        abp.ajax({
            url: abp.appPath + `JobTasks/SupervisorNotesCreateModal?JobTaskId=${jobTasksId}&JobId=${jobId}`,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#SupervisorNotesCreateModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });

    abp.event.on('jobTasks.edited', (data) => {
        _$jobTasksTable.ajax.reload();
    });
    abp.event.on('jobTasks.created', (data) => {
        _$jobTasksTable.ajax.reload();
    });

    function deleteJobs(jobTasksId, jobTasksName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                jobTasksName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _jobTasksService.delete({
                        id: jobTasksId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$jobTasksTable.ajax.reload();

                        var projectId = $("#ProjectId").val();
                        //Add To History 
                        let history = { ProjectId: projectId, ProjectHistoryActions: 2, ProjectHistoryColumns: 3, JobTasksId: jobTasksId };
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
        _$jobTasksTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$jobTasksTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
