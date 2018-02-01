using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;

namespace WindowsFormsApplication1
{
    class Igrac
    {
        static List<Point> kontejnerZaTrenutnuVezu = new List<Point>();
        static Dictionary<Point, List<Point>> recnikSlika = new Dictionary<Point, List<Point>>();

        //Najlevlja kombinacija
        public static List<Point> najLevljaLista = new List<Point>();
        public static int najLevljaKoordinata = 480;
        public static Boolean daLiSeTraziNajLevlji = false;

        static Boolean gotovoJeCiscenje = true;
        static Boolean gotovoSpustanje = true;

        public static System.Timers.Timer bTimer;

        public static Boolean DOZVOLJENO = true;
        public static List<Point> komboLista = new List<Point>();

        public static Boolean daLiSeBrise = true;

        //public static System.Timers.Timer aTimer;
        public static int BROJ = 4;

        public static Dictionary<Point, PictureBox> radniPaneli = new Dictionary<Point, PictureBox>();
        public static Dictionary<Point, PictureBox> rezultantniPaneli = new Dictionary<Point, PictureBox>();
        public static Dictionary<Point, PictureBox> paneliOdSledeceFigure = new Dictionary<Point, PictureBox>();

        public static Point trenutnoPrvo = new Point(0, 0);
        public static Point trenutnoDrugo = new Point(0, 32);
        public static Point trenutnoTrece = new Point(32, 0);
        public static Point trenutnoCetvrto = new Point(32, 32);

        static Point prva = new Point(0, 0);
        static Point druga = new Point(0, 32);
        static Point treca = new Point(32, 0);
        static Point cetvrta = new Point(32, 32);

        static Random rnd = new Random();

        public static Form1.Figura trenutnaFigura = null;
        public static Form1.Figura sledecaFigura = null;
        //
        public static Image BELA = new Bitmap(@"..\..\Ikonice\orange.bmp");
        public static Image CRVENA = new Bitmap(@"..\..\Ikonice\red.bmp");
        public static Image ZUTA = new Bitmap(@"..\..\Ikonice\yellow.bmp");
        public static Image PLAVA = new Bitmap(@"..\..\Ikonice\blue.bmp");
        public static Image SIVA = new Bitmap(@"..\..\Ikonice\black.bmp");
        public static Image ZELENA = new Bitmap(@"..\..\Ikonice\green.bmp");
        public static Image KOMBO = new Bitmap(@"..\..\Ikonice\Smooth.bmp");

        public static Boolean STANI = false;

        //static List<Point> slobodniPointovi { get; set; }

        public Igrac()
        {
            BELA.Tag = 1;
            CRVENA.Tag = 2;
            ZUTA.Tag = 3;
            PLAVA.Tag = 4;
            SIVA.Tag = 5;
            ZELENA.Tag = 6;
            KOMBO.Tag = 7;
            bTimer = new System.Timers.Timer(70);
            bTimer.Stop();
        }

        public static void napraviTrenutnuFiguru(Form1.Figura f)
        {
            refreshujRadnePanele();
            radniPaneli[trenutnoPrvo].Image = f.prvaSlika;
            radniPaneli[trenutnoDrugo].Image = f.drugaSlika;
            radniPaneli[trenutnoTrece].Image = f.trecaSlika;
            radniPaneli[trenutnoCetvrto].Image = f.cetvrtaSlika;
        }

        public static void napraviPocetnuFiguru()
        {
            Form1.Figura f = napraviFiguru();
            trenutnaFigura = f;
            napraviTrenutnuFiguru(f);
        }

        public static void napraviSledecuFiguru()
        {
            Form1.Figura f = napraviFiguru();
            sledecaFigura = f;
            Object prvi = f.prvaSlika.Clone();
            paneliOdSledeceFigure[prva].Image = new Bitmap(f.prvaSlika);
            paneliOdSledeceFigure[druga].Image = new Bitmap(f.drugaSlika);
            paneliOdSledeceFigure[treca].Image = new Bitmap(f.trecaSlika);
            paneliOdSledeceFigure[cetvrta].Image = new Bitmap(f.cetvrtaSlika);

        }

        public static Form1.Figura napraviFiguru()
        {
            Dictionary<int, Image> figura = new Dictionary<int, Image>();
            //Random rnd = new Random();
            int x = 1;
            for (int i = 1; i < 5; i++)
            {
                x = rnd.Next(1, 7);
                figura.Add(i, generisiSliku(x));
            }
            Form1.Figura f = new Form1.Figura(figura[1], figura[2], figura[3], figura[4]);
            return f;
        }

        public static Image generisiSliku(int slika)
        {
            Image i = null;
            if (slika == 1)
            {
                i = BELA;
            }
            else if (slika == 2)
            {
                i = CRVENA;
            }
            else if (slika == 3)
            {
                i = ZUTA;
            }
            else if (slika == 4)
            {
                i = PLAVA;
            }
            else if (slika == 5)
            {
                i = SIVA;
            }
            else if (slika == 6)
            {
                i = ZELENA;
            }
            return i;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        /*
        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (DOZVOLJENO == true)
            {
                if (e.KeyCode == Keys.Right)
                {
                    pomeriDesno();
                }
                else if (e.KeyCode == Keys.Left)
                {
                    pomeriLevo();
                }
                else if (e.KeyCode == Keys.Up)
                {
                    rotirajKaGore();
                }
                else if (e.KeyCode == Keys.Down)
                {
                    rotirajKaDole();
                }
            }
        }
        */
        public static void rotirajKaGore()
        {
            refreshujRadnePanele();
            /*
            Point drugo = trenutnoPrvo;
            Point cetvrto = trenutnoDrugo;
            Point trece = trenutnoCetvrto;
            Point prvo = trenutnoTrece;
            trenutnoPrvo = prvo;
            trenutnoDrugo = drugo;
            trenutnoTrece = trece;
            trenutnoCetvrto = cetvrto;
             */
            trenutnaFigura.rotirajGore();
            napraviTrenutnuFiguru(trenutnaFigura);
        }

        public static void rotirajKaDole()
        {
            refreshujRadnePanele();
            /*
            Point trece = trenutnoPrvo;
            Point cetvrto = trenutnoTrece;
            Point drugo = trenutnoCetvrto;
            Point prvo = trenutnoDrugo;
            trenutnoPrvo = prvo;
            trenutnoDrugo = drugo;
            trenutnoTrece = trece;
            trenutnoCetvrto = cetvrto;
            */
            trenutnaFigura.rotirajDole();
            napraviTrenutnuFiguru(trenutnaFigura);
        }

        public static void pomeriDesno()
        {
            if (trenutnoPrvo.X != 448)
            {
                refreshujRadnePanele();
                trenutnoPrvo = new Point(trenutnoPrvo.X + 32, trenutnoPrvo.Y);
                trenutnoDrugo = new Point(trenutnoDrugo.X + 32, trenutnoDrugo.Y);
                trenutnoTrece = new Point(trenutnoTrece.X + 32, trenutnoTrece.Y);
                trenutnoCetvrto = new Point(trenutnoCetvrto.X + 32, trenutnoCetvrto.Y);
                napraviTrenutnuFiguru(trenutnaFigura);
            }
        }

        public static void pomeriLevo()
        {
            if (trenutnoPrvo.X != 0)
            {
                refreshujRadnePanele();
                trenutnoPrvo = new Point(trenutnoPrvo.X - 32, trenutnoPrvo.Y);
                trenutnoDrugo = new Point(trenutnoDrugo.X - 32, trenutnoDrugo.Y);
                trenutnoTrece = new Point(trenutnoTrece.X - 32, trenutnoTrece.Y);
                trenutnoCetvrto = new Point(trenutnoCetvrto.X - 32, trenutnoCetvrto.Y);
                napraviTrenutnuFiguru(trenutnaFigura);
            }
        }

        public static void refreshujRadnePanele()
        {
            foreach (KeyValuePair<Point, PictureBox> par in radniPaneli)
            {
                par.Value.Image = null;
            }
        }
        /*
        public void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DOZVOLJENO == true)
            {
                if (e.KeyChar == ' ')
                {
                    spustiFiguru();
                }
            }
        }
        */
        public static void spustiFiguru()
        {
            DOZVOLJENO = false;
            refreshujRadnePanele();
            BROJ = 3;
            setTimer();
            //drugaVarijanta();
        }

        /*
        public static void drugaVarijanta()
        {
            staviURezultantnuPanelu(trenutnoDrugo, trenutnaFigura.drugaSlika);
            staviURezultantnuPanelu(trenutnoPrvo, trenutnaFigura.prvaSlika);
            staviURezultantnuPanelu(trenutnoCetvrto, trenutnaFigura.cetvrtaSlika);
            staviURezultantnuPanelu(trenutnoTrece, trenutnaFigura.trecaSlika);
            trenutnaFigura = sledecaFigura;
            napraviSledecuFiguru();
            napraviTrenutnuFiguru(trenutnaFigura);
            DOZVOLJENO = true;
        }
        */

        public static void setTimer()
        {
            // Create a timer with a two second interval.
            bTimer = new System.Timers.Timer(105);
            // Hook up the Elapsed event for the timer. 
            bTimer.Elapsed += ev;
            bTimer.AutoReset = true;
            bTimer.Enabled = true;
        }

        public static void ev(Object source, ElapsedEventArgs e)
        {
            lock (Form1.lockObject2)
            {
                List<Point> poljaZaObradu = new List<Point>();
                if (BROJ == 3)
                {
                    staviURezultantnuPanelu(trenutnoPrvo, trenutnaFigura.drugaSlika);
                    staviURezultantnuPanelu(trenutnoTrece, trenutnaFigura.cetvrtaSlika);
                    BROJ++;
                }
                else if (BROJ == 4)
                {
                    spustiSveSlike();
                    staviURezultantnuPanelu(trenutnoPrvo, trenutnaFigura.prvaSlika);
                    staviURezultantnuPanelu(trenutnoTrece, trenutnaFigura.trecaSlika);
                    BROJ++;
                }
                else if (BROJ == 5)
                {
                    spustiSveSlike();
                    if (gotovoSpustanje == true)
                    {
                        BROJ++;
                    }
                    else
                    {
                        gotovoSpustanje = true;
                        BROJ = 5;
                    }
                }
                else if (BROJ == 6)
                {
                    //komboLista.Add(x.Key);
                    //poljaZaObradu = nadjiKombo(x.Key);
                    //pronadjiJednake(x.Key,poljaZaObradu);
                    //pretvoriUPlave();
                    gotovoJeCiscenje = true;
                    daLiSeBrise = true;
                    formiranjeListeIObrada();
                    BROJ++;
                }
                else if (BROJ == 7)
                {
                    spustiSveSlike();
                    if (gotovoJeCiscenje == false && gotovoSpustanje == true)
                    {
                        gotovoSpustanje = false;
                        //spustiSveSlike();
                        BROJ = 5;
                    }
                    else if (gotovoJeCiscenje == false && gotovoSpustanje == false)
                    {
                        //spustiSveSlike();
                        BROJ = 5;
                    }
                    else if (gotovoSpustanje == true && gotovoJeCiscenje == true)
                    {
                        BROJ++;
                    }
                    else if (gotovoSpustanje == false && gotovoJeCiscenje == true)
                    {
                        gotovoSpustanje = true;
                        BROJ = 6;

                    }
                    opetOcitajRezultantnePanele();
                }
                else
                {
                    trenutnaFigura = sledecaFigura;
                    napraviSledecuFiguru();
                    napraviTrenutnuFiguru(trenutnaFigura);
                    bTimer.Stop();
                    DOZVOLJENO = true;
                    STANI = false;
                    napraviTrenutnuFiguru(trenutnaFigura);
                    //Robot.POKRENI_AKCIJU();
                }
            }
        }

        public static void opetOcitajRezultantnePanele()
        {
            /*
            Image temp = null;
            foreach (KeyValuePair<Point, PictureBox> par in rezultantniPaneli)
            {
                temp = par.Value.Image;
                par.Value.Image = null;
                par.Value.Image = temp;
            }
            */
        }

        public static void spustiSveSlike()
        {
            Image temp;
            for (int i = 15; i >= 0; i--)
            {
                for (int j = 15; j >= 0; j--)
                {
                    Point p = new Point(j * 32, i * 32);
                    if (i != 15 && rezultantniPaneli[p].Image != null)
                    {
                        if (rezultantniPaneli[new Point(p.X, p.Y + 32)].Image == null)
                        {
                            gotovoSpustanje = false;
                            Image im = rezultantniPaneli[p].Image;
                            temp = im;
                            im = null;
                            rezultantniPaneli[p].Image = null;
                            /*
                            for (int x = 0; x < 100000; x++)
                            {
                                x--;
                                x++;
                            }
                             * */
                            rezultantniPaneli[new Point(p.X, p.Y + 32)].Image = null;
                            /*
                            for (int x = 0; x < 100000; x++)
                            {
                                x--;
                                x++;
                            }
                             */
                            rezultantniPaneli[new Point(p.X, p.Y + 32)].Image = temp;
                        }
                    }
                }
            }
            Form1.Instance.refreshujPoene();
        }

        public static void formiranjeListeIObrada()
        {
            List<Image> listaSlika = new List<Image>();
            listaSlika.Add(ZELENA);
            listaSlika.Add(ZUTA);
            listaSlika.Add(CRVENA);
            listaSlika.Add(PLAVA);
            listaSlika.Add(BELA);
            listaSlika.Add(SIVA);
            obradiSlike(listaSlika);
        }

        public static void obradiSlike(List<Image> j)
        {
            foreach (Image img in j)
            {
                List<Point> listaSlika = new List<Point>();
                foreach (KeyValuePair<Point, PictureBox> par in rezultantniPaneli)
                {
                    if (par.Value.Image != null)
                    {
                        Image i = par.Value.Image;
                        if (img.Tag.Equals(i.Tag))
                        {
                            listaSlika.Add(par.Key);
                        }
                    }
                }
                nadjiKomsijeIFormirajRecnik(listaSlika);
            }

        }

        public static void nadjiKomsijeIFormirajRecnik(List<Point> listaSlika)
        {
            recnikSlika = new Dictionary<Point, List<Point>>();
            foreach (Point p in listaSlika)
            {
                List<Point> l = new List<Point>();
                Point levo = new Point(p.X - 32, p.Y);
                Point desno = new Point(p.X + 32, p.Y);
                Point gore = new Point(p.X, p.Y - 32);
                Point dole = new Point(p.X, p.Y + 32);
                foreach (Point q in listaSlika)
                {
                    if (q == levo || q == desno || q == gore || q == dole)
                    {
                        l.Add(q);
                    }
                }
                recnikSlika.Add(p, l);
                formirajKombinacije(recnikSlika);
            }
        }

        public static void formirajKombinacije(Dictionary<Point, List<Point>> recnik)
        {
            List<List<Point>> kombinacije = new List<List<Point>>();
            foreach (KeyValuePair<Point, List<Point>> par in recnik)
            {
                kontejnerZaTrenutnuVezu = new List<Point>();
                kontejnerZaTrenutnuVezu.Add(par.Key);
                pregledajOstaleElemente(par.Value);
                obeleziKomboIBrisi();
            }
        }

        public static void pregledajOstaleElemente(List<Point> lista)
        {
            foreach (Point p in lista)
            {
                if (daLiElementVecPostojiUKontejneru(p) != true)
                {
                    kontejnerZaTrenutnuVezu.Add(p);
                    if (recnikSlika.ContainsKey(p))
                    {
                        pregledajOstaleElemente(recnikSlika[p]);
                    }
                }
            }
        }

        public static Boolean daLiElementVecPostojiUKontejneru(Point p)
        {
            foreach (Point q in kontejnerZaTrenutnuVezu)
            {
                if (p == q)
                {
                    return true;
                }
            }
            return false;
        }

        public static void obeleziKomboIBrisi()
        {
            int count = 0;
            foreach (Point p in kontejnerZaTrenutnuVezu)
            {
                count++;
            }
            if (daLiSeBrise == true)
            {
                if (count >= 3)
                {
                    gotovoJeCiscenje = false;
                    foreach (Point p in kontejnerZaTrenutnuVezu)
                    {
                        if (rezultantniPaneli[p].Image != null)
                        {
                            Form1.igracP++;
                        }
                        rezultantniPaneli[p].Image = null;
                    }
                }
            }
            if (daLiSeTraziNajLevlji == true)
            {
                if (count >= 2)
                {
                    najLevlji();
                }
            }
        }

        public static void staviURezultantnuPanelu(Point p, Image img)
        {
            if (rezultantniPaneli[p].Image == null)
            {
                Image i = null;
                i = img;
                rezultantniPaneli[p].Image = i;
            }
        }
        /*
        public static List<Point> nadjiKombo(Point par)
        {
            Boolean mozeLevo = true;
            Boolean mozeGore = true;
            Boolean mozeDesno = true;
            Boolean mozeDole = true;

            List<Point> mogucaPolja = new List<Point>();

            if (par.X == 0)
            {
                mozeLevo = false;
            }
            if (par.X == 480)
            {
                mozeDesno = false;
            }
            if (par.Y == 0)
            {
                mozeGore = false;
            }
            if (par.Y == 480)
            {
                mozeDole = false;
            }

            if (mozeLevo == true)
            {
                mogucaPolja.Add(new Point(par.X - 32, par.Y));
            }
            if (mozeDesno == true)
            {
                mogucaPolja.Add(new Point(par.X + 32, par.Y));
            }
            if (mozeGore == true)
            {
                mogucaPolja.Add(new Point(par.X, par.Y - 32));
            }
            if (mozeDole == true)
            {
                mogucaPolja.Add(new Point(par.X, par.Y + 32));
            }
            return mogucaPolja;
        }
        */
        /*
        public static void pronadjiJednake(Point sredina, List<Point> polja)
        {
            List<Point> poljaZaObradu = new List<Point>();
            Bitmap slika1 = new Bitmap(rezultantniPaneli[sredina].Image);
            Boolean ispitajSledeci = true;
            foreach (Point s in polja)
            {
                if (rezultantniPaneli[s].Image != null)
                {
                    Bitmap slika2 = new Bitmap(rezultantniPaneli[s].Image);
                    if (porediSlike(slika1, slika2))
                    {
                        foreach (Point t in komboLista)
                        {
                            if (s == t)
                            {
                                ispitajSledeci = false;
                            }
                        }
                        if (ispitajSledeci == true)
                        {
                            komboLista.Add(s);
                            poljaZaObradu = nadjiKombo(s);
                            pronadjiJednake(s, poljaZaObradu);
                        }
                    }
                }
            }
        }
         */

        public static bool porediSlike(Bitmap firstImage, Bitmap secondImage)
        {
            bool flag = true;
            string firstPixel;
            string secondPixel;

            if (firstImage.Width == secondImage.Width && firstImage.Height == secondImage.Height)
            {
                for (int i = 0; i < firstImage.Width; i++)
                {
                    for (int j = 0; j < firstImage.Height; j++)
                    {
                        firstPixel = firstImage.GetPixel(i, j).ToString();
                        secondPixel = secondImage.GetPixel(i, j).ToString();
                        if (firstPixel != secondPixel)
                        {
                            flag = false;
                            break;
                        }
                    }
                }

                if (flag == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /*
        public static void pretvoriUPlave()
        {
            int i = 0;
            foreach (Point s in komboLista)
            {
                i++;
            }

            if (i >= 5)
            {
                foreach (Point s in komboLista)
                {
                    rezultantniPaneli[s].Image = KOMBO;
                }
            }
        }
         */

        public static void najLevlji()
        {
            List<Point> rezultantnaLista = new List<Point>();
            Point iznad = new Point(0, 0);
            foreach (Point p in kontejnerZaTrenutnuVezu)
            {
                if (p.Y != 0)
                {
                    iznad = new Point(p.X, p.Y - 32);
                }
                if (p.X <= najLevljaKoordinata && rezultantniPaneli[iznad].Image == null)
                {
                    //Console.WriteLine("OVO JE NAJVISA" + p.X + p.Y);
                    najLevljaLista = kontejnerZaTrenutnuVezu;
                    najLevljaKoordinata = p.X;
                }
            }
        }

        public static Dictionary<Point, PictureBox> uzmiCeluKolonuOdZnacaja(int koordinata)
        {
            Dictionary<Point, PictureBox> k = new Dictionary<Point, PictureBox>();
            foreach (KeyValuePair<Point, PictureBox> par in rezultantniPaneli)
            {
                if (par.Key.X == koordinata && par.Value.Image != null)
                {
                    k.Add(par.Key, par.Value);
                    Console.WriteLine("DODATI SU" + par.Key.X + par.Key.Y);
                }
            }
            return k;
        }
    }
}
