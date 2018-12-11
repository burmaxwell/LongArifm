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

        public static ulong[] AdditionUlong(ulong[] a, ulong[] b)
        {
            var maxlenght = Math.Max(a.Length, b.Length);
            Array.Resize(ref a, maxlenght);
            Array.Resize(ref b, maxlenght);
            var answer = new ulong[maxlenght + 1];
         
            ulong carry = 0;
            for (int i = 0; i < maxlenght; i++)
            {
                ulong temp = a[i] + b[i] + carry;
                carry = temp >> 32;
                answer[i] = temp & 0xffffffff;
            }
            answer[a.Length] = carry;
            return answer;
        }

        public static string AdditionUlongSt(string a1, string b1)
        {
            var a = toulong32(a1);
            var b = toulong32(b1);
            var ans = AdditionUlong(a, b);
            return UlongToString(ans);
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
                answer[i] = temp & 0xffffffff;
                if (temp <= a[i])
                {
                    borrow = 0;
                }
                else
                {
                    borrow = 1;
                }
            }
            return answer;
        }

        public static string SubtractionUlongSt(string a1, string b1)
        {
            var a = toulong32(a1);
            var b = toulong32(b1);
            var ans = SubtractionUlong(a, b);
            return UlongToString(ans);
        }

        public static ulong[] LongMulOneDigit(ulong[] a, ulong b)
        {
            ulong temp, carry = 0;
            ulong[] c = new ulong[a.Length + 1];
            for (int i = 0; i < a.Length; i++)
            {
                temp = a[i] * b + carry;
                carry = temp >> 32;
                c[i] = temp & 0xffffffff;
            }
            c[a.Length] = carry;
            return c;
        }

        public static ulong[] LongShiftDigitsToHigh(ulong[] a, int ind)
        {
            ulong[] c = new ulong[a.Length + ind];
            for (int i = 0; i < a.Length; i++)
            {
                c[i + ind] = a[i];
            }
            return c;
        }

        public static ulong[] MulUlong(ulong[] a, ulong[] b)
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
            answer=RemoveHighZeros(answer);
            return answer;
        }

        public static string MulUlongSt(string a1, string b1)
        {
            var a = toulong32(a1);
            var b = toulong32(b1);
            var ans = MulUlong(a, b);
            return UlongToString(ans);
        }


        static int LongCmp(ulong[] a, ulong[] b)
        {
            var maxlenght = Math.Max(a.Length, b.Length);
            Array.Resize(ref a, maxlenght);
            Array.Resize(ref b, maxlenght);
            for (int i = a.Length - 1; i > -1; i--)
            {
                if (a[i] < b[i]) { return -1; }
                if (a[i] > b[i]) { return 1; }
            }
            return 0;
        }

        public static int BitLength(ulong[] a)
        {
            int bit = 0;
            int i = a.Length - 1;
            while (a[i] == 0)
            {
                if (i < 0)
                    return 0;
                i--;
            }

            var ai = a[i];

            while (ai > 0)
            {
                bit++;
                ai = ai >> 1;
            }
            bit = bit + 32 * i;
            return bit;
        }

        public static ulong[] LongShiftBitsToHigh(ulong[] a, int b)
        {
            int t = b / 32;
            int s = b - t * 32;
            ulong n, carry = 0;
            ulong[] C = new ulong[a.Length + t + 1];
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

        public static ulong[]  LongDiv(ulong[] a, ulong[]b)
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
            Q = RemoveHighZeros(Q);
            //r = R;
            return Q;
      
        }

        public static string LongDivSt(string a1, string b1)
        {
            var a = toulong32(a1);
            var b = toulong32(b1);
            var ans = LongDiv(a, b);
            return UlongToString(ans);
        }

        public static string UlongToString(ulong[] a)
            {
            string st = string.Concat(a.Select(chunk => chunk.ToString("X").PadLeft(sizeof(ulong), '0')).Reverse()).TrimStart('0');
            
            return st;
            }

        public static ulong[] RemoveHighZeros(ulong[] c)
        {
            int i = c.Length - 1;
            while (c[i] == 0)
            {
                i--;
            }
            ulong[] result = new ulong[i + 1];
            Array.Copy(c, result, i + 1);
            return result;
        }

        public static ulong[] LongPower(ulong[] a , ulong []b)
        {
            string Pow_b = Program.UlongToString(b);
            ulong[] C = new ulong[1];
            C[0] = 0x1;
            ulong[][] D = new ulong[16][];
            D[0] = new ulong [1] {1};
            D[1]=a;
            for (int i=2;i<16;i++)
            {
                D[i] = MulUlong(D[i - 1], a);
                D[i] = RemoveHighZeros(D[i]);
            }
            
            for(int i = 0; i < Pow_b.Length; i++)
            {
                C = MulUlong(C, D[Convert.ToInt32(Pow_b[i].ToString(), 16)]); 
                if (i != Pow_b.Length - 1)
                {
                    for (int k=1;k<=4;k++)
                    {
                        C = MulUlong(C, C);
                        C = RemoveHighZeros(C);
                    }
                }
            }
            return C;
        }

        public static string LongPowerSt(string a1, string b1)
        {
            var a = toulong32(a1);
            var b = toulong32(b1);
            var ans = LongPower(a, b);
            return UlongToString(ans);
        }


        public static ulong[]  GCD(ulong[] a, ulong[] b)
        {
            ulong[] d = new ulong[Math.Min(a.Length, b.Length)];
            d[0] = 0x1;
            var A = a;
            var B = b;
            ulong[] two = new ulong [1];
            two=toulong32("2");

            while (((A[0] & 1) == 0) && ((B[0] & 1) == 0))
            {
                A = LongDiv(A, two);
                B = LongDiv(B, two);
                d = MulUlong(d, two);
            }

            while ((A[0] & 1) == 0)
            {
                A = LongDiv(A, two);
            }
                
            while (B[0] != 0)
            {
                while ((B[0] & 1) == 0)
                {
                    B = LongDiv(B, two);
                }                   
                var CMP = LongCmp(A,B);
                ulong[] Min, Max;
                if (CMP >= 0)
                {
                    Min = B;
                    Max = A;
                }
                else
                {
                    Min = A;
                    Max = B;
                }
                A = Min;
                B =(SubtractionUlong(Max,Min));
            }

            d = MulUlong(d, A);
            return d;
        }


        public static string GCDI(string a1, string b1)
        {
            var a =toulong32(a1);
            var b = toulong32(b1);
            var ans = GCD(a, b);
            return UlongToString(ans);
        }

        public static ulong[] ShiftBitsToLow(ulong[] a, int bits)
        {
            int t = bits / 32;
            int neededbits = bits - t * 32;
            ulong[] c = new ulong[a.Length-t];
            ulong ai, ai_1 = 0;
            for (int i=t;i<a.Length-1;i++)
            {
                ai = a[i];
                ai_1 = a[i + 1];
                ai = ai >> neededbits;
                ai_1 = ai_1 <<(64 - neededbits);
                ai_1 = ai_1 >>(64 - neededbits);
                c[i - t] = ai | (ai_1<<32-neededbits) ;
            }
            c[a.Length-t-1] = a[a.Length - 1] >> neededbits;
            return c;
        }

        public static string ShiftBitsToLowSt(string a1, int k)
        {
            var a = toulong32(a1);
            var ans = ShiftBitsToLow(a, k);
            return UlongToString(ans);
        }

        public static ulong[] Mod(ulong[] a, ulong[] b)
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
            return R;

        }

        public static string ModSt(string a1, string b1)
        {
            var a = toulong32(a1);
            var b = toulong32(b1);
            var ans = Mod(a, b);
            return UlongToString(ans);
        }

         public static ulong[] AddMod(ulong[] a,ulong[]b,ulong[] mod)
        {
            var ans =Mod(AdditionUlong(a,b),mod);
            return ans;
        }

        public static string AddModSt(string a1, string b1, string mod1)
        { 
            var a = toulong32(a1);
            var b = toulong32(b1);
            var mod = toulong32(mod1);
            var ans = AddMod(a, b, mod);
            return UlongToString(ans);
        }

        static public ulong[] SubMod(ulong[] a, ulong[] b, ulong[] mod)
        {
            var ans = Mod(SubtractionUlong(a, b), mod);
            return ans;
        }

        public static string SubModSt(string a1, string b1, string mod1)
        {
            var a = toulong32(a1);
            var b = toulong32(b1);
            var mod = toulong32(mod1);
            var ans = SubMod(a, b, mod);
            return UlongToString(ans);
        }

        public static ulong[] MulMod(ulong[] a, ulong[] b, ulong[] mod)
        {
            var ans = Mod(MulUlong(a, b), mod);
            return ans;
        }

        public static string MulModSt(string a1, string b1, string mod1)
        {
            var a = toulong32(a1);
            var b = toulong32(b1);
            var mod = toulong32(mod1);
            var ans = MulMod(a, b, mod);
            return UlongToString(ans);
        }

        public static ulong[] Mu_For_Barrett(ulong[] n)
        {
            ulong[] Mu = new ulong[1];
            var k = 2*BitLength(n);
            var b = new ulong[] { 0x01 };
            var up = LongShiftBitsToHigh(b,k);
            Mu = LongDiv(up, n);
            return Mu;
        }

        public static ulong[] BarrettReduction(ulong[] x, ulong[] n)
        { 
            var mu = Mu_For_Barrett(n);
            var k = BitLength(n);
            var q = ShiftBitsToLow(x, k - 1);
            q = MulUlong(q, mu);
            q = ShiftBitsToLow(q, k + 1);

            var r = SubtractionUlong(x, MulUlong(q, n));
            if (LongCmp(r,n) >= 0)
            {
                r = SubtractionUlong(r, n);
            }
            return r;
        }

        public static string BarrettReductionSt(string a1,string mod1)
        {
            var a = toulong32(a1);
            var mod = toulong32(mod1);
            var ans = BarrettReduction(a,mod);            
            return UlongToString(ans);
        }

        public static ulong[] LongModPower(ulong[] a, ulong[] b,ulong[] mod)
        {
            string Pow_b = Program.UlongToString(b);
            ulong[] C = new ulong[1];
            C[0] = 0x1;
            ulong[][] D = new ulong[16][];
            D[0] = new ulong[1] { 1 };
            D[1] = a;
            for (int i = 2; i < 16; i++)
            {
                D[i] = MulUlong(D[i - 1], a);
                D[i] = RemoveHighZeros(D[i]);
            }

            for (int i = 0; i < Pow_b.Length; i++)
            {
                C = MulUlong(C, D[Convert.ToInt32(Pow_b[i].ToString(), 16)]);
                C = Mod(C, mod);
                if (i != Pow_b.Length - 1)
                {
                    for (int k = 1; k <= 4; k++)
                    {
                        C = MulUlong(C, C);
                        C = Mod(C, mod);
                        C = RemoveHighZeros(C);
                    }
                }
            }
            return C;
        }

        public static string ModPowerSt(string a1, string b1, string mod1)
        {
            var a = toulong32(a1);
            var b = toulong32(b1);
            var mod = toulong32(mod1);
            var ans = LongModPower(a, b, mod);
            return UlongToString(ans);
        }

        public static string SymbTobyte(char a)
        {
            if (a == '0')
                return "0000";
            if (a == '1')
                return "0001";
            if (a == '2')
                return "0010";
            if (a == '3')
                return "0011";
            if (a == '4')
                return "0100";
            if (a == '5')
                return "0101";
            if (a == '6')
                return "0110";
            if (a == '7')
                return "0111";
            if (a == '8')
                return "1000";
            if (a == '9')
                return "1001";
            if (a == 'A')
                return "1010";
            if (a == 'B')
                return "1011";
            if (a == 'C')
                return "1100";
            if (a == 'D')
                return "1101";
            if (a == 'E')
                return "1110";
            if (a == 'F')
                return "1111";
            else return "";
        }

        /*public static string[] StringToByteArray(String hex)
        {
            string temp = hex;
            //int Lth = BitLength(toulong32(temp));
            var mass = new string[hex.Length];
            for (int i = 0; i <= hex.Length; i++)
                mass[i] = SymbTobyte(i);
            Array.Reverse(mass);
            return mass;
        }*/



        /*public static ulong[] LongModPowerBarrett(ulong[]a,ulong[]b,ulong[]n)
{

   return C;
}*/

            public static ulong [] Karatsuba(ulong[]x,ulong[]y)
        {
            int n = Math.Min(BitLength(x),BitLength(y));
            int m =n / 2;
            ulong[] b = new ulong[1];
            ulong[] a = new ulong[1];
            ulong[] d = new ulong[1];
            ulong[] c = new ulong[1];

            ulong[] ac = new ulong[1];
            ulong[] bd = new ulong[1];
            ulong[] abcd = new ulong[1];
            ulong[] result = new ulong[1];

            b =ShiftBitsToLow (x , m);//x1
            a = SubtractionUlong( x , LongShiftBitsToHigh(b , m));//x2
            d =ShiftBitsToLow (y, m);//y1
            c =SubtractionUlong( y, LongShiftBitsToHigh(d , m));//y2

            ac = MulUlong(a, c);//x2*y2
            bd = MulUlong(b, d);//x1*y1
            abcd = AdditionUlong(MulUlong(b,c), MulUlong(a,d));

            result = AdditionUlong(AdditionUlong((LongShiftBitsToHigh(bd ,n)),LongShiftBitsToHigh(abcd,m)),ac); 

            return result;
        }

        public static string KaratsubaSt(string a1, string b1)
        {
            var a = toulong32(a1);
            var b = toulong32(b1);
            var ans = Karatsuba(a, b);
            return UlongToString(ans);
        }

        static void Main(string[] args)
        {
            string a = "23423424FFF";
            string b = "141234214AAA";
            string mod = "40F43E45D506C892C36A80D1F1AF9D2427179711B7C12C1B5BAAF8453C2CC710CBF9144D2A6B8CE297529C8CB56C5353FDA7E4C03B3BEC4DBBD96AD75F00011BE784C7F2B99F18539700D7189D4089ED88AE5BC02799F74DA331CC430C22E4F2A638812B3CCC1422AD137B81D90E9040949D12D7BEE4EFB17BA5AAEFC739EA2A";
            ulong[] p = new ulong[1];
            ulong[] p1 = new ulong[1];
            ulong[] mod1 = new ulong[1];
            p1 = toulong32(b);
            p = toulong32(a);
            mod1 = toulong32(mod);//
            
            Console.WriteLine("         "+UlongToString( Karatsuba(p,p1)));
            Console.WriteLine("But need:" + "2C3ADE8970188AB107D556");
            Console.ReadKey();

        }
    }
}

    
    