var appName = "OSharpApp";
angular.module(appName, ["kendo.directives"]).controller("app.admin.goods.category", ["$scope", "$state", function ($scope, $state) {
    $scope.title = $state.current.data.pageTitle;
    $scope.treelistOptions = $.osharp.kendo.treelist.Options({
        url: {
            read: "/admin/category/read",
            create: "/admin/category/create",
            update: "/admin/category/update",
            destroy: "/admin/category/delete"
        },
        model: {
            id: "Id",
            parentId:"ParentId",
            fields: {
                Id: { field:"Id", type: "number", editable: false, nullable: false },
                ParentId: { field: "ParentId", type:"number", editable: true, nullable: true },
                Name: { validation: { required: true } },
                SortCode: { type: "number", validation: { min: 0, required: true } },
                Profit: { type: "number", validation: { min: 0, required: true } },
                Distribution: { type: "number", validation: { min: 0,max:100, required: true } }
            }
        },
        columns: [           
            { field: "Name", title: "名称" },
            { field: "Id", title: "编号" },
            { field: "ParentId", title: "父级编号" },
            { field: "SortCode", title: "排序编号" },
            { field: "Profit", title: "利润率%" },
            { field: "Distribution", title: "商城提成百分比%" },            
            {
                command: [
                  { name: "edit", text: "编辑" },
                  { name: "destroy", text: "删除" },
                  { name: "attr", text: "属性/规格..", click: setAttr }], width: 260
            }
        ]
    });


    //属性管理
    function setAttr(e) {
        var tr = $(e.target).closest("tr");
        var data = this.dataItem(tr);        
        $scope.$broadcast("category.setattr", data);
    };   
    
}]);

angular.module(appName).controller("app.admin.goods.category.setattr", ["$scope", "$modal", function ($scope, $modal) {
    $scope.open = function (data, size) {
        $modal.open({
            templateUrl: "goods-category-setattr",
            controller: "app.admin.goods.category.setattr.modal",
            size: size,
            resolve: {//传递数据到控制器
                category: function () {
                    return data;
                }
            }
        });
    };
    $scope.$on("category.setattr", function (event, data) {       
        $scope.open(data,"lg");
    });
}]);


angular.module(appName).controller("app.admin.goods.category.setattr.modal", [
    "$scope", "$http", "$modalInstance", "category", function ($scope, $http, $modalInstance, category) {
        $scope.category = category;

        $scope.attrgridOptions = $.osharp.kendo.grid.Options({
            url: {
                read: "/admin/attr/read?categoryid="+category.Id,
                create: "/admin/attr/create",
                update: "/admin/attr/update",
                destroy: "/admin/attr/delete"
            },
            model: {
                id: "Id",
                fields: {
                    Id: { type: "number", editable: false },
                    CategoryId: { type: "number", editable: true, nullable: false },
                    Name: { type: "string", editable: true, nullable: false },
                    IsMust: { type: "boolean",editable:true },
                    SelectableValues: { type: "string" ,editable:true},
                    Type: { type: "number", editable: true }
                }
            },
            columns: [
                { field: "Id", title: "编号", width: 75 },
                { field: "CategoryId", title: "所属类别", width: 75 },
                { field: "Name", title: "属性名称", width: 100 },
                {
                    field: "Type",
                    title: "类型",
                    editor: function (container, options) {
                        $.osharp.kendo.grid.DropDownListEditor(container, options, [
                            { AttrTypeName: "单选", AttrTypeValue: 0 },
                            { AttrTypeName: "多选", AttrTypeValue: 1 },
                            { AttrTypeName: "输入", AttrTypeValue: 2 },
                            { AttrTypeName: "选输", AttrTypeValue: 3 }
                        ], "AttrTypeName", "AttrTypeValue");
                    },
                    template: "#=Type#"
                },
                { field: "SelectableValues", title: "可选值（项目用,分割）", width: 200 },
                { field: "IsMust", title: "必填", template: function (d) { return $.osharp.tools.renderBoolean(d.IsMust); }, width: 75 },
                { command: [{ name: "destroy", text: "删除" }], width: 130 }
            ],
            pageable: false,
            sortable: false,
            filterable: false,
            height:300
        });

        $scope.specgridOptions = $.osharp.kendo.grid.Options({
            url: {
                read: "/admin/spec/read?categoryid=" + category.Id,
                create: "/admin/spec/create",
                update: "/admin/spec/update",
                destroy: "/admin/spec/delete"
            },
            model: {
                id: "Id",
                fields: {
                    Id: { type: "number", editable: false },
                    CategoryId: { type: "number", editable: true, nullable: false },
                    Name: { type: "string", editable: true, nullable: false },                    
                    SelectableValues: { type: "string", editable: true }                    
                }
            },
            columns: [
                { field: "Id", title: "编号", width: 75 },
                { field: "CategoryId", title: "所属类别", width: 75 },
                { field: "Name", title: "规格名称", width: 100 },                
                { field: "SelectableValues", title: "可选值（项目用,分割）", width: 200 },
                { command: [{ name: "destroy", text: "删除" }], width: 130 }
            ],
            pageable: false,
            sortable: false,
            filterable: false,
            height: 300
        });

        $scope.submit = function () {
            console.info("submit");
        };
        $scope.cancel = function () {
            $modalInstance.dismiss("cancel");
        };
    }
]);