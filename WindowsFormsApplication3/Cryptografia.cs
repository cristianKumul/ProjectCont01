using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication3
{
   class Cryptografia
   {
      public string Encriptar(string key, string source, int opc)
      {
         string output = "";
         List<int> keyList = new List<int>();
         List<int> sourceList = new List<int>();

         //string[] split = key.Split(null as string[], StringSplitOptions.RemoveEmptyEntries);
         char[] keyArray = key.ToCharArray();
         char[] sourceArray = source.ToCharArray();

         foreach (char x in keyArray)
         {

         }

         Console.Write(keyArray.Length);



         return output;

      }
   }
}
