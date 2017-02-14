var appName = "OSharpApp";
angular.module(appName).controller("app.admin.layout", ["$scope","$rootScope", function ($scope, $rootScope) {
    var user = {
        logined: false,
        userId: null,
        userName: null,
        nickName: null,
        email: null,
        roles: []
    };
    $rootScope.user = user;
    $scope.setCurrentUser = function(userInfo) {
        user = !userInfo ? {
            logined: false,
            userId: null,
            userName: null,
            nickName: null,
            email: null,
            roles: []
        } : {
            logined: true,
            userId: userInfo.UserId,
            userName: userInfo.UserName,
            nickName: userInfo.NickName,
            email: userInfo.Email,
            roles: userInfo.UserRole
        };
        $rootScope.user = user;
    };

    var layoutLoaded = false;
    $scope.$on("$viewContentLoaded", function () {
        if (layoutLoaded) {
            Layout.fixContentHeight();
        }
        layoutLoaded = true;
    });
}]);
angular.module(appName).controller("app.admin.layout.header", ["$scope", "$rootScope", "AUTH_EVENTS", "AuthService", function ($scope, $rootScope, AUTH_EVENTS, AuthService) {
    $scope.logout=function() {
        AuthService.logout().then(function(data) {
            if (data.Type === "Success") {
                $rootScope.$broadcast(AUTH_EVENTS.logoutSuccess);
                $scope.setCurrentUser(null);
            }
        });
    }

    $scope.$on("$includeContentLoaded", function () {
        Layout.initHeader();
    });
}]);
angular.module(appName).controller("app.admin.layout.sidebar", ["$scope", function ($scope) {
    var vm = this;
    vm.menu = vm.menu || {};
    vm.menu.items = [
        {
            text: "权限模块",
            items: [
                {
                    text: "身份认证",
                    icon: "icon-key",
                    items: [
                        { text: "用户信息管理", icon: "icon-user", url: "#/users" },
                        { text: "角色信息管理", icon: "icon-badge", url: "#/roles" },
                        { text: "用户角色管理", icon: "icon-users", url: "#/userroles" }
                    ]
                }
            ]
        }, {
            text: "系统模块",
            items: [
                { text: "操作日志", icon: "icon-layers", url: "#/operatelogs" }
            ]
        }
    ];

    $scope.$on("$includeContentLoaded", function () {
        Layout.initSidebar();
    });
}]);
angular.module(appName).controller("app.admin.layout.quicksidebar", ["$scope", function ($scope) {
    $scope.$on("$includeContentLoaded", function () {
        setTimeout(function () {
            QuickSidebar.init();
        }, 2000);
    });
}]);
angular.module(appName).controller("app.admin.layout.themepanel", ["$scope", function ($scope) {
    $scope.$on("$includeContentLoaded", function () {
        Demo.init();
    });
}]);
angular.module(appName).controller("app.admin.layout.footer", ["$scope", function ($scope) {
    $scope.$on("$includeContentLoaded", function () {
        Layout.initFooter();
    });
}
]);