let currentIndex = 0; // Mevcut resim indeksi
const serviceSlider = document.querySelector('.service-slider');
const serviceItems = document.querySelectorAll('.service-info'); // Servis elemanlarını seç
const totalImages = serviceItems.length; // Dinamik olarak eleman sayısını al
const slideInterval = 3000; // Kaydırma aralığı (3 saniye)

// Resimleri kaydırma fonksiyonu
function slideImages() {
    currentIndex = (currentIndex + 1) % totalImages; // İndeksi artır
    const offset = -currentIndex * 100; // Kaydırma miktarını hesapla (vw olarak)
    serviceSlider.style.transform = `translateX(${offset}vw)`; // Resmi kaydır
}

// Belirli bir süre aralığında kaydırmayı başlat
setInterval(slideImages, slideInterval);
