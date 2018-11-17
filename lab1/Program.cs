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
            var checking = "B97928E841554F55171B071F1D5B034A243C234B011F51AD3FEBA540BDF24EAA";
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
                Console.WriteLine("Result we need : " +"B97928E841554F55171B071F1D5B034A243C234B011F51AD3FEBA540BDF24EAA");
            
                Console.Write("    Result     : ");
        
            return ans;
        }

        public static string Subtraction(ulong[] a, ulong[] b)
        {
            var checking = "1EAC8F0992F957472689F3113D23EEF2613420BCA85A860A1122CD80165E738E";
            var maxlenght = Math.Max(a.Length, b.Length);
            Array.Resize(ref a, maxlenght);
            Array.Resize(ref b, maxlenght);
            var answer = new ulong[maxlenght];
            ulong borrow = 0;
            ulong temp = 0;
            for (int i=0;i<a.Length;i++)
            {
                temp=a[i] - b[i] - borrow;
                if (temp<=a[i])
                {
                    answer[i] = temp;
                    borrow = 0;
                }
                else 
                {
                    answer[i] = temp & 0xffffffff;
                    borrow = 1;
                }
       
            }
            
            string ans = string.Concat(answer.Select(chunk => chunk.ToString("X").PadLeft(sizeof(ulong) * 2, '0')).Reverse()).TrimStart('0');
                Console.WriteLine("Result we need : "+ "1EAC8F0992F957472689F3113D23EEF2613420BCA85A860A1122CD80165E738E");
            
                Console.Write("     Result    : " );

            return ans;
        }

        static string  LongCmp(ulong[] a, ulong[] b)
        {
            var maxlenght = Math.Max(a.Length, b.Length);
            Array.Resize(ref a, maxlenght);
            Array.Resize(ref b, maxlenght);
            for (int i = 0; i < maxlenght; i++)
            {
                if (a[i] < b[i]) { return "a<b"; }
                else if (a[i] > b[i]) { return "a>b"; }
            }
            return "a=b" ;
        }

        static void Main(string[] args)
        { 
            string a = "6C12DBF8EA27534E1ED27D182D3F791E42B82203D4BCEBDBA88739606A28611C";
            string b = "4D664CEF572DFC06F8488A06F01B8A2BE18401472C6265D197646BE053C9ED8E";
            ulong[] p = new ulong[1];
            ulong[] p1 = new ulong[1];
            p1 = toulong(b);
            p = toulong(a);
            Console.WriteLine("Addition");
            Console.WriteLine(Addition(p1, p));
            Console.WriteLine("\nSubtraction");
            Console.WriteLine(Subtraction(p1, p));
            Console.Write("\nComparison:");
            Console.WriteLine(LongCmp(p1, p));
            Console.ReadKey();


        }
    }
}

    
    