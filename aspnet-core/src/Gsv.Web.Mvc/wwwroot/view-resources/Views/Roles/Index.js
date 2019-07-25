(function() {
    $(function() {
        var _roleService = abp.services.app.role;
        var _$dg = $('#dgRole');
        var _$dialog = $('#dlg');
        var _$form = _$dialog.find('form');
        var _tenancyName = null;

        // datagrid's url and onSelect event
        $('#dg').datagrid({
            url: 'Roles/GridData',
            onSelect: function (index, row) {
                _tenancyName = row.tenancyName;
                _$dg.datagrid({
                    url: 'Roles/GetTenantRoles/' + _tenancyName
                })
            }
        });

       _$dg.datagrid({
            onSelect: function (index, row) {
                $('#dgPermission').datagrid('clearChecked')
                _roleService.getRolePermissionNames(_tenancyName, row.id).done(function (data) {
                    var rows = $('#dgPermission').datagrid('getRows');
                    $.each(data, function (i, name) {
                        $.each(rows, function (j, r) {
                            if (r.Name === name) {
                                $('#dgPermission').datagrid('checkRow', j);
                            }
                        });
                    });
                });
            }
        })

        $('#tb').children('a[name="add"]').click(function (e) {
            _$dialog.dialog('open');
            _$fm.form('clear');
            $('#Name').next('span').find('input').focus();
        });

        $('#tb').children('a[name="remove"]').click(function (e) {
            let row = _$dg.datagrid('getSelected');
            if (!row) {
                abp.notify.error("选择要删除的行", "", { positionClass : 'toast-top-center'} );
                return;
            }
            abp.message.confirm('确定删除这一行吗？', '请确定', function (isConfirmed) {
                if (isConfirmed) {
                    _roleService.removeRole(tenancyName, row.name).done(function () {
                        _$dialog.dialog('close');
                        _$dg.datagrid('reload'); 
                   });
                }
            });
        });

        $('#dlg-tb').children('a[name="save"]').click(function (e) {
            e.preventDefault();

            if (!_$form.form('validate')) {
                return;
            }

            var role = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js

            abp.ui.setBusy(_$dialog);
            _roleService.createTenantRole(_tenancyName, role).done(function () {
                _$dialog.dialog('close');
                _$dg.datagrid('reload'); 
            }).always(function() {
                abp.ui.clearBusy(_$dialog);
            });
        });

        $('#dlg-tb').children('a[name="cancel"]').click(function (e) {
            _$dialog.dialog('close');
        });

        // updateRolePermissions
        $('#tb').children('a[name="grant"]').click(function (e) {
            var rows = $('#dgPermission').datagrid('getChecked');
            if (rows.length > 0)
            {
                var id = _$dg.datagrid('getSelected').id;
                var names = [];
                for (var i = 0; i < rows.length; i++)
                    names.push(rows[i].Name);

                abp.ui.setBusy(_$dialog);
                _roleService.updateRolePermissions(_tenancyName, {RoleId: id, GrantedPermissionNames: names}).done(function () {
                    abp.notify.info("成功授权");
                }).always(function() {
                    abp.ui.clearBusy(_$dialog);
                });
            }
        });
    });
})();