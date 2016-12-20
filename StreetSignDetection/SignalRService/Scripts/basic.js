var map;

function loadMapScenario() {
    map = new Microsoft.Maps.Map(document.getElementById('myMap'), {
        credentials: 'AuQwytbIBe_f2HGU1kRM0dOwqowcZm-_qgz0yHI4hXGPie60onABnFmhJAlfXDAh',
        center: new Microsoft.Maps.Location(36.12933, -115.152946),
        showScalebar: false,
        zoom: 14
    });

    Microsoft.Maps.Events.addHandler(map, 'click', function (info) { addPushpin(info.location.latitude, info.location.longitude); });
    //Microsoft.Maps.Events.addHandler(map, 'rightclick', function () { ReceiveSQLData(); });
}

function addPushpin(lat, lon) {
    var pushpin = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(lat, lon), {
        icon: 'images/stop.svg',
        anchor: new Microsoft.Maps.Point(12, 39)
    });
    map.entities.push(pushpin);
}

function addCarPushpin(lat, lon) {
    var pushpin = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(lat, lon), {
        icon: 'images/car.svg',
        anchor: new Microsoft.Maps.Point(12, 39)
    });
    map.entities.push(pushpin);
}

function ClearMap() {
    map.entities.clear();
}

function asyncLoop(secTimeout, func, callback) {
    var index = 0;
    var done = false;
    var loop = {
        next: function () {
            index++;
            setTimeout(function () { }, secTimeout * 1000);
            func(loop);
        },

        iteration: function () {
            return index - 1;
        },

        break: function () {
            done = true;
            callback();
        }
    };
    loop.next();
    return loop;
}