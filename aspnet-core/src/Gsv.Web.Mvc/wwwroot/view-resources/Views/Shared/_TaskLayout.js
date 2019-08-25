var task = task || {};

(function($) {
    task.controllerName = '';
    task.objectRow = null;
    task.dd = '';
    task.shelfId = 0;

    task.columns = [[]];
    task.loadList = function () {}

    task.reload = function () {
        $('#dg').datagrid('reload');
        $('#shelf').combobox({
            data: {}
        });
        $('#dgList').datagrid({
            data: {}
        });
    }
    
    task.placeFormatter = function(val, row, index) {
        return row.placeCn + ' ' + row.placeName;
    }
    task.capitalFormatter = function(val, row, index) {
        return row.capitalCn + ' ' + row.capitalName;
    }

    // document ready
    $(function () {
        // alert(task.controllerName);
        $('#dg').datagrid({
            url: '/' + task.controllerName + '/GridData',
            onSelect: function(index, row) {
                // set shelf comboBox
                abp.services.app.task.getObjectShelves(row.id, row.categoryId).done(function (res) {
                    $('#shelf').combobox({
                        data: res,
                        valueField: 'id',
                        textField: 'name'
                    });
                });
                task.objectRow = row; 
                task.loadList();              
            }
        });

        abp.services.app.task.getTodayString().done(function (d) {
            task.dd = d;
            $('#dd').datebox('setValue', task.dd);
        });

        $('#dd').datebox({
            onChange: function(newValue, oldValue) {
                // alert(newValue);
                task.dd = newValue;
                task.loadList();
            }
        });

        $('#shelf').combobox({
            onChange: function(newValue, oldValue) {
                alert(newValue);
                task.shelfId = newValue;
                task.loadList();
            }
        });

        $('#dgList').datagrid({
            columns: task.columns
        });
    });
})(jQuery);