function myMap() {
    var mapProp = {
        center: new google.maps.LatLng(51.05832984377161, -114.08870684636732),
        zoom: 11,
        mapId: "GOOGLE_MAP_ID",
    };

    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
    var marker01 = new google.maps.Marker({
        position: { lat: 51.08429637368886, lng: -114.18889932993825 },
        map: map,
        title: "7038 34 Avenue NW",
    });
    var marker02 = new google.maps.Marker({
        position: { lat: 51.09613820521745, lng: -114.19226291829602 },
        map: map,
        title: "7224 Bow Crescent NW",
    });
    var marker03 = new google.maps.Marker({
        position: { lat: 51.11129095637459, lng: -113.95005332993654 },
        map: map,
        title: "56 Martingrove Way NE",
    });
    var marker04 = new google.maps.Marker({
        position: { lat: 51.088162204222186, lng: -113.94042548946067 },
        map: map,
        title: "15 Templegreen Road NE",
    });
    var marker05 = new google.maps.Marker({
        position: { lat: 51.09660789023076, lng: - 114.1406477452809 },
        map: map,
        title: "2356 Northmount Drive NW",
    });
    var marker06 = new google.maps.Marker({
        position: { lat: 51.1487336450673, lng: -114.16171146389523 },
        map: map,
        title: "50 Hamptons Manor NW",
    });
    google.maps.event.addDomListener(marker01, 'click', function () {
        window.location.href = 'ViewProperty?id=P000001';
    });
    google.maps.event.addDomListener(marker02, 'click', function () {
        window.location.href = 'ViewProperty?id=P000002';
    });
    google.maps.event.addDomListener(marker03, 'click', function () {
        window.location.href = 'ViewProperty?id=P000003';
    });
    google.maps.event.addDomListener(marker04, 'click', function () {
        window.location.href = 'ViewProperty?id=P000004';
    });
    google.maps.event.addDomListener(marker05, 'click', function () {
        window.location.href = 'ViewProperty?id=P000005';
    });
    google.maps.event.addDomListener(marker06, 'click', function () {
        window.location.href = 'ViewProperty?id=P000006';
    });
}