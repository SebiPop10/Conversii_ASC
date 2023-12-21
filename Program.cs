using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversii_ASC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Introduceti numarul in virgula fixa: ");
            string inr = Console.ReadLine();

            Console.WriteLine("Introduceti b1: ");
            int b1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti b2: ");
            int b2 = int.Parse(Console.ReadLine());

            try
            {
                string rezultat = ConvertBase(inr, b1, b2);
                Console.WriteLine($"Rezultatul conversiei: {rezultat}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Eroare: {ex.Message}");
            }
        }
        static string ConvertBase(string nr, int sourceBase, int targetBase)
        {
            if (sourceBase < 2 || sourceBase > 16 || targetBase < 2 || targetBase > 16)
            {
                throw new ArgumentException("Baza trebuie să fie între 2 și 16.");
            }

            string[] parts = nr.Split('.');
            string integerPart = parts[0];
            string fractionalPart = (parts.Length > 1) ? parts[1] : "";
            string resultInteger = ConvertIntegerPart(integerPart, sourceBase, targetBase);
            string resultFractional = ConvertFractionalPart(fractionalPart, sourceBase, targetBase);
            string result = resultInteger + ((resultFractional.Length > 0) ? "." + resultFractional : "");
            return result;
        }

        static string ConvertIntegerPart(string integerPart, int sourceBase, int targetBase)
        {
            int decimalValue = ConvertFromBaseToDecimal(integerPart, sourceBase);
            string resultInteger = ConvertDecimalToBase(decimalValue, targetBase);
            return resultInteger;
        }

        static string ConvertFractionalPart(string fractionalPart, int sourceBase, int targetBase)
        {
            if (fractionalPart.Length == 0)
            {
                return "";
            }

            double fractionalValue = ConvertFractionalPartToDecimal(fractionalPart, sourceBase);
            string resultFractional = ConvertDecimalToBase(fractionalValue, targetBase);
            return resultFractional;
        }

        static int ConvertFromBaseToDecimal(string number, int sourceBase)
        {
            int decimalValue = 0;
            int power = 0;

            for (int i = number.Length - 1; i >= 0; i--)
            {
                int digitValue = GetDigitValue(number[i]);
                decimalValue += digitValue * (int)Math.Pow(sourceBase, power);
                power++;
            }

            return decimalValue;
        }

        static double ConvertFractionalPartToDecimal(string fractionalPart, int sourceBase)
        {
            double fractionalValue = 0.0;

            for (int i = 0; i < fractionalPart.Length; i++)
            {
                int digit = GetDigitValue(fractionalPart[i]);
                fractionalValue += digit / Math.Pow(sourceBase, i + 1);
            }

            return fractionalValue;
        }

        static string ConvertDecimalToBase(int decimalValue, int targetBase)
        {
            if (decimalValue == 0)
            {
                return "0";
            }

            string result = "";
            while (decimalValue > 0)
            {
                int remainder = decimalValue % targetBase;
                result = GetCharFromDigit(remainder) + result;
                decimalValue /= targetBase;
            }

            return result;
        }

        static string ConvertDecimalToBase(double decimalValue, int targetBase)
        {
            const int maxFractionalDigits = 10;
            string result = "";

            for (int i = 0; i < maxFractionalDigits; i++)
            {
                decimalValue *= targetBase;
                int digit = (int)decimalValue;
                result += GetCharFromDigit(digit);
                decimalValue -= digit;
            }

            return result;
        }

        static int GetDigitValue(char digit)
        {
            if (char.IsDigit(digit))
            {
                return digit - '0';
            }
            else
            {
                return char.ToUpper(digit) - 'A' + 10;
            }
        }

        static char GetCharFromDigit(int digit)
        {
            if (digit < 10)
            {
                return (char)('0' + digit);
            }
            else
            {
                return (char)('A' + digit - 10);
            }
        }

    }
        
    
}
