Proje Açıklaması | Project Description
TR:
Bu proje, kullanıcıların çeşitli görüntü işleme algoritmalarını (gri tonlama, histogram germe, kenar bulma, filtreleme vb.) kolayca uygulayabileceği bir Windows Forms tabanlı C# görüntü işleme uygulamasıdır.
Proje, temel görüntü dönüşümleri ve filtreleme tekniklerini öğrenmek isteyen bilgisayar mühendisliği öğrencileri için geliştirilmiştir.

EN:
This project is a C# Windows Forms-based image processing application that allows users to easily apply various image processing algorithms (grayscale conversion, histogram stretching, edge detection, filtering, etc.).
It was developed for computer engineering students who want to learn the fundamentals of image transformations and filtering techniques.

Özellikler | Features 
Gri Tonlama (Grayscale Conversion)
Binary Dönüşüm (Binary Transformation)
Görüntü Döndürme (Rotate by 90° or 180°)
Görüntü Kırpma (Cropping)
HSV ve CMYK Renk Uzayına Dönüştürme (Color Space Conversion)
Histogram Germe (Histogram Stretching)
Aritmetik İşlemler (Image Addition, Division)
Kontrast Ayarlama (Contrast Adjustment)
Kenar Bulma (Prewitt Edge Detection)
Filtreleme: Mean, Median, Unsharp Masking
Gürültü Ekleme (Salt and Pepper Noise)
Morfolojik İşlemler: Genişleme (Dilation), Aşınma (Erosion)

Kullanılan Teknolojiler | Technologies Used
C# (.NET Framework)
Windows Forms App
System.Drawing kütüphanesi (for image manipulation)

Kullanım | Usage
TR:
“Resim Yükle” butonuna tıklayarak bir görsel seçin.
İstediğiniz işlemi (örneğin “Gri Dönüşüm”) seçin.
İşlenmiş görüntü sağdaki alanda görüntülenecektir.
“Kaydet” butonuyla sonucu kaydedebilirsiniz.

EN:
Click “Load Image” to select a picture.
Choose an operation (e.g., “Grayscale Conversion”).
The processed image will appear in the right panel.
Save the result with the “Save” button.

Uygulanan Algoritmalar | Implemented Algorithms
GrayScale: R, G, B ortalaması alınarak gri tonlama.
Binary Thresholding: Piksel değerine göre siyah-beyaz dönüşüm.
Histogram Stretching: Piksel aralığını yeniden ölçekleme.
Prewitt Edge Detection: Kenarların yön tabanlı bulunması.
Mean & Median Filtering: Gürültü azaltma filtreleri.
Morphological Operations: Genişleme (dilation) ve aşınma (erosion).
