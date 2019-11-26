using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Security.Cryptography;




namespace 神之剑
{
    public partial class Form1 : CCSkinMain
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread t = new Thread((ThreadStart)delegate
            {

                printnumberswithdelay();


            });

            t.SetApartmentState(ApartmentState.STA);
            t.IsBackground = true;
            Control.CheckForIllegalCrossThreadCalls = false;
            t.Start();




                       


        }
        /// <summary>
        /// 多线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        void printnumberswithdelay()
        {


            try
            {
                var request = (HttpWebRequest)WebRequest.Create("http://60.169.77.69:81/szjdengluqi/%E5%85%AC%E5%91%8A.txt");
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream(), Encoding.Default).ReadToEnd();
                label1.Text = responseString;


                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                CreateShortcut(desktop + @"\神之剑.lnk", "");

                request = (HttpWebRequest)WebRequest.Create("http://60.169.77.69:81/szjdengluqi/url.txt");
                response = (HttpWebResponse)request.GetResponse();
                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                request = (HttpWebRequest)WebRequest.Create(responseString);
                response = (HttpWebResponse)request.GetResponse();
                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                var responseString1 = Match_Str("分享时间:", "下载", responseString);
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);





                if (responseString1 != ConfigurationManager.AppSettings["ver"])   
                {
                    MessageBox.Show("程序需要更新,请点确认", "系统提示",MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly);
                    config.AppSettings.Settings["ver"].Value = responseString1;     
                    config.Save(ConfigurationSaveMode.Modified);         
                    ConfigurationManager.RefreshSection("appSettings");  
                    string url = Match_Str("class=\"downloadUrl\" value=\"//", "\"", responseString);   
                    System.Diagnostics.Process p = new System.Diagnostics.Process();       
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + @"djupdate.exe";               
                    p.StartInfo.Arguments = @url + " " + AppDomain.CurrentDomain.FriendlyName; 
                    p.Start();                               
                    System.Environment.Exit(0);
                }

            }
            catch (Exception)
            {

            }



        }

        /// <summary>
        /// 文本取中间
        /// </summary>
        /// <param name="startChar"></param>
        /// <param name="endChar"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private string Match_Str(string startChar, string endChar, string str)
        {
            try
            {
                Regex reg = new Regex("(?<=" + startChar + ").*?(?=" + endChar + ")", RegexOptions.IgnoreCase);
                Match m = reg.Match(str);
                if (m.Success)
                {
                    return m.ToString();
                }
            }
            catch (System.Exception ex)
            {

            }
            return "";
        }








        /// <summary>
        /// 创建快捷方式
        /// </summary>
        /// <param name="lnkFilePath"></param>
        /// <param name="args"></param>
        private static void CreateShortcut(string lnkFilePath, string args = "")
        {
            /// <summary>
            /// 为当前正在运行的程序创建一个快捷方式。
            /// </summary>
            /// <param name="lnkFilePath">快捷方式的完全限定路径。</param>
            /// <param name="args">快捷方式启动程序时需要使用的参数。</param>
            var shellType = Type.GetTypeFromProgID("WScript.Shell");
            dynamic shell = Activator.CreateInstance(shellType);
            var shortcut = shell.CreateShortcut(lnkFilePath);
            shortcut.TargetPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            shortcut.Arguments = args;
            shortcut.WorkingDirectory = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            shortcut.Save();
        }



        private void skinButton1_Click(object sender, EventArgs e)
        {



      
            

        }

        private void skinPictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void skinLabel1_Click(object sender, EventArgs e)
        {

        }

        private void skinPictureBox3_Click(object sender, EventArgs e)
        {
            Process.Start("tencent://message/?uin=774045861");
        }

        private void skinPictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start( "http://60.169.77.69:81/szjizhuce.php");
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)

            {

                skinButton3_Click(sender, e);

            }
        }

        private void skinButton3_Click(object sender, EventArgs e)
        {

            if (File.Exists("game.zip"))
            {
                File.Delete("game.zip");
            }

            MD5 mD5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(textBox2.Text);
            byte[] targetData = mD5.ComputeHash(fromData);
            string byte25tring = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte25tring += targetData[i].ToString("x2");
            }
            var request = (HttpWebRequest)WebRequest.Create("http://60.169.77.69:81/logincheck9511.php?u=" + textBox1.Text.Trim() + "&p=" + byte25tring);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            if (responseString.Length <= 3)
            {

                MessageBox.Show("登陆失败,账号或密码错误");
                return;
            }

            string[] data = System.Text.RegularExpressions.Regex.Split(responseString, @"[|]");


            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"game\release_mtp\update_cfg.xml"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"game\release_mtp\update_cfg.xml");
            }

            string srcFileName = AppDomain.CurrentDomain.BaseDirectory + @"download\xys.dll";
            string destFileName = AppDomain.CurrentDomain.BaseDirectory + @"game\release_mtp\update_cfg.xml";
            if (File.Exists(srcFileName))
            {
                File.Move(srcFileName, destFileName);
            }
            //MessageBox.Show(data[0]);
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + @"game\release_mtp\patcher.exe";      
                                                                                                          

           
            p.StartInfo.Arguments = " -u " + data[0] + " -p 1111 -z 1"; 

            p.Start();

            p.WaitForExit();

            File.Move(destFileName, srcFileName);



            System.Environment.Exit(0);


            return;

        }

        private void skinButton1_Click_1(object sender, EventArgs e)
        {

            if (File.Exists("game.zip"))
            {
                File.Delete("game.zip");

            }

           DialogResult dt= MessageBox.Show("版本将强制和远程服务器更新,不然请点 否", "更新", MessageBoxButtons.OKCancel);

            if (dt==DialogResult.OK)
            {

           
               var request = (HttpWebRequest)WebRequest.Create("http://60.169.77.69:81/szjdengluqi/url.txt");
              var  response = (HttpWebResponse)request.GetResponse();
              var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
               
              request = (HttpWebRequest)WebRequest.Create(responseString);
              response = (HttpWebResponse)request.GetResponse();
               responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
               var responseString1 = Match_Str("分享时间:", "下载", responseString);
              Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
              MessageBox.Show("程序需要更新,请点确认", "系统提示");
                config.AppSettings.Settings["ver"].Value = responseString1;     
                config.Save(ConfigurationSaveMode.Modified);        
                ConfigurationManager.RefreshSection("appSettings");  
                string url = Match_Str("class=\"downloadUrl\" value=\"//", "\"", responseString);   
                System.Diagnostics.Process p = new System.Diagnostics.Process();      
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + @"djupdate.exe";              
                p.StartInfo.Arguments = @url + " " + AppDomain.CurrentDomain.FriendlyName;   
                p.Start();  
                          // p.WaitForExit();
                System.Environment.Exit(0);
                return;
            }

        }
    }
}
