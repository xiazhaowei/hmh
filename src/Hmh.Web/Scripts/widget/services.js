
//身份认证服务
hmhApp.factory("AuthService", function ($http) {
    var service = {};

    service.login = function (loginInfo) {
        return $http.post("/account/login", loginInfo)
            .then(function (res) {
                var data = res.data;
                if (data.Type === "Success") {
                    var user = data.Data.User;
                }
                return data;
            });
    };

    service.register = function (registerInfo) {
        return $http.post("/account/register", registerInfo)
            .then(function (res) {
                var data = res.data;
                if(data.Type==="Success")
                {

                }
                return data;
            });
    };
    return service;
});
