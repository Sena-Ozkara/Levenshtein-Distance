using System;

namespace UsingLevenshtein
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMethod("sena", "asena");
            TestMethod("sam", "samantha");
            TestMethod("mike", "mayk");
            TestMethod("ozkra", "özkara");//Örnekleri istediğiniz gibi çoğaltabilirsiniz.
            Console.Read(); // Çıktıyı görebilmek için yazıyoruz.Bekleyip kapanmaması için.
        }

        private static void TestMethod(string Source, string Target)
        {
            int[,] matrix3 = new int[Source.Length, Target.Length];
            int distance3 = Source.FindLevenshteinDistance(Target, out matrix3);
            Console.WriteLine("{0} ve {1}\nDistance : {2}\n", Source, Target, distance3);
            WriteToConsole(matrix3);
          
        }
        static void WriteToConsole(int[,] Matrix)
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    Console.Write("\t{0}  ", Matrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine(); 
            
        }
                
    }

    public static class StringExtensions
    {
        // Genişletme metodu, karşılaştırma matrisini de out parametresi olarak döndürmektedir. 
        public static int FindLevenshteinDistance(this string Source, string Target, out int[,] Matrix)
        {
            int n = Source.Length;
            int m = Target.Length;

            Matrix = new int[n + 1, m + 1]; // Hesaplama matrisi üretilir. 2 boyutlu matrisin boyut uzunlukları ise kaynak ve hedef metinlerin karakter uzunluklarına göre set edilir

            if (n == 0) // Eğer kaynak metin yoksa zaten hedef metnin tüm harflerinin değişimi söz konusu olduğundan, hedef metnin uzunluğu kadar bir yakınlık değeri mümkün olabilir 
                return m;

            if (m == 0) // Yukarıdaki durum hedefin karakter içermemesi halinde de geçerlidir 
                return n;

            // Aşağıdaki iki döngü ile yatay ve düşey eksenlerdeki standart 0,1,2,3,4...n elemanları doldurulur 
            for (int i = 0; i <= n; i++)
                Matrix[i, 0] = i;

            for (int j = 0; j <= m; j++)
                Matrix[0, j] = j;

            // Kıyaslama ve derecelendirme operasyonu yapılır 
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= m; j++)
                {
                    int cost = (Target[j - 1] == Source[i - 1]) ? 0 : 1;
                    Matrix[i, j] = Math.Min(Math.Min(Matrix[i - 1, j] + 1, Matrix[i, j - 1] + 1), Matrix[i - 1, j - 1] + cost);
                }

            return Matrix[n, m]; // sağ alt taraftaki hücre değeri döndürülür 
        }
    }
}