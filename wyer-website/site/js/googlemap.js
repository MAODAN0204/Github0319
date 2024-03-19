// When the window has finished loading create our google map below
google.maps.event.addDomListener(window, 'load', googlemapinit);

function googlemapinit() {
    // Basic options for a simple Google Map
    // For more options see: https://developers.google.com/maps/documentation/javascript/reference#MapOptions
    var mapOptions = {
        // How zoomed in you want the map to start at (always required)
        zoom: 13,
        scrollwheel: false,

        // The latitude and longitude to center the map (always required)
        center: new google.maps.LatLng(24.826784, 121.010174),

        // How you would like to style the map.
        styles: [{ "featureType": "all", "elementType": "labels.text.fill", "stylers": [{ "saturation": 36 }, { "color": "#ffffff" }, { "lightness": 40 }] },
            { "featureType": "all", "elementType": "labels.text.stroke", "stylers": [{ "visibility": "on" }, { "color": "#000000" }, { "lightness": 16 }] },
            { "featureType": "all", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] },
            { "featureType": "administrative", "elementType": "geometry.fill", "stylers": [{ "color": "#000" }, { "lightness": 20 }] },
            { "featureType": "administrative", "elementType": "geometry.stroke", "stylers": [{ "color": "#000000" }, { "lightness": 17 }, { "weight": 1.2 }] },
            { "featureType": "landscape", "elementType": "geometry", "stylers": [{ "color": "#272727" }, { "lightness": 20 }] },
            { "featureType": "poi", "elementType": "geometry", "stylers": [{ "color": "#000000" }, { "lightness": 21 }] },
            { "featureType": "road.highway", "elementType": "geometry.fill", "stylers": [{ "color": "#000000" }, { "lightness": 17 }] },
            { "featureType": "road.highway", "elementType": "geometry.stroke", "stylers": [{ "color": "#000000" }, { "lightness": 29 }, { "weight": 0.2 }] },
            { "featureType": "road.arterial", "elementType": "geometry", "stylers": [{ "color": "#000000" }, { "lightness": 18 }] },
            { "featureType": "road.local", "elementType": "geometry", "stylers": [{ "color": "#000000" }, { "lightness": 16 }] },
            { "featureType": "transit", "elementType": "geometry", "stylers": [{ "color": "#000000" }, { "lightness": 19 }] },
            { "featureType": "water", "elementType": "geometry", "stylers": [{ "color": "#41c4ff" }, { "lightness": 17 }] }]
    };

    // Get the HTML DOM element that will contain your map
    // We are using a div with id="google_map" seen below in the <body>
    var mapElement = document.getElementById('google_map');
    if (mapElement) {
        // Create the Google Map using our element and options defined above
        var map = new google.maps.Map(mapElement, mapOptions);

        // Let's also add a marker while we're at it
        var gimage = 'images/gmarker.png';
        var svgMarker = {
            path: "M-1.547 12l6.563-6.609-1.406-1.406-5.156 5.203-2.063-2.109-1.406 1.406zM0 0q2.906 0 4.945 2.039t2.039 4.945q0 1.453-0.727 3.328t-1.758 3.516-2.039 3.070-1.711 2.273l-0.75 0.797q-0.281-0.328-0.75-0.867t-1.688-2.156-2.133-3.141-1.664-3.445-0.75-3.375q0-2.906 2.039-4.945t4.945-2.039z",
            fillColor: "#F9F900",
            fillOpacity: 1,
            strokeColor: "yellow",
            strokeWeight: 2,
            rotation: 0,
            scale: 2,
            anchor: new google.maps.Point(0, 20),
        };
        var marker = new google.maps.Marker({
            position: new google.maps.LatLng(24.826784, 121.010174),
            map: map,
            icon: svgMarker,
            title: 'Wyer'
        });
    }


}
