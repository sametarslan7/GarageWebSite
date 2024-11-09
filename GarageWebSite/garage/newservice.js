document.addEventListener("DOMContentLoaded", function () {
    const slider = document.querySelector(".service-slider");
    const serviceCount = document.querySelectorAll(".service-info").length;

    // Slider genişliğini her service-info için 100vw olacak şekilde ayarla
    slider.style.width = `${serviceCount * 100}vw`;
});
