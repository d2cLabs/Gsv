(function() {        
    $(function() {    
        abp.services.app.task.getTodayString().done(function (d) {
            task.today = d;
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

        $('#dgList').datagrid({
            columns: task.columns
        });
    });
})();
