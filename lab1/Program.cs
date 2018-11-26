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
            //var checking = "B97928E841554F55171B071F1D5B034A243C234B011F51AD3FEBA540BDF24EAA";
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
                //carry = temp >> 32;
                answer[i] = temp & 0xffffffff;

                
               
            }
            string ans = string.Concat(answer.Select(chunk => chunk.ToString("X").PadLeft(sizeof(ulong), '0')).Reverse()).TrimStart('0');
                Console.WriteLine("Result we need : " + "F71EEAF6B98DC9AA91A720C72624053023858AC2D5A636E2D26630132C80EA8C");
            
                Console.Write("    Result     : ");
        
            return ans;
        }


        public static string Subtraction(ulong[] a, ulong[] b)
        {
            //var checking = "1EAC8F0992F957472689F3113D23EEF2613420BCA85A860A1122CD80165E738E";
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
                Console.WriteLine("Result we need : "+ "8D3075C7AAEC26D7E6A43963E6455AC5C3CD3CF57EDDD2EE233D3EB09050D08A");
            
                Console.Write("     Result    : " );

            return ans;
        }
//-----------------------------------------------------------------------------------

       public static ulong[] AdditionUlong(ulong[] a, ulong[] b)
        {
            //var checking = "B97928E841554F55171B071F1D5B034A243C234B011F51AD3FEBA540BDF24EAA";
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
            //var checking = "1EAC8F0992F957472689F3113D23EEF2613420BCA85A860A1122CD80165E738E";
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
            Console.WriteLine("Result we need : " + "1230C20E8CA5F3DF17237");

            Console.Write("     Result        : ");

            return ans;
        }

//--------------------------------------------------------------------------------------------

        static int  LongCmp(ulong[] a, ulong[] b)
        {
            var maxlenght = Math.Max(a.Length, b.Length);
            Array.Resize(ref a, maxlenght);
            Array.Resize(ref b, maxlenght);
            for (int i = 0; i < maxlenght; i++)
            {
                if (a[i] < b[i]) { return -1; }
                else if (a[i] > b[i]) { return 1; }
            }
            return 0 ;
        }

        public static int BitLength(ulong[] a)
        {
            int t = 0;
            int i = a.Length - 1;
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
            ulong[] C = new ulong[a.Length + t];
            //--------------------------------------------//
            for (int i = 0; i < a.Length; i++)
            {
                n = a[i];
                n = n << s;
                if (i==0) { C[i] = (n & 0xFFFFFFFF) + carry; }
                C[i + t] = (n & 0xFFFFFFFF) + carry;
                carry = (n & 0xFFFFFFFF00000000) >> 32;
            }
            Array.Reverse(C);
            return C;
        }
     
        public static string LongDivInternal(ulong[] a, ulong[] b)
        {
            var maxlenght = Math.Max(a.Length, b.Length);
            Array.Resize(ref a, maxlenght);
            Array.Resize(ref b, maxlenght);
            var k = BitLength(b);
            var R = a;
            ulong[] Q = new ulong[a.Length];
            ulong[] T = new ulong[a.Length];
            T[0] = 0x1;
            ulong[] C;
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
            Console.WriteLine("Result we need : " + "3.56FD3D1E43DE80000000");

            Console.Write("     Result        : ");
            return ans;
        }

        static void Main(string[] args)
        { 
            string a = "ABCDEFABCDEFABCDEFABCDEFABCDEFABCDEFABCDEFABCDEFABCDEFABCDEF0000000000000000000000";
            string b = "ABCDEFABCDEFABCDEFABCDEFABCDEFABCDEFABCDEFABCDEFABCDEFABCDEF0000000000000000000000";
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
             Console.WriteLine(LongShiftBitsToHigh(p, 55));

    
            

            //Console.WriteLine(BitLength(p));
            //Console.WriteLine(BitLength(p1));
 
            Console.ReadKey();


        }
    }
}

    
    