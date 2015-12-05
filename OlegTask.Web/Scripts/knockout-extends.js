define(['knockout', 'utils', 'mediator'], function(ko, utils, mediator) {
    ko.observable.fn.safe = function(prop, def) {
        var item = this();
        if (item != null) {
            prop = item[prop];
            return (ko.isObservable(prop) ? prop() : prop) || def;
        }
        return def;
    };

    ko.bindingHandlers.disabled = {
        update: function(element, valueAccessor) {
            var value = ko.unwrap(valueAccessor());
            $(element)[value ? 'addClass' : 'removeClass']('disable');
        }
    };
    ko.bindingHandlers.hidden = {
        update: function(element, valueAccessor) {
            var value = ko.unwrap(valueAccessor());
            $(element)[value ? 'addClass' : 'removeClass']('hidden');
        }
    };
    ko.bindingHandlers.visible = {
        update: function(element, valueAccessor) {
            var value = ko.unwrap(valueAccessor());
            $(element)[value ? 'addClass' : 'removeClass']('visible');
        }
    };

    ko.bindingHandlers.optionsSelector = {
        init: function(element, valueAccessor, allBindings, viewModel, bindingContext) {
            var context = allBindings();
            var observable = context.value;
            var item = ko.unwrap(observable);
            if (item == null) return;
            var prop = valueAccessor();
            var key = item[prop];
            var options = ko.unwrap(context.options);
            var match = ko.utils.arrayFirst(options, function(item) {
                return item[prop] === key;
            });
            observable(match);
        }
    };

    ko.bindingHandlers.date = {
        init: function (element, valueAccessor) {
            require(['jquery', 'moment'], function ($, moment) {
                var accessor = ko.unwrap(valueAccessor()),
                    format = 'DD.MM.YYYY',
                    date = accessor;
                if (Object.prototype.toString.call(accessor) != '[object Date]' && typeof accessor != "string") {
                    format = accessor.format;
                    date = ko.unwrap(accessor.value);
                    if (accessor.useBrowserUTCOffset) {
                        date = moment(date).add('minutes', new Date().getTimezoneOffset() * -1);
                    }
                }
                $(element).text(date && moment(date).format(format) || "-");
                $(element).val(date && moment(date).format(format) || "-");

                ko.utils.registerEventHandler(element, 'change', function(e) {
                    var setter = (accessor.value || accessor);
                    var value = moment(e.target.value).toDate();
                    setter(value);
                });
            });
        }
    };

    ko.createObject = function (da, modelUrl, refresh, subscriptions) {
        var vm = ko.observable();
        vm.edit = function (item) {
            item = utils.copy(item || {});
            require([modelUrl], function (model) {
                vm(new model(item));
                mediator.broadcast('PageChanged');
            });
        };
        vm.cancel = function () {
            vm(null);
        };
        vm.saving = ko.observable();
        vm.saving.off = function () {
            vm.saving(false);
        };
        vm.save = function () {
            var reject = new $.Deferred().reject();
            if (vm.saving.peek()) return reject;
            var item = vm.peek();
            if (!item.isValid()) return reject;
            var data = item.toJS();
            if (!data) return reject;
            vm.saving(true);
            return da.save(data)
                .then(refresh)
                .then(vm.cancel)
                .always(vm.saving.off);
        };
        vm.create = function () {
            vm.edit();
        };
        vm.deleting = ko.observable();
        vm.deleting.off = function () {
            vm.deleting(false);
        };
        vm.remove = function (item) {
            var reject = new $.Deferred().reject();
            if (vm.deleting.peek()) return reject;
            var result = confirm("Запись будет безвозвратно удалена. Продолжить?");
            if (!result) return reject;
            vm.deleting(true);
            return da.remove(item)
                .then(refresh)
                .then(vm.cancel)
                .always(vm.deleting.off);
        };

        subscriptions.push(vm.subscribe(function (item) {
            vm.selected(item);
        }));

        vm.selected = ko.observable();
        vm.selected.toggle = function (item) {
            if (vm.selected.peek() == item) return vm.selected(null);
            return vm.selected(item);
        };
        vm.selected.edit = function () {
            var selected = vm.selected();
            if (!selected) return new $.Deferred().reject();
            vm.edit(selected);
        };
        vm.selected.remove = function () {
            var selected = vm.selected();
            if (!selected) return new $.Deferred().reject();
            return vm.remove(selected);
        };
        return vm;
    };
});