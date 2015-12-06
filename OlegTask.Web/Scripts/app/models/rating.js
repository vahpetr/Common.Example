define([
    'knockout', 'utils',
    'knockout-validation'
], function (
    ko, utils
) {
    var rating = function (params) {
        params = ko.utils.extend({
            id: 0,
            value: 5,
            driver:null
        }, params || {});
          
        var vm = this;

        vm.id = params.id;
        vm.value = ko.observable(params.value).extend({
            required: true,
            min: 0,
            max: 10
        });
        vm.driver = ko.observable(params.driver).extend({
            required: true
        });
        
        //===

        vm.errors = ko.validation.group(vm);
        vm.isValid = ko.computed(function () {
            var errors = vm.errors();
            var isValid = errors.length === 0;
            vm.errors.showAllMessages(!isValid);
            return isValid;
        });

        return vm;
    };
    rating.prototype.toJS = function () { 
        var dto = {
            id: this.id,
            value: this.value.peek(),
            driverId: this.driver.peek().id
        };
        return dto;
    }
    rating.prototype.dispose = function () {
        this.isValid.dispose();
    }
    return rating;
})