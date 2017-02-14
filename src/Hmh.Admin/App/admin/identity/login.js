var appName = "OSharpApp";
angular.module(appName).controller("app.admin.identity.login", ["$scope", "$rootScope", "AUTH_EVENTS", "AuthService", function ($scope, $rootScope, AUTH_EVENTS, AuthService) {
    $scope.loginInfo = {
        UserName: "admin",
        Password: "xbwf123456",
        VerifyCode: "",
        Remember: false
    };
    $scope.login = function (loginInfo) {
        AuthService.login(loginInfo).then(function (data) {
            if (data.Type === "Success") {
                $rootScope.$broadcast(AUTH_EVENTS.loginSuccess);
                $scope.setCurrentUser(data.Data.User);
                $.osharp.tip2.success(data.Content);
            } else {
                $scope.verifyCode.refresh();
                $scope.loginInfo.VerifyCode = null;
                $rootScope.$broadcast(AUTH_EVENTS.loginFailed);
                $.osharp.tip2.danger("登录失败：" + data.Content);
            }
        });
    };
    $scope.verifyCode = {
        url: "/common/verify-code",
        refresh: function () {
            var num = new Date().getTime();
            this.url = "/common/verify-code?" + num;
        }
    };
    var showOrHideDialog = function () {
        if ($rootScope.user.logined) {
            $("#modal-login").modal("hide");
        } else {
            $("#modal-login").modal("show");
        }
    };
    var showDialog = function () {
        $rootScope.user.logined = false;
        showOrHideDialog();
    };
    var hideDialog = function () {
        $rootScope.user.logined = true;
        showOrHideDialog();
    };
    $scope.$on("$includeContentLoaded", function () {
        //尝试刷新用户在线信息
        AuthService.profile().then(function(data) {
            if (data == null) {
                showOrHideDialog();
                return;
            }
            $rootScope.$broadcast(AUTH_EVENTS.loginSuccess);
            $scope.setCurrentUser(data.User);
        });
    });
    $scope.$on(AUTH_EVENTS.loginSuccess, hideDialog);
    $scope.$on(AUTH_EVENTS.loginFailed, showDialog);
    $scope.$on(AUTH_EVENTS.logoutSuccess, showDialog);
    $scope.$on(AUTH_EVENTS.sessionTimeout, showDialog);
    $scope.$on(AUTH_EVENTS.notAuthenticated, showDialog);
    $scope.$on(AUTH_EVENTS.notAuthorized, showDialog);
}]);

//身份认证事件
angular.module(appName).constant("AUTH_EVENTS", {
    loginSuccess: "auth-login-success",
    loginFailed: "auth-login-failed",
    logoutSuccess: "auth-logout-success",
    sessionTimeout: "auth-session-timeout",
    notAuthenticated: "auth-not-authenticated",
    notAuthorized: "auth-not-authorized"
});

//所有角色信息
angular.module(appName).constant("USER_ROLES", {
    all: "*",
    admin: "admin",
    guest: "guest"
});

//身份认证服务
angular.module(appName).factory("AuthService", function ($http, Session) {
    var service = {};
    service.login = function (loginInfo) {
        return $http.post("/identity/login", loginInfo)
            .then(function (res) {
                var data = res.data;
                if (data.Type === "Success") {
                    var user = data.Data.User;
                    var userRoles = user.UserRole ? user.UserRole.split(",") : [];
                    Session.create(data.Data.SessionId, user, userRoles);
                }
                return data;
            });
    };
    service.logout = function () {
        return $http.post("/identity/logout")
            .then(function (res) {
                var data = res.data;
                if (data.Type === "Success") {
                    Session.destroy();
                }
                return data;
            });
    };
    service.profile = function() {
        return $http.post("/identity/user-profile").then(function(res) {
            var data = res.data;
            if (!data) {
                return null;
            }
            var user = data.User;
            var userRoles = user.UserRole ? user.UserRole.split(",") : [];
            Session.create(data.SessionId, user, userRoles);
            return data;
        });
    };
    service.isAuthenticated = function () {
        return !!Session.userId;
    };
    service.isAuthorized = function (authorizedRoles) {
        if (!angular.isArray(authorizedRoles)) {
            authorizedRoles = [authorizedRoles];
        }
        return service.isAuthenticated() && $.intersect(authorizedRoles, Session.userRoles).length > 0;
    };
    return service;
});
angular.module(appName).service("Session", function () {
    this.create = function (sessionId, user, userRoles) {
        this.id = sessionId;
        this.userId = user.UserId;
        this.userName = user.UserName;
        this.nickName = user.NickName;
        this.email = user.Email;
        this.userRoles = userRoles;
    };
    this.destroy = function () {
        this.id = null;
        this.userId = null;
        this.userName = null;
        this.nickName = null;
        this.email = null;
        this.userRoles = [];
    };
    return this;
});
angular.module(appName).run([
    "$rootScope", "AUTH_EVENTS", "AuthService", function ($rootScope, AUTH_EVENTS, AuthService) {
        
        $rootScope.$on("$stateChangeStart", function (event, next) {
            if (next.data.authorizedRoles == undefined) {
                return;
            }
            var authorizedRoles = next.data.authorizedRoles;
            if (AuthService.isAuthorized(authorizedRoles)) {
                return;
            }
            event.preventDefault();
            if (AuthService.isAuthenticated()) {
                $rootScope.$broadcast(AUTH_EVENTS.notAuthorized);
            } else {
                $rootScope.$broadcast(AUTH_EVENTS.notAuthenticated);
            }
        });
    }
]);
angular.module(appName).config(["$httpProvider", function ($httpProvider) {
    $httpProvider.interceptors.push([
        "$injector", function ($injector) {
            return $injector.get("AuthInterceptor");
        }
    ]);
}]).factory("AuthInterceptor", function ($rootScope, $q, AUTH_EVENTS) {
    return {
        responseError: function (res) {
            $rootScope.$broadcast({
                401: AUTH_EVENTS.notAuthenticated,
                403: AUTH_EVENTS.notAuthorized,
                419: AUTH_EVENTS.sessionTimeout,
                440: AUTH_EVENTS.sessionTimeout
            }[res.status], res);
            return $q.reject(res);
        }
    };
});