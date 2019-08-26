(function ($) {
    //监听加载状态改变
    document.onreadystatechange = completeLoading;
    //$.parser.onComplete = completeLoading;

    //加载状态为complete时移除loading效果
    function completeLoading() {
        //if (document.readyState === "complete") {
            //alert("easyui complete");
            var loadingMask = document.getElementById('loadingDiv');
            if (loadingMask !== null)
                loadingMask.parentNode.removeChild(loadingMask);
        //}
    };

    //Notification handler
    abp.event.on('abp.notifications.received', function (userNotification) {
        abp.notifications.showUiNotifyForUserNotification(userNotification);

        //Desktop notification
        Push.create("Gsv", {
            body: userNotification.notification.data.message,
            icon: abp.appPath + 'images/app-logo-small.png',
            timeout: 6000,
            onClick: function () {
                window.focus();
                this.close();
            }
        });
    });

    // plugins for jQuery
    $.extend({
        displayCapitalText : function (val) {
            var capitals = $('#capital').combobox('getData');
            for (var i = 0; i < capitals.length; i++) {
                if (val === parseInt(capitals[i].id)) 
                    return capitals[i].name;
            };
            return val;
        },
        displayCategoryText : function (val) {
            var categories = $('#category').combobox('getData');
            for (var i = 0; i < categories.length; i++) {
                if (val === parseInt(categories[i].id)) 
                    return categories[i].name;
            };
            return val;
        },
        displayCargoTypeText : function (val) {
            var types = $('#cargoType').combobox('getData');
            for (var i = 0; i < types.length; i++) {
                if (val === parseInt(types[i].id)) 
                    return types[i].typeName;
            };
            return val;
        },

        dateFormatter: function (val) {
            if (val) return val.substr(0, 10);
        },
        
        timeFormatter: function (val) {
            if (val) return val.substr(11, 8);
        },
        
        datetimeFormatter: function (val) {
            if (val) return val.substr(0, 10) + ' ' + val.substr(11, 8);
        },       
    });

    //serializeFormToObject plugin for jQuery
    $.fn.serializeFormToObject = function () {
        //serialize to array
        var data = $(this).serializeArray();

        //add also disabled items
        $(':disabled[name]', this).each(function () {
            data.push({ name: this.name, value: $(this).val() });
        });

        //map to object
        var obj = {};
        data.map(function (x) { obj[x.name] = x.value; });

        return obj;
    };

    //Configure blockUI
    if ($.blockUI) {
        $.blockUI.defaults.baseZ = 2000;
    }

})(jQuery);