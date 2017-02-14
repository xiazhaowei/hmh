var appName = "OSharpApp";

//店铺列表控制器
angular.module(appName, ["kendo.directives"]).controller("app.admin.shop.shops", [
    "$scope", "$rootScope", "$state", function ($scope, $rootScope, $state) {
        $scope.title = $state.current.data.pageTitle;
        $scope.gridOptions = $.osharp.kendo.grid.Options({
            url: {
                read: "/admin/shops/read",
                create: "/admin/shops/create",
                update: "/admin/shops/update",
                destroy: "/admin/shops/delete"
            },
            model: {
                id: "Id",
                fields: {
                    Id: { type: "number", editable: false },
                    UserId: { type: "number", editable: false },
                    Name: { type: "string", editable: false },
                    UserName: { type: "string", editable: false },
                    HCoinLimit: { type: "number", editable: true, validation: { required: true, min: 0 } },
                    AddrDetail: { type: "string", editable: true },
                    LinkMan: { type: "string", editable: false },
                    LinkManPhone:{type:"string",editable:false},
                    BusinessState: { type: "number", editable: true },
                    State: { type: "number", editable: true },
                    CreatedTime: { type: "date", editable: false }
                }
            },
            toolbar:["save", "cancel"],
            columns: [                
                { field: "Id", title: "编号", width: 75 },
                { field: "Name", title: "店铺名称" },                
                { field: "UserName", title: "用户" },
                { field: "LinkMan", title: "联系人" ,width:100},                
                { field: "AddrDetail", title: "详细地址" },
                { field: "HCoinLimit", title: "H币限制" },
                {
                    field: "BusinessState",
                    title: "营业状态",
                    editor: function (container, options) {
                        $.osharp.kendo.grid.DropDownListEditor(container, options, [
                            { StateName: "营业中", StateValue: 0 },
                            { StateName: "已关店", StateValue: 1 }
                        ], "StateName", "StateValue");
                    },
                    template: "#=BusinessState#"
                },
                {
                    field: "State",
                    title: "状态",
                    editor: function (container, options) {
                        $.osharp.kendo.grid.DropDownListEditor(container, options, [
                            { StateName: "正常", StateValue: 0 },
                            { StateName: "锁定", StateValue: 1 },
                            { StateName: "资料不可用", StateValue: 2 },
                            { StateName: "合同未付款", StateValue: 3 },
                            { StateName: "合同不可用", StateValue: 4 }
                        ], "StateName", "StateValue");
                    },
                    template: "#=State#"
                },
                { field: "CreatedTime", title: "创建时间", format: "{0: yyyy-MM-dd HH:mm}",width:100 },
                {
                    command: [
                      {name:"setpermit", text: "资料..", click: setPermit },
                      { name: "shopcontract", text: "合同..", click: setContract },
                      { name: "getinfo", text: "查看..", click: shopInfo }
                    ], width: 240
                }
            ]
        });

        function setPermit(e) {            
            var tr = $(e.target).closest("tr");
            var data = this.dataItem(tr);           
            $scope.$broadcast("shop.setpermit", data);           
        }

        function setContract(e) {
            var tr = $(e.target).closest("tr");
            var data = this.dataItem(tr);
            $scope.$broadcast("shop.setcontract", data);
        }

        function shopInfo(e)
        {
            var tr = $(e.target).closest("tr");
            var data = this.dataItem(tr);
            $scope.$broadcast("shop.getinfo", data);
        }
    }
]);



//店铺认证信息弹窗
angular.module(appName).controller("app.admin.shop.shops.setpermit", ["$scope", "$modal", function ($scope, $modal) {
    $scope.open = function (data, size) {
        $modal.open({
            templateUrl: "shop-shops-setpermit",
            controller: "app.admin.shop.shops.setpermit.modal",
            size: size,
            resolve: {//传递数据到控制器
                shop: function () {
                    return data;
                }
            }
        });
    };
    $scope.$on("shop.setpermit", function (event, data) {
        $scope.open(data);
    });
}]);
//店铺合同弹窗
angular.module(appName).controller("app.admin.shop.shops.setcontract", ["$scope", "$modal", function ($scope, $modal) {
    $scope.open = function (data, size) {
        $modal.open({
            templateUrl: "shop-shops-setcontract",
            controller: "app.admin.shop.shops.setcontract.modal",
            size: size,
            resolve: {//传递数据到控制器
                shop: function () {
                    return data;
                }
            }
        });
    };
    $scope.$on("shop.setcontract", function (event, data) {
        $scope.open(data,"lg");
    });
}]);

//店铺认证信息弹窗
angular.module(appName).controller("app.admin.shop.shops.getinfo", ["$scope", "$modal", function ($scope, $modal) {
    $scope.open = function (data, size) {
        $modal.open({
            templateUrl: "shop-shops-getinfo",
            controller: "app.admin.shop.shops.getinfo.modal",
            size: size,
            resolve: {//传递数据到控制器
                shop: function () {
                    return data;
                }
            }
        });
    };
    $scope.$on("shop.getinfo", function (event, data) {
        $scope.open(data);
    });
}]);

//认证信息内容控制器
angular.module(appName).controller("app.admin.shop.shops.setpermit.modal", [
    "$scope", "$http", "$modalInstance", "shop", function ($scope, $http, $modalInstance, shop) {
        $scope.shop = shop;

        $scope.shoppermitstate = [
            { name: "审核中", value: 0 },
            { name: "审核未通过", value: 1 },
            { name: "审核通过", value: 2 }
        ];

        $scope.isChanged = false;
        $scope.isShowErrorMessage = true;

        $scope.stateChange = function () {
            $scope.isChanged = true;
            $scope.isShowErrorMessage = $scope.shopPermit.State == 2 ? false : true;
        };

        //从服务器获取店铺的认证资料
        $scope.shopPermit = {State:0};

        $http.get("/admin/shops/read-shoppermit?shopid=" + shop.Id)
            .then(function (res) {
                $scope.shopPermit = res.status === 200 ? res.data : {};                
            });

        $scope.submit = function () {
            console.log($scope.isChanged);
            if ($scope.isChanged) {
                //提交状态到服务器器
                $http.post("/admin/shops/post-shoppermit", $scope.shopPermit).success(function (res) {
                    $modalInstance.dismiss("cancel");
                }).error(function (res) {
                    console.log(res.data);
                });
            }
            else
                $modalInstance.dismiss();
        };
        $scope.cancel = function () {
            $modalInstance.dismiss("cancel");
        };
    }
]);

//合同内容控制器
angular.module(appName).controller("app.admin.shop.shops.setcontract.modal", ["$scope", "$http", "$modalInstance", "shop", function ($scope, $http, $modalInstance, shop) {
    $scope.shop = shop;

    $scope.contractgridOptions = $.osharp.kendo.grid.Options({
        url: {
            read: "/admin/contracts/read?shopid="+$scope.shop.Id
        },
        model: {
            id: "Id",
            fields: {
                Id: { type: "number", editable: false },
                Number: { type: "string", editable: false },
                Fee: { type: "number", editable: false },
                Year: { type: "number", editable: false },
                HCoinLimit: { type: "number", editable: false },
                CreatedTime: { type: "date", editable: true },
                BeginTime: { type: "date", editable: false },
                EndTime: { type: "date", editable: false },                
                PayState: { type: "number", editable: false },
                State: { type: "number", editable: false }                
            }
        },
        toolbar: [],
        sortable: false,
        pageable: false,
        editable: false,
        filterable:false,
        columns: [            
            { field: "Number", title: "合同编号" },
            { field: "Fee", title: "保本金额" },
            { field: "CreatedTime", title: "创建时间", format: "{0: yyyy-MM-dd HH:mm}", width: 100 },
            { field: "Year", title: "年限", width: 100 },
            { field: "HCoinLimit", title: "H币限制" }, 
            { field: "PayState", title: "付款状态", width: 100 },
            { field: "State", title: "状态", width: 100 }
        ]
    });

    $scope.submit = function () {
        $modalInstance.dismiss();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss("cancel");
    };
}]);


//合同内容控制器
angular.module(appName).controller("app.admin.shop.shops.getinfo.modal", ["$scope", "$http", "$modalInstance", "shop", function ($scope, $http, $modalInstance, shop) {
    $scope.shop = shop;

    //获取认证信息


    //获取付款信息

    $scope.submit = function () {
        $modalInstance.dismiss();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss("cancel");
    };
}]);



