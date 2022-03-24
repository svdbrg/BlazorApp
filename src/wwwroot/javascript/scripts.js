(function () {
    window.PlTable = {
        setClass: (homeTeam, awayTeam) => {
            document.getElementById(homeTeam).classList.add('mud-theme-secondary');
            document.getElementById(awayTeam).classList.add('mud-theme-secondary');
        },
        removeClass: (homeTeam, awayTeam) => {
            document.getElementById(homeTeam).classList.remove('mud-theme-secondary');
            document.getElementById(awayTeam).classList.remove('mud-theme-secondary');
        }
    };

    window.Fixture = {
        setClass: (team) => {
            if (document.getElementById('f-' + team) != undefined) {
                document.getElementById('f-' + team).parentElement.classList.add('mud-theme-secondary');
            }
        },
        removeClass: (team) => {
            if (document.getElementById('f-' + team) != undefined) {
                document.getElementById('f-' + team).parentElement.classList.remove('mud-theme-secondary');
            }
        }
    };
})();

var map, infobox;
function loadBingMap(mapInfo, bingCredentials) {
    var box = new Microsoft.Maps.LocationRect.fromEdges(mapInfo.box.maxLat, mapInfo.box.minLon, mapInfo.box.minLat, mapInfo.box.maxLon);
    map = new Microsoft.Maps.Map(document.getElementById('map'), {
        credentials: bingCredentials,
        bounds: box
    });

    infobox = new Microsoft.Maps.Infobox(map.getCenter(), {
        visible: false
    });

    infobox.setMap(map);

    for (let index = 0; index < mapInfo.stops.length; index++) {
        var loc = new Microsoft.Maps.Location(mapInfo.stops[index].locationNorthingCoordinate, mapInfo.stops[index].locationEastingCoordinate);
        var pin = new Microsoft.Maps.Pushpin(loc);
        pin.metadata = {
            title: mapInfo.stops[index].stopPointName
        }
        Microsoft.Maps.Events.addHandler(pin, 'click', pushpinClicked);
        map.entities.push(pin);
    }

    return "";
}

function pushpinClicked(e) {
    if (e.target.metadata) {
        infobox.setOptions({
            location: e.target.getLocation(),
            title: e.target.metadata.title,
            visible: true
        });
    }
}