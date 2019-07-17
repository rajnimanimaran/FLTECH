using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
   public class CodeGeneratorcategory
   
       {
           public string GenerateSequence(int num)
           {
               string str = "";
               char achar;
               int mod;

               while (true)
               {
                   mod = (num % 26) + 65;
                   num = (int)(num / 26);
                   achar = (char)mod;
                   if (achar == 'I' || achar == 'O')
                   {
                       achar++;
                   }
                   str = achar + str;

                   if (num > 0) num--;
                   else if (num == 0) break;
               }
               return str;
           }


           public int NumberFromExcelColumn(string column)
           {
               int retVal = 0;
               string col = column.ToUpper();
               for (int iChar = col.Length - 1; iChar >= 0; iChar--)
               {
                   char colPiece = col[iChar];
                   int colNum = colPiece - 64;
                   retVal = retVal + colNum * (int)Math.Pow(26, col.Length - (iChar + 1));
               }
               return retVal;
           }

           public int NextNumber(int num)
           {
               return num + 1;
           }
       }
    }

