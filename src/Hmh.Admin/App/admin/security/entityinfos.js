var appName = "OSharpApp";
angular.module(appName, ["kendo.directives"]).controller("app.admin.security.entityinfos", ["$scope", "$state", function ($scope, $state) {
    $scope.title = $state.current.data.pageTitle;
    $scope.gridOptions = $.osharp.kendo.grid.Options({
        url: {
            read: "/admin/entityinfos/read",
            update: "/admin/entityinfos/update"
        },
        model: {
            id: "Id",
            fields: {
                Name: { type: "string", editable: false },
                ClassName: { type: "string", editable: false },
                DataLogEnabled: { type: "boolean" },
                IsDeleted: { type: "boolean", editable: false }
            }
        },
        columns: [
            //{ command: [{ name: "destroy", text: "" }], width: 50 },
            { field: "Name", title: "实体名称" },
            { field: "DataLogEnabled", title: "数据日志", template: function (d) { return $.osharp.tools.renderBoolean(d.DataLogEnabled); } },
            { field: "IsDeleted", title: "已回收", template: function (d) { return $.osharp.tools.renderBoolean(d.IsDeleted); } },
            { field: "ClassName", title: "实体类型名称" }
        ],
        toolbar: ["save", "cancel"]
    });
}]);