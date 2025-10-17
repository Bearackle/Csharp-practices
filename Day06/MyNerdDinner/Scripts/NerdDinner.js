
var NerdDinner = {};
NerdDinner._map = null;
NerdDinner._marker = null;
NerdDinner.MapDivId = 'map';
NerdDinner.defaultZoom = 13;

NerdDinner.LoadMap = function (latitude, longitude) {
    var lat = latitude || 10.7948236330744;
    var lon = longitude || 106.64059229446866;

    if (!NerdDinner._map) {
        NerdDinner._map = L.map(NerdDinner.MapDivId).setView([lat, lon], NerdDinner.defaultZoom);

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '&copy; OpenStreetMap contributors'
        }).addTo(NerdDinner._map);

        if (latitude && longitude) {
            NerdDinner._marker = L.marker([latitude, longitude]).addTo(NerdDinner._map);
        }

        NerdDinner._map.on('click', function (e) {
            NerdDinner.SetMarker(e.latlng.lat, e.latlng.lng);
        });
    } else {
        NerdDinner._map.setView([lat, lon], NerdDinner.defaultZoom);
    }
};

NerdDinner.SetMarker = function (latitude, longitude) {
    if (NerdDinner._marker) {
        NerdDinner._map.removeLayer(NerdDinner._marker);
    }
    NerdDinner._marker = L.marker([latitude, longitude], { draggable: true }).addTo(NerdDinner._map);

    NerdDinner._marker.on('dragend', function (e) {
        var latlng = e.target.getLatLng();
        NerdDinner.UpdateHiddenFields(latlng.lat, latlng.lng);
    });

    NerdDinner.UpdateHiddenFields(latitude, longitude);
};

NerdDinner.FindAddressOnMap = function (address) {
    if (!address) return;

    var url = "https://nominatim.openstreetmap.org/search?format=json&q=" + encodeURIComponent(address);
    $.getJSON(url, function (data) {
        if (data && data.length > 0) {
            var lat = parseFloat(data[0].lat);
            var lon = parseFloat(data[0].lon);
            NerdDinner._map.setView([lat, lon], 14);
            NerdDinner.SetMarker(lat, lon);
        } else {
            alert("Không tìm thấy địa chỉ này!");
        }
    });
};
NerdDinner.UpdateHiddenFields = function (latitude, longitude) {
    $("#Latitude").val(latitude);
    $("#Longitude").val(longitude);
    $("#Location").val(latitude + "," + longitude);
};
