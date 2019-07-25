
var crud = crud || {};
(function () {
    var _$dg = $('#datagrid');
    var _$dlg = $('#dlg');
    var _$fm = $('#fm');
    var _action;

    // default var
    crud.dgDefault = {
        name: '',
        toolbarItems: "reload, add",
        columns: [[]],
        sortName: 'cn',
        singleSelect: true,
        pagination: true,
        pageSize: 20,
        pageList: [15, 20, 25, 50, 100, 200],
    };
    crud.filter = '';
    crud.children = false;
    crud.parentId = 0;
    crud.parentField = '';
    crud.useSelfSave = false;

    //前置逻辑，将在构造datagrid之前执行
    crud.startfunction = function() { };
    //后置逻辑，将在构造datagrid之后执行
    crud.endfunction = function() { }; 

    crud.setComboBox = function(domid, data) {
        $(domid).combobox({
            data: data, 
            valueField: 'value', 
            textField: 'displayText'
        });
    };

    crud.getComboTextByInt = function(domid, val) {
        var types = $(domid).combobox('getData');
        for (var i = 0; i < types.length; i++) {
            if (val === parseInt(types[i].value)) 
                return types[i].displayText;
        };
        return val;
    }
    crud.reload = function () {
        _$dg.datagrid('reload');
    };

    crud.addNew = function () {
        if (crud.children && crud.parentId === 0) {
            abp.notify.error("请先选择上级记录");
            return;
        }

        _$dlg.dialog('open').dialog('setTitle', '增加');
        _$fm.form('clear');
        $("#id").attr("value", '0');    // 将Id值置为0
        if (crud.parentId > 0) {
            $('#'+crud.parentField).combobox('setValue', crud.parentId);
        }
        _action = crud.useSelfSave ? '/MyCreate' : '/Create';
    };

    crud.edit = function (index) {
        var row = _$dg.datagrid('getRows')[index];
        _$dlg.dialog('open').dialog('setTitle', '编辑');        
        _$fm.form('load', row); //crud.dgDefault.name + '/GetEdit/' + row.id);
        _action = crud.useSelfSave ? '/MyUpdate' : '/Update';
    };
        
    crud.delete = function (index) {
        var row = _$dg.datagrid('getRows')[index];
        if (!row) return;
        abp.message.confirm('确认要删除此记录吗?', '请确认', function (r) {
            if (r) {
                _action = '/Delete';
                crud.sendAjax({id: row.id});
            };
        });
    };

    crud.deletes = function () {
        var checkedRows = _$dg.datagrid("getChecked");
        if (checkedRows.length === 0) {
            abp.notify.warn("请先选中要删除的行。");
            return;
        }
        var ids = [];
        checkedRows.forEach( function(val,index,arr) {
            ids.push(val.id);
        });

        abp.message.confirm('确认要删除这'+ids.length+'条的记录?', '请确认', function (r) {
            if (r) {
                _action = '/Deletes';
                crud.sendAjax({ids: ids});
            };
        });
    };
            
    crud.cancel = function () {
        _$dlg.dialog('close');
    };
        
    crud.save = function () {
        if (!_$fm.form('validate'))
            return;

        var fd = new FormData(document.getElementById('fm'));         
        crud.sendFdAjax(fd);
    };

    crud.sendAjax = function (data) {
        abp.ajax({
            contentType: 'application/x-www-form-urlencoded',
            url: crud.dgDefault.name + _action,
            data: data   
        }).done(function (data) {
            abp.notify.info(data.content);
            _$dg.datagrid('reload');
            _$dlg.dialog('close');
        });
    }

    crud.sendFdAjax = function (data) {
        abp.ajax({
            contentType: false,
            processData: false,
            url: crud.dgDefault.name + _action,
            data: data
        }).done(function (data) {
            abp.notify.info(data.content);
            _$dg.datagrid('reload');
            _$dlg.dialog('close');
        });
    }

    crud.operator = function (val, row, index) {
        var htmlTag = '<a href="javascript:void(0)" onclick="crud.edit(' + index + ')">编辑</a>';
        htmlTag = htmlTag + '<span>&nbsp;&nbsp;&nbsp;&nbsp;</span>';
        htmlTag = htmlTag + '<a href="javascript:void(0)" onclick="crud.delete(' + index + ')">删除</a>';
        return htmlTag;
    }

    // document ready
    $(function () {
        // get toolbardata
        crud.toolbarData=[];

        if (crud.dgDefault.toolbarItems.indexOf('reload') >= 0) {
            crud.toolbarData.push({ text: "刷新", iconCls: "icon-reload", handler: crud.reload} );
        }

        if (crud.dgDefault.toolbarItems.indexOf('add') >= 0) {
            crud.toolbarData.push({ text: "增加", iconCls: "icon-add", handler: crud.addNew });
        }
        if (crud.dgDefault.toolbarItems.indexOf('deletes') >= 0) {
            crud.toolbarData.push("-");
            crud.toolbarData.push({ text: "批量删除", iconCls: "icon-remove", handler: deletes });
            crud.dgDefault.columns[0].unshift({ field: "ck", checkbox: true });
        }

        crud.startfunction();

        crud.dgDefault.columns[0].push({ field: "operator", title: "操作", width: 80, align: "center", formatter: crud.operator });

        // alert(crud.dgDefault.table + '/GridPagedData/');
        // 构造datagrid
        var _url = crud.dgDefault.pagination ? '/GetPagedData/' : '/GetAllData/';
        _$dg.datagrid({
            url: crud.dgDefault.name + _url,
            toolbar: crud.toolbarData,
            fit: true,
            fitColumns: true,
            columns: crud.dgDefault.columns,
            striped: true,
            rownumbers: true,
            checkOnSelect: false,
            sortName: crud.dgDefault.sortName,
            singleSelect: crud.dgDefault.singleSelect,
            pagination: crud.dgDefault.pagination,
            pageSize: crud.dgDefault.pageSize,
            pageList: crud.dgDefault.pageList, 
        });

        _$dlg.dialog({
            buttons:[
                { text:'保存', iconCls: 'icon-save', handler: crud.save },
			    { text:'取消', iconCls: 'icon-cancel', handler: crud.cancel }
			]
        });

        crud.endfunction();
    });

})();