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
                answer[i] = temp;
                
                if (temp < a[i]) { carry = 1; }
                else if (temp > a[i]) { carry = 0; }
               
            }
            string ans = string.Concat(answer.Select(chunk => chunk.ToString("X").PadLeft(sizeof(ulong) * 2, '0')).Reverse()).TrimStart('0');
                Console.WriteLine("Result we need : " + "D5933046D35EB56566F7A16368903E8E599FE62810F41D53B70DD81D10B9317C");
            
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
                    answer[i] = temp ;
                    borrow = 1;
                }
       
            }
            
            string ans = string.Concat(answer.Select(chunk => chunk.ToString("X").PadLeft(sizeof(ulong) * 2, '0')).Reverse()).TrimStart('0');
                Console.WriteLine("Result we need : "+ "A56CB502A7364D185BA759FB74AECD8C0C5437041234FFE160902DF957C436C2");
            
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
            ulong[] answer = new ulong[(a.Length) * 2];
            ulong[] temp;
            for (int i = 0; i < a.Length; i++)
            {
                temp = LongMulOneDigit(a, b[i]);
                temp = LongShiftDigitsToHigh(temp, i);
                answer = AdditionUlong(answer, temp);
            }
            string ans = string.Concat(answer.Select(chunk => chunk.ToString("X").PadLeft(sizeof(ulong), '0')).Reverse()).TrimStart('0');
            Console.WriteLine("Result we need : " + "11D23CDDEC19C973A5168D729155A589FE5153D6184A847CB7D30ED4D25BD8410E7A9D746A6C071E7FB5C954898564FEE645A00118E4DDBC6B771D5013809243");

            Console.Write("     Result        : ");

            return ans;
        }

//--------------------------------------------------------------------------------------------

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
            string a = "BD7FF2A4BD4A813EE14F7DAF6E9F860D32FA0E9611948E9A8BCF030B343EB41F";
            string b = "18133DA21614342685A823B3F9F0B88126A5D791FF5F8EB92B3ED511DC7A7D5D";
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
            Console.WriteLine(LongMul(p=toulong32(a), p1=toulong32(b)));
            Console.ReadKey();


        }
    }
}

    
    