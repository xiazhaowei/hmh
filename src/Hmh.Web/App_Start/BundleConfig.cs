using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Hmh.Web.App_Start
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
                     

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/modernizr").Include(
                        "~/Scripts/plugins/modernizr-*"));           

            //公共js
            bundles.Add(new ScriptBundle("~/hmh").Include(
                    "~/scripts/plugins/jquery/jquery.min.js",
                    "~/scripts/plugins/jquery/jquery.linq.min.js",                                       

                    "~/scripts/plugins/bootstrap/js/bootstrap.min.js",
                    "~/scripts/plugins/bootstrap/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                    //"~/scripts/plugins/bootstrap/bootstrap-datepicker/js/bootstrap-datepicker.js",//移植到前端
                    //"~/scripts/plugins/bootstrap/bootstrap-daterangepicker/daterangepicker.js",
                    //"~/scripts/plugins/bootstrap/bootstrap-daterangepicker/moment.js",
                    "~/scripts/plugins/bootstrap/bootstrap-switch/js/bootstrap-switch.min.js",
                    "~/scripts/plugins/bootstrap/bootstrap-fileinput/bootstrap-fileinput.js",
                    "~/scripts/plugins/bootstrap/bootstrap-toastr/toastr.min.js",//提示插件

                    "~/scripts/plugins/jquery/slick/slick.js",
                    "~/scripts/plugins/jquery/jquery-lazyload/jquery.lazyload.min.js",
                    "~/scripts/plugins/jquery/uniform/jquery.uniform.min.js",
                    //"~/scripts/plugins/jquery/jquery-completer/completer.js",//移植到前端
                    //"~/scripts/plugins/jquery/spinner/js/spinner.js",已创建相同功能的directive
                    "~/scripts/plugins/jquery/jquery-pin/jquery.pin.js",
                    "~/scripts/plugins/jquery/flot/jquery.flot.js",
                     //"~/scripts/plugins/jquery/citypicker/city-picker.data.js",//移植到前端
                     // "~/scripts/plugins/jquery/citypicker/city-picker.js",//移植到前端
                     "~/scripts/plugins/jquery/jquery-jqprint/jquery.jqprint-0.3.js",
                     "~/scripts/plugins/jquery/countdown/jquery.countdown.min.js",

                     "~/scripts/plugins/bootbox/bootbox.min.js",//警告、确认弹出框

                    "~/Scripts/app.js",

                    "~/scripts/plugins/angular/angular.js",
                    "~/scripts/plugins/angular/angular-sanitize/angular-sanitize.js",
                    "~/scripts/plugins/angular/angular-bootstrap/ui-bootstrap-tpls.js",
                    "~/scripts/plugins/angular/angular-ui-load/ui-load.js",
                    "~/scripts/widget/directives/sku/angular-sku.js",                   

                    "~/scripts/widget/directives/pagination/pagination.js",

                    "~/Scripts/widget/hmhshop.js",
                    "~/Scripts/widget/app.js",
                    "~/Scripts/widget/services.js",
                    "~/Scripts/widget/filters.js",
                    "~/Scripts/widget/directives.js",
                    "~/Scripts/widget/controllers.js",
                    "~/Scripts/widget/controller_account.js",
                    "~/Scripts/widget/controller_shop.js",
                    "~/Scripts/widget/controller_order.js"
                ));            
                       
            

            //////////////////////////////公共样式
            bundles.Add(new StyleBundle("~/common/css").Include(                    
                "~/Content/assets/css/bootstrap.css",
                "~/Content/assets/css/font-awesome/css/font-awesome.min.css",                

                "~/scripts/plugins/jquery/uniform/css/uniform.default.css",

                "~/scripts/plugins/bootstrap/bootstrap-switch/css/bootstrap-switch.min.css",
                //"~/scripts/plugins/bootstrap/bootstrap-datepicker/css/datepicker.css",//移植到前端
                //"~/scripts/plugins/bootstrap/bootstrap-daterangepicker/daterangepicker-bs3.css",
                "~/scripts/plugins/bootstrap/bootstrap-fileinput/bootstrap-fileinput.css",
                "~/scripts/plugins/bootstrap/bootstrap-toastr/toastr.min.css",//提示插件

                //"~/scripts/plugins/jquery/citypicker/css/city-picker.css",//移植到前端
                "~/scripts/plugins/jquery/slick/slick.css",
                "~/scripts/plugins/jquery/jquery-completer/completer.css",

                "~/Content/assets/css/style-metronic.css",
                "~/Content/assets/css/style.css",
                "~/Content/assets/css/plugins.css",
                "~/Content/assets/css/custom.min.css"));         
            
        }
    }
}