define([
    'models/car', 'da/cars',
    'knockout', 'moment', 'mediator',
    'knockout-validation'
], function (
    modelCar, daCars,
    ko, moment, mediator
) {
    var driver = function (params) {
        params = ko.utils.extend({
            id: 0,
            surname: '',
            name:'',
            passportNumber:0,
            birthDay: moment(new Date).format(),
            cars: []
        }, params || {});
          
        var vm = this;
        vm.subscriptions = [];

        vm.id = params.id;
        vm.surname = ko.observable(params.surname).extend({
            required: true,
            maxLength: 128
        });
        vm.name = ko.observable(params.name).extend({
            required: true,
            maxLength: 128
        });
        vm.passportNumber = ko.observable(params.passportNumber).extend({
            required: true,
            min: 1,//забыл про это на сервере
            max: 999999
        });
        vm.birthDay = ko.observable(moment(params.birthDay).toDate()).extend({
            required: true
        });
        
        vm.cars = ko.observableArray(params.cars.map(function(item) {
            return new modelCar(item);
        })).extend({
            required: {
                params: true,
                message: "Необходимо указать хотя бы одну машину"
            }
        });
        
        vm.cars.edit = function(car) {
            require(['models/car'], function (model) {
                var cars = vm.cars.peek();
                var params = car || {};
                cars.unshift(new model(params));
                vm.cars(cars);
                mediator.broadcast('PageChanged');
            });
        }
        vm.cars.add = function () {
            vm.cars.edit();
        }
        //===

        vm.errors = ko.validation.group(vm);
        vm.isValid = ko.computed(function () {
            var errors = vm.errors();
            var isValid = errors.length === 0;
            vm.errors.showAllMessages(!isValid);
            return isValid;
        });

        vm.carsList = ko.observableArray([]);
        vm.carsList.load = function() {
            daCars.get({ count: 100 }).then(function(page) {
                vm.carsList(page.data);
            });
        }
        vm.choices = ko.observable();
        vm.subscriptions.push(vm.choices.subscribe(function (car) {
            if (!car) return;
            vm.cars.edit(car);
            vm.choices(undefined);
        }));

        vm.availableCars = ko.computed(function() {
            var cars = vm.cars();
            var list = vm.carsList();
            var res = [];
            var item;

            for (var i = 0, l = list.length; i < l; i++) {
                item = cars.find(function(item) {
                    return item.id == list[i].id;
                });
                if (item != null) continue;
                res.push(list[i]);
            }

            return res;
        });

        vm.carsList.load();

        return vm;
    };
    driver.prototype.toJS = function () {
        var dto = {
            id: this.id,
            surname: this.surname.peek(),
            name: this.name.peek(),
            passportNumber: this.passportNumber.peek(),
            birthDay: moment(this.birthDay.peek()).format(),
            cars: this.cars.peek().map(function (item) {
                return item.toJS();
            })
        };
        return dto;
    }
    driver.prototype.dispose = function () {
        this.subscriptions.forEach(function () {
            this.dispose();
        });
        this.isValid.dispose();
        this.availableCars.dispose();
    }
    return driver;
})