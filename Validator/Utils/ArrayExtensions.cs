using System;
using System.Text;

namespace InputValidator.Utils
{
    public static class ArrayExtensions
    {
        public static T[] Merge<T>(this T[] t1, T[] t2)
        {
            var result = new T[t1.Length + t2.Length];
            Array.Copy(t1, 0, result, 0, t1.Length);
            Array.Copy(t2, 0, result, t1.Length, t2.Length);
            return result;
        }

        public static string ToStringSequence<T>(this T[] t)
        {
            if (t.Length == 0) return string.Empty;

            var builder = new StringBuilder(t[0].ToString());
            for (int i = 1; i < t.Length; i++)
            {
                builder.Append(", ");
                builder.Append(t[i]);
            }

            return builder.ToString();
        }
    }
}

