(function ($) {
    $.hmhShop = $.hmhShop || { vision: 1.0, vtime: (new Date().getTime()) };
})(jQuery);


(function ($) {
    //数据查询
    $.hmhShop.filter = {
        Rule: function (field, value, operate) {
            this.Field = field;
            this.Value = value;
            this.Operate = operate || "equal";
        },
        Group: function () {
            this.Rules = [];
            this.Operate = "and";
            this.Groups = [];
        }
    };
    $.hmhShop.from = {
        post: function (action, params) {//post到服务器
            var postform = $("#jspostform");
            if (postform.length == 0) {
                postform = $('<form method="post" action="" id="jspostform"></form>').appendTo("body");
            }
            postform.attr("action", action);
            postform.empty();

            //添加参数
            $.each(params, function (i, n) {
                var field = $('<input type="hidden" />').appendTo(postform);
                field.attr("name",i);
                field.val(n);
                //postform.append('<input type="hidden" name="' + i + '" value="' + n + '" />');
            });

            postform.submit();
        }
    };
    //工具
    $.hmhShop.tools = {
        url: {
            encode: function (url) {
                return encodeURIComponent(url);
            },
            decode: function (url) {
                return decodeURIComponent(url);
            },
            getQueryString : function (name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
                var r = window.location.search.substr(1).match(reg);
                if (r != null) return (r[2]); return null;
            }
        },
        array: {
            isContainsObjectByName : function (obj, list) {        
                var i;
                for (i = 0; i < list.length; i++) {
                    if (list[i].Name === obj.Name) {
                        return true;
                    }
                }
                return false;
            },
            getContainsObjectByName : function (obj, list) {        
                var i;
                for (i = 0; i < list.length; i++) {
                    if (list[i].Name === obj.Name) {
                        return list[i];
                    }
                }
                return null;
            }
        },
        valueToText: function (value, array, defaultText) {
            var text = defaultText == undefined ? value : defaultText;
            $.each(array, function () {
                if (this.id != undefined && this.id === value) {
                    text = this.text;
                    return false;
                }
                if (this.id != undefined && this.id === value) {
                    text = this.text;
                    return false;
                }
                return true;
            });
            return text;
        },
        //数组用分隔符分割并返回字符串
        expandAndToString: function (array, separator) {
            var result = "";
            if (!separator) {
                separator = ",";
            }
            $.each(array, function (index, item) {
                result = result + item.toString() + separator;
            });
            return result.substring(0, result.length - separator.length);
        },
        renderBoolean: function (flag) {
            return flag ? "<div class=\"checker\"><span class=\"checked\"></span></div>" : "<div class=\"checker\"><span></span></div>";
        },
        timeFormat: function (time, formatStr) {
            if (!(time instanceof Date)) {
                return time;
            }
            if (!Date.prototype.format) {
                Date.prototype.format = function (format) {
                    var o = {
                        "M+": this.getMonth() + 1, //month
                        "d+": this.getDate(), //day
                        "h+": this.getHours(), //hour
                        "m+": this.getMinutes(), //minute
                        "s+": this.getSeconds(), //second
                        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter
                        "S": this.getMilliseconds() //millisecond
                    }
                    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
                    (this.getFullYear() + "").substr(4 - RegExp.$1.length));
                    for (var k in o) if (new RegExp("(" + k + ")").test(format))
                        format = format.replace(RegExp.$1,
                        RegExp.$1.length == 1 ? o[k] :
                        ("00" + o[k]).substr(("" + o[k]).length));
                    return format;
                }
            }
            return time.format(formatStr);
        }
    };

    $.hmhShop.tip2 = {
        msg: function (content, type, title) {
            toastr.options = {
                timeOut: type === "error" ? "6000" : "3000",
                "positionClass": "toast-top-center",
                closeButton: true,
                "newestOnTop": false
            };
            toastr[type](content, title);
        },
        success: function (content) { this.msg(content, "success"); },
        info: function (content) { this.msg(content, "info"); },
        waring: function (content) { this.msg(content, "waring"); },
        danger: function (content) { this.msg(content, "error"); }
    };



    Array.prototype.indexOf = function (val) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == val) return i;
        }
        return -1;
    };
    Array.prototype.remove = function (val) {
        var index = this.indexOf(val);
        if (index > -1) {
            this.splice(index, 1);
        }
    };
    // Array Remove - By John Resig (MIT Licensed)
    Array.prototype.removeAt = function (from, to) {
        var rest = this.slice((to || from) + 1 || this.length);
        this.length = from < 0 ? this.length + from : from;
        return this.push.apply(this, rest);
    };

    Array.prototype.max = function () {   //最大值
        return Math.max.apply({}, this)
    }
    Array.prototype.min = function () {   //最小值
        return Math.min.apply({}, this)
    }

})(jQuery);