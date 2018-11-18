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
            //var checking = "B97928E841554F55171B071F1D5B034A243C234B011F51AD3FEBA540BDF24EAA";
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
                Console.WriteLine("Result we need : " + "BC3E2A381948D7E2C813953A2EEBF6DDA3768FB8FB29CC904CB7C55529CE92CB");
            
                Console.Write("    Result     : ");
        
            return ans;
        }

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
                answer[i] = temp;
                if (temp < a[i]) { carry = 1; }
                else if (temp > a[i]) { carry = 0; }

            }
            return answer;
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
                    answer[i] = temp ;
                    borrow = 1;
                }
       
            }
            
            string ans = string.Concat(answer.Select(chunk => chunk.ToString("X").PadLeft(sizeof(ulong) * 2, '0')).Reverse()).TrimStart('0');
                Console.WriteLine("Result we need : "+ "94531BBBBAB27452FF4CE1D89FEDE9F39A2EEC401F0AA40EF200DFD9FF9795D5");
            
                Console.Write("     Result    : " );

            return ans;
        }

        public static ulong[] LongMulOneDigit(ulong [] a, ulong b)
        {
            ulong temp, carry = 0;
            ulong[] c = new ulong[a.Length + 1];
            for (int i = 0; i < a.Length; i++)
            {
                temp = a[i] * b + carry;
                carry = temp >> 64;
                c[i] = temp ;
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
            ulong[] answer = new ulong[(a.Length) * 2];
            ulong[] temp;
            for (int i = 0; i < a.Length; i++)
            {
                temp = LongMulOneDigit(a, b[i]);
                temp = LongShiftDigitsToHigh(temp, i);
                answer = AdditionUlong(answer, temp);
            }
            string ans = string.Concat(answer.Select(chunk => chunk.ToString("X").PadLeft(sizeof(ulong) * 2, '0')).Reverse()).TrimStart('0');
            Console.WriteLine("Result we need : " + "D1ECA83B42AC9EC53A009B95CAE901FB6F5E3AF4AB1A397D520254B04FB9AE16BA7F90C8C4490AD2111DB1797C92C758297F78BCFF97802C13CD2CA4A7A2270");

            Console.Write("     Result        : ");

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
            string a = "A848A2F9E9FDA61AE3B03B89676CF0689ED2BDFC8D1A384F9F5C529794B31450";
            string b = "13F5873E2F4B31C7E46359B0C77F067504A3D1BC6E0F9440AD5B72BD951B7E7B";
            ulong[] p = new ulong[1];
            ulong[] p1 = new ulong[1];
            p1 = toulong(b);
            p = toulong(a);
            Console.WriteLine("Addition");
            Console.WriteLine(Addition(p, p1));
            Console.WriteLine("\nSubtraction");
            Console.WriteLine(Subtraction(p, p1));
            Console.Write("\nComparison:");
            Console.WriteLine(LongCmp(p, p1));
            Console.Write("\nMul:");
            Console.WriteLine(LongMul(p, p1));
            Console.ReadKey();


        }
    }
}

    
    