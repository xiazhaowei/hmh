
//主导航
hmhApp.controller("hmh.mainnav", ["$scope", "$http", function ($scope, $http) {

    $http.post("/Home/GetMainNavCategory").success(function (data) {
        $scope.pubCategoryData = data;
    });

}]);

//主菜单
hmhApp.controller("hmh.allcategory", ["$scope", "$http", function ($scope, $http) {
    $http.post("/Home/GetMainNavCategory").success(function (data) {
        $scope.pubCategoryData = data;
    });

}]);


//商品列表
hmhApp.controller("hmh.goodslist", ["$scope", "$http", function ($scope, $http) {
    var catid = $.hmhShop.tools.url.getQueryString("catid");

    $scope.subcatisshow = true;
    // 配置分页基本参数
    $scope.paginationConf = {
        currentPage: 1,
        itemsPerPage: 20,
        pagesLength: 15,
        perPageOptions: [20, 24, 30, 40, 50]
    };

    //获取分类数据
    $http.post("/Home/GetCategory", { catid: catid }).success(function (data) {
        $scope.category = data;
        $scope.subcatisshow = $scope.category.Children.length > 0 ? true : false;
    });



    $scope.attrFilters = [];
    $scope.specFilters = [];
    $scope.postData = {
        catId: catid,
        isSelf: false,
        isStock: false,
        isExpressFree: false,
        minPrice: 0,
        maxPrice: 0,
        sort: "sortorder_desc",
        pageIndex: $scope.paginationConf.currentPage,
        pageSize: $scope.paginationConf.itemsPerPage
    };

    $scope.isChecked = function (attrName, attrValue) {
        //判断选择类别中是否已经添加
        var attrFilter = { Name: attrName, Values: [] };

        if ($.hmhShop.tools.array.isContainsObjectByName(attrFilter, $scope.attrFilters)) {
            var attrFilter1 = $.hmhShop.tools.array.getContainsObjectByName(attrFilter, $scope.attrFilters);
            return attrFilter1.Values.indexOf(attrValue) >= 0;
        }
        else
            return false;
    };
    $scope.isChecked2 = function (specName, specValue) {
        //判断选择类别中是否已经添加
        var specFilter = { Name: specName, Values: [] };

        if ($.hmhShop.tools.array.isContainsObjectByName(specFilter, $scope.specFilters)) {
            var specFilter1 = $.hmhShop.tools.array.getContainsObjectByName(specFilter, $scope.specFilters);
            return specFilter1.Values.indexOf(specValue) >= 0;
        }
        else
            return false;
    };

    $scope.checkAttr = function (attrName, attrValue) {
        var attrFilter = { Name: attrName, Values: [attrValue] };
        if ($.hmhShop.tools.array.isContainsObjectByName(attrFilter, $scope.attrFilters)) {
            var attrFilter1 = $.hmhShop.tools.array.getContainsObjectByName(attrFilter, $scope.attrFilters);
            if (attrFilter1.Values.indexOf(attrValue) >= 0)//包含该值
            {
                var idx = attrFilter1.Values.indexOf(attrValue);
                attrFilter1.Values.splice(idx, 1);//删除值

                if (attrFilter1.Values.length == 0)//剩余可选择是否为空删掉整个条件
                {
                    $scope.attrFilters.remove(attrFilter1);
                }
            }
            else {
                attrFilter1.Values.push(attrValue);
            }
        }
        else {
            $scope.attrFilters.push(attrFilter);
        }
        $scope.postData.attr = JSON.stringify($scope.attrFilters);
        reGetDatas();
    };
    
    $scope.checkSpec = function (specName, specValue) {
        var specFilter = { Name: specName, Values: [specValue] };
        if ($.hmhShop.tools.array.isContainsObjectByName(specFilter, $scope.specFilters)) {
            var specFilter1 = $.hmhShop.tools.array.getContainsObjectByName(specFilter, $scope.specFilters);
            if (specFilter1.Values.indexOf(specValue) >= 0)//包含该值
            {
                var idx = specFilter1.Values.indexOf(specValue);
                specFilter1.Values.splice(idx, 1);//删除值

                if (specFilter1.Values.length == 0)//剩余可选择是否为空删掉整个条件
                {
                    $scope.specFilters.remove(specFilter1);
                }
            }
            else {
                specFilter1.Values.push(specValue);
            }
        }
        else {
            $scope.specFilters.push(specFilter);
        }
        $scope.postData.spec = JSON.stringify($scope.specFilters);
        reGetDatas();
    };

    $scope.checkSort = function (sortField, sortOrder) {
        $scope.postData.sort = sortField + "_" + sortOrder;
        reGetDatas();
    };

    $scope.isChecked3 = function (sortField, sortOrder) {
        return $scope.postData.sort == sortField + "_" + sortOrder;
    };


    // 重新获取数据条目
    var reGetDatas = function () {
        // 发送给后台的请求数据 需要处理成后台可识别的形式 
        $scope.postData.pageIndex = $scope.paginationConf.currentPage;
        
        $http({
            method: "post",
            url: "/Home/GetGoodsList",
            data: $scope.postData,
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

    $scope.filterPrice = function () {
        reGetDatas();
    };

    $scope.$watch('paginationConf.currentPage + paginationConf.itemsPerPage', reGetDatas);
    $scope.$watch('postData.isSelf + postData.isStock+postData.isExpressFree', reGetDatas);
}]);

//商品详情
hmhApp.controller("hmh.goodsinfo", ["$scope", "$http",'$log', 'skuConfig', 'utilService', function ($scope, $http,$log,skuConfig,utilService) {
    
    $scope.isSkuCheckAll = true;
    $scope.buyCount = 1;
    $scope.checkedSku = "";
    $scope.price = 0;
    $scope.skuId = 0;

    var id = $.hmhShop.tools.url.getQueryString("id");
    
    $http.post("/Home/GetGoodsInfo", { id: id }).success(function (data) {
        $scope.goods = data;
        $scope.price = $scope.goods.Price;
        $scope.skuData = eval("(" + $scope.goods.Skus + ")");
        $scope.skuInfo = $scope.goods.SkuInfo;
        $scope.isSkuCheckAll = $scope.goods.SkuCount > 0 ? false : true;
    });

    //Sku选择回调
    $scope.skucallback = function (skuInfo) {
        $scope.count = skuInfo.count;
        $scope.price = skuInfo.price>0? skuInfo.price:$scope.price;
        $scope.isSkuCheckAll = skuInfo.ischeckall;
        $scope.checkedSku = skuInfo.skuinfo;
        $scope.skuId = skuInfo.skuid;
    };



    //收件地址变更
    $scope.$watch("receiveAddress", function (newval,oldval) {
        if (newval === oldval) return;
        console.log(newval);
    });    

    //添加购物车
    $scope.addCart = function () {
        if (!$scope.isSkuCheckAll) {
            $.hmhShop.tip2.danger("请选择商品规格");
            return;
        }
        //提交到服务的数据
        var postData = {
            GoodsId: $scope.goods.Id,
            SkuInfo: $scope.checkedSku,
            SkuId: $scope.skuId,
            BuyCount: $scope.buyCount,
            Price: $scope.price,
            ExpressFee: 0,
            Amount: $scope.buyCount * $scope.price,
            Name: $scope.goods.Name,
            Pic: "default.jpg"
        };

        //将商品信息提交到服务器保存
        $http.post("/Cart/AddCartGoods", postData).success(function (data) {
            //添加成功 通过事件广播传递数据到购物车下拉菜单
            $scope.$emit('addCartSuccess_Emit', postData);
        });

    };

    //立即购买
    $scope.buyNew = function () {

        if (!$scope.isSkuCheckAll) {
            $.hmhShop.tip2.danger("请选择商品规格");
            return;
        }
        //包含店铺信息
        var checkGoods = [];
        var checkshop = {
            Name: $scope.goods.Shop.Name,
            Items: []
        };
        var cartGoods = {
            GoodsId: $scope.goods.Id,
            SkuInfo: $scope.checkedSku,
            SkuId: $scope.skuId,
            BuyCount: $scope.buyCount,
            Price: $scope.price,
            ExpressFee: 0,
            Amount: $scope.buyCount * $scope.price,
            Name: $scope.goods.Name,
            Pic: "default.jpg"
        };

        checkshop.Items.push(cartGoods);
        checkGoods.push(checkshop);

        //post 到订单确认页面       
        $.hmhShop.from.post("/cart/confirmorder", { sfrom: "12345678", orderCart: JSON.stringify({ "orderCart": checkGoods }), referer: "goodsitem" });
    };


   
}]);

//商品详情-商品评论
hmhApp.controller("hmh.goodsinfo.goodscomments", ["$scope", "$http", function ($scope, $http) {
    var id = $.hmhShop.tools.url.getQueryString("id");
    // 重新获取数据条目
    var reGetDatas = function () {
        // 发送给后台的请求数据 需要处理成后台可识别的形式
        var postData = {
            goodsId:id,
            pageIndex: $scope.paginationConf.currentPage,
            pageSize: $scope.paginationConf.itemsPerPage
        };

        $http({
            method: "post",
            url: "/Home/GetGoodsComments",
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
            $scope.goodsComments = data.Rows;
        });        
    };

    // 配置分页基本参数
    $scope.paginationConf = {
        currentPage: 1,
        itemsPerPage: 10,
        pagesLength: 15,
        perPageOptions: [10, 15, 20, 30, 40, 50]
    };

    $scope.$watch('paginationConf.currentPage + paginationConf.itemsPerPage', reGetDatas);
}]);

//店铺主页
hmhApp.controller("hmh.shopinfo", ["$scope", "$http", function ($scope,$http) {   
    
    $http.post("/Home/GetShopInfo", { shopId: 3 }).success(function (data) {
        $scope.shop = data;
    });

}]);

//店铺主页-热卖商品
hmhApp.controller("hmh.shopinfo.hotgoods", ["$scope", "$http", function ($scope,$http) {
    $http.post("/Home/GetShopHotGoods", { shopId: 3 }).success(function (data) {
        $scope.hotGoodses = data;
    });
}]);
//店铺主页-新品
hmhApp.controller("hmh.shopinfo.newgoods", ["$scope", "$http", function ($scope, $http) {
    $http.post("/Home/GetShopNewGoods", { shopId: 3 }).success(function (data) {
        $scope.newGoodses = data;
    });
}]);
//店铺主页-商品列表
hmhApp.controller("hmh.shopinfo.shopgoods", ["$scope", "$http", function ($scope, $http) {    
    var shopId = $.hmhShop.tools.url.getQueryString("shopid");
    var catId = $.hmhShop.tools.url.getQueryString("catid");

    // 配置分页基本参数
    $scope.paginationConf = {
        currentPage: 1,
        itemsPerPage: 15,
        pagesLength: 15,
        perPageOptions: [10, 15, 20, 30, 40, 50]
    };


    $scope.postData = {
        shopId: shopId,
        catId: catId,
        isExpressFree: false,
        isStock:false,
        pageIndex: $scope.paginationConf.currentPage,
        pageSize: $scope.paginationConf.itemsPerPage
    };

    $scope.checkSort = function (sortInfo) {
        $scope.postData.sort = sortInfo;
        reGetDatas();
    };

    // 重新获取数据条目
    var reGetDatas = function () {
        $scope.postData.pageIndex = $scope.paginationConf.currentPage;
        //转变为form提交
        $http({
            method: "post",
            url: "/Home/GetShopGoods",
            data: $scope.postData,
            headers: { 'content-Type': 'application/x-www-form-urlencoded' },
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj) {
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                }
                return str.join("&");
            }
        }).success(function (data) {
            $scope.paginationConf.totalItems = data.Total;
            $scope.Goodses = data.Rows;
        });
    };

    

    // 通过$watch currentPage和itemperPage 当他们一变化的时候，重新获取数据条目
    $scope.$watch('paginationConf.currentPage + paginationConf.itemsPerPage', reGetDatas);
    $scope.$watch('postData.isStock+postData.isExpressFree', reGetDatas);
}]);






