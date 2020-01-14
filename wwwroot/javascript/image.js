
var myFunction = function (errId, objId, objName, objType) {
    var imageOverlay = document.getElementById("imageOverlay");
    imageOverlay.src = "../../../../uploads/" + objName;
    var imageDeleteUrl = document.getElementById("imageDeleteUrl");
    imageDeleteUrl.href = "/Admin/Errand/Delete" + objType + "?errId=" + errId + "&objId=" + objId + "&objName=" + objName;
    var imageDesc = document.getElementById("imageDesc");
    imageDesc.innerHTML = objName;
    var modal = document.getElementById('myModal');
    modal.style.display = "block";
    var span = document.getElementsByClassName("close")[0];
    span.onclick = function () {
        modal.style.display = "none";
    };
    window.onclick = function (event) {
        if (event.target == imageOverlay) {
            modal.style.display = "none";
        }
    };
};