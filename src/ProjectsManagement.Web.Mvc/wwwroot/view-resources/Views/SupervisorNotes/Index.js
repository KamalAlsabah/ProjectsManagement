(function ($) {
    var _supervisorNotesService = abp.services.app.supervisorNotes,
        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#SupervisorNotesCreateModal'),
        _$form = _$modal.find('form'),
        _$searchForm = $('#SupervisorNotesSearchForm'),
        _$table = $('#SupervisorNotesTable');

    var _$supervisorNotesTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _supervisorNotesService.getAll,
            inputFilter: function () {
                return _$searchForm.serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$supervisorNotesTable.draw(false)
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
                data: 'note',
                sortable: true,
                
            },
            
            {
                targets: 2,
                data: 'supervisorUserName',
                sortable: true,
                
            },
           
            {
                targets: 3,
                data: 'jobName',
                sortable: true,
                
            },
            {
                targets:4,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-secondary edit-supervisorNotes" data-supervisorNotes-id="${row.id}" data-toggle="modal" data-target="#SupervisorNotesEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i>`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-supervisorNotes" data-supervisorNotes-id="${row.id}" data-supervisorNotes-name="(${row.note})">`,
                        `       <i class="fas fa-trash"></i>`,
                        '   </button>',
                    ].join('');
                }
            }
        ]
    });

    $(document).on('click', '.delete-supervisorNotes', function () {
        var supervisorNotesId = $(this).attr("data-supervisorNotes-id");
        var supervisorNotesName = $(this).attr('data-supervisorNotes-name');
        deleteJobs(supervisorNotesId, supervisorNotesName);
    });

    $(document).on('click', '.edit-supervisorNotes', function (e) {
        var supervisorNotesId = $(this).attr("data-supervisorNotes-id");
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'SupervisorNotes/EditModal?supervisorNotesId=' + supervisorNotesId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#SupervisorNotesEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });
    $(document).on('click', '.create-supervisorNotes', function (e) {
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'SupervisorNotes/CreateModal?JobsId=' + _$searchForm.find("input[name='JobId']").val(),
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#SupervisorNotesCreateModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });

    abp.event.on('supervisorNotes.edited', (data) => {
        _$supervisorNotesTable.ajax.reload();
    });
    abp.event.on('supervisorNotes.created', (data) => {
        _$supervisorNotesTable.ajax.reload();
    });

    function deleteJobs(supervisorNotesId, supervisorNotesName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                supervisorNotesName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _supervisorNotesService.delete({
                        id: supervisorNotesId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$supervisorNotesTable.ajax.reload();
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
        _$supervisorNotesTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$supervisorNotesTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
