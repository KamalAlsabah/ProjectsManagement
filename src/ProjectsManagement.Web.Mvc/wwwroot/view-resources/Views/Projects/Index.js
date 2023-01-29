(function ($) {
    var _projectsService = abp.services.app.projects,
        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#ProjectsCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#ProjectsTable');

    var _$projectssTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _projectsService.getAll,
            inputFilter: function () {
                return $('#ProjectssSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$projectssTable.draw(false)
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
                sortable: false
            },
            {
                targets: 2,
                data: 'description',
                sortable: false
            },
            {
                targets: 3,
                data: 'startDate',
                sortable: false,
                render: (data, type, row, meta) => {
                    return `${data.split("T")[0]}`
                }
            },
            {
                targets: 4,
                data: 'endDate',
                sortable: false,
                render: (data, type, row, meta) => {
                    return `${data.split("T")[0]}`
                }
            },
            {
                targets: 5,
                data: 'status',
                sortable: false
            },
            {
                targets: 6,
                data: 'testUrl',
                sortable: false
            },
            {
                targets: 7,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                width: "200px",
                className: "text-center",
                render: (data, type, row, meta) => {
                    if (IsAdmin) {
                        return `
                        <div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-toggle="dropdown" aria-expanded="false">
                                <i class="fa fa-cog"></i>
                          </button>
                          <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                            <li>
                                <a type="button" class="dropdown-item edit-projects" data-projects-id="${row.id}" data-toggle="modal" data-target="#ProjectsEditModal" title="Edit">
                                    <i class="fas fa-pencil-alt"></i> Edit
                                </a>
                            </li>
                            <li>
                                <a type="button" class="dropdown-item delete-projects" data-projects-id="${row.id}" data-projects-name="${row.name}" title="Delete">
                                    <i class="fas fa-trash"></i> Delete
                                </a>
                            </li>
                            <li>
                                <a href="/Jobs/Index?projectId=${row.id}" class=" dropdown-item" title="Jobs" >
                                    <i class="fas fa-list"></i>  Jobs
                                </a>
                            </li>
                            <li>
                                <a href="/Sprints/Index?projectId=${row.id}" class="dropdown-item" title="Sprints">
                                    <i class="fas fa-circle-notch"></i> Sprints
                                </a>
                            </li>
                            <li>
                                <a href="/ProjectWorkers/Index?projectId=${row.id}" class="dropdown-item" title="Workers">
                                    <i class="fas fa-user"></i> Workers
                                </a>
                            </li>
                            <li>
                                <a href="/ProjectSupervisor?projectid=${row.id}" class="dropdown-item" title="Supervisor">
                                  <i class="fas fa-users"></i> Supervisor
                                </a>
                            </li>
                          </ul>
                        </div>
                    `
                    }
                    else if(IsWorker){
                        return `
                        <div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-toggle="dropdown" aria-expanded="false">
                                <i class="fa fa-cog"></i>
                          </button>
                          <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                            <li>
                                <a href="/Jobs/Index?projectId=${row.id}" class=" dropdown-item" title="Jobs" >
                                    <i class="fas fa-list"></i>  Jobs
                                </a>
                            </li>
                            <li>
                                <a href="/ProjectWorkers/Index?projectId=${row.id}" class="dropdown-item" title="Workers">
                                    <i class="fas fa-user"></i> Workers
                                </a>
                            </li>
                          </ul>
                        </div>
                    `
                    }
                    else if (IsSupervisor) {
                        return `
                        <div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-toggle="dropdown" aria-expanded="false">
                                <i class="fa fa-cog"></i>
                          </button>
                          <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                            <li>
                                <a href="/Jobs/Index?projectId=${row.id}" class=" dropdown-item" title="Jobs" >
                                    <i class="fas fa-list"></i>  Jobs
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


    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var projects = _$form.serializeFormToObject();
       

        abp.ui.setBusy(_$modal);
        _projectsService
            .create(projects)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l('SavedSuccessfully'));
                _$projectssTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    $(document).on('click', '.delete-projects', function () {
        var projectsId = $(this).attr("data-projects-id");
        var projectsName = $(this).attr('data-projects-name');

        deleteProjects(projectsId, projectsName);
    });

    $(document).on('click', '.edit-projects', function (e) {
        var projectsId = $(this).attr("data-projects-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Projects/EditModal?projectsId=' + projectsId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#ProjectsEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });

    abp.event.on('projects.edited', (data) => {
        _$projectssTable.ajax.reload();
    });

    function deleteProjects(projectsId, projectsName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                projectsName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _projectsService.delete({
                        id: projectsId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$projectssTable.ajax.reload();
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
        _$projectssTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$projectssTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
