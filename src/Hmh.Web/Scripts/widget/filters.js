hmhApp.filter("split", function () {
    return function (data) {
        if(data)
            return data.split(",");
    }
});

hmhApp.filter("rmbcointransactiontype", function () {
    return function (data) {        
        switch(data)
        {
            case 0:
                return "充值";
            case 1:
                return "提现";
            case 2:
                return "购物";
            case 3:
                return "转账";
            case 4:
                return "三级分销";
            case 5:
                return "八代分润";
            case 6:
                return "其他";
            default:
                return "其他";
        }                 
    }
});

hmhApp.filter("rmbcointransactionstate", function () {
    return function (data) {
        switch (data) {
            case 0:
                return "交易成功";
            case 1:
                return "待确认";            
            default:
                return "其他";
        }
    }
});

