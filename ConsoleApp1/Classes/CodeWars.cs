using System.Collections.Generic;
using System;

public static class CodeWars
{
    public static string Tops(string msg)
    {
        int strLen = msg.Length;
        // Console.WriteLine($"str len: {strLen}");
        if (strLen < 3)
        {
            return "";
        }

        int startIndex = 2;
        int subStrLen = 2;
        int jump = 7;

        string str = "";

        while (startIndex < strLen)
        {
            if (startIndex + subStrLen > strLen)
            {
                int diff = strLen - startIndex;
                // Console.WriteLine($"i: {startIndex}, l: {subStrLen}, diff: {diff}, j: {jump}");
                str = msg.Substring(startIndex, diff) + str;
                break;
            }
            else
            {
                str = msg.Substring(startIndex, subStrLen) + str;
                startIndex = startIndex + jump;
                subStrLen = subStrLen + 1;
                jump = jump + 4;
            }
            // Console.WriteLine($"i: {startIndex}, l: {subStrLen}, j: {jump}");
        }

        // Console.WriteLine($"str: {str}");

        return str;
    }

    public static bool validBraces(String braces)
    {
        if (braces.Length % 2 != 0)
        {
            return false;
        }

        int i = 0;
        int j = 1;
        while (braces.Length > 1)
        {
            char c = braces[i];
            char n = braces[j];

            // Console.WriteLine($"c: {c} | n: {n}");

            if (i == 0 && !IsOpen(c))
            {
                return false;
            }

            if (IsOpen(c) && IsOpen(n))
            {
                i++;
                j++;

                if (j >= braces.Length)
                {
                    return false;
                }

                continue;
            }
            else if (IsOpen(c) && !IsOpen(n))
            {
                if (!IsMatch(c, n))
                {
                    // Console.WriteLine($"NOT EQUAL -- c: {c} | n: {n}");
                    return false;
                }
                else
                {
                    braces = braces.Remove(i, 2);
                    i = 0;
                    j = 1;
                }
            }
        }

        return true;
    }

    public static bool IsOpen(char bracket)
    {
        if (bracket == '[' || bracket == '(' || bracket == '{')
        {
            return true;
        }

        return false;
    }

    public static bool IsMatch(char open, char close)
    {
        if (open == '(' && close == ')')
        {
            return true;
        }
        else if (open == '[' && close == ']')
        {
            return true;
        }
        else if (open == '{' && close == '}')
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static int solve(long n)
    {
        Console.WriteLine($"solve: {n}");

        // old answer
        // long highestSum = GetDigitSum(n, 0);
        // long counter = 1;
        // n--;
        // while (n >= counter)
        // {
        //     long remainder = n % 10;

        //     if (remainder < 9)// && counter % 10 < 10)
        //     {
        //         //Console.WriteLine($"skipping: {n} + {counter}");
        //         n -= remainder + 1;
        //         counter += remainder + 1;
        //         continue;
        //     }

        //     long digitSum = GetDigitSum(n, counter);
        //     // Console.WriteLine($"{n} + {counter} = {digitSum}");
        //     if (digitSum > highestSum)
        //     {
        //         highestSum = digitSum;
        //     }

        //     n -= 10;
        //     counter += 10;
        // }

        // long numLen = n.ToString().Length;
        // long firstDigit = (long)(n / Math.Pow(10, numLen - 1)) - 1;
        // long backDigits = 0;
        // long power = 1;
        // for (int i = 0; i < numLen - 1; i++)
        // {
        //     backDigits += 9 * power;
        //     power *= 10;
        // }
        // long newDigits = firstDigit * (long)Math.Pow(10, numLen - 1) + backDigits;
        // long diff = n - newDigits;

        // long highestSum = GetDigitSum(newDigits, diff);

        // return (int)highestSum;

        if (n == 0) return 0;
        var a = n > 9 ? 9 : n;
        var b = (n - a) % 10;
        return (int)(a + b + solve((n - a - b) / 10));
    }

    public static long GetDigitSum(long a, long b)
    {
        long sum = 0;

        while (a != 0)
        {
            sum += a % 10;
            a /= 10;
        }

        while (b != 0)
        {
            sum += b % 10;
            b /= 10;
        }

        return sum;
    }
}
