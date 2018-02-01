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
    class Robot
    {
        private static int FAZA = 0;
        public static int kreniOdLevo = 0;
        public static Point radnaPozicija = new Point(0,0);

        public static Point vazanPoint = new Point(0,0);

        public static Boolean nadjenJeNajmanji = false;

        public static Boolean rotirajOvaj = true;

        public static System.Timers.Timer GLAVNI_TIMER;

        public static Point rezultat = new Point(0, 0);

        static Dictionary<Image, List<Point>> korisceneSlikeFigure { get; set; }
        static List<Point> pointoviZauzetihRadnihPanela { get; set; }
        static Dictionary<Image, List<Point>> parovi { get; set; }

        static Dictionary<Image, List<Point>> korisceneRezultantneSlike { get; set; }
        static List<Point> slobodniPointovi { get; set; }

        static Dictionary<Point, Image> pointoviOdZnacaja { get; set; }

        static Dictionary<Point, List<Point>> recnikKomsija { get; set; }

        public Robot()
        {
            korisceneSlikeFigure = new Dictionary<Image, List<Point>>();
            pointoviZauzetihRadnihPanela = new List<Point>();

            korisceneRezultantneSlike = new Dictionary<Image, List<Point>>();
            slobodniPointovi = new List<Point>();

            pointoviOdZnacaja = new Dictionary<Point, Image>();

            recnikKomsija = new Dictionary<Point, List<Point>>();

            parovi = new Dictionary<Image, List<Point>>();

            napuniListuZauzetihRadnihPanela(pointoviZauzetihRadnihPanela);
            napraviRecnik(korisceneSlikeFigure);
            dodajPointoveURecnik(korisceneSlikeFigure, pointoviZauzetihRadnihPanela);
            nadjiParove();

            napuniListuSlobodnihMesta(slobodniPointovi);

            nadjiPointoveOdZnacaja(slobodniPointovi, pointoviOdZnacaja);
        }

        public static void ROTIRAJ_FIGURU()
        {
            Point ispodRadnePozicije = new Point(vazanPoint.X, vazanPoint.Y + 32);
            Point rezultat = new Point(0, 0);
            if (Form1.rezultantniPaneli.ContainsKey(ispodRadnePozicije))
            {
                if (Form1.rezultantniPaneli[ispodRadnePozicije].Image == null)
                {
                    rezultat.Y = 0;
                }
                else
                {
                    rezultat.Y = 32;
                }
                rezultat.X = vazanPoint.X;
            }
            if (pointoviOdZnacaja.Count != 0)
            {
                if (!Form1.radniPaneli[Form1.trenutnoPrvo].Image.Tag.Equals(pointoviOdZnacaja[vazanPoint].Tag))
                {
                    rotirajOvaj = true;
                    if (rezultat.X == 0 && rezultat.X == Form1.trenutnoPrvo.X)
                    {
                        Form1.rotirajKaGore();
                    }
                    else if (rezultat.X == 0 && rezultat.X != Form1.trenutnoPrvo.X)
                    {
                        Form1.rotirajKaGore();
                    }
                    else if (rezultat.X == 32 && rezultat.X == Form1.trenutnoPrvo.X)
                    {
                        Form1.rotirajKaDole();
                    }
                    else
                    {
                        Form1.rotirajKaDole();
                    }
                }
                else
                {
                    rotirajOvaj = false;
                }
            }
        }

        public static void POMERI_FIGURU()
        {
            //radnaPozicija = nadjiRadnuPoziciju();
            if (radnaPozicija.X < Form1.trenutnoPrvo.X)
            {
                Form1.pomeriLevo();
            }
            else if (radnaPozicija.X > Form1.trenutnoPrvo.X)
            {
                Form1.pomeriDesno();
            }
        }

        public static Point nadjiRadnuPoziciju()
        {
            nadjenJeNajmanji = false;
            Point p = new Point(480, 0);
            int najmanjiX = 480;
            int i = 0;

            foreach (KeyValuePair<Point, Image> par in pointoviOdZnacaja)
            {
                if (par.Key.X <= najmanjiX)
                {
                    nadjenJeNajmanji = true;
                    najmanjiX = par.Key.X;
                    vazanPoint = par.Key;
                }
            }
            //if (najmanjiX == 480)
            //{
                //najmanjiX = 448;
            //}
            if (nadjenJeNajmanji == true)
            {
                p.X = najmanjiX;
            }
            else
            {
                /*
                for (int i = 0; i < 8; i++)
                {
                    if (Form1.rezultantniPaneli[new Point(i * 32, 0)].Image != null || Form1.rezultantniPaneli[new Point(i * 32, 32)].Image != null || Form1.rezultantniPaneli[new Point((i + 1) * 32, 0)].Image != null || Form1.rezultantniPaneli[new Point((i + 1) * 32, 32)].Image != null)
                    {
                        kreniOdLevo += 2;
                    }
                }
                if (kreniOdLevo > 14)
                {
                    GLAVNI_TIMER.Stop();
                    for (int j = 0; j < 20; j++)
                    {
                        Console.WriteLine("GAME OVER!!!");
                    }
                }
                 * */
                int test = 0 + kreniOdLevo * 32;
                for (i = 0; i < 16; i++)
                {
                    if (Form1.rezultantniPaneli[new Point(test, 0)].Image == null || Form1.rezultantniPaneli[new Point(test + 32, 0)].Image == null)
                    {
                        p.X = 0 + kreniOdLevo * 32;
                        vazanPoint.X = p.X;
                        if (kreniOdLevo == 14)
                        {
                            kreniOdLevo = 0;
                        }
                        else
                        {
                            kreniOdLevo += 2;
                        }
                        break;
                    }
                    else
                    {
                        if (kreniOdLevo == 14)
                        {
                            kreniOdLevo = 0;
                            test = 0;
                        }
                        else
                        {
                            kreniOdLevo += 2;
                            test = 0 + kreniOdLevo * 32;
                        }
                    }
                }
                if (i >= 16)
                {
                    p = new Point(-1, -1);
                }
            }
            return p;
        }

        public static void NADJI_POINTOVE()
        {
            korisceneSlikeFigure = new Dictionary<Image, List<Point>>();
            pointoviZauzetihRadnihPanela = new List<Point>();

            korisceneRezultantneSlike = new Dictionary<Image, List<Point>>();
            slobodniPointovi = new List<Point>();

            pointoviOdZnacaja = new Dictionary<Point, Image>();

            recnikKomsija = new Dictionary<Point, List<Point>>();

            parovi = new Dictionary<Image, List<Point>>();

            napuniListuZauzetihRadnihPanela(pointoviZauzetihRadnihPanela);
            napraviRecnik(korisceneSlikeFigure);
            dodajPointoveURecnik(korisceneSlikeFigure, pointoviZauzetihRadnihPanela);
            nadjiParove();

            napuniListuSlobodnihMesta(slobodniPointovi);

            nadjiPointoveOdZnacaja(slobodniPointovi, pointoviOdZnacaja);
        }

        public static void POKRENI_AKCIJU()
        {
            // Create a timer with a two second interval.
            GLAVNI_TIMER = new System.Timers.Timer(100);
            // Hook up the Elapsed event for the timer. 
            GLAVNI_TIMER.Elapsed += GLAVNA_AKCIJA;
            GLAVNI_TIMER.AutoReset = true;
            GLAVNI_TIMER.Enabled = true;
            FAZA = 1;
        }

        public static void GLAVNA_AKCIJA(Object source, ElapsedEventArgs e)
        {
            lock (Form1.lockObject)
            {
                if (FAZA == 1)
                {
                    NADJI_POINTOVE();
                    radnaPozicija = nadjiRadnuPoziciju();
                    if (radnaPozicija.X == -1 && radnaPozicija.Y == -1)
                    {
                        GLAVNI_TIMER.Stop();
                        for (int i = 0; i < 30; i++)
                        {
                            Console.WriteLine("NO RESULTS FOUND! DOES NOT COMPUTE!!!!!!!!!!!!!!");
                        }
                    }
                    FAZA++;
                }
                if (FAZA == 2)
                {
                    if (Form1.trenutnoPrvo.X != radnaPozicija.X)
                    {
                        POMERI_FIGURU();
                        FAZA = 2;
                    }
                    else
                    {
                        FAZA++;
                        rotirajOvaj = false;
                    }

                    if (radnaPozicija.X == 480 && Form1.trenutnoPrvo.X == 448)
                    {
                        FAZA++;
                        rotirajOvaj = false;
                    }
                }
                if (FAZA == 3)
                {
                    Point ispodRadnePozicije = new Point(vazanPoint.X, vazanPoint.Y + 32);
                    rezultat = new Point(0, 32);
                    if (Form1.rezultantniPaneli.ContainsKey(ispodRadnePozicije))
                    {
                        if (Form1.rezultantniPaneli[ispodRadnePozicije].Image == null)
                        {
                            rezultat.Y = 0;
                        }
                    }
                    rezultat.X = radnaPozicija.X;
                    FAZA++;
                }
                if (FAZA == 4)
                {
                    Console.WriteLine("FAZA 4!!!!!!!!!!!");
                    if (pointoviOdZnacaja.ContainsKey(vazanPoint))
                    {
                        if (Form1.radniPaneli[Form1.trenutnoPrvo].Image.Tag != pointoviOdZnacaja[vazanPoint].Tag && Form1.radniPaneli[Form1.trenutnoDrugo].Image.Tag != pointoviOdZnacaja[vazanPoint].Tag && Form1.radniPaneli[Form1.trenutnoTrece].Image.Tag != pointoviOdZnacaja[vazanPoint].Tag && Form1.radniPaneli[Form1.trenutnoCetvrto].Image.Tag != pointoviOdZnacaja[vazanPoint].Tag)
                        {
                            FAZA++;
                        }
                        else
                        {
                            //Point ovajRotiraj = new Point(0, 0);

                            /*if (Form1.radniPaneli[Form1.trenutnoPrvo].Image.Tag == pointoviOdZnacaja[vazanPoint].Tag)
                            {
                                ovajRotiraj = Form1.trenutnoPrvo;
                            }
                            if (Form1.radniPaneli[Form1.trenutnoDrugo].Image.Tag == pointoviOdZnacaja[vazanPoint].Tag)
                            {
                                ovajRotiraj = Form1.trenutnoDrugo;
                            }
                            if (Form1.radniPaneli[Form1.trenutnoTrece].Image.Tag == pointoviOdZnacaja[vazanPoint].Tag)
                            {
                                ovajRotiraj = Form1.trenutnoTrece;
                            }
                            if (Form1.radniPaneli[Form1.trenutnoCetvrto].Image.Tag == pointoviOdZnacaja[vazanPoint].Tag)
                            {
                                ovajRotiraj = Form1.trenutnoCetvrto;
                            }
                            */
                            if (Form1.radniPaneli[rezultat].Image.Tag != pointoviOdZnacaja[vazanPoint].Tag && !parovi.ContainsKey(pointoviOdZnacaja[vazanPoint]))
                            {
                                Console.WriteLine("NIJE PAR!!!!");
                                Form1.rotirajKaGore();
                                FAZA = 4;
                            }
                            else if (parovi.ContainsKey(pointoviOdZnacaja[vazanPoint]))
                            {
                                Console.WriteLine("JESTE PAR!!!!");
                                Point r1 = new Point(rezultat.X, 0);
                                Point r2 = new Point(rezultat.X, 32);
                                if (Form1.radniPaneli[r1].Image.Tag != pointoviOdZnacaja[vazanPoint].Tag || Form1.radniPaneli[r2].Image.Tag != pointoviOdZnacaja[vazanPoint].Tag)
                                {
                                    Form1.rotirajKaDole();
                                    FAZA = 4;
                                }
                                else
                                {
                                    FAZA++;
                                }
                            }
                            else
                            {
                                FAZA++;
                            }
                        }
                    }
                    else
                    {
                        FAZA++;
                    }
                }
                if (FAZA == 5)
                {
                    GLAVNI_TIMER.Stop();
                    Form1.Instance.povecajPotezRobot();
                    Form1.spustiFiguru();
                }
            }
        }

        public static void nadjiParove()
        {
            foreach (KeyValuePair<Image, List<Point>> entry in korisceneSlikeFigure)
            {
                if (entry.Value.Count == 4 || entry.Value.Count == 3)
                {
                    parovi.Add(entry.Key, entry.Value);
                }
                else if (entry.Value.Count == 2)
                {
                    Point p1 = entry.Value[0];
                    Point p2 = entry.Value[1];
                    if (p1.X == p2.X || p1.Y == p2.Y)
                    {
                        parovi.Add(entry.Key, entry.Value);
                    }
                }
            }
        }

        public static void napuniListuZauzetihRadnihPanela(List<Point> l)
        {
            l.Add(Form1.trenutnoPrvo);
            l.Add(Form1.trenutnoDrugo);
            l.Add(Form1.trenutnoTrece);
            l.Add(Form1.trenutnoCetvrto);
        }

        private static void napraviRecnik(Dictionary<Image, List<Point>> d)
        {
            d.Add(Form1.BELA, new List<Point>());
            d.Add(Form1.CRVENA, new List<Point>());
            d.Add(Form1.ZUTA, new List<Point>());
            d.Add(Form1.PLAVA, new List<Point>());
            d.Add(Form1.SIVA, new List<Point>());
            d.Add(Form1.ZELENA, new List<Point>());
        }

        private static void dodajPointoveURecnik(Dictionary<Image, List<Point>> d, List<Point> l)
        {
            foreach (Point p in l)
            {
                dodajPointURecnik(p, d);
            }
        }

        private static void dodajPointURecnik(Point p, Dictionary<Image, List<Point>> d)
        {
            foreach(KeyValuePair<Image, List<Point>> entry in d)
            {
                if(Form1.radniPaneli[p].Image.Tag.Equals(entry.Key.Tag))
                {
                    entry.Value.Add(p);
                    break;
                }
            }
        }

        public static void napuniListuSlobodnihMesta(List<Point> l)
        {
            for(int i = 0; i < 16; i++)
            {
                for (int j = 15; j > -1; j--)
                {
                    Point p1 = new Point(i*32, j*32);
                    Point p2 = new Point(p1.X, p1.Y - 32);
                    if (Form1.rezultantniPaneli[p1].Image == null)
                    {
                        l.Add(p1);
                        if (p1.Y != 0)
                        {
                            l.Add(p2);
                        }
                        break;
                    }
                }
            }
        }

        public static void nadjiPointoveOdZnacaja(List<Point> svi, Dictionary<Point, Image> znacajni)
        {
            recnikKomsija = nadjiKomsije(svi);
            Boolean tajSeTrazi = false;
            foreach (KeyValuePair<Point, List<Point>> par in recnikKomsija)
            {
                //Provera veze u figuri
                foreach (Point p in par.Value)
                {
                    tajSeTrazi = false;
                    //Point levo = new Point(p.X - 32, p.Y);
                    //Point desno = new Point(p.X + 32, p.Y);
                    //Point dole = new Point(p.X, p.Y + 32);

                    foreach (KeyValuePair<Image, List<Point>> par2 in parovi)
                    {
                        if (Form1.rezultantniPaneli[p].Image != null)
                        {
                            if (par2.Key.Tag.Equals(Form1.rezultantniPaneli[p].Image.Tag))
                            {
                                if(tajSeTrazi != true)
                                {
                                    if (!znacajni.ContainsKey(par.Key))
                                    {
                                        if (par.Key.Y != 0)
                                        {
                                            Console.WriteLine("OVO JE PAR!!!!!!!!!!!!!!!!!!!!");
                                            znacajni.Add(par.Key, par2.Key);
                                        }
                                        tajSeTrazi = true;
                                    }
                                }
                            }
                        }
                    }
                }
                Point p1 = new Point(0, 0);
                Point p2 = new Point(0, 0);
                Point p3 = new Point(0, 0);

                //Provera Jednakih
                if (par.Value.Count == 2)
                {
                    p1 = par.Value[0];
                    p2 = par.Value[1];
                    if (Form1.rezultantniPaneli[p1].Image != null && Form1.rezultantniPaneli[p2].Image != null)
                    {
                        if (Form1.rezultantniPaneli[p1].Image.Tag.Equals(Form1.rezultantniPaneli[p2].Image.Tag))
                        {
                            if (korisceneSlikeFigure[Form1.rezultantniPaneli[p1].Image].Count != 0)
                            {
                                if(tajSeTrazi != true)
                                {
                                    if (!znacajni.ContainsKey(par.Key))
                                    {
                                        znacajni.Add(par.Key, Form1.rezultantniPaneli[p1].Image);
                                        tajSeTrazi = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (par.Value.Count == 3)
                {
                    p1 = par.Value[0];
                    p2 = par.Value[1];
                    p3 = par.Value[2];
                    if (Form1.rezultantniPaneli[p1].Image != null && Form1.rezultantniPaneli[p2].Image != null)
                    {
                        if (Form1.rezultantniPaneli[p1].Image.Tag.Equals(Form1.rezultantniPaneli[p2].Image.Tag))
                        {
                            if (korisceneSlikeFigure[Form1.rezultantniPaneli[p1].Image].Count != 0)
                            {
                                if(tajSeTrazi != true)
                                {
                                    if (!znacajni.ContainsKey(par.Key))
                                    {
                                        znacajni.Add(par.Key, Form1.rezultantniPaneli[p1].Image);
                                        tajSeTrazi = true;
                                    }
                                }
                            }
                        }
                    }
                    if (Form1.rezultantniPaneli[p1].Image != null && Form1.rezultantniPaneli[p3].Image != null)
                    {
                        if (Form1.rezultantniPaneli[p1].Image.Tag.Equals(Form1.rezultantniPaneli[p3].Image.Tag))
                        {
                            if (korisceneSlikeFigure[Form1.rezultantniPaneli[p1].Image].Count != 0)
                            {
                                if(tajSeTrazi != true)
                                {
                                    if (!znacajni.ContainsKey(par.Key))
                                    {
                                        znacajni.Add(par.Key, Form1.rezultantniPaneli[p1].Image);
                                        tajSeTrazi = true;
                                    }
                                }
                            }
                        }
                    }
                    if (Form1.rezultantniPaneli[p2].Image != null && Form1.rezultantniPaneli[p3].Image != null)
                    {
                        if (Form1.rezultantniPaneli[p2].Image.Tag.Equals(Form1.rezultantniPaneli[p3].Image.Tag))
                        {
                            if (korisceneSlikeFigure[Form1.rezultantniPaneli[p2].Image].Count != 0)
                            {
                                if(tajSeTrazi != true)
                                {
                                    if (!znacajni.ContainsKey(par.Key))
                                    {
                                        znacajni.Add(par.Key, Form1.rezultantniPaneli[p2].Image);
                                        tajSeTrazi = true;
                                    }
                                }
                            }
                        }
                    }
                    if (Form1.rezultantniPaneli[p1].Image != null && Form1.rezultantniPaneli[p2].Image != null && Form1.rezultantniPaneli[p3].Image != null)
                    {
                        if (Form1.rezultantniPaneli[p1].Image.Tag.Equals(Form1.rezultantniPaneli[p2].Image.Tag) && Form1.rezultantniPaneli[p2].Image.Tag.Equals(Form1.rezultantniPaneli[p3].Image.Tag))
                        {
                            if (korisceneSlikeFigure[Form1.rezultantniPaneli[p1].Image].Count != 0)
                            {
                                if(tajSeTrazi != true)
                                {
                                    if (!znacajni.ContainsKey(par.Key))
                                    {
                                        znacajni.Add(par.Key, Form1.rezultantniPaneli[p1].Image);
                                        tajSeTrazi = true;
                                    }
                                }
                            }
                        }
                    }
                }

                //Provera veze u prostoru
                Dictionary<Point, List<Point>> komsije = new Dictionary<Point, List<Point>>();
                komsije = nadjiKomsije(par.Value);
                foreach (KeyValuePair<Point, List<Point>> poredjenje in komsije)
                {
                    foreach (Point p in poredjenje.Value)
                    {
                        if (Form1.rezultantniPaneli[p].Image != null && Form1.rezultantniPaneli[poredjenje.Key].Image != null)
                        {
                            if (Form1.rezultantniPaneli[p].Image.Equals(Form1.rezultantniPaneli[poredjenje.Key].Image))
                            {
                                if (korisceneSlikeFigure[Form1.rezultantniPaneli[p].Image].Count != 0)
                                {
                                    if (tajSeTrazi != true)
                                    {
                                        if (!znacajni.ContainsKey(par.Key))
                                        {
                                            znacajni.Add(par.Key, Form1.rezultantniPaneli[p].Image);
                                            tajSeTrazi = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static Dictionary<Point, List<Point>> nadjiKomsije(List<Point> listaSlika)
        {
            Dictionary<Point, List<Point>>  recnikSlika = new Dictionary<Point, List<Point>>();
            foreach (Point p in listaSlika)
            {
                List<Point> l = new List<Point>();
                Point levo = new Point(p.X - 32, p.Y);
                Point desno = new Point(p.X + 32, p.Y);
                Point dole = new Point(p.X, p.Y + 32);
                if (levo.X >= 0)
                {
                    l.Add(levo);
                }
                if (desno.X <= 480)
                {
                    l.Add(desno);
                }
                if (dole.Y <= 480)
                {
                    l.Add(dole);
                }
                recnikSlika.Add(p, l);
            }
            return recnikSlika;
        }
    }
}
