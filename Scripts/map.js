function myMap() {
    var mapProp = { 
        center: new google.maps.LatLng(51.09321995647789, - 114.06998222312538),
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
    var marker07 = new google.maps.Marker({
        position: { lat: 51.154198360157245, lng:  - 114.2237791741131},
        map: map,
        title: "120 Royal Oak Terrace NW",
    });
   
    var marker08 = new google.maps.Marker({
        position: { lat: 51.13254967927305, lng: - 114.25615451320024 },
        map: map,
        title: "45 Tuscany Ridge NW",
    });
    var marker09 = new google.maps.Marker({
        position: { lat: 50.939934884133066, lng: - 113.9845638317983 },
        map: map,
        title: "3107 Douglasdale Blvd SE",
    });
    var marker10 = new google.maps.Marker({
        position: { lat: 50.890937555550465, lng: - 113.98826137412959 },
        map: map,
        title: "58 Cranston Drive SE",
    });
    var marker11 = new google.maps.Marker({
        position: { lat: 51.17528729322901, lng: - 114.10351784527592 },
        map: map,
        title: "150 Evanston Way NW", 
    });
    var marker12 = new google.maps.Marker({
        position: { lat: 51.041183710835504, lng: - 114.21118963179191 },
        map: map, 
        title: "23 Aspen Stone Blvd SW",
    });
    var marker13 = new google.maps.Marker({
        position: { lat: 50.926108411135104, lng: - 114.09484573179915 },
        map: map,
        title: "78 Evergreen Street SW",
    });
    var marker14 = new google.maps.Marker({
        position: { lat: 51.1697285691906, lng: - 114.05315291829133 },
        map: map,
        title: "12 Coventry Hills Way NE",
    });
    var marker15 = new google.maps.Marker({
        position: { lat: 51.15789322048899, lng: - 113.96151094527703 }, 
        map: map,
        title: "29 Skyview Shores Manor NE",
    });
    var marker16 = new google.maps.Marker({
        position: { lat: 51.17205728123805, lng: - 114.15767160294784 },
        map: map,
        title: "85 Nolan Hill Blvd NW",
    });
    var marker17 = new google.maps.Marker({
        position: { lat: 50.92317416528538, lng: - 113.93222132994843 },  
        map: map,
        title: "107 Copperpond Blvd SE",
    });
    var marker18 = new google.maps.Marker({
        position: { lat: 50.92381227186465, lng: - 114.02822076063512 },
        map: map, 
        title: "63 Parkland Blvd SE",
    });
    var marker19 = new google.maps.Marker({
        position: { lat: 50.923731116318365, lng: - 114.02824221830683 },
        map: map,
        title: "14 Bridlewood Road SW",
    });
    var marker20 = new google.maps.Marker({
        position: { lat: 50.88694323816372, lng: - 114.07372891830909 },
        map: map,
        title: "22 Silverado Blvd SW",
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
    google.maps.event.addDomListener(marker07, 'click', function () {
        window.location.href = 'ViewProperty?id=P000007';
    });
   google.maps.event.addDomListener(marker08, 'click', function () {
        window.location.href = 'ViewProperty?id=P000008';
    });
    google.maps.event.addDomListener(marker09, 'click', function () {
        window.location.href = 'ViewProperty?id=P000009';
    });
    google.maps.event.addDomListener(marker10, 'click', function () {
        window.location.href = 'ViewProperty?id=P000010';
    });
    google.maps.event.addDomListener(marker11, 'click', function () {
        window.location.href = 'ViewProperty?id=P000011';
    });
    google.maps.event.addDomListener(marker12, 'click', function () {
        window.location.href = 'ViewProperty?id=P000012';
    });
    google.maps.event.addDomListener(marker13, 'click', function () {
        window.location.href = 'ViewProperty?id=P000013';
    });
    google.maps.event.addDomListener(marker14, 'click', function () {
        window.location.href = 'ViewProperty?id=P000014';
    });
    google.maps.event.addDomListener(marker15, 'click', function () {
        window.location.href = 'ViewProperty?id=P000015';
    });
    google.maps.event.addDomListener(marker16, 'click', function () {
        window.location.href = 'ViewProperty?id=P000016';
    });
    google.maps.event.addDomListener(marker17, 'click', function () {
        window.location.href = 'ViewProperty?id=P000017';
    });
    google.maps.event.addDomListener(marker18, 'click', function () {
        window.location.href = 'ViewProperty?id=P000018';
    });
    google.maps.event.addDomListener(marker19, 'click', function () {
        window.location.href = 'ViewProperty?id=P000019';
    });
    google.maps.event.addDomListener(marker20, 'click', function () {
        window.location.href = 'ViewProperty?id=P000020';
    });
}