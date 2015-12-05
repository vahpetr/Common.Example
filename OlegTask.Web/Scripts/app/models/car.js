define([
    'knockout',
    'knockout-validation'
], function (
    ko
) {
    var car = function (params) {
        params = ko.utils.extend({
            id: 0,
            number:'',
            brand:'',
            model:'',
            color:''
        }, params || {});
          
        var vm = this;

        vm.id = params.id;
        vm.number = ko.observable(params.number).extend({
            required: true,
            maxLength: 32
        });
        vm.brand = ko.observable(params.brand).extend({
            required: true,
            maxLength: 64
        });
        vm.model = ko.observable(params.model).extend({
            required: true,
            maxLength: 64
        });
        vm.color = ko.observable(params.color).extend({
            required: true,
            maxLength: 64
        });
        
        //===

        vm.errors = ko.validation.group(vm);
        vm.isValid = ko.computed(function () {
            var errors = vm.errors();
            return errors.length === 0;
        });

        return vm;
    };
    car.prototype.toJS = function () {
        var dto = {
            id: this.id,
            number: this.number.peek(),
            brand: this.brand.peek(),
            model: this.model.peek(),
            color: this.color.peek()
        };
        return dto;
    }
    car.prototype.dispose = function () {
        this.isValid.dispose();
    }
    return car;
})