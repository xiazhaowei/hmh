//用法
//<div ng-app="hmh-lazy">
//    <div class="hide">
//        <div id="sampleLoader">loading...</div>
//    </div>
    
//    <div lazy-class="full" lazy-loader="#sampleLoader" lazy-background="http://upload.wikimedia.org/wikipedia/commons/9/97/The_Earth_seen_from_Apollo_17.jpg" class="img"></div>    
//    <div lazy-class="full" lazy-loader="#sampleLoader" lazy-background="http://4.bp.blogspot.com/_pKFEEX6T1AU/S8S9RqSzYcI/AAAAAAAAACE/__q43tvl6nU/s1600/earth.gif" class="img2"></div> 
//</div>

angular.module("hmh-directive", []).
directive("hmhLazybackground", ["$document",function ($document) {
    return {
        restrict: "A",
        link: function (scope, iElement, iAttrs) {
            //默认加载指示
            var loader = angular.element('<div>...</div>');
            if(angular.isDefined(iAttrs.lazyLoader))
            {
                loader = angular.element($document[0].querySelector(iAttrs.lazyLoader)).clone();
            }

            iElement.append(loader);

            var src = iAttrs.lazyBackground + "?r=" + Math.random();
            var img = document.createElement("img");
            img.onLoad = function () {
                loader.remove();
                if(angular.isDefined(iAttrs.lazyClass))
                {
                    iElement.addClass(iAttrs.lazyClass);
                }
                iElement.css({
                    "background-image":"url("+this.src+")"
                });
                delete this;
            };

            img.onerror = function () {
                console.log("error");
            };
            img.src = src;
        }
    };
}]);