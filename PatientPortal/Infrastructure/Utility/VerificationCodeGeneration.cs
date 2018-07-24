using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientPortal.Infrastructure.Utility
{
    public static class VerificationCodeGeneration
    {
        private const string Chars = "1234567890";
        private const int LengthDeviceVerification = 6;
        private const int LengthSerialNumber = 8;
        private static readonly Random srRandom = new Random();
        public static string GenerateDeviceVerificationCode()
        {
            return new string(Enumerable.Repeat(Chars, LengthDeviceVerification)
              .Select(s => s[srRandom.Next(s.Length)]).ToArray());
        }
        public static string GetSerialNumber()
        {
            string code = new string(Enumerable.Repeat(Chars, LengthSerialNumber)
              .Select(s => s[srRandom.Next(s.Length)]).ToArray());
            return code + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
        }
    }
}