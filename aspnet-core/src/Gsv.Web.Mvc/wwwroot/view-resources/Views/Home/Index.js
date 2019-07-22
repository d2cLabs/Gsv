(function () {
    $(function() {
        $("#main-tab").tabs({
            onContextMenu: function (e, title) {
                e.preventDefault();
                $("#tab-menu").menu("show", { left: e.pageX, top: e.pageY })
                    .data("tabTitle", title); //将点击的Tab标题加到菜单数据中
            }
        });

        $("#tab-menu").menu({
            onClick: function (item) {
                tabHandle(this, item.id);
            }
        });
        
        var x = document.getElementsByClassName('easyui-tree');
        for (var i = 0; i < x.length; i++)
        {
            $(x[i]).tree({
                onClick: function(node) {
                    addTab(node.text, node.attributes.url);
                }
            });
        }
    });

    function addTab(title, url, icon) {
        var $mainTabs = $("#main-tab");
        if ($mainTabs.tabs("exists", title)) {
            $mainTabs.tabs("select", title);
        } else {
            $mainTabs.tabs("add", {
                title: title,
                closable: true,
                icon: icon,
                content: createFrame(url)
            });
        }
    }

    function createFrame(url) {
        var html = '<iframe scrolling="auto" frameborder="0" src="' + url + '" style="width:100%; height:99%"></iframe>';
        return html;
    }

    // utils for sub
    function closeTab(title) {
        $("#main-tab").tabs('close', title);
    }

    function tabHandle(menu, type) {
        var title = $(menu).data("tabTitle");
        var $tab = $("#main-tab");
        var tabs = $tab.tabs("tabs");
        var index = $tab.tabs("getTabIndex", $tab.tabs("getTab", title));
        var closeTitles = [];
        switch (type) {
            case "tab-menu-refresh":
                var iframe = $(".tabs-panels .panel").eq(index).find("iframe");
                if (iframe) {
                    var url = iframe.attr("src");
                    iframe.attr("src", url);
                }
                break;
            case "tab-menu-openFrame":
                var iframe = $(".tabs-panels .panel").eq(index).find("iframe");
                if (iframe) {
                    window.open(iframe.attr("src"));
                }
                break;
            case "tab-menu-close":
                closeTitles.push(title);
                break;
            case "tab-menu-closeleft":
                if (index == 0) {
                    mfx.notify.warn("左边没有可关闭标签。");
                    return;
                }
                for (var i = 0; i < index; i++) {
                    var opt = $(tabs[i]).panel("options");
                    if (opt.closable) {
                        closeTitles.push(opt.title);
                    }
                }
                break;
            case "tab-menu-closeright":
                if (index == tabs.length - 1) {
                    mfx.notify.warn("右边没有可关闭标签。");
                    return;
                }
                for (var i = index + 1; i < tabs.length; i++) {
                    var opt = $(tabs[i]).panel("options");
                    if (opt.closable) {
                        closeTitles.push(opt.title);
                    }
                }
                break;
            case "tab-menu-closeother":
                for (var i = 0; i < tabs.length; i++) {
                    if (i == index) {
                        continue;
                    }
                    var opt = $(tabs[i]).panel("options");
                    if (opt.closable) {
                        closeTitles.push(opt.title);
                    }
                }
                break;
            case "tab-menu-closeall":
                for (var i = 0; i < tabs.length; i++) {
                    var opt = $(tabs[i]).panel("options");
                    if (opt.closable) {
                        closeTitles.push(opt.title);
                    }
                }
                break;
        }
        for (var i = 0; i < closeTitles.length; i++) {
            $tab.tabs("close", closeTitles[i]);
        }
    }
})();