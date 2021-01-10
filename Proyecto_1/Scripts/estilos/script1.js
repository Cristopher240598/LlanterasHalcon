$(document).ready(function () {

    $(function () {
        $('#imagenLlanta').change(function (e) {
            addImage(e);
        });

        function addImage(e) {
            var file = e.target.files[0],
                imageType = /image.*/;

            if (!file.type.match(imageType))
                return;

            var reader = new FileReader();

            reader.onload = function (e) {
                var result = e.target.result;
                $('#imagenSalida').attr("src", result);
            }

            reader.readAsDataURL(file);
        }
    });
    $(function () {
        $('#imagenMarca').change(function (e) {
            addImage(e);
        });

        function addImage(e) {
            var file = e.target.files[0],
                imageType = /image.*/;

            if (!file.type.match(imageType))
                return;

            var reader = new FileReader();

            reader.onload = function (e) {
                var result = e.target.result;
                $('#imagenSalidam').attr("src", result);
            }

            reader.readAsDataURL(file);
        }
    });
});