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
            var checking = "1328E85EC5F063C3365B199CE8BD083F88D38E795453DBB5B9FC7D7B12CE32236";
            var maxlenght = Math.Max(a.Length, b.Length);
            var answer = new ulong[maxlenght];
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


            static void Main(string[] args)
        { 
            string a = "D40B81552C3BC8701911D1156C2FF1868C774C2225B073FEBA59AEDED175AB18";
            string b = "5E83049732CA73C34C9FC8B91FA0927200C19B731F8D475CE56E28D25B6D771E";
            ulong[] p = new ulong[1];
            ulong[] p1 = new ulong[1];
            p1 = toulong(b);
            p = toulong(a);
            Console.WriteLine(Addition(p1, p));
     
            Console.ReadKey();


        }
    }
}

    
    