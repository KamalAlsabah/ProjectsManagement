(function ($) {
    var _StatisticsHomeService = abp.services.app.homeStatistics,
        l = abp.localization.getSource('ProjectsManagement'),
        _$modal = $('#StatisticsHomeCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#StatisticsHomeTable');

    var _$StatisticsHomeTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _StatisticsHomeService.getAll,
            //inputFilter: function () {
            //    return $('#statisticsHomeSearchForm').serializeFormToObject(true);
            //}
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$StatisticsHomeTable.draw(false)
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
                data: 'value',
                sortable: false
            },
            {
                targets: 3,
                data: 'icon',
                sortable: false

            },
           
            {
                targets: 4,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-secondary edit-statisticsHome" data-statisticsHome-id="${row.id}" data-toggle="modal" data-target="#StatisticsHomeEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-statisticsHome" data-statisticsHome-id="${row.id}" data-statisticsHome-name="${row.name}">`,
                        `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                        '   </button>'
                    ].join('');
                }
            }
        ]
    });


    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var statisticsHome = _$form.serializeFormToObject();


        abp.ui.setBusy(_$modal);
        _StatisticsHomeService
            .create(statisticsHome)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l('SavedSuccessfully'));
                _$StatisticsHomeTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    $(document).on('click', '.delete-statisticsHome', function () {
        var statisticsHomeId = $(this).attr("data-statisticsHome-id");
        var statisticsHomeName = $(this).attr('data-statisticsHome-name');

        deletestatisticsHome(statisticsHomeId, statisticsHomeName);
    });

    $(document).on('click', '.edit-statisticsHome', function (e) {
        var statisticsHomeId = $(this).attr("data-statisticsHome-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'StatisticsHome/EditModal?statisticsHomeId=' + statisticsHomeId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#StatisticsHomeEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });

    abp.event.on('projects.edited', (data) => {
        _$StatisticsHomeTable.ajax.reload();
    });

    function deletestatisticsHome(statisticsHomeId, statisticsHomeName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                statisticsHomeName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _StatisticsHomeService.delete({
                        id: statisticsHomeId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$StatisticsHomeTable.ajax.reload();
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
        _$StatisticsHomeTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$StatisticsHomeTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
