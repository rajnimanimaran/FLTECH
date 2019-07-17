using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Net.Mail;
using System.Configuration;

namespace BusinessServices.Utility
{
    public class Secures
    {

        public string AuthKey(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            System.Text.StringBuilder res = new System.Text.StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public String Decrypt_String(String Str,String Key)
        {
            string Dec_Text = "";
            try
            {

                String Dec_Key = Key;
                Str = Str.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(Str);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Dec_Key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        Dec_Text = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return Dec_Text;
        }

        public String Encrytp_String(String Str,String Key)
        {
            String Enc_Text ="", Encrypt_Key = Key;
            try
            {
                Byte[] clearBytes = Encoding.Unicode.GetBytes(Str);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Encrypt_Key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        Enc_Text = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return Enc_Text;
        }
    }
}