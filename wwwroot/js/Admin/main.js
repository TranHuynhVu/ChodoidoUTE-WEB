$(document).ready(function () {
    const $userAdmin = $('[userAdminAvatar]');
    const $userAdminLi = $('[userAdminLi]');
    if ($userAdmin) {
        $userAdmin.on('click', function (e) {
            e.preventDefault();
            const $showLiItem = $userAdminLi.find('[userAdminDropdown]');
            $showLiItem.toggleClass('show');
        });
    }
});



const uploadImage = document.querySelector('[upload-image]');
if (uploadImage) {
    console.log(uploadImage);
    const inputImage = uploadImage.querySelector('[upload-image-input]');
    const previewImage = uploadImage.querySelector('[upload-image-preview]');
    if (inputImage && previewImage) {
        inputImage.addEventListener('change', (e) => {
            const [file] = inputImage.files;
            if (file) {
                previewImage.src = URL.createObjectURL(file);
                previewImage.style.width = "350px";
                previewImage.style.height = "350px";
            }
        })
    }
}