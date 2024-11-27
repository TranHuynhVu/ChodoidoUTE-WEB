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
