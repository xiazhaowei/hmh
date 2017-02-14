var appName = "OSharpApp";


angular.module(appName, ["kendo.directives"]).controller("app.admin.identity.users", [
    "$scope", "$rootScope", "$state", function ($scope, $rootScope, $state) {
        $scope.title = $state.current.data.pageTitle;
        $scope.gridOptions = $.osharp.kendo.grid.Options({
            url: {
                read: "/admin/users/read",
                create: "/admin/users/create",
                update: "/admin/users/update",
                destroy: "/admin/users/delete"
            },
            model: {
                id: "Id",
                fields: {
                    Id: { type: "number", editable: false },
                    Email: { type: "email" },
                    EmailConfirmed: { type: "boolean" },
                    PhoneNumberConfirmed: { type: "boolean" },
                    CreatedTime: { type: "date", editable: false }
                }
            },
            columns: [                
                { field: "Id", title: "编号", width: 75 },
                { field: "UserName", title: "用户名" },
                { field: "NickName", title: "用户昵称" },
                { field: "Email", title: "邮箱" },
                { field: "EmailConfirmed", title: "邮箱确认", template: function(d) { return $.osharp.tools.renderBoolean(d.EmailConfirmed); } },
                { field: "PhoneNumber", title: "手机号码" },
                { field: "PhoneNumberConfirmed", title: "手机确认", template: function(d) { return $.osharp.tools.renderBoolean(d.PhoneNumberConfirmed); } },
                { field: "CreatedTime", title: "创建时间", format: "{0: yyyy-MM-dd HH:mm}" },
                { command: [{ name: "destroy", text: "删除" }, {name:"setpermission", text: "权限..", click: setPermission }], width: 130 }
            ]
        });
        function setPermission(e) {            
            var tr = $(e.target).closest("tr");
            var data = this.dataItem(tr);
            console.log(tr);
            $scope.$broadcast("users.setpermission", data);           
        }
    }
]);


angular.module(appName).controller("app.admin.identity.users.setpermission", ["$scope", "$modal", function ($scope, $modal) {   
    $scope.open = function (data, size) {        
        $modal.open({
            templateUrl: "identity-users-setpermission",
            controller: "app.admin.identity.users.setpermission.modal",
            size: size,
            resolve: {
                user: function() {
                    return data;
                }
            }
        });
    };
    $scope.$on("users.setpermission", function (event, data) {       
        $scope.open(data,'200px');
    });
}]);


angular.module(appName).controller("app.admin.identity.users.setpermission.modal", [
    "$scope", "$http", "$modalInstance", "user", function ($scope, $http, $modalInstance, user) {
        $scope.user = user;
        $scope.roles = [];
        $http.get("/admin/roles/read-check-node?userId=" + user.Id)
            .then(function(res) {
                $scope.roles = res.status === 200 ? res.data : [];
            });
        $scope.submit = function() {
            console.info("submit");
        };
        $scope.cancel = function() {
            $modalInstance.dismiss("cancel");
        };
    }
]);