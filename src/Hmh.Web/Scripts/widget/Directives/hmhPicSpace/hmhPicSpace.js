//图片空间组件
angular.module("hmh-directive", [])
.directive("hmhPicspace", function () {
    return {
        restrict: "AE",
        require: "ngModel",
        scope: {},
        template: '<button><img src="{{value}}" />sssss</button>',
        link: function (scope,iElement,iAttrs,ngModelController) {

        }
    };
});