(function() {
    $(function() {
        var _tenantService = abp.services.app.tenant;
        var _$dg = $('#dg');
        var _$dialog = $('#dlg');
        var _$form = _$dialog.find('form');
    
        edit = function() {
            var row = _$dg.datagrid('getSelected');
            _$dialog.dialog('open').dialog('setTitle', '编辑');
            alert(row.Id);
            _tenantService.get(row.Id).done(function (data) {
                alert(data);
                _$form.form('load', data);
            });
        };

        remove = function(index) {

        };

        $('#tb').children('a[name="add"]').click(function (e) {
            _$dialog.dialog('open');
            $('#TenancyName').next('span').find('input').focus();
        });

        $('#dlg-tb').children('a[name="save"]').click(function (e) {
            e.preventDefault();

            if (!_$form.form('validate')) {
                return;
            }

            var tenant = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js

            abp.ui.setBusy(_$dialog);
            _tenantService.createTenant(tenant).done(function () {
                _$dialog.dialog('close');
                location.reload(true); //reload page to see new tenant!
            }).always(function() {
                abp.ui.clearBusy(_$dialog);
            });
        });
        
        $('#dlg-tb').children('a[name="cancel"]').click(function (e) {
            _$dialog.dialog('close');
        });
    });
})();