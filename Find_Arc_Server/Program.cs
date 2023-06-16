using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using ArcServer;
using Firebase.Database;
using Firebase.Database.Query;

namespace MyFirstApp
{
    public class Pos
    {
        public int num;
        public bool isFounded = false;
        public double lat { get; set; }
        public double lon { get; set; }

        public Pos(double lat, double lon)
        {
            this.lat = lat;
            this.lon = lon;
        }
        public Pos(int num, bool isf, double lat, double lon)
        {
            this.num = num;
            this.isFounded = isf;
            this.lat = lat;
            this.lon = lon;
        }
    }


    class Program
    {
        static Random random = new Random();
        static FirebaseClient firebase = new FirebaseClient("https://arrrr-c617e-default-rtdb.asia-southeast1.firebasedatabase.app/");
        static ConcurrentQueue<Pos> poses = new ConcurrentQueue<Pos>();
        static Polygon polygon = new Polygon();

        static void Main(string[] args)
        {
            polygon = CreateKorPoly();
            DateTime dt = DateTime.Now;
            TimeSpan ts = new TimeSpan();

            while (poses.Count < 100)
            {
                poses.Enqueue(AddAroundPos(37.57052, 126.9851));
                //Console.WriteLine(poses.Count);
            }

            while (poses.Count < 1000)
            {
                poses.Enqueue(CreateRandomPos());
                //Console.WriteLine(poses.Count);
            }
            Console.WriteLine("Enqueue Done");
            Task.Factory.StartNew(async () =>
            {
                await UpdateData();
            });

            while (true)
            {
                ts = DateTime.Now - dt;                

                if (ts.Hours >= 2)
                {
                    Task.Factory.StartNew(async () =>
                    {
                        await UpdateData();
                    });

                    dt = DateTime.Now;
                }
                if(poses.Count > 1200)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    poses.Enqueue(CreateRandomPos());
                }
            }   
        }
        public static Polygon CreateKorPoly()
        {
            Polygon poly = new Polygon();
            poly.addPoint(37.7619069, 126.6643665);
            poly.addPoint(37.7154131, 126.1809608);
            poly.addPoint(38.2867926, 127.1151097);
            poly.addPoint(38.3329254, 128.1341811);
            poly.addPoint(38.6142097, 128.3497539);
            poly.addPoint(37.2852625, 129.3492278);
            poly.addPoint(36.0434252, 129.6105281);
            poly.addPoint(35.1029807, 129.2839027);
            poly.addPoint(33.99464, 127.3437474);
            poly.addPoint(33.2056987, 126.9517968);
            poly.addPoint(33.118202, 126.1417656);
            poly.addPoint(34.0325438, 125.0639016);
            poly.addPoint(34.7763228, 125.8739328);
            poly.addPoint(36.1489943, 126.4879886);
            poly.addPoint(36.7794232, 126.0372455);
            poly.addPoint(37.2644698, 126.4095985);
            return poly;
        }
        public static Pos AddAroundPos(double lat, double lon)
        {
            double resultLat = lat + random.NextDouble()/50 - 0.01;
            double resultLon = lon + random.NextDouble()/50 - 0.01;
            return new Pos(resultLat, resultLon);
        }
        public static Pos CreateRandomPos()
        {
            while(true)
            {
                int wido, gyungdo;
                double c_wido, c_gyungdo;
                double result_w, result_g;
                gyungdo = random.Next(126, 130);
                wido = random.Next(33, 39);
                c_wido = random.NextDouble();
                c_gyungdo = random.NextDouble();
                result_w = wido + c_wido;
                result_g = gyungdo + c_gyungdo;
                if(polygon.isPointInPolygon(result_w, result_g))
                {
                    return new Pos(result_w, result_g);
                }
            }
        }


        private static async Task UpdateData()
        {
            Console.WriteLine("Start." + DateTime.Now);
            Pos pos = new Pos(0, 0);
            for (int i = 0; i < 1000; i++)
            {
                if(poses.TryDequeue(out pos))
                {
                    pos.num = i;
                    await firebase
                  .Child("Box")
                  .Child($"{i}")
                  .PutAsync(pos);
                }
                else
                {
                    pos = CreateRandomPos();
                    pos.num = i;
                    await firebase
                  .Child("Box")
                  .Child($"{i}")
                  .PutAsync(pos);
                }
            }
            Console.WriteLine("Done." + DateTime.Now);
        }
    }
}