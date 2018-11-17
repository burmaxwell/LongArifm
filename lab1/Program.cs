using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSROM
{

    class Program
    {
        static public ulong[] toulong(string a)
        {
            string temp = a;
            while (temp.Length % 16 != 0)
            {
                temp = "0" + temp;
            }

            var mass = new ulong[temp.Length / 16];
            for (int i = 0; i < temp.Length; i += 16)
            {
                mass[i / 16] = Convert.ToUInt64(temp.Substring(i, 16), 16);
            }
            Array.Reverse(mass);
            return mass;

        }
    
        public static string Addition(ulong[] a, ulong[] b)
        {
            var checking = "8368BE597855204D694B8AEDD994C2510905AB358E2746B5E4399CE3832EDC8";
            var maxlenght = Math.Max(a.Length, b.Length);
            var answer = new ulong[maxlenght+1];
            Array.Resize(ref a, maxlenght);
            Array.Resize(ref b, maxlenght);
            ulong carry = 0;
           
            for (int i = 0; i < maxlenght; i++)
            {
                ulong temp =a[i] + b[i] + carry;
                answer[i] = temp;
                if (temp < a[i]) { carry = 1; }
                else if (temp > a[i]) { carry = 0; }
            }
            string ans = string.Concat(answer.Select(chunk => chunk.ToString("X").PadLeft(sizeof(ulong) * 2, '0')).Reverse()).TrimStart('0');
            if (ans != checking)
            {
                Console.WriteLine("VSYO FIGIVO, DYADYA!");
            }
            else
                Console.WriteLine("VSYO NARMALNA");
        
            return ans;
        }

        public static string Subtraction(ulong[] a, ulong[] b)
        {
            var checking = "5DB3F77B1BAEF00A775DCC36FE2FCD4FBCFF03A3F10479FA092073A2688A00C";//
            var maxlenght = Math.Max(a.Length, b.Length);
            Array.Resize(ref a, maxlenght);
            Array.Resize(ref b, maxlenght);
            var answer = new ulong[maxlenght];
            ulong borrow = 0;
            ulong temp = 0;
            for (int i=0;i<a.Length;i++)
            {
                temp=a[i] - b[i] - borrow;
                if (temp>=0)
                {
                    answer[i] = temp;
                    borrow = 0;
                }
                else
                {
                    answer[i] = temp & 0xFFFFFFFF;
                    borrow = 1;
                }
            }
            string ans = string.Concat(answer.Select(chunk => chunk.ToString("X").PadLeft(sizeof(ulong) * 2, '0')).Reverse()).TrimStart('0');
            if (ans != checking)
            {
                Console.WriteLine("VSYO FIGIVO, DYADYA!");
            }
            else
                Console.WriteLine("VSYO NARMALNA");

            return ans;
        }
        static void Main(string[] args)
        { 
            string a = "708E5AEA4A02082BF054AB926BE247D06302576CBF95E057F6AD0842F5DC6EA";//
            string b = "12DA636F2E53182178F6DF5B6DB27A80A60353C8CE91665DED8C94A08D526DE";//
            ulong[] p = new ulong[1];
            ulong[] p1 = new ulong[1];
            p1 = toulong(b);
            p = toulong(a);
            Console.WriteLine(Addition(p1, p));
            Console.WriteLine(Subtraction(p1, p));
            Console.ReadKey();


        }
    }
}

    
    