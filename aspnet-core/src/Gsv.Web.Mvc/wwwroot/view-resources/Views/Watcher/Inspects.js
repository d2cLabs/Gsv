(function() {        
    $(function() {
        task.withoutDate = true;
        task.dd = '2000-01-01';
        $('#dd').datebox({
            onChange: function(newValue, oldValue) {
                // alert(newValue);
                task.dd = newValue;
                task.loadList();
            }
        });

        $('#dgList').datagrid({
            columns: task.columns,
            pagination: true,
            pageSize: 20,
        });
    });
})();
