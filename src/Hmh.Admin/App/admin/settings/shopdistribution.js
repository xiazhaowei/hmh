var appName = "OSharpApp";
angular.module(appName, ["kendo.directives"]).controller("app.admin.settings.shopdistribution", ["$scope", "$state", function ($scope, $state) {
    $scope.title = $state.current.data.pageTitle;
    $scope.gridOptions = $.osharp.kendo.grid.Options({
        url: {
            read: "/admin/shopdistribution/read",
            create: "/admin/shopdistribution/create",
            update: "/admin/shopdistribution/update",
            destroy: "/admin/shopdistribution/delete"
        },
        model: {
            id: "Id",
            fields: {
                Id: { type: "number", editable: false },
                Name: { type: "string", editable: true },
                Money: { type: "number", editable: true },
                Persent: { type: "number", editable: true },
                RewardType: { type: "number", editable: true }
            }
        },
        filterable: false,
        scrollable: false,
        sortable: false,
        pageable: false,
        columns: [            
            { field: "Id", title: "编号", width: 75 },
            { field: "Name", title: "名称" },
            { field: "Money", title: "奖金"},
            { field: "Persent", title: "提成比例%" },
            {
                field: "RewardType",
                title: "奖金方式",
                editor: function (container, options) {
                    $.osharp.kendo.grid.DropDownListEditor(container, options, [
                        { RewardTypeName: "固定奖金", RewardTypeValue: 0 },
                        { RewardTypeName: "业绩提成", RewardTypeValue: 1 }
                    ], "RewardTypeName", "RewardTypeValue");
                },
                template: "#=RewardType#"
            },
            { command: [{ name: "destroy", text: "删除" }], width: 120 }
        ]
    });
}]);