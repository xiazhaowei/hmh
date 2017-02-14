
//购物车-下拉
hmhApp.controller("hmh.mycartgoodses", ["$scope", "$http", function ($scope, $http) {
    $scope.isCartEmpty = true;
    $scope.cartGoodsCount = 0;//商品总计
    $scope.goodsCount = 0;//商品种类总计
    $http.post("/Cart/GetCartGoods").success(function (data) {
        $scope.cartGoodses = data;
        $scope.goodsCount = $scope.cartGoodses.length;
        $scope.isCartEmpty = $scope.cartGoodses.length ? false : true;
        angular.forEach($scope.cartGoodses, function (cartGoods) {
            $scope.cartGoodsCount = $scope.cartGoodsCount + cartGoods.BuyCount;
        });
    });
    //计算购物车总价值
    $scope.getCartGoodsAmount = function () {
        var amount = 0;
        angular.forEach($scope.cartGoodses, function (cartGoods) {
            amount += cartGoods.Amount;
        });
        return amount;
    };
    //删除购物车商品
    $scope.del = function (cartGoods) {
        $http.post("/Cart/DelCartGoods", { id: cartGoods.Id }).success(function (data) {
            $scope.cartGoodses.remove(cartGoods);
            $scope.goodsCount--;
            $scope.cartGoodsCount -= cartGoods.BuyCount;
        });
    };

    //接收添加购物车成功事件
    $scope.$on('addCartSuccess_Broadcast', function (event, obj) {
        console.log(obj);
        //判断重复性如果是相同商品相同规格只改变数量
        var isRepeat = false;
        angular.forEach($scope.cartGoodses, function (cartGoods) {
            if (cartGoods.GoodsId == obj.GoodsId && cartGoods.SkuId == obj.SkuId) {
                cartGoods.BuyCount += obj.BuyCount;
                isRepeat = true;
            }
        });
        if (!isRepeat) {
            $scope.cartGoodses.push(obj);
            $scope.goodsCount++;
        }
        $scope.cartGoodsCount += obj.BuyCount;
    });

}]);

//购物车
hmhApp.controller("hmh.cart.index", ["$scope", "$http", function ($scope, $http) {

    $http.post("/Cart/GetCartGoodsGrouped").success(function (data) {
        $scope.cartGoodses = data;
    });

    $scope.isCheckAll = false;

    $scope.$watch("isCheckAll", function (newval) {
        //获取店铺下的所有商品
        angular.forEach($scope.cartGoodses, function (shop) {
            angular.forEach(shop.Goodses, function (cartGoods) {
                cartGoods.Checked = newval;
            });
        });
    });

    //店铺级别全选
    $scope.shopCheckAll = function ($event, key) {
        var checkbox = $event.target;
        //获取店铺下的所有商品
        angular.forEach($scope.cartGoodses, function (shop) {
            if (shop.Key == key) {
                angular.forEach(shop.Goodses, function (cartGoods) {
                    cartGoods.Checked = checkbox.checked;
                });
            }
        });

    };

    //删除购物车商品
    $scope.del = function (cartGoods) {
        $http.post("/Cart/DelCartGoods", { id: cartGoods.Id }).success(function (data) {
            //删除浏览器端数据
            angular.forEach($scope.cartGoodses, function (shop) {
                angular.forEach(shop.Goodses, function (cg) {
                    if (cartGoods.Id == cg.Id) {
                        shop.Goodses.remove(cg);
                        return;
                    }
                });
            });
        });
    };

    //获取选中商品数量
    $scope.getCheckCount = function () {
        var count = 0;
        angular.forEach($scope.cartGoodses, function (shop) {
            angular.forEach(shop.Goodses, function (cartGoods) {
                if (cartGoods.Checked) {
                    count++;
                }
            });
        });
        return count;
    };
    //获取选中商品总价
    $scope.getCheckAmount = function () {
        var amount = 0;
        angular.forEach($scope.cartGoodses, function (shop) {
            angular.forEach(shop.Goodses, function (cartGoods) {
                if (cartGoods.Checked) {
                    amount += cartGoods.Price * cartGoods.BuyCount;
                }
            });
        });
        return amount;
    };

    //提交结算
    $scope.confirmOrder = function () {
        if ($scope.getCheckCount() == 0) {
            $.hmhShop.tip2.danger("至少选择一种商品");
            return;
        }
        var checkGoods = [];
        //获取选中的商品
        angular.forEach($scope.cartGoodses, function (shop) {
            angular.forEach(shop.Goodses, function (cartGoods) {
                if (cartGoods.Checked) {
                    var checkshop = {
                        Name: shop.Key,
                        Items: []
                    };
                    //判断是否已经添加店铺
                    if ($.hmhShop.tools.array.isContainsObjectByName(checkshop, checkGoods)) {
                        $.hmhShop.tools.array.getContainsObjectByName(checkshop, checkGoods).Items.push(cartGoods);
                    }
                    else {
                        checkshop.Items.push(cartGoods);
                        checkGoods.push(checkshop);
                    }
                }
            });
        });
        console.log(checkGoods);
        console.log(JSON.stringify({ "orderCart": checkGoods }));
        //post到订单确认页
        $.hmhShop.from.post("/cart/confirmorder", { sfrom: "12345678", orderCart: JSON.stringify({ "orderCart": checkGoods }), referer: "cart" });
    };
}]);

//确认购物车
hmhApp.controller("hmh.cart.confirmorder", ["$rootScope", "$scope", "$http", "ACCOUNT_EVENTS", "$modal", function ($rootScope, $scope, $http, ACCOUNT_EVENTS, $modal) {
    $scope.orderInfo = {
        OrderExpress: null,
        Shops: []
    };

    var sfrom = $("#hid_sfrom").val();
    //用户提交页面判断购买数量是否可以修改
    $scope.referer = $("#hid_referer").val();
    var orderCartStr = $("#hid_ordercart").val();

    //判断数据是否有误
    if (sfrom == "") {
        $.hmhShop.tip2.danger("数据错误，请重试~");
    }

    $scope.orderCarts = eval("(" + orderCartStr + ")");

    //订单店铺和店铺的商品，提交到后端做分单处理
    $scope.orderInfo.Shops = $scope.orderCarts.orderCart;



    //店铺小计
    $scope.getShopAmount = function (shop) {
        var result = 0;
        angular.forEach(shop.Items, function (goods) {
            result += (goods.Price * goods.BuyCount);
        });
        return result;
    };


    //提交订单
    $scope.submitOrder = function () {
        $http.post("/Cart/CreateOrder", JSON.stringify({ "orderInfo": $scope.orderInfo })).success(function (data) {

        }).error(function (data) {
            $.hmhShop.tip2.danger(data.Content);
        });
    };



    //获取用户的收件地址 系统返回当前登录用户的收件地址
    $http.post("/Account/GetMyDeliverAddress").success(function (data) {
        $scope.myDeliverAddresses = data;
        //设置订单的收件信息为默认值
        angular.forEach($scope.myDeliverAddresses, function (deliverAddress) {
            if (deliverAddress.IsDefault) {
                $scope.orderInfo.OrderExpress = deliverAddress;
            }
        });
    });
    //设置订单的收货地址
    $scope.setOrderExpress = function (expressInfo) {
        $scope.orderInfo.OrderExpress = expressInfo;
    };
    //使用新的收件地址
    $scope.addDeliverAddress = function (data) {
        $modal.open({
            templateUrl: "/scripts/tpls/createdeliveraddressmodal.html",
            controller: "hmh.account.mydeliveraddresses.createdeliveraddress.modal",
            resolve: {
                deliveraddress: function () {
                    return data;
                }
            }
        });
    };
    //监听添加收件地址成功事件
    $rootScope.$on(ACCOUNT_EVENTS.createDeliverAddressSuccess, function (event, data) {
        var newDeliverAddress = data;
        if (!newDeliverAddress.Id) {
            newDeliverAddress.IsDefault = true;
            $scope.myDeliverAddresses.push(newDeliverAddress);
            //订单地址设置为新添加的地址
            $scope.orderInfo.OrderExpress = newDeliverAddress;
        }
    });
}]);

