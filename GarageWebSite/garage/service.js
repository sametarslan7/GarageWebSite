let currentIndex = 0; // Mevcut resim indeksi
const totalImages = 3; // Toplam resim sayısı
const slideInterval = 3000; // Kaydırma aralığı (2 saniye)
const serviceSlider = document.querySelector('.service-slider');

// Resimleri kaydırma fonksiyonu
function slideImages() {
    currentIndex = (currentIndex + 1) % totalImages; // İndeksi artır
    const offset = -currentIndex * 33.5; // Kaydırma miktarını hesapla (yüzde)
    serviceSlider.style.transform = `translateX(${offset}%)`; // Resmi kaydır
}

// Belirli bir süre aralığında kaydırmayı başlat
setInterval(slideImages, slideInterval);

