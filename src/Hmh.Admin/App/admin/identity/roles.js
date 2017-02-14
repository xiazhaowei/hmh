var appName = "OSharpApp";
angular.module(appName, ["kendo.directives"]).controller("app.admin.identity.roles", ["$scope", "$state", function ($scope, $state) {
    $scope.title = $state.current.data.pageTitle;
    $scope.gridOptions = $.osharp.kendo.grid.Options({
        url: {
            read: "/admin/roles/read",
            create: "/admin/roles/create",
            update: "/admin/roles/update",
            destroy: "/admin/roles/delete"
        },
        model: {
            id: "Id",
            fields: {
                Id: { type: "number", editable: false },
                IsAdmin: { type: "boolean" },
                IsLocked: { type: "boolean" },
                IsSystem: { type: "boolean", editable: false },
                CreatedTime: { type: "date", editable: false }
            }
        },
        columns: [            
            { field: "Id", title: "编号", width: 75 },
            { field: "Name", title: "角色名" },
            { field: "Remark", title: "角色描述" },
            { field: "IsAdmin", title: "是否管理", template: function (d) { return $.osharp.tools.renderBoolean(d.IsAdmin); } },
            { field: "IsSystem", title: "是否系统", template: function (d) { return $.osharp.tools.renderBoolean(d.IsSystem); } },
            { field: "IsLocked", title: "是否锁定", template: function (d) { return $.osharp.tools.renderBoolean(d.IsLocked); } },
            { field: "CreatedTime", title: "创建时间", format: "{0: yyyy-MM-dd HH:mm}" },
            { command: [{ name: "destroy", text: "" }], width: 50 }
        ]
    });
}]);