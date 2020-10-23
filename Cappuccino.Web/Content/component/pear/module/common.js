layui.define(['jquery', 'popup'], function (exports) {
    var $ = layui.jquery;
    var popup = layui.popup;
    var common = {
        /**
        * 是否前后端分离
        */
        isFrontendBackendSeparate: false,
        /**
         * 服务器地址
         */
        baseUrl: "http://localhost:8080",
        /**
         * ajax()函数二次封装
         * @param url
         * @param type
         * @param params
         * @param load
         * @returns {*|never|{always, promise, state, then}}
         */
        ajax: function (url, type, params, load, async) {
            var deferred = $.Deferred();
            var loadIndex;
            $.ajax({
                url: common.isFrontendBackendSeparate ? common.baseUrl + url : url,
                type: type || "get",
                data: params || {},
                dataType: "json",
                async: async,
                beforeSend: function () {
                    if (load) {
                        loadIndex = layer.load(0, { shade: false });
                    }
                },
                success: function (data) {
                    if (data.status == "0") {
                        // 业务正常
                        deferred.resolve(data)
                    } else if (data.status == "2") {
                        popup.warming(data.msg, function () { window.top.location ="/Account/Login" })
                    } else {
                        // 业务异常
                        popup.warming(data.msg)
                        deferred.reject("common.ajax warn: " + data.msg);
                    }
                },
                complete: function () {
                    if (load) {
                        layer.close(loadIndex);
                    }
                },
                error: function () {
                    layer.close(loadIndex);
                    popup.failure("服务器错误")
                    deferred.reject("common.ajax error: 服务器错误");
                }
            });
            return deferred.promise();
        },
    };
    //输出接口
    exports('common', common);
});