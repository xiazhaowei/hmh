var osharpApp = angular.module("OSharpApp", ["ui.router", "ui.bootstrap", "oc.lazyLoad", "ngSanitize"]);
/* Configure ocLazyLoader(refer: https://github.com/ocombe/ocLazyLoad) */
osharpApp.config(["$ocLazyLoadProvider", function($ocLazyLoadProvider) {
    $ocLazyLoadProvider.config({
        // global configs go here
    });
}]);

//配置controller
osharpApp.config(["$controllerProvider", function($controllerProvider) {
    $controllerProvider.allowGlobals();
}]);



/* Setup global settings */
osharpApp.factory("settings", ["$rootScope", function($rootScope) {
    var settings = {
        layout: {
            pageSidebarClosed: false,
            pageContentWhite: true,
            pageBodySolid: false,
            pageAutoScrollOnLoad: 1000
        },
        assetsPath: "/scripts/assets",
        globalPath: "/scripts/assets/global",
        layoutPath: "/scripts/assets/layouts/layout"
    };
    return settings; 
}]);


/* Init global settings and run the app */
osharpApp.run(["$rootScope", "settings", "$state", function ($rootScope, settings, $state) {
    $rootScope.$state = $state; // state to be accessed from view
    $rootScope.settings = settings; // state to be accessed from view
}]);