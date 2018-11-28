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
                Console.WriteLine("Result we need : " + "D31C2A660F5CFAB821AE0075CD6AD1276E8C69A3FF33B6995D481B8D768F3F0906748F7FBC51EE32561CCA7592D9198E62A6482026CC05F4273AE15C4EF0771A");
            
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
                Console.WriteLine("Result we need : "+ "7443F4F4B2F41A16BB20D68FF811FE5B56483CF24178E02326133CE2C59BA57B4134E383130C3DA324F295B165636138B57BC11449A710A12339D52984C269C");
            
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
            Console.WriteLine("Result we need : " + "2B78A8D560B1E846ABF8B3A78FE5B28969C7F8894145A16A32F3735A3942BFCD7E173C4B4F7D6A64777AFFEAA7E28E9A4B9B2D7192F98EB08DE591BEE4D939445D3D3BA67759EAF0C30568742A041F2E587D0273265C70A2FCED488C88A75ED010B28E2BEFBCA994848C974284460EB6CAC5D6049300FC5CB15A882CCB6D9FE5");

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
            Console.WriteLine("Result we need : " + "1.111");

            Console.Write("     Result        : ");
            return ans;
        }


        static void Main(string[] args)
        { 
            string a = "6D3034DAAD461E2CC6B006EF6675F88691F876B991A5A24DC7D4A7ADD1747CB05D43EEDBF6C159064435F9E85497A7D0F6FF0218B5B33B7F1CB73F57739E4EDB";
            string b = "65EBF58B6216DC8B5AFDF98666F4D8A0DC93F2EA6D8E144B957373DFA51AC258A930A0A3C590952C11E6D08D3E4171BD6BA746077118CA750A83A204DB52283F";
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

    
    