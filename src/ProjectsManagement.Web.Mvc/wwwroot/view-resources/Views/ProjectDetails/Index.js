(function ($) {
    var _projectDetailsService = abp.services.app.projectDetails,
        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#ProjectDetailsCreateModal'),
        _$form = _$modal.find('form'),
        _$searchForm = $('#ProjectDetailsSearchForm'),
        _$table = $('#ProjectDetailsTable');

    var _$projectDetailsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _projectDetailsService.getAll,
            inputFilter: function () {
                return _$searchForm.serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$projectDetailsTable.draw(false)
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
                data: 'projectName',
                sortable: true,

            },

            {
                targets: 2,
                data: 'details',
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
                        `   <button type="button" class="btn btn-sm bg-secondary edit-projectDetails" data-projectDetails-id="${row.id}" data-toggle="modal" data-target="#ProjectDetailsEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i>`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-projectDetails" data-projectDetails-id="${row.id}" data-projectDetails-name="(${row.note})">`,
                        `       <i class="fas fa-trash"></i>`,
                        '   </button>',
                    ].join('');
                }
            }
        ]
    });

    $(document).on('click', '.delete-projectDetails', function () {
        var projectDetailsId = $(this).attr("data-projectDetails-id");
        var projectDetailsName = $(this).attr('data-projectDetails-name');
        deleteJobs(projectDetailsId, projectDetailsName);
    });

    $(document).on('click', '.edit-projectDetails', function (e) {
        var projectDetailsId = $(this).attr("data-projectDetails-id");
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'ProjectDetails/EditModal?projectDetailsId=' + projectDetailsId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#ProjectDetailsEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });
    $(document).on('click', '.create-projectDetails', function (e) {
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'ProjectDetails/CreateModal?projectId=' + _$searchForm.find("input[name='ProjectId']").val(),
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#ProjectDetailsCreateModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });

    abp.event.on('projectDetails.edited', (data) => {
        _$projectDetailsTable.ajax.reload();
    });
    abp.event.on('projectDetails.created', (data) => {
        _$projectDetailsTable.ajax.reload();
    });

    function deleteJobs(projectDetailsId, projectDetailsName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                projectDetailsName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _projectDetailsService.delete({
                        id: projectDetailsId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$projectDetailsTable.ajax.reload();
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
        _$projectDetailsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$projectDetailsTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
