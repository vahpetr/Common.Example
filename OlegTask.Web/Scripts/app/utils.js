define(function() {
    "use strict";
    var getType = (function () {
        var toString = {}.toString;
        return function (obj) {
            return toString.call(obj);
        }
    })();
    var isFunc = function (obj) {
        return getType(obj) === '[object Function]';
    };
    var init = function (m, obj) {
        for (var prop in obj) {
            if (m[prop] === undefined) continue;
            isFunc(m[prop]) && m[prop](obj[prop]) || (m[prop] = obj[prop]);
        }
    };
    var format = function (text, arg) {
        for (var i = 0, l = arg.length; i < l; i++) {
            text = text.replace(new RegExp('\\{' + i + '\\}', 'g'), arg[i]);
        }
        return text;
    };
    var copy = function (obj) {
        return JSON.parse(JSON.stringify(obj));
    }
    var isEmpty = function (text) {
        return !text.replace(/ /g, '').replace(/\n/g, '').replace(/\r/g, '').replace(/\t/g, '');
    };
    return {
        init: init,
        format: format,
        copy: copy,
        isEmpty: isEmpty
    };
});