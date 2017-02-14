
//邮件输入辅助插件 jquery
hmhApp.directive("hmhEmailcomplate", ["JQ_CONFIG", "uiLoad", "$http", function (JQ_CONFIG,uiLoad, $http) {
    return {
        require:'?ngModel',
        link:function (scope, iElement, iAttrs, ngModelController) {            
            uiLoad.load(JQ_CONFIG.complate).then(function () {
                iElement.completer({
                    complete: function () {
                        var value = iElement.val();
                        scope.$apply(function () {
                            ngModelController.$setViewValue(value);
                        });
                    },
                    separator: "@",
                    source: ["qq.com", "163.com", "126.com", "hotmail.com", "gmail.com", "yahoo.com", "hotmail.com"]
                });
            });                       
        }
    }; 
   
}]);

//唯一性验证
hmhApp.directive("hmhUnique", function ($http) {
    return {
        require: "?ngModel",        
        link: function (scope,iElemnt,iAttrs,ngModelController) {
            //判断邮件是否唯一 应该放到服务中
            scope.$watch(iAttrs.ngModel, function (newval, oldval) {
                if (newval === oldval) return;//初始化
                $http.post(iAttrs.checkUrl, { name: ngModelController.$viewValue }).success(function (data, status, headers, cfg) {
                    ngModelController.$setValidity("unique", data.isQnique);//设置字段验证成功或失败
                }).error(function () {
                    ngModelController.$setValidity("unique", false);//错误直接验证失败
                });
            });
        }
    };
});

//倒计时发送邮件按钮
hmhApp.directive("hmhTimerbutton", ["$timeout", "$interval", "$http", function ($timeout, $interval, $http) {
    return {
        restrict: 'AE',        
        replace: true,
        scope: {
            emailTo:"@"
        },
        link: function (scope, iElement, iAttrs) {
            scope.showtimer = false;
            scope.timer = false;
            scope.timeout = 60;
            scope.timerCountDown = scope.timeout;
            scope.text = "获取邮箱验证码";
            
            scope.$watch('emailTo', function (newval, oldval) {
                if (newval === oldval) return;
                if ($.hmhShop.tools.isEmail(newval))
                    scope.text = "获取邮箱验证码";
            });

            //私有函数判断是否是邮件
            var isEmail = function (strEmail) {
                var reg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
                return reg.test(strEmail);
            };
            
            iElement.on("click", function (e) {
                e.preventDefault();               

                //判断邮箱的合法性
                if (!isEmail(scope.emailTo)) {
                    scope.text = "请输入正确的邮箱地址";
                }
                else {
                    //调用服务发送邮件               
                    $http.post(iAttrs.postUrl, { email: scope.emailTo }).success(function () {
                        scope.showtimer = true;
                        scope.timer = true;
                        scope.text = "秒后重新获取";
                        var counter = $interval(function () {
                            scope.timerCountDown -= 1;
                        }, 1000);

                        $timeout(function () {
                            scope.text = "获取邮箱验证码";
                            scope.timer = false;
                            $interval.cancel(counter);
                            scope.showtimer = false;
                            scope.timerCountDown = scope.timeout;
                        }, scope.timeout * 1000);
                    }).error(function () {
                        scope.text = '发送验证码错误'
                    });
                }

                scope.$apply();
            });
           
            
        },
        template: '<button type="button" class="btn btn-success" ng-disabled="timer"><span ng-if="showtimer">{{ timerCountDown }}</span>{{text}}</button>'
    };
}]);

hmhApp.directive("hmhCollectbutton", ["$http",function ($http) {
    return {
        restrict: 'AE',
        replace: true,
        scope: {
            aboutid: '=',
            collectcheckurl:"@",
            collectdourl: "@",
            collectype:"@"
        },
        template: '<button class="btn btn-sm btn-default" ng-click="docollect()"><span class="{{fa}}"></span> {{btntext}}</button>',
        link: function (scope,iElement,iAttrs) {
            scope.btntext = "收藏";
            scope.fa = "fa fa-heart-o";

            //通过监控解决延迟问题
            scope.$watch('aboutid', function (newval, oldval) {
                if (newval === oldval) return;

                $http.post(scope.collectcheckurl, { AboutId: scope.aboutid, Type: scope.collectype }).success(function (data) {
                    if (data.Content == "未登录") {
                        scope.btntext = "收藏";
                    }
                    else {
                        scope.btntext = data.Content;
                    }

                    scope.fa = data.Content == "已收藏" ? "fa fa-heart" : "fa fa-heart-o";
                });
            });            

            scope.docollect = function () {
                //scope.btntext = "加载中";
                scope.collectInfo = {
                    AboutId: scope.aboutid,
                    Type: scope.collectype
                };
                $http.post(scope.collectdourl, scope.collectInfo).success(function (data) {
                    if (data.Content == "未登录")
                    {
                        $("#modal-login").modal("show");
                    }
                    else
                    {
                        scope.btntext = data.Content;
                    }
                    
                    
                    scope.fa = data.Content == "已收藏" ? "fa fa-heart" : "fa fa-heart-o";
                });               
            };
        }
    };
}]);

//选择发布分类
hmhApp.directive("hmhPublishcategory", function ($http) {
    return {
        restrict: "AE",        
        replace: true,
        scope:{},        
        link: function (scope,iElement,iAttrs) {
            scope.category1Data = [];

            //从服务器获取三级类别
            $http.post("/shop/pubcategorys").success(function (res) {
                scope.category1Data = res;                
            });

            scope.isEnd = false;
            scope.category2Data = [];
            scope.category3Data = [];
            scope.endCategory = {};
            scope.selectedCategory = {};

            scope.$watch('selectedCategory1', function (newValue, oldValue) {                
                if (newValue != oldValue) {                   
                    if (!newValue) { //判断选择的是否选择省份，如果没有则重置市区
                        scope.category2Data = [];
                        scope.category3Data = [];
                        return;
                    }
                    for (i = 0; i < scope.category1Data.length; i++) {
                        //初始化选中类别的二级子类别
                        if (scope.category1Data[i].id == scope.selectedCategory1) {
                            scope.category2Data = scope.category1Data[i].children;
                            scope.selectedCategory.category1 = scope.category1Data[i].name;
                        }
                    }
                    scope.category3Data = [];
                    scope.selectedCategory.category2 = "";
                    scope.selectedCategory.category3 = "";
                    scope.isEnd = false;
                }
            });

            scope.$watch('selectedCategory2', function (newValue, oldValue) {
                //debugger;
                if (newValue != oldValue) {
                    if (!newValue) { //作用同上
                        scope.category3Data = [];
                        return;
                    }                    
                    for (i=0; i < scope.category2Data.length; i++) {
                        if (scope.category2Data[i].id == scope.selectedCategory2) {
                            scope.category3Data = scope.category2Data[i].children;
                            scope.selectedCategory.category2 = scope.category2Data[i].name;
                        }
                    }
                    scope.selectedCategory.category3 = "";
                    scope.isEnd = false;
                }
            });
            
            scope.$watch('selectedCategory3', function (newValue, oldValue) {
                if (newValue != oldValue) {                   
                    for (i=0; i < scope.category3Data.length ; i++) {
                        if (scope.category3Data[i].id == scope.selectedCategory3) {
                            scope.endCategory = scope.category3Data[i];
                            scope.selectedCategory.category3 = scope.category3Data[i].name;
                        }
                    }
                    scope.isEnd = true;
                }
            });


            //绑定按钮事件
            var button = iElement.find("button");
            if(button)
            {
                button.on("click", function (e) {
                    e.preventDefault();
                    //跳转到发布产品页面
                    location.href = "/shop/creategoods/" + scope.endCategory.id;
                });
            }
        },
        templateUrl:"/scripts/tpls/publishcategory.html"
    };
});

hmhApp.directive("hmhCitypicker", ["JQ_CONFIG","uiLoad",function (JQ_CONFIG,uiLoad) {
    return {
        require: '?ngModel',
        link: function (scope,iElement,iAttrs,ngModelController) {            
            uiLoad.load(JQ_CONFIG.cityPacker).then(function () {
                iElement.citypicker({
                    complete: function () {
                        var value = iElement.val();
                        ngModelController.$setViewValue(value);
                    }
                });
            });     
        }
    };
}]);

//日期/日期范围选择
hmhApp.directive("hmhDatepicker", ["JQ_CONFIG", "uiLoad", function (JQ_CONFIG,uiLoad) {
    return {
        require: '?ngModel',
        link: function (scope,iElement,iAttrs,ngModelController) {
            uiLoad.load(JQ_CONFIG.bootstrapDataPicker).then(function () {
                iElement.datepicker({
                    language: "zh-CN",
                    autoclose:true,
                    dateFormat: 'yyyy-mm-dd',
                    onSelect: function (date) {
                        ngModelController.$setViewValue(date);
                    }
                });
            });
        }
    };
}]);

//选择输入
hmhApp.directive("hmhEditableselect", ["JQ_CONFIG","uiLoad",function (JQ_CONFIG,uiLoad) {
    return {
        require: "?ngModel",
        link: function (scope,iElement,iAttrs,ngModelController) {
            uiLoad.load(JQ_CONFIG.editableSelect).then(function () {
                iElement.editableSelect({ effects: 'fade' })
                .on('select.editable-select', function (e,li) {
                    var value = li.text();
                    ngModelController.$setViewValue(value);
                })
                .on('complete.editable-select', function (e,inputvalue) {
                    ngModelController.$setViewValue(inputvalue);
                });
            });
        }
    };
}]);

//多选框
hmhApp.directive("hmhMultiselectdroplist", ["JQ_CONFIG", "uiLoad", function (JQ_CONFIG, uiLoad) {
    return {
        require: "?ngModel",
        scope: {
            selectList: "@"
        },
        link: function (scope, iElement, iAttrs, ngModelController) {            
            uiLoad.load(JQ_CONFIG.multiSelectDropList).then(function () {
                var data = scope.selectList;                
                iElement.MultiSelectDropList(
                    {                        
                        data: eval(data),
                        complete: function (newval) {                            
                            ngModelController.$setViewValue(newval);
                        }
                    });
            });
        }
    };
}]);

//图片空间插件--未完成
hmhApp.directive("hmhPicspace", function () {
    return {
        restrict: "AE",
        require: "ngModel",
        scope: {},
        replace: true,
        template: '<button><img src="{{value}}" /></button>',
        link: function (scope, iElement, iAttrs, ngModelController) {

        }
    };
});

//购买数量组件
hmhApp.directive("hmhStepper", function () {
    return {
        restrict: "AE",       
        require: "ngModel",        
        scope: {
            maxValue:'@'
        },
        template: '<button ng-disable="value==0" ng-click="decrement()">-</button>' +
                '<div>{{value}}{{maxValue}}</div>' +
                '<button ng-click="increment()">+</button>',        
        link: function (scope, iElement, iAttrs, ngModelController) {
            // when model change, update our view (just update the div content)
            //在ngModel发生改变的时候框架自动调用，同步$modelvalue和$viewValue,即刷新页面
            ngModelController.$render = function () {
                iElement.find("div").text(ngModelController.$viewValue);
            };

            // update the model then the view
            function updateModel(offset) {
                if (ngModelController.$viewValue == 1 && offset<0)
                {
                    return;
                }
                // call $parsers pipeline then update $modelValue
                //此方法同步更新$modelValue
                ngModelController.$setViewValue(ngModelController.$viewValue + offset);
                // update the local view
                ngModelController.$render();                
                
            }            
            scope.decrement = function () {
                updateModel(-1);
            };
            scope.increment = function () {
                updateModel(1);
            }
        }
    };
});

hmhApp.directive("tabs", function () {
    return {
        restrict: 'E',
        transclude: true,
        scope: {},
        controller: function ($scope,$element) {
            var panes = $scope.panes = [];

            $scope.select = function (pane) {
                angular.forEach(panes, function (pane) {
                    pane.selected = false;
                });
                pane.selected = true;
            };

            this.addPane = function (pane) {
                if (panes.length == 0) $scope.select(pane);
                panes.push(pane);
            };
        },
        template:
            '<div class="tabbable">' +
              '<ul class="nav nav-tabs">' +
                '<li ng-repeat="pane in panes" ng-class="{active:pane.selected}">' +
                  '<a href="" ng-click="select(pane)">{{pane.title}}</a>' +
                '</li>' +
              '</ul>' +
              '<div class="tab-content" ng-transclude></div>' +
            '</div>',
        replace:true
    };
});
hmhApp.directive("pane", function () {
    return {
        require: '^tabs',
        restrict: 'E',
        transclude: true,
        scope: { title: '@' },
        link: function (scope,element,attrs,tabsController) {
            tabsController.addPane(scope);
        },
        template:
            '<div class="tab-pane" ng-class="{active: selected}" ng-transclude>' +
            '</div>',
        replace:true
    };
});

