using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GoruntuProje
{

    public partial class Form1 : Form
    {
        Pixel[,] originalMatrix;
        Bitmap orijinalResim;
        Bitmap islenmisResim;

        private Bitmap GetAktifGoruntu()
        {
            if (islenmisResim != null)
                return new Bitmap(islenmisResim);
            else if (orijinalResim != null)
                return new Bitmap(orijinalResim);
            else
                return null; 
        }

        public Form1()
        {
            InitializeComponent();
            txtGenislik.Text = "Genislik";
            txtYukseklik.Text = "Yükseklik";
            txtGenislik.ForeColor = Color.Gray;
            txtYukseklik.ForeColor = Color.Gray;


        }
        private void txtGenislik_Enter(object sender, EventArgs e)
        {
            if (txtGenislik.Text == "Genislik")
            {
                txtGenislik.Text = "";
                txtGenislik.ForeColor = Color.Black;
            }
        }

        private void txtGenislik_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGenislik.Text))
            {
                txtGenislik.Text = "Genislik";
                txtGenislik.ForeColor = Color.Gray;
            }
        }
        private void txtYukseklik_Enter(object sender, EventArgs e)
        {
            if (txtYukseklik.Text == "Yükseklik")
            {
                txtYukseklik.Text = "";
                txtYukseklik.ForeColor = Color.Black;
            }
        }

        private void txtYukseklik_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtYukseklik.Text))
            {
                txtYukseklik.Text = "Yükseklik";
                txtYukseklik.ForeColor = Color.Gray;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog opn = new OpenFileDialog();
            opn.Title = "Bir resim dosyası seçiniz";
            opn.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";

            if (opn.ShowDialog() == DialogResult.OK)
            {
                orijinalResim = new Bitmap(opn.FileName);
                islenmisResim = null;
                pictureBoxOrjinal.Image = orijinalResim;
                pictureBoxIslenmis.Image = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu();

            if (kaynak != null)
            {
                Bitmap sonuc = Algoritmalar.GriDonusum(kaynak);
                islenmisResim = sonuc;
                pictureBoxIslenmis.Image = sonuc;

                Console.WriteLine("Gri dönüşüm yapıldı.");
            }
            else
            {
                MessageBox.Show("Lütfen önce bir resim yükleyin.");
            }
        }

        private void btnSifirla_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu();
            kaynak = null;
            islenmisResim = null;
            pictureBoxIslenmis.Image = null;
            trackBarZoom.Value = 5;
            

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (pictureBoxIslenmis.Image != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Kaydet";
                sfd.Filter = "PNG Dosyası|*.png|JPEG Dosyası|*.jpg|Bitmap Dosyası|*.bmp";
                sfd.FileName = "islenmis_resim";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    pictureBoxIslenmis.Image.Save(sfd.FileName);
                    MessageBox.Show("Resim başarıyla kaydedildi.");
                }
            }
            else
            {
                MessageBox.Show("Kaydedilecek bir işlenmiş resim yok.");
            }
        }

        private void btnBinary_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu();

            if (kaynak != null)
            {
                Bitmap sonuc = Algoritmalar.BinaryDonusum(kaynak);
                islenmisResim = sonuc;
                pictureBoxIslenmis.Image = sonuc;
            }
            else {
                MessageBox.Show("Lütfen Bir Resim Seçiniz");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu();

            if (kaynak != null && comboBox1.SelectedItem != null)
            {
                string secilenAci = comboBox1.SelectedItem.ToString();

                float aci;
                if (secilenAci == "90")
                    aci = 90;
                else if (secilenAci == "180")
                    aci = 180;
                else
                {
                    MessageBox.Show("Sadece 90 veya 180 derece döndürme yapılabilir.");
                    return;
                }

                Bitmap sonuc =Algoritmalar.ResmiDondur(kaynak, aci, pictureBoxIslenmis.Width, pictureBoxIslenmis.Height);

                islenmisResim = sonuc;
                pictureBoxIslenmis.Image = sonuc;
                pictureBoxIslenmis.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                MessageBox.Show("Lütfen önce bir resim yükleyin ve açı seçin.");
            }
        }

        private void goruntuKirp_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu();

            if (kaynak != null)
            {
                int genislik, yukseklik;


                if (int.TryParse(txtGenislik.Text, out genislik) && int.TryParse(txtYukseklik.Text, out yukseklik))
                {
                    if (genislik > 0 && yukseklik > 0)
                    {
                        Bitmap sonuc = Algoritmalar.Kirp(kaynak, genislik, yukseklik);
                        islenmisResim = sonuc;
                        pictureBoxIslenmis.Image = sonuc;
                        pictureBoxIslenmis.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        MessageBox.Show("Genişlik ve yükseklik sıfırdan büyük olmalıdır.");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli sayılar girin.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen önce bir resim yükleyin.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu();
            if(kaynak == null) return;

            Bitmap bmp = new Bitmap(kaynak);
            Pixel[,] matrix = ConvertBitmapToPixelMatrix(bmp);

            Pixel[,] hsvMatrix = ConvertToHSV(matrix);

            islenmisResim = ConvertPixelMatrixToBitmap(hsvMatrix); ;
			pictureBoxIslenmis.Image = ConvertPixelMatrixToBitmap(hsvMatrix);

        }

        private Pixel[,] ConvertToHSV(Pixel[,] original)
        {
            int h = original.GetLength(0), w = original.GetLength(1); 
            Pixel[,] result = new Pixel[h, w]; 

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    Pixel p = original[y, x]; 
                    double r = p.R / 255.0;  
                    double g = p.G / 255.0;
                    double b = p.B / 255.0;

                    
                    double max = r > g ? (r > b ? r : b) : (g > b ? g : b);
                    double min = r < g ? (r < b ? r : b) : (g < b ? g : b);
                    double delta = max - min;

                    double hVal = 0, s = 0, v = max;

                    if (max != 0)
                        s = delta / max; 

                   
                    if (delta != 0)
                    {
                        if (max == r)
                            hVal = (g - b) / delta + (g < b ? 6 : 0);
                        else if (max == g)
                            hVal = (b - r) / delta + 2;
                        else
                            hVal = (r - g) / delta + 4;

                        hVal /= 6;
                    }

                   
                    s = s * 1.5;
                    if (s > 1) s = 1;

                    v = v * 1.5;
                    if (v > 1) v = 1;

                    
                    double r1 = 0, g1 = 0, b1 = 0;

                    if (s == 0)
                    {
                        r1 = g1 = b1 = v;
                    }
                    else
                    {
                        hVal *= 6; 
                        int i = (int)hVal; 
                        double f = hVal - i;
                        double p1 = v * (1 - s);
                        double q = v * (1 - s * f);
                        double t = v * (1 - s * (1 - f));

                       
                        switch (i % 6)
                        {
                            case 0: r1 = v; g1 = t; b1 = p1; break;
                            case 1: r1 = q; g1 = v; b1 = p1; break;
                            case 2: r1 = p1; g1 = v; b1 = t; break;
                            case 3: r1 = p1; g1 = q; b1 = v; break;
                            case 4: r1 = t; g1 = p1; b1 = v; break;
                            case 5: r1 = v; g1 = p1; b1 = q; break;
                        }
                    }

                   
                    result[y, x] = new Pixel(
                        (byte)ClampToByte(r1 * 255),
                        (byte)ClampToByte(g1 * 255),
                        (byte)ClampToByte(b1 * 255)
                    );
                }
            }
            return result; 
        }

        private Pixel[,] ConvertToCMYK(Pixel[,] matrix)
        {
            int h = matrix.GetLength(0);
            int w = matrix.GetLength(1);
            Pixel[,] result = new Pixel[h, w];

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    Pixel p = matrix[y, x];
                    float r = p.R / 255f;
                    float g = p.G / 255f;
                    float b = p.B / 255f;

                   
                    float k = 1f - Math.Max(r, Math.Max(g, b));
                    float c = (1f - r - k) / (1f - k + 0.0001f);
                    float m = (1f - g - k) / (1f - k + 0.0001f);
                    float yC = (1f - b - k) / (1f - k + 0.0001f);

                   
                    k += 0.2f;
                    if (k > 1f) k = 1f;

               
                    float R = 255 * (1 - c) * (1 - k);
                    float G = 255 * (1 - m) * (1 - k);
                    float B = 255 * (1 - yC) * (1 - k);

     
                    byte rByte = (byte)(R < 0 ? 0 : (R > 255 ? 255 : R));
                    byte gByte = (byte)(G < 0 ? 0 : (G > 255 ? 255 : G));
                    byte bByte = (byte)(B < 0 ? 0 : (B > 255 ? 255 : B));

                    result[y, x] = new Pixel(rByte, gByte, bByte);
                }
            }
            return result;
        }

        private int ClampToByte(double val)
        {
            if (val < 0) return 0;
            if (val > 255) return 255;
            return (int)val;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (pictureBoxOrjinal.Image == null) return;


            string[] aralik = textBox1.Text.Split(',');
            if (aralik.Length != 2 || !int.TryParse(aralik[0], out int minDeger) || !int.TryParse(aralik[1], out int maxDeger))
            {
                MessageBox.Show("Lütfen 'min,max' formatında geçerli sayılar giriniz. Örn: 50,200");
                return;
            }


            Bitmap giris = GetAktifGoruntu();
            Bitmap griResim = new Bitmap(giris.Width, giris.Height);
            Bitmap cikis = new Bitmap(giris.Width, giris.Height);

            int min = 255, max = 0;

           
            for (int y = 0; y < giris.Height; y++)
            {
                for (int x = 0; x < giris.Width; x++)
                {
                    Color renk = giris.GetPixel(x, y);
                    int gri = (renk.R + renk.G + renk.B) / 3;
                    griResim.SetPixel(x, y, Color.FromArgb(gri, gri, gri));

                    if (gri < min) min = gri;
                    if (gri > max) max = gri;
                }
            }

            for (int y = 0; y < griResim.Height; y++)
            {
                for (int x = 0; x < griResim.Width; x++)
                {
                    Color renk = griResim.GetPixel(x, y);
                    int gri = renk.R;

                    int yeniDeger = (gri - minDeger) * 255 / (maxDeger - minDeger);
                    yeniDeger = Math.Max(0, Math.Min(255, yeniDeger));

                    Color yeniRenk = Color.FromArgb(yeniDeger, yeniDeger, yeniDeger);
                    cikis.SetPixel(x, y, yeniRenk);
                }
            }
			islenmisResim = cikis;
			pictureBoxIslenmis.Image = cikis;
        }

        private void histGerme_Click(object sender, EventArgs e)
        {
            if (pictureBoxOrjinal.Image == null)
            {
                MessageBox.Show("Lütfen önce bir resim yükleyin.");
                return;
            }

           
            string[] aralik = textBox2.Text.Split('-');
            if (aralik.Length != 2 || !int.TryParse(aralik[0], out int a) || !int.TryParse(aralik[1], out int b))
            {
                MessageBox.Show("Lütfen 'min-max' formatında bir değer giriniz. Örn: 50-200");
                return;
            }

            
            a = Math.Max(0, Math.Min(255, a));
            b = Math.Max(0, Math.Min(255, b));
            if (a >= b)
            {
                MessageBox.Show("Min değer, max değerden küçük olmalıdır.");
                return;
            }

            Bitmap orjinalBitmap = GetAktifGoruntu();
            Bitmap gri = new Bitmap(orjinalBitmap.Width, orjinalBitmap.Height);

            
            for (int y = 0; y < orjinalBitmap.Height; y++)
            {
                for (int x = 0; x < orjinalBitmap.Width; x++)
                {
                    Color renk = orjinalBitmap.GetPixel(x, y);
                    int griDeger = (renk.R + renk.G + renk.B) / 3;
                    gri.SetPixel(x, y, Color.FromArgb(griDeger, griDeger, griDeger));
                }
            }

            
            int c = 255, d = 0;
            for (int y = 0; y < gri.Height; y++)
            {
                for (int x = 0; x < gri.Width; x++)
                {
                    int piksel = gri.GetPixel(x, y).R;
                    if (piksel < c) c = piksel;
                    if (piksel > d) d = piksel;
                }
            }

            if (d == c) d = c + 1;

           
            Bitmap sonuc = new Bitmap(gri.Width, gri.Height);
            for (int y = 0; y < gri.Height; y++)
            {
                for (int x = 0; x < gri.Width; x++)
                {
                    int Pgiris = gri.GetPixel(x, y).R;
                    int Pcikis = (Pgiris - c) * (b - a) / (d - c) + a;
                    Pcikis = Math.Max(0, Math.Min(255, Pcikis));
                    sonuc.SetPixel(x, y, Color.FromArgb(Pcikis, Pcikis, Pcikis));
                }
            }

			islenmisResim = sonuc;
			pictureBoxIslenmis.Image = sonuc;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (pictureBoxOrjinal.Image == null)
            {
                MessageBox.Show("Öncelikle bir ana resim açmalısınız!");
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            Bitmap img1 = new Bitmap(pictureBoxOrjinal.Image);
            Bitmap img2 = new Bitmap(ofd.FileName); 
            Bitmap resizedImg2 = new Bitmap(img2, img1.Width, img1.Height);

            Bitmap dst = new Bitmap(img1.Width, img1.Height);

            for (int y = 0; y < img1.Height; y++)
            {
                for (int x = 0; x < img1.Width; x++)
                {
                    Color c1 = img1.GetPixel(x, y);
                    Color c2 = resizedImg2.GetPixel(x, y);

                    int r = Math.Min(c1.R + c2.R, 255);
                    int g = Math.Min(c1.G + c2.G, 255);
                    int b = Math.Min(c1.B + c2.B, 255);

                    dst.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

			islenmisResim = dst;
			pictureBoxIslenmis.Image = dst;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (pictureBoxOrjinal.Image == null)
            {
                MessageBox.Show("Lütfen önce ana resmi yükleyin.");
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            Bitmap img1 = new Bitmap(pictureBoxOrjinal.Image);
            Bitmap img2 = new Bitmap(ofd.FileName);
            Bitmap resizedImg2 = new Bitmap(img2, img1.Width, img1.Height);

            Bitmap dst = new Bitmap(img1.Width, img1.Height);

            for (int y = 0; y < img1.Height; y++)
            {
                for (int x = 0; x < img1.Width; x++)
                {
                    Color c1 = img1.GetPixel(x, y);
                    Color c2 = resizedImg2.GetPixel(x, y);

                    int r = (c2.R == 0) ? 0 : Math.Min((int)((float)c1.R / c2.R * 255), 255);
                    int g = (c2.G == 0) ? 0 : Math.Min((int)((float)c1.G / c2.G * 255), 255);
                    int b = (c2.B == 0) ? 0 : Math.Min((int)((float)c1.B / c2.B * 255), 255);

                    dst.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

			islenmisResim = dst;
			pictureBoxIslenmis.Image = dst;
        }

        private void btnKontrast_Click(object sender, EventArgs e)
        {
            if (pictureBoxOrjinal.Image == null) return;

            if (!double.TryParse(textBox3.Text, out double faktor) || faktor < 1.0)
            {
                MessageBox.Show("Lütfen 1 veya daha büyük bir sayı giriniz. Örn: 1.2, 1.5, 2");
                return;
            }

            Bitmap kaynak = GetAktifGoruntu();
            int w = kaynak.Width;
            int h = kaynak.Height;
            Bitmap sonuc = new Bitmap(w, h);

            long toplam = 0;
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
                    Color renk = kaynak.GetPixel(x, y);
                    int gri = (renk.R + renk.G + renk.B) / 3;
                    toplam += gri;
                }

            double ort = toplam / (double)(w * h);

            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
                    Color renk = kaynak.GetPixel(x, y);
                    int gri = (renk.R + renk.G + renk.B) / 3;

                    int yeniDeger = (int)(faktor * (gri - ort) + ort);
                    yeniDeger = Math.Max(0, Math.Min(255, yeniDeger));

                    sonuc.SetPixel(x, y, Color.FromArgb(yeniDeger, yeniDeger, yeniDeger));
                }

			islenmisResim = sonuc;
			pictureBoxIslenmis.Image = sonuc;
        }

        private void btnMean_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu();
            if (kaynak == null) return;

            Bitmap sonuc = Algoritmalar.MeanFiltresiUygula(kaynak);
			islenmisResim = sonuc;
			pictureBoxIslenmis.Image = sonuc;
        }

        private void btnTekEsikle_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu();
            if (kaynak == null) return;

            int esik;
            if (!int.TryParse(textBoxEsik.Text, out esik) || esik < 0 || esik > 255)
            {
                MessageBox.Show("Lütfen 0-255 arasında geçerli bir eşik değeri girin.");
                return;
            }

            Bitmap sonuc = Algoritmalar.TekEsiklemeUygula(kaynak, esik);
			islenmisResim = sonuc;
			pictureBoxIslenmis.Image = sonuc;
        }

        private void btnPrewitt_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu();
            if (kaynak == null) return;

           
            Bitmap gri = Algoritmalar.GriDonusum(kaynak);

        
            Bitmap sonuc = Algoritmalar.PrewittKenarBul(gri);
			islenmisResim = sonuc;
			pictureBoxIslenmis.Image = sonuc;
        }

        private void btnSaltPepper_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu();
            if (kaynak == null) return;

            Bitmap sonuc = Algoritmalar.SaltPepperEkle(kaynak);
			islenmisResim = sonuc;
			pictureBoxIslenmis.Image = sonuc;
        }

        private void btnMedian_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu();
            if (kaynak == null) return;

            Bitmap sonuc = Algoritmalar.MedianFiltrele(kaynak);
			islenmisResim = sonuc;
			pictureBoxIslenmis.Image = sonuc;
        }

        private void btnUnsharp_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu();
            if (kaynak == null) return;

            Bitmap sonuc = Algoritmalar.BasitUnsharpMasking(kaynak);
			islenmisResim = sonuc;
			pictureBoxIslenmis.Image = sonuc;
        }

        private void btnGenisleme_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu(); 

            if (kaynak == null)
            {
                MessageBox.Show("Görüntü yüklenmedi.");
                return;
            }

           
            Bitmap binaryResim = Algoritmalar.BinaryDonusum(kaynak);

            Bitmap sonuc = Algoritmalar.Genisleme(binaryResim);
			islenmisResim = sonuc;
			pictureBoxIslenmis.Image = sonuc;
        }

        private void btnAsinma_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu(); 

            if (kaynak == null)
            {
                MessageBox.Show("Görüntü yüklenmedi.");
                return;
            }

            
            Bitmap binaryResim = Algoritmalar.BinaryDonusum(kaynak);

            
            Bitmap sonuc = Algoritmalar.Asinma(binaryResim); // Asinma metodu da Algoritmalar içinde
			islenmisResim = sonuc;
			pictureBoxIslenmis.Image = sonuc;
        }

        private void btnAcma_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu();
            if (kaynak == null) return;

            
            Bitmap binaryResim = Algoritmalar.BinaryDonusum(kaynak);

           
            Bitmap sonuc = Algoritmalar.Acma(binaryResim);

			islenmisResim = sonuc;
			pictureBoxIslenmis.Image = sonuc;
        }

        private void btnKapama_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu();
            if (kaynak == null) return;

            
            Bitmap binaryResim = Algoritmalar.BinaryDonusum(kaynak);

           
            Bitmap sonuc = Algoritmalar.Kapama(binaryResim);

			islenmisResim = sonuc;
			pictureBoxIslenmis.Image = sonuc;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
   
                
            
        }

        private void trackBarZoom_Scroll(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu();
            originalMatrix = ConvertBitmapToPixelMatrix(kaynak);
            if (originalMatrix == null) return;

            int value = trackBarZoom.Value;
            Pixel[,] zoomedMatrix;

            if (value == 5)
            {
               
                zoomedMatrix = originalMatrix;
            }
            else if (value < 5)
            {
               
                int scale = 6 - value; 
                zoomedMatrix = ZoomOut_Averaging(originalMatrix, scale);
            }
            else
            {
                
                int scale = value - 4; 
                zoomedMatrix = ZoomIn_NearestNeighbor(originalMatrix, scale);
            }

            Bitmap bmp = ConvertPixelMatrixToBitmap(zoomedMatrix);
            pictureBoxIslenmis.Image = bmp;
            pictureBoxIslenmis.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxIslenmis.Width = bmp.Width;
            pictureBoxIslenmis.Height = bmp.Height;
        }

        private Pixel[,] ZoomIn_NearestNeighbor(Pixel[,] original, int scale)
        {
            int originalHeight = original.GetLength(0);
            int originalWidth = original.GetLength(1);

            int newHeight = originalHeight * scale;
            int newWidth = originalWidth * scale;

            Pixel[,] zoomed = new Pixel[newHeight, newWidth];

            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                   
                    int nearestY = y / scale;
                    int nearestX = x / scale;

                    zoomed[y, x] = original[nearestY, nearestX];
                }
            }

            return zoomed;
        }

        private Pixel[,] ZoomOut_Averaging(Pixel[,] original, int scale)
        {
            int originalHeight = original.GetLength(0);  
            int originalWidth = original.GetLength(1);   

            int newHeight = originalHeight / scale;  
            int newWidth = originalWidth / scale;   

            Pixel[,] zoomed = new Pixel[newHeight, newWidth];  

            
            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    int rSum = 0, gSum = 0, bSum = 0;

                   
                    for (int dy = 0; dy < scale; dy++)
                    {
                        for (int dx = 0; dx < scale; dx++)
                        {
                            Pixel p = original[y * scale + dy, x * scale + dx];  
                            rSum += p.R;  
                            gSum += p.G;  
                            bSum += p.B;  
                        }
                    }
                    int count = scale * scale;  
                    zoomed[y, x] = new Pixel((byte)(rSum / count), (byte)(gSum / count), (byte)(bSum / count));  // Ortalama rengi yeni pikselle yerleştir
                }
            }
            return zoomed;  
        }

        private Pixel[,] ConvertBitmapToPixelMatrix(Bitmap image)
        {
            int h = image.Height, w = image.Width;

            var matrix = new Pixel[h, w];

            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
         
                    var c = image.GetPixel(x, y);
                    matrix[y, x] = new Pixel(c.R, c.G, c.B);
                }
            return matrix;
        }

        private Bitmap ConvertPixelMatrixToBitmap(Pixel[,] matrix)
        {
            int h = matrix.GetLength(0), w = matrix.GetLength(1);
            var bmp = new Bitmap(w, h);

            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
                   
                    var p = matrix[y, x];
                    bmp.SetPixel(x, y, Color.FromArgb(p.R, p.G, p.B));
                }
            return bmp;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Bitmap kaynak = GetAktifGoruntu();
            if (kaynak == null) return;

            Bitmap bmp = new Bitmap(kaynak);
            Pixel[,] matrix = ConvertBitmapToPixelMatrix(bmp);

            Pixel[,] yuvMatrix = ConvertToCMYK(matrix);
			islenmisResim = ConvertPixelMatrixToBitmap(yuvMatrix);
			pictureBoxIslenmis.Image = ConvertPixelMatrixToBitmap(yuvMatrix);
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

