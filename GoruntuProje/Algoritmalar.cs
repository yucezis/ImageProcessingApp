using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace GoruntuProje
{
    public class Algoritmalar
    {
        public static Bitmap GriDonusum(Bitmap kaynak)
        {
            Bitmap griResim = new Bitmap(kaynak.Width, kaynak.Height);

            for (int y = 0; y < kaynak.Height; y++)
            {
                for (int x = 0; x < kaynak.Width; x++)
                {
                    Color piksel = kaynak.GetPixel(x, y);
                    int griDeger = (piksel.R + piksel.G + piksel.B) / 3;
                    Color gri = Color.FromArgb(griDeger, griDeger, griDeger);
                    griResim.SetPixel(x, y, gri);
                }
            }

            return griResim;
        }
        public static Bitmap BinaryDonusum(Bitmap kaynak)
        {
            Bitmap sonuc = new Bitmap(kaynak.Width, kaynak.Height);
            int esik = 128;

            for (int y = 0; y < kaynak.Height; y++)
            {
                for (int x = 0; x < kaynak.Width; x++)
                {
                    Color piksel = kaynak.GetPixel(x, y);
                    int griDeger = (piksel.R + piksel.G + piksel.B) / 3;

                    if (griDeger >= esik)
                        sonuc.SetPixel(x, y, Color.White);
                    else
                        sonuc.SetPixel(x, y, Color.Black);
                }
            }

            return sonuc;
        }

		public static Bitmap ResmiDondur(Bitmap kaynak, float aci, int genislik, int yukseklik)
		{
			int kaynakGenislik = kaynak.Width;
			int kaynakYukseklik = kaynak.Height;
			Bitmap donmusResim;

			if (aci == 90)
			{
				donmusResim = new Bitmap(kaynakYukseklik, kaynakGenislik);

				for (int y = 0; y < kaynakYukseklik; y++)
				{
					for (int x = 0; x < kaynakGenislik; x++)
					{
						Color renk = kaynak.GetPixel(x, y);
						donmusResim.SetPixel(kaynakYukseklik - 1 - y, x, renk);
					}
				}
			}
			else if (aci == 180)
			{
				donmusResim = new Bitmap(kaynakGenislik, kaynakYukseklik);

				for (int y = 0; y < kaynakYukseklik; y++)
				{
					for (int x = 0; x < kaynakGenislik; x++)
					{
						Color renk = kaynak.GetPixel(x, y);
						donmusResim.SetPixel(kaynakGenislik - 1 - x, kaynakYukseklik - 1 - y, renk);
					}
				}
			}
			else
			{
				MessageBox.Show("Sadece 90 veya 180 derece desteklenmektedir.");
				return kaynak;
			}

			return donmusResim;
		}

		public static Bitmap Kirp(Bitmap kaynak, int genislik, int yukseklik)
        {

            if (genislik > kaynak.Width)
                genislik = kaynak.Width;
            if (yukseklik > kaynak.Height)
                yukseklik = kaynak.Height;

            Bitmap kirpilmis = new Bitmap(genislik, yukseklik);

            for (int y = 0; y < yukseklik; y++)
            {
                for (int x = 0; x < genislik; x++)
                {
                    Color renk = kaynak.GetPixel(x, y);
                    kirpilmis.SetPixel(x, y, renk);
                }
            }

            return kirpilmis;
        }
        public static Bitmap MeanFiltresiUygula(Bitmap kaynak)
        {
            int genislik = kaynak.Width;
            int yukseklik = kaynak.Height;
            Bitmap sonuc = new Bitmap(genislik, yukseklik);

            for (int y = 1; y < yukseklik - 1; y++)
            {
                for (int x = 1; x < genislik - 1; x++)
                {
                    int toplamR = 0, toplamG = 0, toplamB = 0;

                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            Color renk = kaynak.GetPixel(x + kx, y + ky);
                            toplamR += renk.R;
                            toplamG += renk.G;
                            toplamB += renk.B;
                        }
                    }

                    int ortR = toplamR / 9;
                    int ortG = toplamG / 9;
                    int ortB = toplamB / 9;

                    sonuc.SetPixel(x, y, Color.FromArgb(ortR, ortG, ortB));
                }
            }

            return sonuc;
        }
        public static Bitmap TekEsiklemeUygula(Bitmap kaynak, int esik)
        {
            int genislik = kaynak.Width;
            int yukseklik = kaynak.Height;
            Bitmap sonuc = new Bitmap(genislik, yukseklik);

            for (int y = 0; y < yukseklik; y++)
            {
                for (int x = 0; x < genislik; x++)
                {
                    Color renk = kaynak.GetPixel(x, y);
                    int gri = (renk.R + renk.G + renk.B) / 3;

                    int yeniDeger = (gri >= esik) ? 255 : 0;

                    sonuc.SetPixel(x, y, Color.FromArgb(yeniDeger, yeniDeger, yeniDeger));
                }
            }

            return sonuc;
        }

        public static Bitmap PrewittKenarBul(Bitmap griGoruntu)
        {
            int genislik = griGoruntu.Width;
            int yukseklik = griGoruntu.Height;
            Bitmap sonuc = new Bitmap(genislik, yukseklik);

            for (int y = 1; y < yukseklik - 1; y++)
            {
                for (int x = 1; x < genislik - 1; x++)
                {
                    int gx = 0, gy = 0;

                    // X yönü
                    gx += -1 * griGoruntu.GetPixel(x - 1, y - 1).R;
                    gx += 0 * griGoruntu.GetPixel(x, y - 1).R;
                    gx += 1 * griGoruntu.GetPixel(x + 1, y - 1).R;
                    gx += -1 * griGoruntu.GetPixel(x - 1, y).R;
                    gx += 0 * griGoruntu.GetPixel(x, y).R;
                    gx += 1 * griGoruntu.GetPixel(x + 1, y).R;
                    gx += -1 * griGoruntu.GetPixel(x - 1, y + 1).R;
                    gx += 0 * griGoruntu.GetPixel(x, y + 1).R;
                    gx += 1 * griGoruntu.GetPixel(x + 1, y + 1).R;

                    // Y yönü
                    gy += 1 * griGoruntu.GetPixel(x - 1, y - 1).R;
                    gy += 1 * griGoruntu.GetPixel(x, y - 1).R;
                    gy += 1 * griGoruntu.GetPixel(x + 1, y - 1).R;
                    gy += 0 * griGoruntu.GetPixel(x - 1, y).R;
                    gy += 0 * griGoruntu.GetPixel(x, y).R;
                    gy += 0 * griGoruntu.GetPixel(x + 1, y).R;
                    gy += -1 * griGoruntu.GetPixel(x - 1, y + 1).R;
                    gy += -1 * griGoruntu.GetPixel(x, y + 1).R;
                    gy += -1 * griGoruntu.GetPixel(x + 1, y + 1).R;

                    int kenar = (int)Math.Sqrt(gx * gx + gy * gy);
                    kenar = Math.Min(255, Math.Max(0, kenar));

                    sonuc.SetPixel(x, y, Color.FromArgb(kenar, kenar, kenar));
                }
            }

            return sonuc;
        }


        public static Bitmap SaltPepperEkle(Bitmap kaynak)
        {
            Random rnd = new Random();
            Bitmap sonuc = new Bitmap(kaynak.Width, kaynak.Height);

            double oran = 0.05; 

            for (int y = 0; y < kaynak.Height; y++)
            {
                for (int x = 0; x < kaynak.Width; x++)
                {
                    if (rnd.NextDouble() < oran)
                    {
                        
                        int gürültü = rnd.Next(2) == 0 ? 0 : 255;
                        sonuc.SetPixel(x, y, Color.FromArgb(gürültü, gürültü, gürültü));
                    }
                    else
                    {
                        Color renk = kaynak.GetPixel(x, y);
                        sonuc.SetPixel(x, y, renk);
                    }
                }
            }

            return sonuc;
        }

        public static Bitmap MedianFiltrele(Bitmap kaynak)
        {
            int genislik = kaynak.Width;
            int yukseklik = kaynak.Height;
            Bitmap sonuc = new Bitmap(genislik, yukseklik);

            int kernelBoyutu = 3; 
            int offset = kernelBoyutu / 2;

            for (int y = offset; y < yukseklik - offset; y++)
            {
                for (int x = offset; x < genislik - offset; x++)
                {
                    List<int> redValues = new List<int>();
                    List<int> greenValues = new List<int>();
                    List<int> blueValues = new List<int>();


                    for (int ky = -offset; ky <= offset; ky++)
                    {
                        for (int kx = -offset; kx <= offset; kx++)
                        {
                            Color renk = kaynak.GetPixel(x + kx, y + ky);
                            redValues.Add(renk.R);
                            greenValues.Add(renk.G);
                            blueValues.Add(renk.B);
                        }
                    }

                    
                    redValues.Sort();
                    greenValues.Sort();
                    blueValues.Sort();

                    int medianR = redValues[redValues.Count / 2];
                    int medianG = greenValues[greenValues.Count / 2];
                    int medianB = blueValues[blueValues.Count / 2];

                    sonuc.SetPixel(x, y, Color.FromArgb(medianR, medianG, medianB));
                }
            }

            return sonuc;
        }

        public static Bitmap BasitUnsharpMasking(Bitmap kaynak)
        {
            Bitmap gri = GriDonusum(kaynak);

            Bitmap bulanik = MeanFiltresiUygula(gri);
            Bitmap sonuc = new Bitmap(kaynak.Width, kaynak.Height);
            for (int y = 0; y < kaynak.Height; y++)
            {
                for (int x = 0; x < kaynak.Width; x++)
                {
                    Color orijinalRenk = kaynak.GetPixel(x, y);
                    Color bulanikRenk = bulanik.GetPixel(x, y);


                    int farkR = orijinalRenk.R - bulanikRenk.R;
                    int farkG = orijinalRenk.G - bulanikRenk.G;
                    int farkB = orijinalRenk.B - bulanikRenk.B;

 
                    int yeniR = Math.Min(255, Math.Max(0, orijinalRenk.R + farkR));
                    int yeniG = Math.Min(255, Math.Max(0, orijinalRenk.G + farkG));
                    int yeniB = Math.Min(255, Math.Max(0, orijinalRenk.B + farkB));

                    sonuc.SetPixel(x, y, Color.FromArgb(yeniR, yeniG, yeniB));
                }
            }

            return sonuc;
        }
        public static Bitmap Genisleme(Bitmap kaynak)
        {
            int genislik = kaynak.Width;
            int yukseklik = kaynak.Height;
            Bitmap sonuc = new Bitmap(genislik, yukseklik);

            int kernelBoyutu = 3;
            int offset = kernelBoyutu / 2;

            for (int y = offset; y < yukseklik - offset; y++)
            {
                for (int x = offset; x < genislik - offset; x++)
                {
                    bool siyahVarmi = false;

                    for (int ky = -offset; ky <= offset; ky++)
                    {
                        for (int kx = -offset; kx <= offset; kx++)
                        {
                            Color renk = kaynak.GetPixel(x + kx, y + ky);
                            if (renk.R == 0)
                            {
                                siyahVarmi = true;
                                break;
                            }
                        }
                        if (siyahVarmi) break;
                    }

                    if (siyahVarmi)
                        sonuc.SetPixel(x, y, Color.Black);
                    else
                        sonuc.SetPixel(x, y, Color.White);
                }
            }

            return sonuc;
        }


        public static Bitmap Asinma(Bitmap kaynak)
        {
            int genislik = kaynak.Width;
            int yukseklik = kaynak.Height;
            Bitmap sonuc = new Bitmap(genislik, yukseklik);

            int kernelBoyutu = 3;
            int offset = kernelBoyutu / 2;

            for (int y = offset; y < yukseklik - offset; y++)
            {
                for (int x = offset; x < genislik - offset; x++)
                {
                    bool tamamenBeyaz = true;

                    for (int ky = -offset; ky <= offset; ky++)
                    {
                        for (int kx = -offset; kx <= offset; kx++)
                        {
                            Color renk = kaynak.GetPixel(x + kx, y + ky);
                            if (renk.R != 255)
                            {
                                tamamenBeyaz = false;
                                break;
                            }
                        }
                        if (!tamamenBeyaz) break;
                    }

                    if (tamamenBeyaz)
                        sonuc.SetPixel(x, y, Color.White);
                    else
                        sonuc.SetPixel(x, y, Color.Black);
                }
            }

            return sonuc;
        }

        public static Bitmap Acma(Bitmap kaynak)
        {
            return Genisleme(Asinma(kaynak));
        }

        public static Bitmap Kapama(Bitmap kaynak)
        {
            return Asinma(Genisleme(kaynak));
        }

    }
}