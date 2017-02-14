
//账户-登录
hmhApp.controller('hmh.account.login', ["$scope", "AuthService", function ($scope, AuthService) {
    $scope.loginInfo = {
        UserName: "",
        Password: "",
        ReturnUrl: $('input[name="ReturnUrl"]').val(),
        Remember: false
    };

    $scope.inError = false;
    $scope.submitted = false;
    $scope.login = function (loginInfo) {
        if ($scope.loginForm.$valid) {
            AuthService.login(loginInfo).then(function (data) {
                if (data.Type === "Success") {
                    location.href = loginInfo.ReturnUrl;
                } else {
                    showErr(data.Content);
                }
            });
        }
        else {
            $scope.submitted = true;
        }
    };

    $scope.$watch('loginInfo.UserName', function () {
        $scope.inError = false;
        $('div.alert-danger').find("span").last().html("");
    });


    var showErr = function (msg) {
        $scope.inError = true;
        $('div.alert-danger').find("span").last().html(msg);
    };
}]);

//账号-登录-弹出框
hmhApp.controller("hmh.account.login.modal", ["$scope", "$http", "AuthService", function ($scope, $http, AuthService) {
    $scope.loginInfo = {
        UserName: "",
        Password: "",
        Remember: false
    };

    $scope.inError = false;
    $scope.submitted = false;
    $scope.login = function (loginInfo) {
        AuthService.login(loginInfo).then(function (data) {
            if (data.Type === "Success") {
                location.reload();
            } else {
                showErr(data.Content);
            }
        });
        $scope.submitted = true;
    };
    var showErr = function (msg) {
        $scope.inError = true;
        $('div.alert-danger').find("span").last().html(msg);
    };
}]);

//账户-注册
hmhApp.controller("hmh.account.register", ["$scope", "AuthService", "$location", function ($scope, AuthService, $location) {

    $scope.registerInfo = {
        UserName: "",
        NickName: "商城用户",
        Email: "",
        EmailCode: "",
        Password: "",
        RecommendId: $location.search().recommendid || 1

    };
    console.log($location.search());
    $scope.inError = false;
    $scope.register = function (registerInfo) {
        AuthService.register(registerInfo).then(function (data) {
            if (data.Type === "Success") {
                location.href = "/Account/Welcome";
            } else {
                showErr(data.Content);
            }
        });
    };
    var showErr = function (msg) {
        $scope.inError = true;
        $('div.alert-danger').find("span").last().html(msg);
    };
}]);



//////////////////////////////////////用户中心/////////////////////////

//账户-我邀请的人
hmhApp.controller("hmh.account.myinvotes", ["$scope", "$http", function ($scope, $http) {
    // 重新获取数据条目
    var reGetDatas = function () {
        var postData = {
            pageIndex: $scope.paginationConf.currentPage,
            pageSize: $scope.paginationConf.itemsPerPage
        };

        //转变为form提交
        $http({
            method: "post",
            url: "/Account/GetMyinvotes",
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
            $scope.paginationConf.totalItems = data.Total;
            $scope.myInvotes = data.Rows;
        });
    };

    // 配置分页基本参数
    $scope.paginationConf = {
        currentPage: 1,
        itemsPerPage: 15,
        pagesLength: 15,
        perPageOptions: [15, 15, 20, 30, 40, 50]
    };

    // 通过$watch currentPage和itemperPage 当他们一变化的时候，重新获取数据条目
    $scope.$watch('paginationConf.currentPage + paginationConf.itemsPerPage', reGetDatas);

}]);



//用户中心-编辑个人资料
hmhApp.controller("hmh.account.editprofile", ["$scope", "$http", function ($scope, $http) {
    $scope.userBaseInfo = {
        NickName: "",
        PhoneNumber: "",
        Sex: "",
        Birthday: ""
    };

    //从服务器获取用户信息
    $http.get("/Account/GetEditProfile").success(function (data) {
        $scope.userBaseInfo = data;
    });

    //提交到服务器
    $scope.editProfile = function (userBaseInfo) {
        $http.post("/Account/EditProfile", userBaseInfo).success(function (data) {
            $.hmhShop.tip2.success(data.Content);
        })
        .error(function (data) {
            $.hmhShop.tip2.danger(data.Content);
        });
    };
}]);

//用户中心-收件地址
hmhApp.controller("hmh.account.mydeliveraddresses", ["$rootScope", "$scope", "$http", "ACCOUNT_EVENTS", "$modal", function ($rootScope, $scope, $http, ACCOUNT_EVENTS, $modal) {
    $http.post("/Account/GetMyDeliverAddress").success(function (data) {
        $scope.myDeliverAddresses = data;
    });

    $scope.add = function () {
        $modal.open({
            templateUrl: "/scripts/tpls/createdeliveraddressmodal.html",
            controller: "hmh.account.mydeliveraddresses.createdeliveraddress.modal",
            resolve: {
                deliveraddress: function () {
                    return {};
                }
            }
        });
    };

    $scope.edit = function (index) {
        var deliveraddress = $scope.myDeliverAddresses[index];
        $modal.open({
            templateUrl: "/scripts/tpls/createdeliveraddressmodal.html",
            controller: "hmh.account.mydeliveraddresses.createdeliveraddress.modal",
            resolve: {
                deliveraddress: function () {
                    return deliveraddress;
                }
            }
        });
    };

    $scope.del = function (index) {
        var deliveraddress = $scope.myDeliverAddresses[index];
        //弹出框确认
        bootbox.confirm({
            message: "确定要删除吗？",
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
                    $http.post("/Account/DelDeliverAddress", { id: deliveraddress.Id }).success(function (data) {
                        if (data.Type == "Success") {
                            $.hmhShop.tip2.success(data.Content);
                            $scope.myDeliverAddresses.removeAt(index);
                        }
                        else {
                            $.hmhShop.tip2.danger("删除失败：" + data.Content);
                        }
                    });
                }
            }
        });
    };

    //监听添加收件地址成功事件
    $rootScope.$on(ACCOUNT_EVENTS.createDeliverAddressSuccess, function (event, data) {
        var newDeliverAddress = data;
        if (!newDeliverAddress.Id) {
            $scope.myDeliverAddresses.push(newDeliverAddress);
        }
    });
}]);

//用户中心-收件地址-添加
hmhApp.controller("hmh.account.mydeliveraddresses.createdeliveraddress.modal", ["$scope", "$http", "ACCOUNT_EVENTS", "$modalInstance", "deliveraddress", function ($scope, $http, ACCOUNT_EVENTS, $modalInstance, deliveraddress) {
    $scope.deliveraddress = deliveraddress;

    $scope.submit = function () {
        $http.post("/Account/CreateDeliverAddress", $scope.deliveraddress).success(function (data) {
            if (data.Type == "Success") {
                //广播添加地址成功事件
                $scope.$emit(ACCOUNT_EVENTS.createDeliverAddressSuccess, $scope.deliveraddress);
                $modalInstance.dismiss("cancel");
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

//用户中心-银行卡管理
hmhApp.controller("hmh.account.mybankcards", ["$rootScope", "$scope", "$http", "ACCOUNT_EVENTS", "$modal", function ($rootScope, $scope, $http, ACCOUNT_EVENTS, $modal) {
    $http.post("/Account/GetMyBankCard").success(function (data) {
        $scope.myBankCards = data;
    });
    $scope.add = function () {
        $modal.open({
            templateUrl: "/scripts/tpls/createbankcardmodal.html",
            controller: "hmh.account.mybankcards.createbankcard.modal",
            resolve: {
                bankcard: function () {
                    return {};
                }
            }
        });
    };
    $scope.del = function (index) {
        var bankcard = $scope.myBankCards[index];
        //弹出框确认
        bootbox.confirm({
            message: "确定要删除吗？",
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
                    $http.post("/Account/DelBankCard", { id: bankcard.Id }).success(function (data) {
                        if (data.Type == "Success") {
                            $.hmhShop.tip2.success(data.Content);
                            $scope.myBankCards.removeAt(index);
                        }
                        else {
                            $.hmhShop.tip2.danger("删除失败：" + data.Content);
                        }
                    });
                }
            }
        });
    };

    //监听添加银行卡成功事件
    $rootScope.$on(ACCOUNT_EVENTS.createBankCardSuccess, function (event, data) {
        var newbankcard = data;
        $scope.myBankCards.push(newbankcard);
    });

}]);
//用户中心-收藏的商品
hmhApp.controller("hmh.account.mygoodscollects", ["$scope", "$http", function ($scope, $http) {
    $http.post("/Account/GetMyGoodsCollects").success(function (data) {
        $scope.myGoodsCollects = data;
    });

    $scope.del = function (index) {
        var collect = $scope.myGoodsCollects[index];
        //弹出框确认
        bootbox.confirm({
            message: "确定要删除吗？",
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
                    $http.post("/Account/DelCollect", { id: collect.Id }).success(function (data) {
                        if (data.Type == "Success") {
                            $.hmhShop.tip2.success(data.Content);
                            $scope.myGoodsCollects.removeAt(index);
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

//用户中心-银行卡-添加
hmhApp.controller("hmh.account.mybankcards.createbankcard.modal", ["$scope", "$http", "ACCOUNT_EVENTS", "$modalInstance", "bankcard", function ($scope, $http, ACCOUNT_EVENTS, $modalInstance, bankcard) {
    $scope.bankcard = bankcard;

    $scope.submit = function () {
        $http.post("/Account/CreateBankCard", $scope.bankcard).success(function (data) {
            if (data.Type == "Success") {
                //广播添加地址成功事件
                $scope.$emit(ACCOUNT_EVENTS.createBankCardSuccess, $scope.bankcard);
                $modalInstance.dismiss("cancel");
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

//用户中心-设置新密码邮箱验证
hmhApp.controller("hmh.account.passwordemailverify", ["$scope", "$http", function ($scope, $http) {
    $scope.verifyInfo = {
        emailCode: ""
    };

    $scope.postPwdEmailVerify = function (verifyInfo) {
        $http.post("/Account/PasswordEmailVerify", verifyInfo).success(function (data) {
            if (data.Type == "success") {
                location.href = "/Account/SetNewPassword";
            }
            else {
                $.hmhShop.tip2.danger(data.Content);
            }
        });
    };
}]);

//用户中心-设置新密码
hmhApp.controller("hmh.account.setnewpassword", ["$scope", "$http", function ($scope, $http) {
    $scope.newPassword = {
        Password: ""
    };

    $scope.postNewPassword = function (newPassword) {
        $http.post("/Account/SetNewPassword", newPassword).success(function (data) {
            if (data.Type == "Success") {
                location.href = "/Account/SafeCenter";
            }
            else {
                $.hmhShop.tip2.danger(data.Content);
            }
        });
    };
}]);

//用户中心-财务管理
hmhApp.controller("hmh.account.moneymgr", ["$scope", "$modal", function ($scope, $modal) {
    $scope.widthdraw = function () {
        $modal.open({
            templateUrl: "account.moneylog.widthdraw",
            controller: "hmh.account.widthdraw.modal"
        });
    };
    $scope.transaction = function () {
        $modal.open({
            templateUrl: "account.moneylog.transaction",
            controller: "hmh.account.transaction.modal"
        });
    };
}]);

//用户中心-现金记录
hmhApp.controller("hmh.account.moneylog", ["$scope", "$http", function ($scope, $http) {
    $scope.filters = [
        {
            Field: "StreamId",
            Value: "",
            Operate: "contains"
        },
        {
            Field: "Type",
            Value: "All",
            Operate: "equal"
        },
        {
            Field: "Direction",
            Value: "All",
            Operate: "equal"
        },
        {
            Field: "CreatedTime",
            Value: "",
            Operate: "greater"
        },
        {
            Field: "CreatedTime",
            Value: "",
            Operate: "less"
        }
    ];

    // 重新获取数据条目
    var reGetDatas = function () {

        var group = new $.hmhShop.filter.Group();
        for (var i = 0; i < $scope.filters.length; i++) {
            var filter1 = $scope.filters[i];
            if (filter1.Value && filter1.Value != 'All') {
                group.Rules.push(getFilterRule(filter1));
            }
        }

        function getFilterRule(filter) {
            var rule = new $.hmhShop.filter.Rule(filter.Field, filter.Value, filter.Operate);
            return rule;
        }

        // 发送给后台的请求数据 需要处理成后台可识别的形式
        var postData = {
            pageIndex: $scope.paginationConf.currentPage,
            pageSize: $scope.paginationConf.itemsPerPage,
            where: JSON.stringify(group)
        };

        $http({
            method: "post",
            url: "/Account/GetMoneyLog",
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
            $scope.RmbCoinTransactions = data.Rows;
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

    $scope.filterData = function () {
        reGetDatas();
    }
}]);

//用户中心-H币记录
hmhApp.controller("hmh.account.hmoneylog", ["$scope", "$http", function ($scope, $http) {

    $scope.filters = [
        {
            Field: "StreamId",
            Value: "",
            Operate: "contains"
        },
        {
            Field: "Type",
            Value: "All",
            Operate: "equal"
        },
        {
            Field: "Direction",
            Value: "All",
            Operate: "equal"
        },
        {
            Field: "CreatedTime",
            Value: "",
            Operate: "greater"
        },
        {
            Field: "CreatedTime",
            Value: "",
            Operate: "less"
        }
    ];

    // 重新获取数据条目
    var reGetDatas = function () {

        var group = new $.hmhShop.filter.Group();
        for (var i = 0; i < $scope.filters.length; i++) {
            var filter1 = $scope.filters[i];
            if (filter1.Value && filter1.Value != 'All') {
                group.Rules.push(getFilterRule(filter1));
            }
        }

        function getFilterRule(filter) {
            var rule = new $.hmhShop.filter.Rule(filter.Field, filter.Value, filter.Operate);
            return rule;
        }

        // 发送给后台的请求数据 需要处理成后台可识别的形式
        var postData = {
            pageIndex: $scope.paginationConf.currentPage,
            pageSize: $scope.paginationConf.itemsPerPage,
            where: JSON.stringify(group)
        };

        $http({
            method: "post",
            url: "/Account/GetHMoneyLog",
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
            $scope.HCoinTransactions = data.Rows;
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

    $scope.filterData = function () {
        reGetDatas();
    }
}]);

//用户中心-转账-弹出框
hmhApp.controller("hmh.account.transaction.modal", ["$scope", "$http", "$modalInstance", function ($scope, $http, $modalInstance) {
    $scope.transactionInfo = {
        OtherSizeUserName: "",
        Amount: 0,
        Password: ""
    };
    $scope.otherSizeInfo = "";

    $scope.$watch('transactionInfo.OtherSizeUserName', function (newval, oldval) {
        if (newval === oldval) return;//初始化

        //检查用户的有效性
        $http.post("/Account/GetUserInfo", { userName: newval }).success(function (data) {
            $scope.otherSizeInfo = data.Content;
        });

    });
    $scope.submit = function () {
        $http.post("/Account/CreateTransaction", $scope.transactionInfo).success(function (data) {
            if (data.Type == "Success") {
                $.hmhShop.tip2.success(data.Content);
                $modalInstance.dismiss("cancel");
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

//用户中心-提现-弹出框
hmhApp.controller("hmh.account.widthdraw.modal", ["$scope", "$http", "$modalInstance", function ($scope, $http, $modalInstance) {
    $scope.myBankCards = [];
    $scope.isMyBankCountZero = true;

    $http.post("/Account/GetMyBankCard").success(function (data) {
        $scope.myBankCards = data;
        $scope.isMyBankCountZero = $scope.myBankCards.length ? false : true;
    });

    $scope.widthdrawInfo = {
        BankName: "",
        Amount: 0,
        Password: ""
    };

    $scope.submit = function () {
        $http.post("/Account/CreateWidthdraw", $scope.widthdrawInfo).success(function (data) {
            if (data.Type == "Success") {
                $.hmhShop.tip2.success(data.Content);
                $modalInstance.dismiss("cancel");
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