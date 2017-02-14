var appName = "OSharpApp";
angular.module(appName, ["kendo.directives"]).controller("app.admin.settings.settings", ["$scope", "$state", function ($scope, $state) {
    $scope.title = $state.current.data.pageTitle;
    $scope.gridOptions = $.osharp.kendo.grid.Options({
        url: {
            read: "/admin/settings/read",
            update: "/admin/settings/update"
        },
        model: {
            id: "Id",
            fields: {
                Key: { type: "string", editable: false },
                Description: { type: "string", editable: false },
                Value: { type: "string",editable:true }
            }
        },
        filterable: false,
        scrollable: false,
        sortable: false,
        pageable:false,
        columns: [
            //{ command: [{ name: "destroy", text: "" }], width: 50 },
            { field: "Key", title: "参数Key" },
            { field: "Description", title: "说明"},
            { field: "Value", title: "设置值" }
        ],
        toolbar: ["save", "cancel"]
    });
}]);