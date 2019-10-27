var task = task || {};

(function($) {
    task.controllerName = '';
    task.today = '';

    task.withoutDate = false;
    task.dd = '';
    task.objectRow = null;
    task.shelfId = 0;

    task.columns = [[]];
    task.loadList = function () {}

    task.reload = function () {
        $('#dg').datagrid('reload');

        if (task.withoutDate) {
            task.dd = '2000-01-01';
            $('#dd').datebox('clear');
        }

        $('#dgList').datagrid({url: ''});
        $('#dgList').datagrid('loadData',{total:0,rows:[]})
        task.objectRow = null;

        $('#shelf').tree({
            data: []
        });
        task.shelfId = 0;
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
                    var treeData = [];
                    res.forEach( function (val, index, arr) {
                        var displayText = val.name;
                        if (val.inventory != null) {
                            displayText = displayText + '【库存:' + val.inventory.toFixed(3) + '】';
                        }
                        treeData.push({ id: val.id, text: displayText });
                    });
                    $('#shelf').tree({
                        data: treeData,
                        onSelect: function(row) {
                            task.shelfId = row.id;
                            task.loadList();
                        }
                    });
                });
                task.objectRow = row; 
                task.shelfId = 0;
                task.loadList();
            }
        });
    });
})(jQuery);