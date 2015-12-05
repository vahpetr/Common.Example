define(['jquery', 'knockout', 'mediator', 'errors', 'text', 'bootstrap', 'knockout-validation'], function ($, ko, mediator, errors) {

    var updateModalHeight = function (el) {
        var viewportHeight = $(window).height();
        var offset = 60;
        var header = $('.modal-header', el).outerHeight(true);
        var footer = $('.modal-footer', el).outerHeight(true);
        $('.modal-body', el).css('height', viewportHeight - header - footer - offset);
    }

    $('.modal.auto').on('shown.bs.modal', updateModalHeight);

    var updateModalsHeights = function () {
        $('.modal.auto').each(function () {
            updateModalHeight(this);
        });
    }

    $(window).resize(function () {
        updateModalsHeights();
    });

    //===

    mediator.add('app', {
        onPageChanged: function () {
            setTimeout(function () { 
                updateModalsHeights();
                $('[data-toggle="tooltip"]').tooltip();
                $('[data-toggle="popover"]').popover();
            }, 0);
        }
    });

    //===

    ko.validation.init({
        insertMessages:false,
        decorateElement: true,
        errorClass: 'has-error',//help-block not-valid
        errorElementClass: 'has-error',
        errorMessageClass: 'help-block',//validation-message
        errorsAsTitle: false,
        grouping: {
            deep: true
        }
    });

    ko.validation.rules['required'] = {
        validator: function (val, required) {
            if (!required) {
                return true;
            }

            if (val === undefined || val === null) {
                return false;
            }

            if (val.length) return true;

            var stringTrimRegEx = /^\s+|\s+$/g,
                testVal = val;

            if (typeof (val) == "string") {
                testVal = val.replace(stringTrimRegEx, '');
            }

            return (testVal + '').length > 0;
        },
        message: 'Необходимо заполнить это поле.'
    };

    ko.validation.registerExtenders();

    ko.validation.localize({
        required: 'Необходимо заполнить это поле',
        min: 'Значение должно быть больше или равно {0}',
        max: 'Значение должно быть меньше или равно {0}',
        minLength: 'Длина поля должна быть не меньше {0} символов',
        maxLength: 'Длина поля должна быть не больше {0} символов',
        pattern: 'Не допустимый символ или формат',
        step: 'Значение поле должно изменяться с шагом {0}',
        email: 'Введите в поле правильный адрес email',
        date: 'Пожалуйста введите правильную дату',
        dateISO: 'Пожалуйста введите правильную дату в формате ISO',
        number: 'Поле должно содержать число',
        digit: 'Поле должно содержать цифры',
        phoneUS: 'Поле должно содержать правильный номер телефона',
        equal: 'Значения должны быть равны',
        notEqual: 'Пожалуйста выберите другое значение',
        unique: 'Значение должно быть уникальным'
    });

});