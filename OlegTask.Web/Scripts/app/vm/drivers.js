define([
    'jquery', 'knockout',
    'da/drivers', 'da/cars', 'da/ratings',
    'knockout-extends'
], function (
    $, ko,
    daDrivers, daCars, daRatings
) {
    var drivers = function () {
        var vm = this;

        vm.subscriptions = [];

        vm.page = ko.observable();
        vm.page.loading = ko.observable();
        vm.page.loading.off = function() {
            vm.page.loading(false);
        };
        vm.page.load = function() {
            if (vm.page.loading.peek()) return new $.Deferred().reject();
            vm.page.loading(true);
            var filter = {};
            return daDrivers.get(filter).then(vm.page);
        };

        vm.load = function () {
            return vm.page.load().always(vm.page.loading.off);
        };

        vm.driver = ko.createObject(daDrivers, 'models/driver', vm.load, vm.subscriptions);
        vm.car = ko.createObject(daCars, 'models/car', vm.load, vm.subscriptions);
        vm.rating = ko.createObject(daRatings, 'models/rating', vm.load, vm.subscriptions);

        (function (origin) {
            vm.rating.edit = function (driver) {
                origin({
                    driver: driver
                });
            }
        })(vm.rating.edit);

        //для отладки, временно
        window.vm = vm;
        window.ko = ko;

        return vm;
    };
    drivers.prototype.dispose = function () {
        this.subscriptions.forEach(function() {
            this.dispose();
        });
        this.driver.dispose();
    }
    return drivers;
});