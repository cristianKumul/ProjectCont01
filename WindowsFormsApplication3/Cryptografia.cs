using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication3
{
   class Cryptografia
   {
      private static readonly Encoding Iso88591 = Encoding.GetEncoding("ISO8859-1");
      public string Encriptar(string key, string source, int opc)
      {
         string output = "";
         int j = 0 ;
         List<int> keyList = new List<int>();
         List<int> sourceList = new List<int>();
         Encoding ascii = Encoding.ASCII;

         //string[] split = key.Split(null as string[], StringSplitOptions.RemoveEmptyEntries);
         char[] keyArray = key.ToCharArray();
         char[] sourceArray = source.ToCharArray();

         int[] asciiKey = new int[keyArray.Length];
         int[] asciiText = new int[sourceArray.Length];

         int N = keyArray.Length;
         //string foo = "ž“Ÿ¡m£˜¤žjš";
         //Console.Write(foo);
         for (int i = 0; i < keyArray.Length; i++)
         {
            asciiKey[i] = (int)keyArray[i];

         }
         foreach (char x in keyArray)
         {
            keyList.Add((int)x);

            //Console.WriteLine((int)x);
         }
        
         foreach (char x in sourceArray)
         {
            sourceList.Add((int)x);
         }

         if (opc == 0)
         {
            for (int i = 0; i < sourceArray.Length; i++)
            {
               j = j+1 >= N ? 1 : j+1;


               int temp = sourceList[i] + keyList[j - 1];
               
               if (temp > 255)
               {
                  temp = temp - 255;
               }
               //Console.WriteLine(temp);
               char letra = Convert.ToChar(temp);
               var bytes = new Byte[] { 158 };
               var chars = Iso88591.GetChars(bytes);

               string x = Encoding.ASCII.GetString(new byte[] { 158});

               
               byte[] _bytearray = BitConverter.GetBytes(158);
               char[] _chararray = Encoding.GetEncoding(437).GetChars(_bytearray);
               char result = _chararray[0];
               Console.WriteLine(result);
               output += temp;//System.Text.Encoding.ASCII.GetString(new byte[] { letra });
            }
         }

         if (opc == 1)
         {
            for (int i = 0; i < sourceArray.Length; i++)
            {
               j = j + 1 >= N ? 1 : j + 1;
               //Console.WriteLine(sourceList[i]);
               int temp = sourceList[i] - keyList[j-1];

               if (temp < 0)
               {
                  temp = temp + 255;
               }
               
               output += (char)temp;
            }
         }


         Console.WriteLine(output);
        
         return output;

      }
   }
}
