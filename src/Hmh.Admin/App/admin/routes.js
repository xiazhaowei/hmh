var kendofiles = [

    staticsUrlPerfix + "/assets/global/plugins/kendoui/kendo.web.min.js",

    staticsUrlPerfix + "/assets/global/plugins/kendoui/kendo.common.min.css",
    staticsUrlPerfix + "/assets/global/plugins/kendoui/kendo.office365.min.css",
    
    staticsUrlPerfix + "/assets/global/plugins/kendoui/cultures/kendo.messages.zh-CN.min.js",
    staticsUrlPerfix + "/assets/global/plugins/kendoui/cultures/kendo.culture.zh-CN.min.js"
];

//路由配置 ui router
osharpApp.config(["$stateProvider", "$urlRouterProvider", function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise("/dashboard");
    $stateProvider
        .state("dashboard", {
            url: "/dashboard",
            templateUrl: "/app/admin/dashboard/dashboard.html",
            data: { pageTitle: "信息汇总" },
            controller: "app.admin.dashboard",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: [
                                staticsUrlPerfix + "/assets/global/plugins/morris/morris.css",
                                staticsUrlPerfix + "/assets/global/plugins/morris/morris.min.js",
                                staticsUrlPerfix + "/assets/global/plugins/morris/raphael-min.js",
                                staticsUrlPerfix + "/assets/global/plugins/jquery.sparkline.min.js",
                                staticsUrlPerfix + "/assets/pages/scripts/dashboard.min.js",
                                "/app/admin/dashboard/dashboard.js?v=" + $.osharp.vtime
                            ]
                        });
                    }
                ]
            }
        }).state("users", {
            url: "/users",
            templateUrl: "/app/admin/identity/users.html",
            data: { pageTitle: "用户信息管理" },
            controller: "app.admin.identity.users",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: kendofiles.concat([
                                "/app/admin/identity/users.js?v=" + $.osharp.vtime
                            ])
                        });
                    }
                ]
            }
        }).state("roles", {
            url: "/roles",
            templateUrl: "/app/admin/identity/roles.html",
            data: { pageTitle: "角色信息管理" },
            controller: "app.admin.identity.roles",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: kendofiles.concat([
                                "/app/admin/identity/roles.js?v=" + $.osharp.vtime
                            ])
                        });
                    }
                ]
            }
        }).state("userroles", {
            url: "/userroles",
            templateUrl: "/app/admin/identity/userroles.html",
            data: { pageTitle: "用户角色管理" },
            controller: "app.admin.identity.userroles",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: kendofiles.concat([
                                "/app/admin/identity/userroles.js?v=" + $.osharp.vtime
                            ])
                        });
                    }
                ]
            }
        }).state("category", {
            url: "/category",
            templateUrl: "/app/admin/goods/category.html",
            data: { pageTitle: "发布分类管理" },
            controller: "app.admin.goods.category",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: kendofiles.concat([
                                "/app/admin/goods/category.js?v=" + $.osharp.vtime
                            ])
                        });
                    }
                ]
            }
        }).state("showcategory", {
            url: "/showcategory",
            templateUrl: "/app/admin/goods/showcategory.html",
            data: { pageTitle: "筛选分类管理" },
            controller: "app.admin.goods.showcategory",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: kendofiles.concat([
                                "/app/admin/goods/showcategory.js?v=" + $.osharp.vtime
                            ])
                        });
                    }
                ]
            }
        }).state("shops", {
            url: "/shops",
            templateUrl: "/app/admin/shop/shops.html",
            data: { pageTitle: "店铺管理" },
            controller: "app.admin.shop.shops",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: kendofiles.concat([
                                "/app/admin/shop/shops.js?v=" + $.osharp.vtime
                            ])
                        });
                    }
                ]
            }
        }).state("shoppermits", {
            url: "/shoppermits",
            templateUrl: "/app/admin/shop/shoppermits.html",
            data: { pageTitle: "合同付款记录" },
            controller: "app.admin.shop.shoppermits",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: kendofiles.concat([
                                "/app/admin/shop/shoppermits.js?v=" + $.osharp.vtime
                            ])
                        });
                    }
                ]
            }
        }).state("contractpays", {
            url: "/contractpays",
            templateUrl: "/app/admin/shop/contractpays.html",
            data: { pageTitle: "合同付款记录" },
            controller: "app.admin.shop.contractpays",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: kendofiles.concat([
                                "/app/admin/shop/contractpays.js?v=" + $.osharp.vtime
                            ])
                        });
                    }
                ]
            }
        }).state("functions", {
            url: "/functions",
            templateUrl: "/app/admin/security/functions.html",
            data: { pageTitle: "功能信息管理" },
            controller: "app.admin.security.functions",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: kendofiles.concat([
                                "/app/admin/security/functions.js?v=" + $.osharp.vtime
                            ])
                        });
                    }
                ]
            }
        }).state("functionroles", {
            url: "/functionroles",
            templateUrl: "/app/admin/security/functionroles.html",
            data: { pageTitle: "角色功能权限管理" },
            controller: "app.admin.security.functionroles",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: kendofiles.concat([
                                "/app/admin/security/functionroles.js?v=" + $.osharp.vtime
                            ])
                        });
                    }
                ]
            }
        }).state("functionusers", {
            url: "/functionusers",
            templateUrl: "/app/admin/security/functionusers.html",
            data: { pageTitle: "用户功能权限管理" },
            controller: "app.admin.security.functionusers",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: kendofiles.concat([
                                "/app/admin/security/functionusers.js?v=" + $.osharp.vtime
                            ])
                        });
                    }
                ]
            }
        }).state("entityinfos", {
            url: "/entityinfos",
            templateUrl: "/app/admin/security/entityinfos.html",
            data: { pageTitle: "实体信息管理" },
            controller: "app.admin.security.entityinfos",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: kendofiles.concat([
                                "/app/admin/security/entityinfos.js?v=" + $.osharp.vtime
                            ])
                        });
                    }
                ]
            }
        }).state("shopdistribution", {
            url: "/shopdistribution",
            templateUrl: "/app/admin/settings/shopdistribution.html",
            data: { pageTitle: "三级分销设置" },
            controller: "app.admin.settings.shopdistribution",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: kendofiles.concat([
                                "/app/admin/settings/shopdistribution.js?v=" + $.osharp.vtime
                            ])
                        });
                    }
                ]
            }
        }).state("userdistribution", {
            url: "/userdistribution",
            templateUrl: "/app/admin/settings/userdistribution.html",
            data: { pageTitle: "八代分润设置" },
            controller: "app.admin.settings.userdistribution",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: kendofiles.concat([
                                "/app/admin/settings/userdistribution.js?v=" + $.osharp.vtime
                            ])
                        });
                    }
                ]
            }
        }).state("settings", {
            url: "/settings",
            templateUrl: "/app/admin/settings/settings.html",
            data: { pageTitle: "系统参数设置" },
            controller: "app.admin.settings.settings",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: kendofiles.concat([
                                "/app/admin/settings/settings.js?v=" + $.osharp.vtime
                            ])
                        });
                    }
                ]
            }
        }).state("regions", {
            url: "/regions",
            templateUrl: "/app/admin/settings/regions.html",
            data: { pageTitle: "地区设置" },
            controller: "app.admin.settings.regions",
            resolve: {
                deps: [
                    "$ocLazyLoad",
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            serie: true,
                            insertBefore: "#ng_load_plugins_before",
                            files: kendofiles.concat([
                                "/app/admin/settings/regions.js?v=" + $.osharp.vtime
                            ])
                        });
                    }
                ]
            }
        });
}]);
