define(["toastr"],
    function(toastr) {
        "use strict";

        var options = {
            timeOut: 5000,
            closeButton: true
        };

        var error = function(data, header) {
            if (data.responseJSON) {
                var res = data.responseJSON;
                if (res.exceptionMessage) {
                    var exception = res;
                    while ('innerException' in exception) {
                        exception = exception.innerException;
                    }
                    toastr.error(exception.exceptionMessage, 'Исключение', options);
                } else if (res.modelState) {
                    var modelState = res.modelState;
                    // ReSharper disable once MissingHasOwnPropertyInForeach
                    for (var model in modelState) {
                        var errors = modelState[model].reduce(function(str, text) {
                            return str + '\r\n' + text;
                        }, '');
                        toastr.error(errors, model, options);
                    }
                } else {
                    toastr.error(data.responseText, header, options);
                }
            } else {
                toastr.error(data.responseText, header, options);
            }
        };

        return {
            load: function(data) {
                error(data, 'Ошибка загрузки данных');
            },
            create: function(data) {
                error(data, 'Ошибка при создании');
            },
            update: function(data) {
                error(data, 'Ошибка при обновлении');
            },
            remove: function(data) {
                error(data, 'Ошибка при удалении');
            },
            upload: function(data) {
                error(data, 'Ошибка при отправке файлов');
            },
            download: function(data) {
                error(data, 'Ошибка при скачивании файла');
            },
            set: function(data) {
                error(data, 'Ошибка при изменении');
            },
            request: function(data) {
                error(data, 'Ошибка при запросе');
            },
            jsnop: function(data) {
                error(data, 'Ошибка при обращении к другому серверу');
            },

            forbidden: function (data) {
                error(data, 'Отказано в доступе');
            }
        };
    }
);