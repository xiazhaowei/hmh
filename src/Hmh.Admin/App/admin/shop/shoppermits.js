var appName = "OSharpApp";

//店铺列表控制器
angular.module(appName, ["kendo.directives"]).controller("app.admin.shop.shoppermits", [
    "$scope", "$rootScope", "$state", function ($scope, $rootScope, $state) {
        $scope.title = $state.current.data.pageTitle;
        $scope.gridOptions = $.osharp.kendo.grid.Options({
            url: {
                read: "/admin/shoppermits/read"                
            },
            model: {
                id: "Id",
                fields: {
                    Id: { type: "number", editable: false },                    
                    ShopName: { type: "string", editable: false },
                    UserName: { type: "string", editable: false },
                    UserCardNum: { type: "string", editable: false },
                    BusinessLicenseNum: { type: "string", editable: false },
                    UserCardFront: { type: "string", editable: false },
                    UserCardReverse: { type: "string", editable: false },
                    AuthLicensePic: { type: "string", editable: false },
                    BusinessLicensePic: { type: "string", editable: false },
                    CreatedTime: { type: "date", editable: false },
                    State: { type: "number", editable: true }                    
                }
            },
            toolbar:["save", "cancel"],
            columns: [                
                { field: "Id", title: "编号", width: 75 },                
                { field: "ShopName", title: "店铺名称" },
                { field: "UserName", title: "法人" },
                { field: "UserCardNum", title: "法人证件号"},
                { field: "BusinessLicenseNum", title: "工商编号" },                           
                { field: "CreatedTime", title: "创建时间", format: "{0: yyyy-MM-dd HH:mm}"},
                {
                    command: [
                      {name:"setpermit", text: "资料..", click: setPermit }                      
                    ], width: 120
                }
            ]
        });

        function setPermit(e) {            
            var tr = $(e.target).closest("tr");
            var data = this.dataItem(tr);
            console.log(data);
            $scope.$broadcast("shop.setpermit", data);           
        }        
    }
]);



//店铺认证信息弹窗
angular.module(appName).controller("app.admin.shop.shops.shoppermitinfo", ["$scope", "$modal", function ($scope, $modal) {
    $scope.open = function (data, size) {
        $modal.open({
            templateUrl: "shop-shops-shoppermitinfo",
            controller: "app.admin.shop.shops.shoppermitinfo.modal",
            size: size,
            resolve: {//传递数据到控制器
                shoppermit: function () {
                    return data;
                }
            }
        });
    };
    $scope.$on("shop.setpermit", function (event, data) {
        $scope.open(data);
    });
}]);


//认证信息内容控制器
angular.module(appName).controller("app.admin.shop.shops.shoppermitinfo.modal", [
    "$scope", "$http", "$modalInstance", "shoppermit", function ($scope, $http, $modalInstance, shoppermit) {
        $scope.shoppermit = shoppermit;

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

        $http.get("/admin/shops/read-shoppermit?shopid=" + shoppermit.Id)
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




