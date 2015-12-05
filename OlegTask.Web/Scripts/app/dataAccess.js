define(["jquery", 'errors'],
    function ($, errors) {
        "use strict";
        
        $.GET = $.getJSON;
        $.POST = $.post;
        $.PUT = function (url, data, callback) {
            return $.ajax(url, {
                type: 'PUT',
                data: data,
                dataType: 'json',
                statusCode: {
                    200: callback
                }
            });
        };
        $.DELETE = function (url, data, callback) {
            return $.ajax(url, {
                type: 'DELETE',
                data: data,
                dataType: 'json',
                statusCode: {
                    200: callback
                }
            });
        };
        //$.ajaxSettings.traditional = false;
        var DaBase = function (controller) {
            var da = this;
            da.get = function (method, request) {
                return (function () {
                    if (!method) {
                        return $.GET(controller);
                    }
                    if (typeof request == 'number') {
                        return $.GET(controller + '/' + method + '/' + request);
                    }
                    if (typeof method == 'string') {
                        return $.GET(controller + '/' + method, request);
                    }
                    if (typeof method == 'object') {
                        return $.GET(controller, method);
                    }
                    return $.GET(controller + '/' + method);
                })().fail(errors.load);
            };
            da.save = function (obj) {
                if (obj.id == 0) {
                    return $.POST(controller, obj).fail(errors.create);
                } else {
                    return $.PUT(controller + '/' + obj.id, obj).fail(errors.update);
                }
            };
            da.create = function (obj) {
                return $.POST(controller, obj).fail(errors.create);
            };
            da.update = function (method, obj) {
                return $.PUT(controller + '/' + method, obj).fail(errors.update);
            };
            da.remove = function (request) {
                return (function () {
                    if (Array.isArray(request)) {
                        var ids = typeof request[0] == 'number' ? request : request.filter(function (i) { return i.id; });
                        return $.DELETE(controller, ids);
                    }
                    var id = typeof request == 'number' ? request : request.id;
                    return $.DELETE(controller + '/' + id);
                })().fail(errors.remove);
            };
            da.set = function (method, request) {
                return (function () {
                    if (typeof request == 'number') {
                        return $.PUT(controller + '/' + method + '/' + request);
                    }
                    return $.PUT(controller + '/' + method, request);
                })().fail(errors.set);
            };
            da.request = function (method, request) {
                return $.POST(controller + '/' + method, request).fail(errors.request);
            };
            da.jsnop = function (method, request) {
                return $.GET(controller + '/' + method + toQuery(request)).fail(errors.jsnop);
            };
        };
        function toQuery(request) {
            var params = [];
            // ReSharper disable once MissingHasOwnPropertyInForeach
            for (var param in request) {
                params.push(param + '=' + request[param]);
            }
            return '?callback=?&' + params.join('&');
        }
        return function (controller, methods) {
            if (!controller) {
                throw 'Не задан контроллер';
            }
            var base = function () { };
            base.prototype = new DaBase(controller);
            // ReSharper disable once MissingHasOwnPropertyInForeach
            for (var method in methods) {
                base.prototype[method] = methods[method];
            }
            return new base;
        };
    }
);