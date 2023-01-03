(function ($) {
    var _projectSupervisorService = abp.services.app.projectSupervisor,
        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#ProjectSupervisorCreateModal'),
        _$form = _$modal.find('form'),
        _$searchForm = $('#ProjectSupervisorSearchForm'),
        _$table = $('#ProjectSupervisorTable');

    var _$projectSupervisorTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _projectSupervisorService.getAll,
            inputFilter: function () {
                return _$searchForm.serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$projectSupervisorTable.draw(false)
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
                data: 'supervisorUserName',
                sortable: true,

            },
            {
                targets: 2,
                data: 'projectName',
                sortable: true,

            },
            {
                targets: 3,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                 
                        `   <button type="button" class="btn btn-sm bg-danger delete-projectSupervisor" data-ProjectSupervisor-id="${row.id}" data-ProjectSupervisor-name="${row.projectName}">`,
                        `       <i class="fas fa-trash"></i>`,
                        '   </button>'
                    ].join('');
                }
            }
        ]
    });

    $(document).on('click', '.delete-projectSupervisor', function () {
        var projectSupervisorId = $(this).attr("data-projectSupervisor-id");
        var projectSupervisorName = $(this).attr('data-projectSupervisor-name');
        deleteProjectSupervisor(projectSupervisorId, projectSupervisorName);
    });

    $(document).on('click', '.edit-projectSupervisor', function (e) {
        var projectSupervisorId = $(this).attr("data-projectSupervisor-id");
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'ProjectSupervisor/EditModal?projectSupervisorId=' + projectSupervisorId +`&ProjectId=' + ${_$searchForm.find("input[name='ProjectId']").val()}`,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#ProjectSupervisorEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });
    $(document).on('click', '.create-projectSupervisor', function (e) {
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'ProjectSupervisor/CreateModal?ProjectId=' + _$searchForm.find("input[name='ProjectId']").val(),
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#ProjectSupervisorCreateModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });

    abp.event.on('projectSupervisor.edited', (data) => {
        _$projectSupervisorTable.ajax.reload();
    });
    abp.event.on('projectSupervisor.created', (data) => {
        _$projectSupervisorTable.ajax.reload();
    });

    function deleteProjectSupervisor(projectSupervisorId, projectSupervisorName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                projectSupervisorName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _projectSupervisorService.delete({
                        id: projectSupervisorId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$projectSupervisorTable.ajax.reload();
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
        _$projectSupervisorTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$projectSupervisorTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
