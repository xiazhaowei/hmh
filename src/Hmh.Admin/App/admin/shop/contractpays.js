var appName = "OSharpApp";

//店铺列表控制器
angular.module(appName, ["kendo.directives"]).controller("app.admin.shop.contractpays", [
    "$scope", "$rootScope", "$state", function ($scope, $rootScope, $state) {
        $scope.title = $state.current.data.pageTitle;
        $scope.gridOptions = $.osharp.kendo.grid.Options({
            url: {
                read: "/admin/contractpays/read",               
                update: "/admin/contractpays/update"
            },
            model: {
                id: "Id",
                fields: {
                    Id: { type: "number", editable: false },
                    Money: { type: "number", editable: false },
                    PayType: { type: "number", editable: false },
                    PayStreamId: { type: "string", editable: false },
                    ContractFee: { type: "number", editable: false },
                    PayState: { type: "number", editable: true },
                    CreatedTime: { type: "date", editable: false },
                    ShopName:{type:"string",editable:false}
                }
            },
            toolbar:["save", "cancel"],
            columns: [                
                { field: "Id", title: "编号", width: 75 },
                { field: "ShopName", title: "店铺" },
                { field: "Money", title: "付款金额" },
                { field: "ContractFee", title: "合同应付"},
                { field: "PayType", title: "付款方式" },
                { field: "PayStreamId", title: "流水号" },                                
                { field: "CreatedTime", title: "付款时间", format: "{0: yyyy-MM-dd HH:mm}"},
                {
                    field: "PayState",
                    title: "审核状态",
                    editor: function (container, options) {
                        $.osharp.kendo.grid.DropDownListEditor(container, options, [                            
                            { PayStateName: "审核中", PayStateValue: 0 },
                            { PayStateName: "已付款", PayStateValue: 1 },
                            { PayStateName: "审核失败", PayStateValue:2 }
                        ], "PayStateName", "PayStateValue");
                    },
                    template: "#=PayState#"
                }
            ]
        });
       
    }
]);





