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
            ulong zero = 0;
            ulong one = 1;
            var checking = "B07427E61F4C59F0B910CC30465D25DA6DCA32330EBE54A130472AC0B2F1792E33428B2BE4A8FB819B1C4987BE9206AEAC7B8FBF5C5C292D6AF392D8FD4079F0";//4E241A97360C1FEDA29089CC42E6DA239FAE939DC72F0F211C162500BEC35A83
            var maxlenght = Math.Max(a.Length, b.Length);
            var answer = new ulong[maxlenght];
            ulong carry = 0;
            for (int i = 0; i < maxlenght; i++)
            {
                ulong temp = unchecked(a[i] + b[i] + carry);//проверяем переполнение 
                answer[i] = temp;
                carry = temp < a[i] ? one : zero;
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
        {   // нужно сделать контроль длины!!!! а то фигня 
            string a = "8411F4C10C8AA2F2E069E0F72723E85AAAA";//9989588F322261D0164C1442EE82728769EF4B545719A1B9ECC13969CCBF6243
            string b = "BA46127D4F4870C3688B16E875D08C5994EC8ECABE2C320CDE9E4CBD79D47FFC";//5968F5604ABBB4765D3EB71197F35C54DCFA91E97A50CDCA600F9A69FE05A9C
            ulong[] p = new ulong[1];
            ulong[] p1 = new ulong[1];
            p1 = toulong(b);
            p = toulong(a);

            Console.WriteLine(Addition(p1, p));
            /*  Console.WriteLine("------");
            for (int i=0;i<p1.Length;i++)
            {
                Console.WriteLine(p1[i]);
            }
            Console.WriteLine("----");

            for (int i = 0; i < p.Length; i++)
            {
                Console.WriteLine(p[i]);
            }*/
            /*Console.WriteLine("реверс");
            for (int j = 0; j < p.Length; j++)
            {
                Console.WriteLine(p[j]);
            }*/
            Console.ReadKey();


        }
    }
}

    
    