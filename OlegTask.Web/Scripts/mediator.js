define([], function() {
    var Mediator = function() {
        var debug = function() {
            if (typeof console !== 'undefined' && console.log) {
                console.log(arguments[0]);
            }
        };

        var components = {};

        var broadcast = function(event, args, source) {
            var e = event || false;
            var a = args || [];
            if (!e) {
                return;
            }
            if (a.constructor != Array) {
                a = [a];
            }
            debug(["mediator received", e, a].join(' '));
            for (var c in components) {
                if (typeof components[c]["on" + e] == "function") {
                    var s = source || components[c];
                    try {
                        debug("mediator calling " + e + " on " + c);
                        components[c]["on" + e].apply(s, a);
                    } catch (err) {
                        debug(["mediator error.", e, a, s, err].join(' '));
                    }
                }
            }
        };

        var addComponent = function(name, component, extend) {
            if (name in components) {
                if (extend) {
                    for (var prop in component) {
                        components[name][prop] && debug(prop + ' overwriten!');
                        components[name][prop] = component[prop];
                    }
                    return;
                } else {
                    throw new Error('mediator name conflict: ' + name);
                }
            }
            components[name] = component;
        };

        var removeComponent = function(name) {
            if (name in components) {
                delete components[name];
            }
        };

        var getComponent = function(name) {
            return components[name] || false;
        };

        var contains = function(name) {
            return (name in components);
        };

        return {
            name: "mediator",
            broadcast: broadcast,
            add: addComponent,
            remove: removeComponent,
            get: getComponent,
            contains: contains
        };
    }
    var mediator = new Mediator();
    return mediator;
});
