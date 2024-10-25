let currentImageIndex = 0; // Mevcut resim indeksi
let images = []; // Resim dizisi

function openOverlay(title, imageSrc, details, price, otherImages) {
    document.getElementById('overlayTitle').innerText = title;
    images = otherImages; // Diğer resimleri al
    currentImageIndex = 0; // İndeksi sıfırla
    updateImage(imageSrc); // İlk resmi güncelle
    document.getElementById('overlayDetails').innerHTML = details;
    document.getElementById('overlayPrice').innerText = price;

    document.getElementById('carOverlay').style.display = 'flex'; // Overlay'i göster
}

function updateImage(imageSrc) {
    document.getElementById('overlayImage').src = images[currentImageIndex] || imageSrc; // Mevcut resmi güncelle
}

function changeImage(direction) {
    currentImageIndex += direction; // İndeksi değiştir
    if (currentImageIndex < 0) {
        currentImageIndex = images.length - 1; // En son resme dön
    } else if (currentImageIndex >= images.length) {
        currentImageIndex = 0; // İlk resme dön
    }
    updateImage(); // Resmi güncelle
}

function closeOverlay() {
    document.getElementById('carOverlay').style.display = 'none'; // Overlay'i gizle
}

// Dışarıya tıklanıldığında overlay'i kapat
document.getElementById('carOverlay').addEventListener('click', function (event) {
    if (event.target === this) { // Eğer dışarıya tıkladıysanız
        closeOverlay();
    }
});
