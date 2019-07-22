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
        Push.create("Clc", {
            body: userNotification.notification.data.message,
            icon: abp.appPath + 'images/app-logo-small.png',
            timeout: 6000,
            onClick: function () {
                window.focus();
                this.close();
            }
        });
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