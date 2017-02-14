var hmhApp = angular.module("hmhApp", ["ngSanitize", "ui.load", "ui.bootstrap", "hmh.paginaction", "ui.angularSku"]);

//配置controller
hmhApp.config(["$controllerProvider", function ($controllerProvider) {
    $controllerProvider.allowGlobals();
}]);
hmhApp.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode({
        enable: true,
        requireBase: false
    });
}]);

hmhApp.constant("JQ_CONFIG", {
    cityPacker: [
        "/scripts/plugins/jquery/citypicker/css/city-picker.css",
        "/scripts/plugins/jquery/citypicker/city-picker.data.js",
        "/scripts/plugins/jquery/citypicker/city-picker.js"
    ],
    editableSelect: [
        "/scripts/plugins/jquery/jquery-editable-select/src/jquery-editable-select.css",
        "/scripts/plugins/jquery/jquery-editable-select/src/jquery-editable-select.js"
    ],
    complate: ["/scripts/plugins/jquery/jquery-completer/completer.js"],

    multiSelectDropList: [
        "/scripts/plugins/jquery/jquery-multiselectdroplist/MultiSelectDropList.css",
        "/scripts/plugins/jquery/jquery-multiselectdroplist/MultiSelectDropList.js"
    ],

    bootstrapDataPicker: [
        "/scripts/plugins/bootstrap/bootstrap-datepicker/css/datepicker.css",
        "/scripts/plugins/bootstrap/bootstrap-datepicker/js/bootstrap-datepicker.js",
        "/scripts/plugins/bootstrap/bootstrap-datepicker/js/locales/bootstrap-datepicker.zh-CN.js"
    ]
});

//身份事件
hmhApp.constant("ACCOUNT_EVENTS", {
    createBankCardSuccess: "account-create-bankcard-success",
    createDeliverAddressSuccess: "account-create-deliveraddress-success"    
});

//平级controller 事件传递中间人
hmhApp.run(function ($rootScope) {
    $rootScope.$on('addCartSuccess_Emit', function (event,obj) {
        $rootScope.$broadcast('addCartSuccess_Broadcast',obj);
    });
});


$(function () {
    App.init();
});