var appName = "OSharpApp";
angular.module(appName, ["kendo.directives"]).controller("app.admin.settings.regions", ["$scope", "$state", function ($scope, $state) {
    $scope.title = $state.current.data.pageTitle;
    $scope.gridOptions = $.osharp.kendo.grid.Options({
        url: {
            read: "/admin/regions/read",
            update: "/admin/regions/update"
        },
        model: {
            id: "Id",
            fields: {
                Id: { type: "number", editable: true },               
                Province: { type: "string", editable: true },
                City: { type: "string", editable: true },
                County: { type: "string",editable:true },
                Street: { type: "string", editable: true },
                IsOpenServices: { type: "boolean", editable: true }
            }
        },        
        columns: [
            { field: "Id", title: "地区编号", width: 100 },           
            { field: "Province", title: "省" },
            { field: "City", title: "市" },
            { field: "County", title: "县区" },            
            { field: "Street", title: "商圈" },
            { field: "IsOpenServices", title: "开启业务", template: function (d) { return $.osharp.tools.renderBoolean(d.IsOpenServices); }},
            { command: [{ name: "destroy", text: "删除" }], width: 100 }
        ]
    });
}]);