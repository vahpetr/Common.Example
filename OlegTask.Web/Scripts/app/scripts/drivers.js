define(['knockout', 'vm/drivers', 'main', 'domReady!'], function (ko, driversVm) {
    var drivers = new driversVm();
    window.drivers = drivers;
    drivers.load().then(function () {
        ko.applyBindings(drivers);
    });
    setInterval(drivers.load, 60 * 1000);//авторефреш раз в минуту
});