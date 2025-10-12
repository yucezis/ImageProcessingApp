# Image Processing Application | Görüntü İşleme Uygulaması

## Project Description | Proje Açıklaması

**TR:**
Bu proje, kullanıcıların çeşitli görüntü işleme algoritmalarını (gri tonlama, histogram germe, kenar bulma, filtreleme vb.) kolayca uygulayabileceği bir **Windows Forms tabanlı C# görüntü işleme uygulamasıdır**.  
Proje, temel görüntü dönüşümleri ve filtreleme tekniklerini öğrenmek isteyen **bilgisayar mühendisliği öğrencileri** için geliştirilmiştir.

**EN:**
This project is a **C# Windows Forms-based image processing application** that allows users to easily apply various image processing algorithms *(grayscale conversion, histogram stretching, edge detection, filtering, etc.)*.  
It was developed for **computer engineering students** who want to learn the fundamentals of image transformations and filtering techniques.

---

## Features | Özellikler

- Grayscale Conversion *(Gri Tonlama)*
- Binary Transformation *(İkili Dönüşüm)*
- Rotate by 90° or 180° *(Görüntü Döndürme)*
- Cropping *(Kırpma)*
- Color Space Conversion to HSV and CMYK *(Renk Uzayına Dönüştürme)*
- Histogram Stretching *(Histogram Germe)*
- Arithmetic Operations *(Toplama, Bölme)*
- Contrast Adjustment *(Kontrast Ayarı)*
- Edge Detection (Prewitt)
- Filtering: Mean, Median, Unsharp Masking
- Noise Addition *(Salt and Pepper Noise)*
- Morphological Operations: Dilation, Erosion

---

## Implemented Algorithms | Uygulanan Algoritmalar

- **Grayscale:** Converts image using the average of R, G, B values.  
- **Binary Thresholding:** Converts image to black and white based on pixel threshold.  
- **Histogram Stretching:** Rescales pixel intensity values.  
- **Prewitt Edge Detection:** Detects edges based on gradient direction.  
- **Mean & Median Filtering:** Reduces noise using smoothing filters.  
- **Morphological Operations:** Includes *dilation* and *erosion* for shape processing.

---

## Technologies Used | Kullanılan Teknolojiler

- C# (.NET Framework)  
- Windows Forms  
- `System.Drawing` library *(used for image manipulation)*

---

## Usage | Kullanım

**TR:**
1. “Resim Yükle” butonuna tıklayarak bir görsel seçin.  
2. İstediğiniz işlemi (örneğin “Gri Dönüşüm”) seçin.  
3. İşlenmiş görüntü sağdaki alanda görüntülenecektir.  
4. “Kaydet” butonuyla sonucu kaydedebilirsiniz.

**EN:**
1. Click **“Load Image”** to select a picture.  
2. Choose an operation (e.g., **“Grayscale Conversion”**).  
3. The processed image will appear in the right panel.  
4. Click **“Save”** to store the result.
