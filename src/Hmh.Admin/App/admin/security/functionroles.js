var appName = "OSharpApp";
angular.module(appName, ["kendo.directives"]).controller("app.admin.security.functionroles", [
    "$scope", "$state", function($scope, $state) {
        $scope.title = $state.current.data.pageTitle;
        $scope.gridOptions = $.osharp.kendo.grid.Options({
            url: {
                read: "/admin/function-role-maps/read",
                create: "/admin/function-role-maps/create",
                update: "/admin/function-role-maps/update",
                destroy: "/admin/function-role-maps/delete"
            },
            model: {
                id: "Id",
                fields: {
                    Id: { type: "number", editable: false },
                    RoleId: { type: "number" },
                    RoleName: { type: "string" },
                    FunctionId: { type: "string" },
                    FunctionName: { type: "string" },
                    FilterType: { type: "number" },
                    IsLocked: { type: "boolean" },
                    CreatedTime: { type: "date", editable: false }
                }
            },
            columns: [
                { command: [{ name: "destroy", text: "" }], width: 50 },
                { field: "Id", title: "编号", width: 75 },
                {
                    field: "RoleId",
                    title: "角色",
                    editor: function(container, options) {
                        $.osharp.kendo.grid.RemoteDropDownListEditor(container, options, "/admin/roles/read-node", "RoleName", "RoleId");
                    },
                    template: "#=RoleName#"
                }, {
                    field: "FunctionId",
                    title: "功能",
                    editor: function(container, options) {
                        $.osharp.kendo.grid.RemoteDropDownListEditor(container, options, "/admin/functions/read-node", "FunctionName", "FunctionId");
                    },
                    template: "#=FunctionName#"
                }, {
                    field: "FilterType",
                    title: "映射类型",
                    editor: function(container, options) {
                        $.osharp.kendo.grid.DropDownListEditor(container, options, $.osharp.data.filterType);
                    },
                    template: function(d) { return $.osharp.tools.valueToText(d.FilterType, $.osharp.data.filterType); }
                },
                { field: "IsLocked", title: "已锁定", template: function(d) { return $.osharp.tools.renderBoolean(d.IsLocked); } },
                { field: "CreatedTime", title: "创建时间", format: "{0: yyyy-MM-dd HH:mm}" }
            ]
        });
    }
]);