var appName = "OSharpApp";
angular.module(appName, ["kendo.directives"]).controller("app.admin.goods.showcategory", ["$scope", "$state", function ($scope, $state) {
    $scope.title = $state.current.data.pageTitle;
    $scope.treelistOptions = $.osharp.kendo.treelist.Options({
        url: {
            read: "/admin/showcategory/read",
            create: "/admin/showcategory/create",
            update: "/admin/showcategory/update",
            destroy: "/admin/showcategory/delete"
        },
        model: {
            id: "Id",
            parentId:"ParentId",
            fields: {
                Id: { field:"Id", type: "number", editable: false, nullable: false },
                ParentId: { field: "ParentId", type: "number", editable: true, nullable: true },
                Name: { validation: { required: true } },
                Logo: { type: "string"},
                SortCode: { type: "number", validation: { min: 0, required: true } },
                Link: { type: "string", validation: { required: true } },
                IsShow: { type: "boolean" }
            }
        },
        columns: [           
            { field: "Name", title: "名称" },
            { field: "Id", title: "编号" },
            { field: "ParentId", title: "父级编号" },
            { field: "SortCode", title: "排序编号" },
            { field: "Logo", title: "Logo" },
            { field: "Link", title: "网址" },
            { field: "IsShow", title: "前端显示", template: function (d) { return $.osharp.tools.renderBoolean(d.IsShow); } },
            {
                command: [
                  { name: "edit", text: "编辑" },
                  { name: "destroy", text: "删除" }], width: 200
            }
        ]
    });
}]);
