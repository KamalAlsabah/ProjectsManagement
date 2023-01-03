(function ($) {
    var _projectWorkersService = abp.services.app.projectWorkers,
        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#ProjectWorkersCreateModal'),
        _$form = _$modal.find('form'),
        _$searchForm = $('#ProjectWorkersSearchForm'),
        _$table = $('#ProjectWorkersTable');

    var _$projectWorkersTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _projectWorkersService.getAll,
            inputFilter: function () {
                return _$searchForm.serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$projectWorkersTable.draw(false)
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
                data: 'workerFullName',
                sortable: true,
                
            },
            //{
            //    targets: 2,
            //    data: 'projectName',
            //    sortable: true,
                
            //},
            {
                targets:2,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        //`   <button type="button" class="btn btn-sm bg-secondary edit-projectWorkers" data-projectWorkers-id="${row.id}" data-toggle="modal" data-target="#ProjectWorkersEditModal">`,
                        //`       <i class="fas fa-pencil-alt"></i>`,
                        //'   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-projectWorkers" data-projectWorkers-id="${row.id}" data-projectWorkers-name="${row.workerFullName}">`,
                        `       <i class="fas fa-trash"></i>`,
                        '   </button>',
                    ].join('');
                }
            }
        ]
    });

    $(document).on('click', '.delete-projectWorkers', function () {
        var projectWorkersId = $(this).attr("data-projectWorkers-id");
        var projectWorkersName = $(this).attr('data-projectWorkers-name');
        deleteProjectWorkers(projectWorkersId, projectWorkersName);
    });

    $(document).on('click', '.edit-projectWorkers', function (e) {
        var projectWorkersId = $(this).attr("data-projectWorkers-id");
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'ProjectWorkers/EditModal?projectWorkersId=' + projectWorkersId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#ProjectWorkersEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });
    $(document).on('click', '.create-projectWorkers', function (e) {
        var projectId = _$searchForm.find("#ProjectId").val()
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'ProjectWorkers/CreateModal?projectId=' + projectId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#ProjectWorkersCreateModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });

    abp.event.on('projectWorkers.edited', (data) => {
        _$projectWorkersTable.ajax.reload();
    });
    abp.event.on('projectWorkers.created', (data) => {
        _$projectWorkersTable.ajax.reload();
    });

    function deleteProjectWorkers(projectWorkersId, projectWorkersName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                projectWorkersName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _projectWorkersService.delete({
                        id: projectWorkersId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$projectWorkersTable.ajax.reload();
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
        _$projectWorkersTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$projectWorkersTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
