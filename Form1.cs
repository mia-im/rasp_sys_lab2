using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Lab2
{
    public partial class Form1 : Form
    {

        static int[] a;
        static int[] b;
        static int k;
        static int sum;
        static int E;


        public Form1()
        {
            InitializeComponent();
        }

        //функция обработки элементов вектора
        static void Eq(object o)  
        {
            int Start = ((int[])o)[0];
            int End = ((int[])o)[1];
            for (int i = Start; i < End; i++)
                for (int j = 0; j < E; j++)
                    if (a[i] % k == 0)
                      b[i] = a[i];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox7.Clear();
            textBox8.Clear();
            string l1 = textBox1.Text;
            string l2 = textBox2.Text;
            string l3 = textBox3.Text;
            string l4 = textBox4.Text;
            int N = Convert.ToInt32(l1);
            int M = Convert.ToInt32(l2);
            k = Convert.ToInt32(l3);
            E = Convert.ToInt32(l4);

            //инициализация и заполнение массива a
            a = new int[N];
            b = new int[N];
            var Rand = new Random();
            for (int i = 0; i < N; i++)
                a[i] = Rand.Next(10, 20);

            DateTime dt1 = DateTime.Now;
            //при M=1 последовательная обработка элементов вектора
            if (M == 1)
            {
                for (int i = 0; i < 1000; i++)
                {
                    var thr = new Thread(Eq);
                    thr.Start(new int[] { 0, N });
                    thr.Join();
                }
            }
            else
            {
                //массив потоков 
                for (int i = 0; i < 1000; i++)
                {
                    //стартовые и конечные позиции, шаг (равное кол-о элементов)
                    int Step = N / M;
                    int Start = -Step;
                    int End = 0;
                    Thread[] arrThr = new Thread[M];
                    //инициализация и запуск потоков в цикле
                    for (int j = 0; j < M; j++)
                    {
                        arrThr[j] = new Thread(Eq);
                        arrThr[j].Start(new int[] { Start += Step, End += Step });
                    }
                    //последовательное завершение(блокировка потоков)
                    for (int j = 0; j < M; j++)
                        arrThr[j].Join();
                }
            }
            for (int j = 0; j < N; j++)
                sum = sum + b[j];
            DateTime dt2 = DateTime.Now;

            textBox5.Text = sum.ToString();
            textBox6.Text = ((dt2 - dt1).TotalMilliseconds / 1000).ToString();
            textBox7.Text += (string.Join(" ", a).ToString());
            textBox8.Text += (string.Join(" ", b).ToString());
            a = null;
            b = null;
            sum = 0;
        }

    }
}
