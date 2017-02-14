angular.module("OSharpApp").controller("app.admin.dashboard", function($scope, $rootScope, $http, $timeout) {
    $scope.$on("$viewContentLoaded", function() {
        App.initAjax();
    });
    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});