var appName = "OSharpApp";
angular.module(appName, ["kendo.directives"]).controller("app.admin.security.functions", [
    "$scope","$state", function ($scope,$state) {
        $scope.title = $state.current.data.pageTitle;
        $scope.gridOptions = $.osharp.kendo.grid.Options({
            url: {
                read: "/admin/functions/read",
                create: "/admin/functions/create",
                update: "/admin/functions/update",
                destroy: "/admin/functions/delete"
            },
            model: {
                id: "Id",
                fields: {
                    Name: { type: "string" },
                    FunctionType: { type: "number" },
                    OperateLogEnabled: { type: "boolean" },
                    DataLogEnabled: { type: "boolean" },
                    CacheExpirationSeconds: { type: "number" },
                    IsCacheSliding: { type: "boolean" },
                    IsLocked: { type: "boolean" },
                    IsAjax: { type: "boolean", editable: false },
                    IsChild: { type: "boolean", editable: false },
                    IsCustom: { type: "boolean", editable: false },
                    IsDeleted: { type: "boolean", editable: false },
                    Area: { type: "string", editable: false },
                    Controller: { type: "string", editable: false },
                    Action: { type: "string", editable: false },
                    Url: { type: "string" }
                }
            },
            columns: [
                //{ command: [{ name: "destroy", text: "" }], width: "90px" },
                { command: [{ name: "destroy", text: "" }], width: 50 },
                { field: "Name", title: "功能名称", width: 200 },
                {
                    field: "FunctionType",
                    title: "功能类型",
                    width: 100,
                    template: function (d) { return $.osharp.tools.valueToText(d.FunctionType, $.osharp.data.functionTypes); },
                    editor: function (container, options) { $.osharp.kendo.grid.ComboBoxEditor(container, options, $.osharp.data.functionTypes); }
                },
                { field: "CacheExpirationSeconds", title: "缓存秒数" },
                { field: "OperateLogEnabled", title: "操作日志", template: function (d) { return $.osharp.tools.renderBoolean(d.OperateLogEnabled); } },
                { field: "DataLogEnabled", title: "数据日志", template: function (d) { return $.osharp.tools.renderBoolean(d.DataLogEnabled); } },
                { field: "IsCacheSliding", title: "滑动过期", template: function (d) { return $.osharp.tools.renderBoolean(d.IsCacheSliding); } },
                { field: "IsLocked", title: "已锁定", template: function (d) { return $.osharp.tools.renderBoolean(d.IsLocked); } },
                { field: "IsAjax", title: "Ajax访问", hidden: true, template: function (d) { return $.osharp.tools.renderBoolean(d.IsAjax); } },
                { field: "IsChild", title: "子功能", hidden: true, template: function (d) { return $.osharp.tools.renderBoolean(d.IsChild); } },
                { field: "IsCustom", title: "自定义", template: function (d) { return $.osharp.tools.renderBoolean(d.IsCustom); } },
                { field: "IsDeleted", title: "已回收", hidden: true, template: function (d) { return $.osharp.tools.renderBoolean(d.IsDeleted); } },
                { field: "Area", title: "区域", hidden: true },
                { field: "Controller", title: "控制器", hidden: true },
                { field: "Action", title: "功能方法", hidden: true },
                { field: "Url", title: "URL地址" }
            ],
            pageSize: 15,
            columnMenu: { sortable: false },
            group: [{ field: "Area" }, { field: "Controller" }]
        });
    }
]);