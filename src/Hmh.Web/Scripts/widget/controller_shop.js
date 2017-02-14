
//店铺-创建店铺
hmhApp.controller("hmh.shop.createshop", ["$scope", "$http", "$modal", function ($scope, $http, $modal) {
    $scope.shopInfo = {
        Name: "",
        RecommendId: 0,
        RegionStr: "",
        AddrDetail: "",
        LinkMan: "",
        LinkManPhone: ""
    };

    //显示开店协议
    $scope.showShopAgreement = function () {
        $modal.open({
            templateUrl: "/scripts/tpls/shopagreement.html",
            controller: "hmh.shop.shopagreement.modal"
        });
    };

    //创建店铺
    $scope.createShop = function (shopInfo) {
        $http.post("/Shop/CreateShop", shopInfo).success(function (data) {
            if (data.Type == "Success") {
                location.href = "/shop/shoppermit";
            }
        });
    };
}]);

//店铺-开店协议弹窗
hmhApp.controller("hmh.shop.shopagreement.modal", ["$scope", "$modalInstance", function ($scope, $modalInstance) {
    $scope.cancel = function () {
        $modalInstance.dismiss("cancel");
    };
}]);

//店铺-资料
hmhApp.controller("hmh.shop.shoppermint", ["$scope", "$http", function ($scope, $http) {

    $scope.shopPermit = {
        UserName: "",
        UserCardNum: "",
        UserCardFront: "",
        UserCardReverse: "",
        BusinessLicenseNum: "",
        BusinessLicensePic: ""
    };

    //提交信息
    $scope.submitShopPermit = function (shopPermit) {
        $http.post("/Shop/ShopPermit", shopPermit).success(function (data) {
            if (data.Type == "Success") {
                location.href = "/Shop/SignContract";
            }
        });
    };
}]);

//店铺-签合同
hmhApp.controller("hmh.shop.signcontract", ["$scope", "$http", "$modal", function ($scope, $http, $modal) {
    $scope.contractInfo = {
        year: 1,
        initalfee: 10000,
        selectedcontractLevel: "普通店铺"
    };

    $scope.ContractLevels = [];
    $http.get("/Shop/GetContractLevel").then(function (response) {
        $scope.ContractLevels = response.data;
    });

    //预览默认店铺
    $scope.genDefaultContract = function () {
        getContract(1, "普通店铺");
    };

    //预览自定义店铺
    $scope.genContract = function () {
        getContract($scope.contractInfo.year, $scope.contractInfo.selectedcontractLevel);
    };

    //获取加盟费
    $scope.$watch('contractInfo.selectedcontractLevel', function (newval, oldval) {
        if (newval === oldval) return;

        var querResult = $.Enumerable.From($scope.ContractLevels)
                .Where(function (x) { return x.Name == newval; })
                .ToArray();

        if (querResult.length > 0) {
            $scope.contractInfo.initalfee = querResult[0].InitalFee;
        }
    });

    //预览合同
    var getContract = function (year, contractLevel) {
        $modal.open({
            templateUrl: "/scripts/tpls/perviewcontract.html",
            controller: "hmh.shop.signcontract.modal",
            resolve: {
                shop: function () {
                    return {
                        Name: "我的店铺",
                        Year: year,
                        ContractLevel: contractLevel
                    };
                }
            }
        });
    };
}]);

//店铺-签订合同预览-模态框
hmhApp.controller("hmh.shop.signcontract.modal", ["$scope", "$http", "$modalInstance", "shop", function ($scope, $http, $modalInstance, shop) {
    $scope.shop = shop;
    //签订合同
    $scope.signContract = function () {
        $http.post("/Shop/SignContract", $scope.shop).success(function (data) {
            if (data.Type == "Success") {
                location.href = "/Shop/ContractPay";
            }
            else {
                $.hmhShop.tip2.danger(data.Content);
            }
        });
    };
    $scope.cancel = function () {
        $modalInstance.dismiss("cancel");
    };
}]);

//店铺-保证金支付
hmhApp.controller("hmh.shop.paycontract", ["$scope", "$http", function ($scope, $http) {

    $scope.payInfo = {
        Money: null,
        PayStreamId: ""
    };

    $scope.submitContractPay = function () {
        $http.post("/Shop/ContractPay", $scope.payInfo).success(function (data) {
            if (data.Type == "Success") {
                location.href = "/Shop/OpenShop";
            }
        });
    }
}]);



///////////////////////////////////////店铺后台/////////////////////////////////

//店铺后台-商品管理
hmhApp.controller("hmh.shop.goodses", ["$scope", "$http", function ($scope, $http) {
    // 重新获取数据条目
    var reGetDatas = function () {
        // 发送给后台的请求数据 需要处理成后台可识别的形式
        var postData = {
            pageIndex: $scope.paginationConf.currentPage,
            pageSize: $scope.paginationConf.itemsPerPage
        };

        $http({
            method: "post",
            url: "/Shop/GetGoodses",
            data: postData,
            headers: { 'content-Type': 'application/x-www-form-urlencoded' },
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj) {
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                }
                return str.join("&");
            }
        }).success(function (data) {
            // 变更分页的总数
            $scope.paginationConf.totalItems = data.Total;
            // 变更产品条目
            $scope.goodses = data.Rows;
        });
    };

    // 配置分页基本参数
    $scope.paginationConf = {
        currentPage: 1,
        itemsPerPage: 10,
        pagesLength: 15,
        perPageOptions: [10, 15, 20, 30, 40, 50]
    };

    // 通过$watch currentPage和itemperPage 当他们一变化的时候，重新获取数据条目
    $scope.$watch('paginationConf.currentPage + paginationConf.itemsPerPage', reGetDatas);

    $scope.del = function (id) {
        //弹出框确认
        bootbox.confirm({
            message: "确定要删除该商品吗？",
            buttons: {
                confirm: {
                    label: '删除',
                    className: 'btn-danger'
                },
                cancel: {
                    label: '取消',
                    className: 'btn-success'
                }
            },
            callback: function (result) {
                if (result) {
                    //提交到服务器
                    $http.post("/shop/delGoods", { id: id }).success(function (data) {
                        if (data.Type == "Success") {
                            $.hmhShop.tip2.success(data.Content);
                            reGetDatas();
                        }
                        else {
                            $.hmhShop.tip2.danger("删除失败：" + data.Content);
                        }
                    });
                }
            }
        });
    };
}]);
//店铺后台-添加商品
hmhApp.controller("hmh.shop.creategoods", ["$scope", "$http", function ($scope, $http) {
    //组合函数
    var combine = function (arr) {
        var r = [];
        (function f(t, a, n) {
            if (n == 0) return r.push(t);
            for (var i = 0; i < a[n - 1].length; i++) {
                f(t.concat(a[n - 1][i]), a, n - 1);
            }
        })([], arr, arr.length);
        return r;
    };


    $scope.goodsInfo = {
        CategoryId: 3,
        Name: "",
        Description: "",
        Detail: "",
        Detail: "",
        GoodsPics: "",
        Price: 0,
        Stock: 0,
        GoodsNumber: "",
        BarCode: "",
        BeginTime: "",
        IsReceipt: false,
        IsGuarantee: false,
        IsReplacement: false,
        IsCommend: true,
        IsSevenDayReplacement: true,
        GoodsAttrs: [],
        Skus: []
    };

    $scope.skudata = {
        Price: "",
        Stock: 0,
        GoodsNumber: "",
        BarCode: ""
    };

    $scope.attrs = [];

    //通过请求参数获取分类ID
    //获取发布分类的属性
    $http.post("/Shop/GetAttrs", { catid: 3 }).success(function (data) {
        $scope.attrs = data;

        //便利属性绑定到商品对象的数组上
        angular.forEach($scope.attrs, function (attr) {
            $scope.goodsInfo.GoodsAttrs.push({
                IsMust: attr.IsMust,
                Type: attr.Type,
                SelectableValues: attr.SelectableValues,
                SortCode: attr.SortCode,
                AttrName: attr.Name,
                AttrValue: ""
            });
        })

    });

    $scope.expressTemplates = [];

    $http.post("/Shop/GetExpressTemplates").success(function (data) {
        $scope.expressTemplates = data.Rows;
    });

    ///////////////////////规格开始//////////////////////////////////
    $scope.isCreateGoodsSpecTable = false;
    $scope.isStockEnable = true;

    $scope.goodsSpecifications = [];
    $scope.selectedgoodspeces = [];
    //获取规格
    $http.post("/Shop/GetGoodsSpecifications", { catid: 3 }).success(function (data) {
        $scope.goodsSpecifications = data;
        //初始化已选项目
        angular.forEach($scope.goodsSpecifications, function (goodsspecification) {
            $scope.selectedgoodspeces.push({
                name: goodsspecification.Name,
                values: []
            });
        });
    });



    //判断选择框是否被选中
    $scope.isChecked = function (goodspecname, goodspecid, selectvalueid) {
        return $scope.selectedgoodspeces[goodspecid].values.indexOf(selectvalueid) >= 0;
    };
    //选择框选择改变
    $scope.updateSelection = function ($event, goodspecname, goodspecid, selectvalueid) {
        var checkbox = $event.target;
        var checked = checkbox.checked;
        if (checked) {
            $scope.selectedgoodspeces[goodspecid].values.push(selectvalueid);
        } else {
            $scope.selectedgoodspeces[goodspecid].values.remove(selectvalueid);
        }

        var ischeckall = true;
        angular.forEach($scope.selectedgoodspeces, function (selectedgoodspece) {
            if (!selectedgoodspece.values.length) {
                ischeckall = false;
            }
        });
        //判断是否全选
        if (ischeckall) {
            $scope.isCreateGoodsSpecTable = true;
            $scope.isStockEnable = false;
            createTable();
        }
        else {
            $scope.goodsInfo.Skus = [];
            $scope.isCreateGoodsSpecTable = false;
            $scope.isStockEnable = true;
        }
    };

    $scope.names = [];
    $scope.arr = [];

    //存放每列的rowspan
    $scope.tdrowspan = [];

    var createTable = function () {
        $scope.names = [];
        $scope.arr = [];
        $scope.goodsInfo.Skus = [];
        //存放每列的rowspan
        $scope.tdrowspan = [];

        angular.forEach($scope.selectedgoodspeces, function (selectedgoodspece) {
            $scope.names.push(selectedgoodspece.name);
            $scope.arr.push(selectedgoodspece.values);
        });
        $scope.arr.reverse();

        //6行数据
        $scope.res = combine($scope.arr);
        console.log($scope.res);

        //转换为对象形式
        angular.forEach($scope.res, function (res) {
            $scope.goodsInfo.Skus.push(
                {
                    names: $scope.names,
                    values: res,
                    Price: 0,
                    Stock: 0,
                    GoodsNumber: "",
                    BarCode: ""
                }
            );
        });

        //计算每列的rowspan
        var getRowSpan = function () {
            var rowspan = $scope.res.length;
            for (var n = $scope.arr.length - 1; n > -1; n--) {
                $scope.tdrowspan[n] = parseInt(rowspan / $scope.arr[n].length);
                rowspan = $scope.tdrowspan[n];
            }
            //存放每列的rowspan
            $scope.tdrowspan.reverse();
        };
        getRowSpan();

    };

    $scope.rowSpan = function (trmark, rowspan) {
        if (trmark % rowspan == 0 && rowspan > 1)
            return rowspan;
        else
            return 0;
    };
    $scope.rowShow = function (trmark, rowspan) {
        if (trmark % rowspan == 0 && rowspan > 1)
            return true;
        else if (rowspan == 1)
            return true;
    };

    //批量添加
    $scope.groupAddGoodsSpec = function () {
        var stocktotal = 0;
        //如果输入正确
        if (angular.isNumber($scope.skudata.Stock) && angular.isNumber($scope.skudata.Price)) {
            //更新sku
            angular.forEach($scope.goodsInfo.Skus, function (sku, index, array) {
                $scope.goodsInfo.Skus[index].Price = $scope.skudata.Price;
                $scope.goodsInfo.Skus[index].Stock = $scope.skudata.Stock;
                $scope.goodsInfo.Skus[index].GoodsNumber = $scope.skudata.GoodsNumber;
                $scope.goodsInfo.Skus[index].BarCode = $scope.skudata.BarCode;
                stocktotal += $scope.skudata.Stock;
            });
            //更新一口价
            $scope.goodsInfo.Price = $scope.skudata.Price;
            $scope.goodsInfo.Stock = stocktotal;
            $scope.goodsInfo.GoodsNumber = $scope.skudata.GoodsNumber;
            $scope.goodsInfo.BarCode = $scope.skudata.BarCode;
        }
    };

    //sku列表中价格改变
    $scope.skuPriceValueChange = function (inputval) {

        //如果输入正确 将sku中最小的值添加到商品价格上
        if (angular.isNumber(inputval)) {
            var prices = [];
            angular.forEach($scope.goodsInfo.Skus, function (sku) {
                if (angular.isNumber(sku.Price)) {
                    prices.push(sku.Price);
                }
            });
            $scope.goodsInfo.Price = prices.min();
        }

    };

    $scope.skuStockValueChange = function (inputval) {
        if (angular.isNumber(inputval)) {
            var totalstock = 0;
            angular.forEach($scope.goodsInfo.Skus, function (sku) {
                if (angular.isNumber(sku.Stock)) {
                    totalstock += sku.Stock;
                }
            });
            $scope.goodsInfo.Stock = totalstock;
        }
    };

    /////////////////////////////规格结束////////////////////////

    $scope.isShowBeginTime = false;


    $scope.postgoods = function (goodsInfo) {
        $http.post("/Shop/CreateGoods", JSON.stringify({ "dto": goodsInfo })).success(function (data) {
            $.hmhShop.tip2.success(data.Content);
            location.href = "/Shop/Goodses";
        }).error(function (data) {
            $.hmhShop.tip2.danger(data.Content);
        });
    }
}]);

//店铺后台-运费模板管理
hmhApp.controller("hmh.shop.expresstemplates", ["$scope", "$http", function ($scope, $http) {

    // 重新获取数据条目
    var reGetDatas = function () {
        // 发送给后台的请求数据 需要处理成后台可识别的形式
        var postData = {
            pageIndex: $scope.paginationConf.currentPage,
            pageSize: $scope.paginationConf.itemsPerPage
        };

        $http({
            method: "post",
            url: "/Shop/GetExpressTemplates",
            data: postData,
            headers: { 'content-Type': 'application/x-www-form-urlencoded' },
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj) {
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                }
                return str.join("&");
            }
        }).success(function (data) {
            // 变更分页的总数
            $scope.paginationConf.totalItems = data.Total;
            // 变更产品条目
            $scope.expressTemplates = data.Rows;
        });

        //直接post提交是json方式  requestpayload  后台request.param[]取不到输入值
        /*$http.post('/shop/GetExpressTemplates', postData).success(function (data) {
            // 变更分页的总数
            $scope.paginationConf.totalItems = data.Total;
            // 变更产品条目
            $scope.expressTemplates = data.Rows;
        });*/
    };

    // 配置分页基本参数
    $scope.paginationConf = {
        currentPage: 1,
        itemsPerPage: 10,
        pagesLength: 15,
        perPageOptions: [10, 15, 20, 30, 40, 50]
    };

    // 通过$watch currentPage和itemperPage 当他们一变化的时候，重新获取数据条目
    $scope.$watch('paginationConf.currentPage + paginationConf.itemsPerPage', reGetDatas);

    $scope.del = function (id) {
        //弹出框确认
        bootbox.confirm({
            message: "确定要删除该运费模板吗？",
            buttons: {
                confirm: {
                    label: '删除',
                    className: 'btn-danger'
                },
                cancel: {
                    label: '取消',
                    className: 'btn-success'
                }
            },
            callback: function (result) {
                if (result) {
                    //提交到服务器
                    $http.post("/shop/delExpressTemplate", { id: id }).success(function (data) {
                        if (data.Type == "Success") {
                            $.hmhShop.tip2.success(data.Content);
                            reGetDatas();
                        }
                        else {
                            $.hmhShop.tip2.danger("删除失败：" + data.Content);
                        }
                    });
                }
            }
        });
    };

}]);

//店铺后台-创建运费模板
hmhApp.controller("hmh.shop.createexpresstemplate", ["$scope", "$http", function ($scope, $http) {
    $scope.expressTemplateInfo = {
        Name: "",
        DeliverAddress: "",
        DeliverTime: "",
        Count: 0,
        Price: 0,
        CountAdd: 0,
        PriceAdd: 0,
        IsFree: true,
        SpecialExpressAddresses: []

    };

    //添加特殊地区
    $scope.addSpecialExpressAddress = function () {
        $scope.expressTemplateInfo.SpecialExpressAddresses.push({ Address: "", Count: 0, Price: 0, CountAdd: 0, PriceAdd: 0 });
    };
    //删除特殊地区
    $scope.delSpecialExpressAddress = function (specialExpressAddress) {
        $scope.expressTemplateInfo.SpecialExpressAddresses.remove(specialExpressAddress);
    };

    $scope.postExpressTemplate = function (expressTemplateInfo) {
        $http.post("/Shop/CreateExpressTemplate", JSON.stringify({ "dto": expressTemplateInfo })).success(function (data) {
            $.hmhShop.tip2.success(data.Content);
            location.href = "/Shop/ExpressTemplates";
        }).error(function (data) {
            $.hmhShop.tip2.danger(data.Content);
        });

        //var option = {
        //    url: "/shop/createexpresstemplate",
        //    type: "POST",
        //    contentType: 'application/json; charset=utf-8',
        //    dataType: 'json',
        //    data: JSON.stringify({ "dto": expressTemplateInfo }),
        //    traditional:true,
        //    success: function (result) {
        //        console.log(result);
        //    }
        //};
        //$.ajax(option)
    }
}]);