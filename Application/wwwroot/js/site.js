// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
$(".checkbox-menu").on("change", "input[type='checkbox']", function () {
    $(this).closest("li").toggleClass("active", this.checked);
});

$(document).on('click', '.allow-focus', function (e) {
    e.stopPropagation();
});

function getSrc(){
    const src = document.getElementById('avatar-input');
    const img = document.getElementById('avatar-img');
    img.src = src.value;
}

function setImgSrcDefault() {
    console.log("setImgSrcDefault()");
    const img = document.getElementById('avatar-img');
    img.src = 'https://stihi.ru/pics/2018/02/08/11668.jpg';
}

document.getElementById('avatar-input').onkeyup = getSrc;