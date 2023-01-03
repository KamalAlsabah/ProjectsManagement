(function ($) {
    var _suggestionsService = abp.services.app.suggestions,
        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#SuggestionsCreateModal'),
        _$form = _$modal.find('form'),
        _$searchForm = $('#SuggestionsSearchForm'),
        _$table = $('#SuggestionsTable');

    var _$suggestionsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _suggestionsService.getAll,
            inputFilter: function () {
                return _$searchForm.serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$suggestionsTable.draw(false)
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
                data: 'supervisorName',
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
                targets:5,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-secondary edit-suggestions" data-suggestions-id="${row.id}" data-toggle="modal" data-target="#SuggestionsEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i>`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-suggestions" data-suggestions-id="${row.id}" data-suggestions-name="${row.projectName}">`,
                        `       <i class="fas fa-trash"></i>`,
                        '   </button>',
                    ].join('');
                }
            }
        ]
    });

    $(document).on('click', '.delete-suggestions', function () {
        var suggestionsId = $(this).attr("data-suggestions-id");
        var suggestionsName = $(this).attr('data-suggestions-name');
        deleteJobs(suggestionsId, suggestionsName);
    });

    $(document).on('click', '.edit-suggestions', function (e) {
        var suggestionsId = $(this).attr("data-suggestions-id");
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Suggestions/EditModal?suggestionsId=' + suggestionsId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#SuggestionsEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });
    $(document).on('click', '.create-suggestions', function (e) {
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Suggestions/CreateModal?ProjectId=' + _$searchForm.find("input[name='ProjectId']").val(),
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#SuggestionsCreateModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });

    abp.event.on('suggestions.edited', (data) => {
        _$suggestionsTable.ajax.reload();
    });
    abp.event.on('suggestions.created', (data) => {
        _$suggestionsTable.ajax.reload();
    });

    function deleteJobs(suggestionsId, suggestionsName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                suggestionsName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _suggestionsService.delete({
                        id: suggestionsId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$suggestionsTable.ajax.reload();
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
        _$suggestionsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$suggestionsTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
