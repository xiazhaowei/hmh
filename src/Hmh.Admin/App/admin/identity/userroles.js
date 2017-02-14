var appName = "OSharpApp";
angular.module(appName, ["kendo.directives"]).controller("app.admin.identity.userroles", ["$scope", "$state", function ($scope, $state) {
    $scope.title = $state.current.data.pageTitle;
    $scope.gridOptions = $.osharp.kendo.grid.Options({
        url: {
            read: "/admin/user-role-maps/read",
            create: "/admin/user-role-maps/create",
            update: "/admin/user-role-maps/update",
            destroy: "/admin/user-role-maps/delete"
        },
        model: {
            id: "Id",
            fields: {
                Id: { type: "number", editable: false },
                UserId: { type: "number" },
                UserName: { type: "string" },
                NickName: { type: "string" },
                RoleId: { type: "number" },
                RoleName: { type: "string" },
                BeginTime: { type: "date", nullable: true, parse: $.osharp.kendo.timeParse },
                EndTime: { type: "date", nullable: true, parse: $.osharp.kendo.timeParse },
                IsLocked: { type: "boolean" },
                CreatedTime: { type: "date", editable: false }
            }
        },
        columns: [            
            { field: "Id", title: "编号", width: 75 },
            {
                field: "UserId",
                title: "用户",
                editor: function (container, options) {
                    $.osharp.kendo.grid.RemoteDropDownListEditor(container, options, "/admin/users/read-node", "NickName", "UserId");
                },
                template: "#=NickName#"
            }, {
                field: "RoleId",
                title: "角色",
                editor: function (container, options) {
                    $.osharp.kendo.grid.RemoteDropDownListEditor(container, options, "/admin/roles/read-node", "RoleName", "RoleId");
                },
                template: "#=RoleName#"
            },
            { field: "BeginTime", title: "生效时间" },
            { field: "EndTime", title: "过期时间", format: "{0: yyyy-MM-dd HH:mm:ss}" },
            { field: "IsLocked", title: "已锁定", template: function (d) { return $.osharp.tools.renderBoolean(d.IsLocked); } },
            { field: "CreatedTime", title: "创建时间", format: "{0: yyyy-MM-dd HH:mm:ss}" },
            { command: [{ name: "destroy", text: "" }], width: 50 }
        ]
    });
}]);