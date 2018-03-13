var la = document.getElementById("Latitude");
var lo = document.getElementById("Longitude");

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    }

}
function showPosition(position) {
    document.getElementById("Latitude").value = position.coords.latitude;
    document.getElementById("Longitude").value = position.coords.longitude;
}