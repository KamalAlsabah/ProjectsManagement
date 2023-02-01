(function ($) {
    var _projectHistoryService = abp.services.app.projectHistory,
        l = abp.localization.getSource('ProjectsManagement'),
        _$searchForm = $('#ProjectHistorySearchForm'),
        _$table = $('#ProjectHistoryTable');

    var _$projectHistoryTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _projectHistoryService.getAll,
            inputFilter: function () {
                return _$searchForm.serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$projectHistoryTable.draw(false)
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
                data: 'userName',
                sortable: true,

            },
            {
                targets: 3,
                data: 'sprintName',
                sortable: true,

            },
            {
                targets: 4,
                data: 'jobTasksName',
                sortable: true,

            },
            {
                targets: 5,
                data: 'projectWorkersName',
                sortable: true,

            },
            {
                targets: 6,
                data: 'projectSupervisorsName',
                sortable: true,

            },
            {
                targets: 7,
                data: 'supervisorNotesName',
                sortable: true,
               
            },
            {
                targets: 8,
                data: 'creationTime',
                sortable: true,

            },
            {
                targets: 9,
                data: 'projectHistoryActions',
                sortable: true,
                
            },
            {
                targets: 10,
                data: 'projectHistoryColumns',
                sortable: true,
                
            },
            {
                targets: 11,
                data: 'description',
                sortable: true,

            }
            
        ]
    });

 
    $('.btn-search').on('click', (e) => {
        _$projectHistoryTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$projectHistoryTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
