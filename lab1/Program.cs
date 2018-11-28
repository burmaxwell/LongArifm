using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSROM
{

    class Program
    { 
        static public ulong[] toulong32(string a)
        {
            string temp = a;
            while (temp.Length % 8 != 0)
            {
                temp = "0" + temp;
            }

            var mass = new ulong[temp.Length / 8];
            for (int i = 0; i < temp.Length; i += 8)
            {
                mass[i / 8] = Convert.ToUInt64(temp.Substring(i, 8), 16);
            }
            Array.Reverse(mass);
           
            return mass;

        }

        public static string Addition(ulong[] a, ulong[] b)
        {
            var maxlenght = Math.Max(a.Length, b.Length);
            var answer = new ulong[maxlenght+1];
            Array.Resize(ref a, maxlenght);
            Array.Resize(ref b, maxlenght);
            ulong carry = 0;
            for (int i = 0; i < maxlenght; i++)
            {
                ulong temp =a[i] + b[i] + carry;
                if (temp < a[i]) { carry = 1; }
                else if (temp > a[i]) { carry = 0; }
                answer[i] = temp & 0xffffffff;

                
               
            }
            string ans = string.Concat(answer.Select(chunk => chunk.ToString("X").PadLeft(sizeof(ulong), '0')).Reverse()).TrimStart('0');
                Console.WriteLine("Result we need : " + "DA54690ADF08F78A8586C4B9C133D117F4EE570F65087EC4D34D5EC02AD80456");
            
                Console.Write("    Result     : ");
        
            return ans;
        }


        public static string Subtraction(ulong[] a, ulong[] b)
        {
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
                    answer[i] = temp&0xffffffff ;
                    borrow = 1;
                }
       
            }
            
            string ans = string.Concat(answer.Select(chunk => chunk.ToString("X").PadLeft(sizeof(ulong) , '0')).Reverse()).TrimStart('0');
                Console.WriteLine("Result we need : "+ "689D2A3F6D49E3E348ACF689351C325329D534110E1241B2901FF9413F6EB402");
            
                Console.Write("     Result    : " );

            return ans;
        }

       public static ulong[] AdditionUlong(ulong[] a, ulong[] b)
        {
            var maxlenght = Math.Max(a.Length, b.Length);
            var answer = new ulong[maxlenght + 1];
            Array.Resize(ref a, maxlenght);
            Array.Resize(ref b, maxlenght);
            ulong carry = 0;
            for (int i = 0; i < maxlenght; i++)
            {
                ulong temp = a[i] + b[i] + carry;
                carry = temp >> 32;
                answer[i] = temp & 0xffffffff;
            }
            return answer;
        }

        public static ulong[] SubtractionUlong(ulong[] a, ulong[] b)
        {
            var maxlenght = Math.Max(a.Length, b.Length);
            Array.Resize(ref a, maxlenght);
            Array.Resize(ref b, maxlenght);
            var answer = new ulong[maxlenght];
            ulong borrow = 0;
            ulong temp = 0;
            for (int i = 0; i < a.Length; i++)
            {
                temp = a[i] - b[i] - borrow;
                if (temp <= a[i])
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
         return answer;
        }

        public static ulong[] LongMulOneDigit(ulong [] a, ulong b)
        {
            ulong temp, carry = 0;
            ulong[] c = new ulong[a.Length + 1];
            for (int i = 0; i < a.Length; i++)
            {
                temp = a[i] * b + carry;
                carry = temp >> 32;
                c[i] = temp&0xffffffff ;
            }
            c[a.Length] = carry;

            return c;
        }

        public static ulong [] LongShiftDigitsToHigh(ulong [] a, int ind)
        {
            ulong[] c = new ulong[a.Length + ind];
            for (int i = 0; i < a.Length; i++)
            {
                c[i + ind] = a[i];
            }
            return c;
        }

        public static  string LongMul(ulong [] a, ulong[] b)
        {
            var maxlenght = Math.Max(a.Length, b.Length);
            Array.Resize(ref a, maxlenght);
            Array.Resize(ref b, maxlenght);
            ulong[] answer = new ulong[(a.Length) * 2];
            ulong[] temp;
            for (int i = 0; i < a.Length; i++)
            {
                temp = LongMulOneDigit(a, b[i]);
                temp = LongShiftDigitsToHigh(temp, i);
                answer = AdditionUlong(answer, temp);
            }
            string ans = string.Concat(answer.Select(chunk => chunk.ToString("X").PadLeft(sizeof(ulong), '0')).Reverse()).TrimStart('0');
            Console.WriteLine("Result we need : " + "23DCF2FACF21A662203114F71EEA204CBF306A3ED17432A279050FE70CA21EC48CF31F961224F87E07E064C57A6FB9B5C32BDF08670E6717B56191CDD739FF38");

            Console.Write("     Result        : ");

            return ans;
        }

        static int  LongCmp(ulong[] a, ulong[] b)
        {
            var maxlenght = Math.Max(a.Length, b.Length);
            Array.Resize(ref a, maxlenght);
            Array.Resize(ref b, maxlenght);
            for (int i = a.Length-1; i>-1; i--)
            {
                if (a[i] < b[i]) { return -1; }
                if (a[i] > b[i]) { return 1; }
            }
            return 0 ;
        }

        public static int BitLength(ulong[] a)
        {
            int t = 0;
            int i = a.Length-1 ; 
            while (a[i] == 0)
            {
                if (i < 0)
                    return 0;
                i--;
            }
            var n = a[i];
            while (n > 0)
            {
                t++;
                n = n >> 1;
            }

            t = t + 32 * i;
            return t;
        }


        public static ulong[] LongShiftBitsToHigh(ulong[] a, int b)
        {
            int t = b / 32;
            int s = b - t * 32;
            ulong n, carry = 0;
            ulong[] C = new ulong[a.Length + t+1];
            for (int i = 0; i < a.Length; i++)
            {
                n = a[i];
                n = n << s;
                C[i + t] = (n & 0xFFFFFFFF) + carry;
                carry = (n & 0xFFFFFFFF00000000) >> 32;

            }
            C[C.Length - 1] = carry;
            return C;
        }
     
        public static string LongDivInternal(ulong[] a, ulong[] b)
        {
            var k = BitLength(b);
            var R = a;
            ulong[] Q = new ulong[a.Length];
            ulong[] T = new ulong[a.Length];
            ulong[] C = new ulong[a.Length];
            T[0] = 0x1;
            
            while (LongCmp(R, b) >= 0)
            {
                var t = BitLength(R);
                C = LongShiftBitsToHigh(b, t - k);
                if (LongCmp(R, C) == -1)
                {
                    t = t - 1;
                    C = LongShiftBitsToHigh(b, t - k);
                }
                R = SubtractionUlong(R, C);
                Q = AdditionUlong(Q, LongShiftBitsToHigh(T, t - k));
            }

            string ans = string.Concat(Q.Select(chunk => chunk.ToString("X").PadLeft(sizeof(ulong), '0')).Reverse()).TrimStart('0');
            Console.WriteLine("Result we need : " + "2.D704EC9A84D1C0000000");

            Console.Write("     Result        : ");
            return ans;
        }


        static void Main(string[] args)
        { 
            string a = "A178C9A526296DB6E719DDA17B2801B58F61C590398D603BB1B6AC00B5235C2C";
            string b = "38DB9F65B8DF89D39E6CE718460BCF62658C917F2B7B1E892196B2BF75B4A82A";
            ulong[] p = new ulong[1];
            ulong[] p1 = new ulong[1];
            p1 = toulong32(b);
            p = toulong32(a);
             Console.WriteLine("Addition");
             Console.WriteLine(Addition(p, p1));
             Console.WriteLine("\nSubtraction");
             Console.WriteLine(Subtraction(p, p1));
             Console.Write("\nComparison:");
             Console.WriteLine(LongCmp(p, p1));
             Console.Write("\nMul:");
             Console.WriteLine(LongMul(p, p1));
             Console.Write("\nDiv:");
             Console.WriteLine(LongDivInternal(p, p1));
             Console.WriteLine(BitLength(p));
             Console.WriteLine(BitLength(p1));
            Console.ReadKey();


        }
    }
}

    
    