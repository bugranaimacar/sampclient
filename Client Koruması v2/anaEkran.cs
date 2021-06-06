using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Management;



namespace Client_Koruması_v2
{

    public partial class anaEkran : Form
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll")]
        private static extern Int32 MultiByteToWideChar(UInt32 CodePage, UInt32 dwFlags, [MarshalAs(UnmanagedType.LPStr)] String lpMultiByteStr, Int32 cbMultiByte, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpWideCharStr, Int32 cchWideChar);

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool GetKernelObjectSecurity(IntPtr Handle, int securityInformation, [Out] byte[] pSecurityDescriptor,
        uint nLength, out uint lpnLengthNeeded);


        /* --> Sunucu Ip & Port Ayarı <-- */
        public static string SunucuIP = "localhost";
        public static int sunucuPort = 7777;
        public static int maxSunucuIsmi = 40;
        public static int maxOyuncu = 1000; // 
        public static string ilkdizin = "";
        public static string ipadressofuser = "";
        public static string clientismi = "";
        public static string clientheader = "Client_57684286";

        public static string bliss = "cleo";

        public static string modyarras = "modloader";

        public static string ahmetabi40yasinda;
        public static string windowshashspecialkey = "";
        public static string tombalaci;

        public static int oyunbasladi = 0;

        /****************************************/
        ushort Port = Convert.ToUInt16(sunucuPort);

        int mouseX = 0, mouseY = 0;
        bool mouseDown;
        public static string oyunDizin;
        public static int temp_Oyuncu;


        public static void wait(int milliseconds)
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();
            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
            };
            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
        public anaEkran()
        {
            InitializeComponent();
        }

        private void yenihasholustur()
        {
            tombalaci = "jjjkkkkkk2tttttttttzzzz*****//!!!!!!!!5555555";
            windowshashspecialkey = "";

            Random r = new Random();
            int randomIndex = 0;
            int uzunluk = tombalaci.Length;
            for (int i = uzunluk; i > 0; i--)
            {
                randomIndex = r.Next(0, uzunluk);
                windowshashspecialkey += tombalaci[randomIndex];
                tombalaci = tombalaci.Remove(randomIndex, 1);
                uzunluk = tombalaci.Length;
            }
            hashkeyyerlestir();

        }

        public void hashkeygetir()
        {
            try
            {
                RegistryKey localKey;
                if (Environment.Is64BitOperatingSystem)
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                else
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);

                string value = localKey.OpenSubKey("Software\\WindowsNS").GetValue("windowshashkey").ToString();
                windowshashspecialkey = value;
            }
            catch
            {
                RegistryKey rk = Registry.CurrentUser;
                RegistryKey sk1 = rk.CreateSubKey(@"Software\WindowsNS");
                yenihasholustur();

            }

            
            int result1 = windowshashspecialkey.ToCharArray().Count(c => c == 'j');
            int result2 = windowshashspecialkey.ToCharArray().Count(c => c == 'k');
            int result3 = windowshashspecialkey.ToCharArray().Count(c => c == '2');
            int result4 = windowshashspecialkey.ToCharArray().Count(c => c == 't');
            int result5 = windowshashspecialkey.ToCharArray().Count(c => c == 'z');
            int result6 = windowshashspecialkey.ToCharArray().Count(c => c == '*');
            int result7 = windowshashspecialkey.ToCharArray().Count(c => c == '/');
            int result8 = windowshashspecialkey.ToCharArray().Count(c => c == '!');
            int result9 = windowshashspecialkey.ToCharArray().Count(c => c == '5');
            if (result1 != 3 || result2 != 6 || result3 != 1 || result4 != 9 || result5 != 4 || result6 != 5 || result7 != 2 || result8 != 8 || result9 != 7 || windowshashspecialkey.Length != 45)
            {
                MessageBox.Show("Bir hata oluştu, lütfen Geliştirici ile iletişime Geçin.\nHata Kodu: R42", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(Environment.ExitCode);
            }

        }


        public void hashkeyyerlestir()
        {
            RegistryKey rk = Registry.CurrentUser;
            RegistryKey sk1 = rk.CreateSubKey(@"Software\WindowsNS");
            sk1.SetValue("windowshashkey", windowshashspecialkey);
        }

        public void hesapidgetir()
        {
            try
            {
                RegistryKey localKey;
                if (Environment.Is64BitOperatingSystem)
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                else
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);

                string value = localKey.OpenSubKey("Software\\roleplay").GetValue("hesapad").ToString();
                textBox2.Text = value;
            }
            catch
            {
                RegistryKey rk = Registry.CurrentUser;
                RegistryKey sk1 = rk.CreateSubKey(@"Software\roleplay");
                sk1.SetValue("hesapad", textBox2.Text.ToString());
            }

        }


        


        public void hesapidyerlestir()
        {
            RegistryKey rk = Registry.CurrentUser;
            RegistryKey sk1 = rk.CreateSubKey(@"Software\roleplay");
            sk1.SetValue("hesapad", textBox2.Text.ToString());
        }

       

        public string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            Aes encryptor = Aes.Create();
            encryptor.Mode = CipherMode.CBC;
            byte[] aesKey = new byte[32];
            Array.Copy(key, 0, aesKey, 0, 32);
            encryptor.Key = aesKey;
            encryptor.IV = iv;
            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);
            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
            return cipherText;
        }
        public string DecryptString(string cipherText, byte[] key, byte[] iv)
        {
            Aes encryptor = Aes.Create();
            encryptor.Mode = CipherMode.CBC;
            byte[] aesKey = new byte[32];
            Array.Copy(key, 0, aesKey, 0, 32);
            encryptor.Key = aesKey;
            encryptor.IV = iv;
            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);
            string plainText = String.Empty;
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] plainBytes = memoryStream.ToArray();
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }
            finally
            {
                memoryStream.Close();
                cryptoStream.Close();
            }
            return plainText;
        }

        static void naimxd()
        {
            RegistryKey yey;
            if (Environment.Is64BitOperatingSystem)
                yey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            else
                yey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);

            string yey2 = yey.OpenSubKey("Software\\SAMP").GetValue("gta_sa_exe").ToString();
            string s = yey2.ToString();
            string v = s.Replace("gta_sa.exe", "");
            ilkdizin = v;
            return;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            int x = 0;
            Process[] process = System.Diagnostics.Process.GetProcessesByName("gta_sa");
            if (process.Length > 0)
            {
                for (int i = 0; i < process.Length; i++)
                {
                    x++;
                }
            }
            if (x >= 1)
            {
                MessageBox.Show("gta_sa açıkken Clienti kapatamazsınız !", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else
            {
                
                
                
                
                byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
                string encrypted = this.EncryptString(GetMacAddress(), Patch.anakey, iv);

                string urlAddress2 = "http://" + SunucuIP + "/erdal/lazziya.php";
                using (WebClient client = new WebClient())
                {
                    
                    client.Headers.Add(HttpRequestHeader.Referer, clientheader);
                    NameValueCollection postData = new NameValueCollection() 
       { 
              { "macid", encrypted }
       };

                    string pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress2, postData));
                }
                Environment.Exit(Environment.ExitCode);
            }
        }
        public string cleolar;
        public string asilistesi;
        public string enblistesi;
        public void SunucuBilgileriYukle()
        {

    try
    {
        WebClient enbler = new WebClient();
        enbler.Proxy = null;
        Stream stream = enbler.OpenRead("http://google.com/generate_204");
        StreamReader reader = new StreamReader(stream);
        String content = reader.ReadToEnd();
                 
    }
    catch
    {
        MessageBox.Show("Nova Roleplay Sunucularına şu anda ulaşılamıyor! NO:204", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(Environment.ExitCode);
    }

            try
            {
                WebClient client = new WebClient();
                
                Stream stream = client.OpenRead("http://" + SunucuIP + "/erdal/usercount.php");
                StreamReader reader = new StreamReader(stream);
                String content = reader.ReadToEnd();
                string usercount = content.ToString();
                //label4.Text = usercount;
            }
            catch
            {
                MessageBox.Show("Nova Roleplay Sunucularına şu anda ulaşılamıyor! NO:06", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(Environment.ExitCode);
            }

            try
            {
                WebClient asis = new WebClient();
                asis.Proxy = null;
                asis.Headers.Add(HttpRequestHeader.Referer, clientheader);
                Stream stream = asis.OpenRead("http://" + SunucuIP + "/erdal/asilist.php");
                StreamReader reader = new StreamReader(stream);
                String content = reader.ReadToEnd();
                asilistesi = content.ToString();
            }
            finally
            {
            }

            try
            {
                WebClient cleolar2 = new WebClient();
                cleolar2.Proxy = null;
                cleolar2.Headers.Add(HttpRequestHeader.Referer, clientheader);
                Stream stream = cleolar2.OpenRead("http://" + SunucuIP + "/erdal/" + bliss + "list.php");
                StreamReader reader = new StreamReader(stream);
                String content = reader.ReadToEnd();
                cleolar = content.ToString();
            }
            finally
            {
            }

            try
            {
                WebClient enbler = new WebClient();
                enbler.Proxy = null;
                enbler.Headers.Add(HttpRequestHeader.Referer, clientheader);
                Stream stream = enbler.OpenRead("http://" + SunucuIP + "/erdal/enblist.php");
                StreamReader reader = new StreamReader(stream);
                String content = reader.ReadToEnd();
                enblistesi = content.ToString();
            }
            finally
            {
            }



        }

        public static bool isVirtualMachine()
        {
            const string MICROSOFTCORPORATION = "microsoft corporation";
            const string VMWARE = "vmware";
            const string VIRTUALBOX = "virtualbox";

            foreach (var item in new ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
            {
                string manufacturer = item["Manufacturer"].ToString().ToLower();
                if (manufacturer.Contains(MICROSOFTCORPORATION) || manufacturer.Contains(VMWARE) || manufacturer.Contains(VIRTUALBOX))
                {
                    MessageBox.Show("Clienti sanal sunucularda çalıştıramazsın!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(Environment.ExitCode);
                }


                if (item["Model"] != null)
                {
                    string model = item["Model"].ToString().ToLower();
                    if (model.Contains(MICROSOFTCORPORATION) || model.Contains(VMWARE) || model.Contains(VIRTUALBOX))
                    {
                        MessageBox.Show("Clienti sanal sunucularda çalıştıramazsın!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(Environment.ExitCode);
                    }
                }
            }
            return false;
        }
        string getOSInfo()
        {
            //Get Operating system information.
            OperatingSystem os = Environment.OSVersion;
            //Get version information about the os.
            Version vs = os.Version;

            //Variable to hold our return value
            string operatingSystem = "";

            if (os.Platform == PlatformID.Win32Windows)
            {
                //This is a pre-NT version of Windows
                switch (vs.Minor)
                {
                    case 0:
                        operatingSystem = "95";
                        break;
                    case 10:
                        if (vs.Revision.ToString() == "2222A")
                            operatingSystem = "98SE";
                        else
                            operatingSystem = "98";
                        break;
                    case 90:
                        operatingSystem = "Me";
                        break;
                    default:
                        break;
                }
            }
            else if (os.Platform == PlatformID.Win32NT)
            {
                switch (vs.Major)
                {
                    case 3:
                        operatingSystem = "NT 3.51";
                        break;
                    case 4:
                        operatingSystem = "NT 4.0";
                        break;
                    case 5:
                        if (vs.Minor == 0)
                            operatingSystem = "2000";
                        else
                            operatingSystem = "XP";
                        break;
                    case 6:
                        if (vs.Minor == 0)
                            operatingSystem = "Vista";
                        else if (vs.Minor == 1)
                            operatingSystem = "7";
                        else if (vs.Minor == 2)
                            operatingSystem = "8";
                        else
                            operatingSystem = "8.1";
                        break;
                    case 10:
                        operatingSystem = "10";
                        break;
                    default:
                        break;
                }
            }
            //Make sure we actually got something in our OS check
            //We don't want to just return " Service Pack 2" or " 32-bit"
            //That information is useless without the OS version.
            if (operatingSystem != "")
            {
                //Got something.  Let's prepend "Windows" and get more info.
                operatingSystem = "Windows " + operatingSystem;
                //See if there's a service pack installed.
                if (os.ServicePack != "")
                {
                    //Append it to the OS name.  i.e. "Windows XP Service Pack 3"
                    operatingSystem += " " + os.ServicePack;
                }
                //Append the OS architecture.  i.e. "Windows XP Service Pack 3 32-bit"
                //operatingSystem += " " + getOSArchitecture().ToString() + "-bit";
            }
            //Return the information we've gathered.
            return operatingSystem;
        }

        


        private void anaEkran_Load(object sender, EventArgs e)
        {
            
            isVirtualMachine();
            WebClient client = new WebClient();
            try
            {
                
                client.Headers.Add(HttpRequestHeader.Referer, clientheader);   
                byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
                Stream stream = client.OpenRead("http://" + SunucuIP + "/erdal/polatalemdar.php?macid=" + this.EncryptString(GetMacAddress(), Patch.anakey, iv) + "&cpuid=" + this.EncryptString(GetProcessorId(), Patch.anakey, iv) + "&hddserial=" + this.EncryptString(GetHDDSerialNo(), Patch.anakey, iv));
                StreamReader reader = new StreamReader(stream);
                String content = reader.ReadToEnd();
                string namo = content.ToString();

                string decrypted = this.DecryptString(namo, Patch.anakey, iv);
                if (decrypted == "yes")
                {
                    MessageBox.Show("ERR: Sunucudan Uzaklaştırıldınız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(Environment.ExitCode);
                    return;
                }
                if (decrypted != "no")
                {
                    Process.Start("https://youtu.be/BbqY5nVG8w0?t=16");
                    Environment.Exit(Environment.ExitCode);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Nova Roleplay Sunucularına şu anda ulaşılamıyor! No: 99", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(Environment.ExitCode);
            }

            Process[] process = System.Diagnostics.Process.GetProcessesByName("gta_sa");
            if (process.Length > 0)
            {
                for (int i = 0; i < process.Length; i++)
                {
                    MessageBox.Show("Client açıkken gta_sa açık olamaz !", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    process[i].Kill();
                }
            }

            process = System.Diagnostics.Process.GetProcessesByName("samp");
            if (process.Length > 0)
            {
                for (int i = 0; i < process.Length; i++)
                {
                    MessageBox.Show("Client programı açık durumdayken SA-MP Client açık olamaz !", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    process[i].Kill();
                }
            }
            else
            {
            }
            process = System.Diagnostics.Process.GetProcessesByName("SbieCtrl");
            if (process.Length > 0)
            {
                for (int i = 0; i < process.Length; i++)
                {
                    MessageBox.Show("Clienti sandbox ile çalıştıramazsın!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(Environment.ExitCode);
                }
            }

            

            RandomString();
            SunucuBilgileriYukle();
            ArkaPlanKontrol.Start();
            naimxd();
            dizinkontrol.Start();
            concheck.Start();
            hesapidgetir();
            hashkeygetir();
            getipadress();
            clientupdater.Start();
            //AntiSuspend.RunWorkerAsync();
            GenelHileKontrol.Start();
            ModLoaderControl.Start();

        }

      

        public void getipadress()
        {
            try
            {
                WebClient client = new WebClient();
                
                client.Headers.Add(HttpRequestHeader.Referer, clientheader);
                Stream stream = client.OpenRead("http://" + SunucuIP + "/erdal/getip.php");
                StreamReader reader = new StreamReader(stream);
                String content = reader.ReadToEnd();
                ipadressofuser = content.ToString();
            }

            catch
            {
                MessageBox.Show("Nova Roleplay Sunucularına ulaşılamıyor! No: 136", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(Environment.ExitCode);
            }

        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
            mouseDown = true;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.SetDesktopLocation(MousePosition.X - mouseX, MousePosition.Y - mouseY);
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void HileKontrol_Tick(object sender, EventArgs e)
        {
            HileKontrol.Stop();

            label9.Text = "Hile kontrolü yapılıyor...";
            string hileIsimler = "Aşağıdaki Hileleri silin !\n\n";
            int hileCount = 0;

            try
            {
                if (bliss != "cleo")
                {
                    Process.Start("https://youtu.be/BbqY5nVG8w0?t=16");
                    wait(1500);
                    Environment.Exit(Environment.ExitCode);

                }
                if (modyarras != "modloader")
                {
                    Process.Start("https://youtu.be/BbqY5nVG8w0?t=16");
                    wait(1500);
                    Environment.Exit(Environment.ExitCode);

                }
            }

            catch
            {
                
                wait(2000);
                Process.Start("https://youtu.be/BbqY5nVG8w0?t=16");
                Process[] process = System.Diagnostics.Process.GetProcessesByName("gta_sa");
                if (process.Length > 0)
                {
                    for (int i = 0; i < process.Length; i++)
                    {
                        process[i].Kill();
                    }
                }
                try
                {
                    
                    
                    
                    
                    byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
                    string encrypted = this.EncryptString(GetMacAddress(), Patch.anakey, iv);

                    string urlAddress2 = "http://" + SunucuIP + "/erdal/lazziya.php";
                    using (WebClient client = new WebClient())
                    {
                        
                        client.Headers.Add(HttpRequestHeader.Referer, clientheader);
                        NameValueCollection postData = new NameValueCollection() 
       { 
              { "macid", encrypted }
       };

                        string pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress2, postData));
                    }

                }

                catch
                {
                    MessageBox.Show("Nova Roleplay Sunucularına şu anda ulaşılamıyor! No: 315", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(Environment.ExitCode);
                }
                Environment.Exit(Environment.ExitCode);
            }


            List<string> asiIcerik = new List<string>();
            string[] asiler = Directory.GetFiles(oyunDizin, "*.asi");
            foreach (string asiEkle in asiler)
            {
                string dosyaIsim = Path.GetFileName(asiEkle);
                asiIcerik.Add(dosyaIsim);
            }

            foreach (string asiKontrol in asiIcerik)
            {
                if (File.Exists(oyunDizin + asiKontrol))
                {

                    long filesize = new FileInfo(oyunDizin + "\\" + asiKontrol).Length;
                    string ahmet = filesize.ToString();
                    try
                    {
                        
                        byte[] iv2 = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

                        string decrypted2 = this.DecryptString(asilistesi, Patch.anakey, iv2);



                        if (decrypted2.ToLower().Contains(ahmet) == false)
                        {
                            hileIsimler += "\\" + asiKontrol + "\n";
                            hileCount++;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Nova Roleplay sunucularına ulaşılaı! No: 882", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(Environment.ExitCode);
                    }
                }
            }


            List<string> sfIcerik = new List<string>();
            string[] sfler = Directory.GetFiles(oyunDizin, "*.sf");
            foreach (string sfEkle in sfler)
            {
                string dosyaIsim = Path.GetFileName(sfEkle);
                sfIcerik.Add(dosyaIsim);
            }

            foreach (string sfKontrol in sfIcerik)
            {
                hileIsimler += sfKontrol + "\n";
                hileCount++;
            }

            List<string> d3d9Icerik = new List<string>();
            string[] d3d9ler = Directory.GetFiles(oyunDizin, "*d3d9.dll");
            foreach (string d3d9ekle in d3d9ler)
            {
                string dosyaIsim = Path.GetFileName(d3d9ekle);
                d3d9Icerik.Add(dosyaIsim);
            }

            foreach (string d3d9kontrol in d3d9Icerik)
            {
                if (File.Exists(oyunDizin + d3d9kontrol))
                {

                    long filesize = new FileInfo(oyunDizin + "\\" + d3d9kontrol).Length;
                    string ahmet = filesize.ToString();

                    try
                    {

                        
                        byte[] iv2 = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

                        string decrypted2 = this.DecryptString(enblistesi, Patch.anakey, iv2);

                        if (decrypted2.ToLower().Contains(ahmet) == false)
                        {
                            hileIsimler += "\\" + d3d9kontrol + "\n";
                            hileCount++;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Nova Roleplay sunucularına ulaşılamadı! No:516", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(Environment.ExitCode);
                    }
                }
            }

            bool dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\" + bliss);
            if (dizinVarmi)
            {
                string[] fileArray = Directory.GetFiles(oyunDizin + "\\" + bliss + "\\", "*.cs");
                foreach (string file in fileArray)
                {
                    string fileName = Path.GetFileName(file);
                    long filesize = new FileInfo(oyunDizin + "\\" + bliss + "\\" + fileName).Length;
                    string ahmet = filesize.ToString();
                    try
                    {
                        
                        byte[] iv2 = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

                        string decrypted2 = this.DecryptString(cleolar, Patch.anakey, iv2);


                        if (decrypted2.ToLower().Contains(ahmet) == false)
                        {
                            hileIsimler += "\\cleo\\" + fileName + "\n";
                            hileCount++;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Nova Roleplay sunucularına ulaşılamadı! No:834", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(Environment.ExitCode);
                    }

                }
            }


            dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\SAMPFUNCS");
            if (dizinVarmi)
            {
                hileIsimler += "SAMPFUNCS\n";
                hileCount++;
            }

            dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\BlueEclipseMod");
            if (dizinVarmi)
            {
                hileIsimler += "BlueEclipseMod\n";
                hileCount++;
            }

            dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\OverLight_Mod");
            if (dizinVarmi)
            {
                hileIsimler += "OverLight_Mod\n";
                hileCount++;
            }

            dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\mod_sa");
            if (dizinVarmi)
            {
                hileIsimler += "mod_sa\n";
                hileCount++;
            }

            dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\sik_sa");
            if (dizinVarmi)
            {
                hileIsimler += "sik_sa\n";
                hileCount++;
            }

            dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\berksa");
            if (dizinVarmi)
            {
                hileIsimler += "berksa\n";
                hileCount++;
            }

            dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\sobreit");
            if (dizinVarmi)
            {
                hileIsimler += "sobreit\n";
                hileCount++;
            }

            if (File.Exists(oyunDizin + "\\gta_sa.exe"))
            {
                foreach (string text4 in File.ReadLines(oyunDizin + "\\gta_sa.exe"))
                {
                    if (text4.Contains("//RenderWare/RW36Active/rwsdk/src/babinfrm.c#1") && !text4.Contains("\0d3d9.dll\0"))
                    {
                        hileIsimler += "gta_sa.exe\n";
                        hileCount++;
                    }
                }
            }

            if (File.Exists(oyunDizin + "\\vorbisFile.dll"))
            {
                foreach (string text5 in File.ReadLines(oyunDizin + "\\vorbisFile.dll"))
                {
                    if (text5.Contains("[excludes]") && !text5.Contains("\0*.asi\0"))
                    {
                        hileIsimler += "vorbisFile.dll\n";
                        hileCount++;
                    }
                }
            }

            if (hileCount >= 1)
            {
                label9.Text = "Hile dosyaları bulundu oyun başlatılamıyor.";
                try
                {
                    
                    
                    
                    
                    byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
                    string encrypted = this.EncryptString(GetMacAddress(), Patch.anakey, iv);

                    string urlAddress2 = "http://" + SunucuIP + "/erdal/lazziya.php";
                    using (WebClient client = new WebClient())
                    {
                        
                        client.Headers.Add(HttpRequestHeader.Referer, clientheader);
                        NameValueCollection postData = new NameValueCollection() 
       { 
              { "macid", encrypted }
       };

                        string pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress2, postData));
                    }

                }

                catch
                {
                    MessageBox.Show("Nova Roleplay Sunucularına şu anda ulaşılamıyor! No:461", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(Environment.ExitCode);
                }
                Process[] process = System.Diagnostics.Process.GetProcessesByName("gta_sa");
                if (process.Length > 0)
                {
                    for (int i = 0; i < process.Length; i++)
                    {
                        process[i].Kill();
                    }
                }
                this.Activate();
                MessageBox.Show(hileIsimler, "Hata: " + hileCount + " Hile var !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Activate();
                textBox2.ReadOnly = false;

            }
            else
            {
                label9.Text = "Hile bulunmadı, oyun başlıyor...";
                int x = 0;
                Process[] process = System.Diagnostics.Process.GetProcessesByName("gta_sa");
                if (process.Length > 0)
                {
                    for (int i = 0; i < process.Length; i++)
                    {
                        x++;
                    }
                }

                clientac.RunWorkerAsync();

            }
        }

        public static String GetProcessorId()
        {
            ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * From Win32_BaseBoard");
            foreach (ManagementObject getserial in MOS.Get())
            {
                ahmetabi40yasinda = getserial["SerialNumber"].ToString();
            }
            return ahmetabi40yasinda;
        }
        private void ArkaPlanKontrol_Tick(object sender, EventArgs e)
        {

            if (oyunbasladi == 1)
            {
                return;
            }

            Process[] process = System.Diagnostics.Process.GetProcessesByName("samp");
            if (process.Length > 0)
            {
                for (int i = 0; i < process.Length; i++)
                {
                    process[i].Kill();
                    MessageBox.Show("Client programı açık durumdayken SA-MP Client açık olamaz !", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        public static String GetHDDSerialNo()
        {
            ManagementClass mangnmt = new ManagementClass("Win32_LogicalDisk");
            ManagementObjectCollection mcol = mangnmt.GetInstances();
            string result = "";
            foreach (ManagementObject strt in mcol)
            {
                result += Convert.ToString(strt["VolumeSerialNumber"]);
            }
            return result;
        }
        private string GetMacAddress()
        {
            string macAddresses = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }

            return macAddresses;
        }



        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void dizinkontrol_Tick_1(object sender, EventArgs e)
        {
            RegistryKey localKey;
            if (Environment.Is64BitOperatingSystem)
                localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            else
                localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);

            string value = localKey.OpenSubKey("Software\\SAMP").GetValue("gta_sa_exe").ToString();
            try
            {
                if (Environment.Is64BitOperatingSystem)
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                else
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);

                string value2 = localKey.OpenSubKey("Software\\SAMP").GetValue("gta_sa_exe").ToString();
                if (value2 == "")
                {
                    MessageBox.Show("Samp daha önce hiç kurulmamış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(Environment.ExitCode);
                }
            }
            catch
            {
                MessageBox.Show("Samp daha önce hiç kurulmamış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(Environment.ExitCode);
            }
            string naimdizin = "";
            string s = value.ToString();
            string v = s.Replace("gta_sa.exe", "");
            naimdizin = v;

            if (ilkdizin != naimdizin)
            {
                Process[] process = System.Diagnostics.Process.GetProcessesByName("gta_sa");
                if (process.Length > 0)
                {
                    for (int i = 0; i < process.Length; i++)
                    {
                        process[i].Kill();
                    }
                }
                dizinkontrol.Stop();
                try
                {
                    
                    
                    
                    
                    byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
                    string encrypted = this.EncryptString(GetMacAddress(), Patch.anakey, iv);

                    string urlAddress2 = "http://" + SunucuIP + "/erdal/lazziya.php";
                    using (WebClient client = new WebClient())
                    {
                        
                        client.Headers.Add(HttpRequestHeader.Referer, clientheader);
                        NameValueCollection postData = new NameValueCollection() 
       { 
              { "macid", encrypted }
       };

                        string pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress2, postData));
                    }
                }
                catch
                {
                    MessageBox.Show("Nova Roleplay Sunucularına ulaşılamadı! No:928", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(Environment.ExitCode);
                }
                MessageBox.Show("Dizin değişikliği algılandı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(Environment.ExitCode);

            }
            dizinkontrol.Start();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

            try
            {
                textBox2.ReadOnly = true;
                WebClient client2 = new WebClient();
                client2.Proxy = null;
                client2.Headers.Add(HttpRequestHeader.Referer, clientheader);
                Stream stream2 = client2.OpenRead("http://" + SunucuIP + "/erdal/cakir.php");
                StreamReader reader2 = new StreamReader(stream2);
                String content2 = reader2.ReadToEnd();
                string namo2 = content2.ToString();

                
                byte[] iv2 = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

                string decrypted2 = this.DecryptString(namo2, Patch.anakey, iv2);

                
                if (decrypted2 != "PD0.1")
                {
                    MessageBox.Show("Clientin yeni sürümü mevcut lütfen sitemizden indirin Güncel Sürüm: " + decrypted2, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(Environment.ExitCode);
                }
            }

            catch
            {
                MessageBox.Show("Nova Roleplay Sunucularına şu anda ulaşılamıyor! No:571", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(Environment.ExitCode);
            }

            try
            {
                Query.Query sQuery = new Query.Query(SunucuIP, sunucuPort);
                sQuery.Send('p');  
            }
            catch
            {
                MessageBox.Show("Nova Roleplay Oyun Sunucusuna ulaşılamıyor", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(Environment.ExitCode);
            }

            

            getipadress();
            WebClient client = new WebClient();
            
            client.Headers.Add(HttpRequestHeader.Referer, clientheader);
            
            
            
            
            byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

            Stream stream = client.OpenRead("http://" + SunucuIP + "/erdal/polatalemdar.php?macid=" + this.EncryptString(GetMacAddress(), Patch.anakey, iv) + "&cpuid=" + this.EncryptString(GetProcessorId(), Patch.anakey, iv) + "&hddserial=" + this.EncryptString(GetHDDSerialNo(), Patch.anakey, iv));
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();
            
            string namo = content.ToString();
            string decrypted = this.DecryptString(namo, Patch.anakey, iv);
            if (decrypted == "yes")
            {
                MessageBox.Show("ERR: Sunucudan Uzaklaştırıldınız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(Environment.ExitCode);
                return;
            }
            if (decrypted != "no")
            {
                Process.Start("https://youtu.be/BbqY5nVG8w0?t=16");
                Environment.Exit(Environment.ExitCode);
                return;
            }
            string metin = textBox2.Text.Trim();
            textBox2.Text = metin;
            if (String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("İsim kısmını boş bırakmayın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.ReadOnly = false;
                return;
            }
            if (textBox2.TextLength < 3)
            {
                MessageBox.Show("Oyuncu İsmi en az 3 karakterli olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.ReadOnly = false;
                return;
            }
            int x = 0;
            Process[] process2 = System.Diagnostics.Process.GetProcessesByName("gta_sa");
            if (process2.Length > 0)
            {
                for (int i = 0; i < process2.Length; i++)
                {
                    x++;
                }
            }
            if (x >= 1)
            {
                MessageBox.Show("GTA SA Açıkken oyuna tekrar giremezsin !", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            RegistryKey yey;
            if (Environment.Is64BitOperatingSystem)
                yey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            else
                yey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);

            string yey2 = yey.OpenSubKey("Software\\SAMP").GetValue("gta_sa_exe").ToString();
            string s2 = yey2.ToString();
            string v2 = s2.Replace("gta_sa.exe", "");
            string dizin3 = v2;
            string naim = dizin3 + "\\SAMP\\";
            String dizin = "";
            label9.Text = "Anahtar dizini kontrol ediliyor...";
            try
            {
                RegistryKey localKey;
                if (Environment.Is64BitOperatingSystem)
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                else
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);

                string value = localKey.OpenSubKey("Software\\SAMP").GetValue("gta_sa_exe").ToString();

                string s = value.ToString();
                string v = s.Replace("gta_sa.exe", "");
                dizin = v;
                oyunDizin = v;
            }
            catch
            {
                label9.Text = "Anahtar dizini doğrulandı.";
                textBox2.Enabled = true;
            }
            finally
            {

                label9.Text = "Anahtar dizini doğruland, oyun başlatılıyor...";
                Process process = new Process();
                oyunbasladi = 1;
                process.StartInfo.FileName = dizin + "\\samp.exe";
                process.StartInfo.Arguments = "-c -h" + SunucuIP + "-p" + sunucuPort + "-n" + "Client_" + clientismi;
                process.Start();
                HileKontrol.Stop();
                HileKontrol.Start();
                hesapidyerlestir();
                wait(5000);
                oyunbasladi = 0;
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {


        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {


        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {


        }

        private void concheck_Tick(object sender, EventArgs e)
        {
            try
            {
                WebClient client2 = new WebClient();
                client2.Proxy = null;
                Stream stream = client2.OpenRead("http://google.com/generate_204");
            }

            catch
            {
                MessageBox.Show("İnternet Bağlantısı yok!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(Environment.ExitCode);
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public static void RandomString()
        {
            Random ran = new Random();

            String b = "012345HIJjklmnorpsjaifdlaqxzwtyuKLM0123456789NOPQRS0123456789TUVWXYZ0123456789";

            int length = 8;

            

            for (int i = 0; i < length; i++)
            {
                int a = ran.Next(26);
                clientismi = clientismi + b.ElementAt(a);
            }
        }

      

        private void label8_Click(object sender, EventArgs e)
        {

        }


        

        private void GenelHileKontrol_Tick(object sender, EventArgs e)
        {
            GenelHileKontrol.Stop();




            string hileIsimler = "Aşağıdaki Hileleri silin !\n\n";
            int hileCount = 0;

            try
            {
                RegistryKey localKey;
                if (Environment.Is64BitOperatingSystem)
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                else
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);

                string value = localKey.OpenSubKey("Software\\SAMP").GetValue("gta_sa_exe").ToString();

                string s = value.ToString();
                string v = s.Replace("gta_sa.exe", "");

                oyunDizin = v;
            }
            catch
            {

            }




            List<string> asiIcerik = new List<string>();
            string[] asiler = Directory.GetFiles(oyunDizin, "*.asi");
            foreach (string asiEkle in asiler)
            {
                string dosyaIsim = Path.GetFileName(asiEkle);
                asiIcerik.Add(dosyaIsim);
            }

            foreach (string asiKontrol in asiIcerik)
            {
                if (File.Exists(oyunDizin + asiKontrol))
                {

                    long filesize = new FileInfo(oyunDizin + "\\" + asiKontrol).Length;
                    string ahmet = filesize.ToString();


                    
                    byte[] iv2 = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

                    string decrypted2 = this.DecryptString(asilistesi, Patch.anakey, iv2);


                    try
                    {
                        if (decrypted2.ToLower().Contains(ahmet) == false)
                        {
                            hileIsimler += "\\" + asiKontrol + "\n";
                            hileCount++;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Nova Roleplay sunucularına ulaşılamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(Environment.ExitCode);
                    }
                }
            }


            List<string> d3d9Icerik = new List<string>();
            string[] d3d9ler = Directory.GetFiles(oyunDizin, "*d3d9.dll");
            foreach (string d3d9ekle in d3d9ler)
            {
                string dosyaIsim = Path.GetFileName(d3d9ekle);
                d3d9Icerik.Add(dosyaIsim);
            }



            foreach (string d3d9kontrol in d3d9Icerik)
            {
                if (File.Exists(oyunDizin + d3d9kontrol))
                {

                    long filesize = new FileInfo(oyunDizin + "\\" + d3d9kontrol).Length;
                    string ahmet = filesize.ToString();

                    try
                    {

                        
                        byte[] iv2 = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

                        string decrypted2 = this.DecryptString(enblistesi, Patch.anakey, iv2);


                        if (decrypted2.ToLower().Contains(ahmet) == false)
                        {
                            hileIsimler += "\\" + d3d9kontrol + "\n";
                            hileCount++;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Nova Roleplay sunucularına ulaşılamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(Environment.ExitCode);
                    }
                }
            }


            List<string> sfIcerik = new List<string>();
            string[] sfler = Directory.GetFiles(oyunDizin, "*.sf");
            foreach (string sfEkle in sfler)
            {
                string dosyaIsim = Path.GetFileName(sfEkle);
                sfIcerik.Add(dosyaIsim);
            }

            foreach (string sfKontrol in sfIcerik)
            {
                hileIsimler += sfKontrol + "\n";
                hileCount++;
            }




            bool dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\" + bliss);
            if (dizinVarmi)
            {
                string[] fileArray = Directory.GetFiles(oyunDizin + "\\" + bliss + "\\", "*.cs");
                foreach (string file in fileArray)
                {
                    string fileName = Path.GetFileName(file);
                    long filesize = new FileInfo(oyunDizin + "\\" + bliss + "\\" + fileName).Length;
                    string ahmet = filesize.ToString();



                    try
                    {

                        
                        
                        
                        
                        byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };


                        string decrypted = this.DecryptString(cleolar, Patch.anakey, iv);



                        if (decrypted.ToLower().Contains(ahmet) == false)
                        {
                            hileIsimler += "\\" + bliss + "\\" + fileName + "\n";
                            hileCount++;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Nova Roleplay sunucularına ulaşılamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(Environment.ExitCode);
                    }

                }
            }


            dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\SAMPFUNCS");
            if (dizinVarmi)
            {
                hileIsimler += "SAMPFUNCS\n";
                hileCount++;
            }

            dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\berksa");
            if (dizinVarmi)
            {
                hileIsimler += "berksa\n";
                hileCount++;
            }

            dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\BlueEclipseMod");
            if (dizinVarmi)
            {
                hileIsimler += "BlueEclipseMod\n";
                hileCount++;
            }

            dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\OverLight_Mod");
            if (dizinVarmi)
            {
                hileIsimler += "OverLight_Mod\n";
                hileCount++;
            }

            dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\mod_sa");
            if (dizinVarmi)
            {
                hileIsimler += "mod_sa\n";
                hileCount++;
            }

            dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\sik_sa");
            if (dizinVarmi)
            {
                hileIsimler += "sik_sa\n";
                hileCount++;
            }

            dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\sobreit");
            if (dizinVarmi)
            {
                hileIsimler += "sobreit\n";
                hileCount++;
            }


            if (hileCount >= 1)
            {
                Process[] process = System.Diagnostics.Process.GetProcessesByName("gta_sa");
                if (process.Length > 0)
                {
                    for (int i = 0; i < process.Length; i++)
                    {
                        process[i].Kill();
                    }
                }
                DialogResult Soru = MessageBox.Show(hileIsimler, "Hata: " + hileCount + " Hile var !", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }



            foreach (var process in Process.GetProcessesByName("HTTPDebuggerUIs"))
            {
                GenelHileKontrol.Stop();
                Process.Start("https://youtu.be/BbqY5nVG8w0?t=15");
                process.Kill();
                MessageBox.Show("Olacak O kadar");
                Environment.Exit(Environment.ExitCode);
            }

            foreach (var process in Process.GetProcessesByName("ProcessHackers"))
            {
                GenelHileKontrol.Stop();
                Process.Start("https://youtu.be/BbqY5nVG8w0?t=15");
                process.Kill();
                MessageBox.Show("Olacak O kadar");
                Environment.Exit(Environment.ExitCode);
            }

            foreach (var process in Process.GetProcessesByName("cheatengine-x86_64"))
            {
                GenelHileKontrol.Stop();
                Process.Start("https://youtu.be/BbqY5nVG8w0?t=15");
                process.Kill();
                MessageBox.Show("Olacak O kadar");
                Environment.Exit(Environment.ExitCode);
            }

            GenelHileKontrol.Start();
        }


        


        private void label2_Click(object sender, EventArgs e)
        {

        }
        

        

 

        private void CheatEngine_Tick(object sender, EventArgs e)
        {


            
                 try
            {
                if (bliss != "cleo")
                {
                    Process.Start("https://youtu.be/BbqY5nVG8w0?t=16");
                    wait(1500);
                    Environment.Exit(Environment.ExitCode);

                }
                if (modyarras != "modloader")
                {
                    Process.Start("https://youtu.be/BbqY5nVG8w0?t=16");
                    wait(1500);
                    Environment.Exit(Environment.ExitCode);

                }
            }

            catch
            {
                
                wait(2000);
                Process.Start("https://youtu.be/BbqY5nVG8w0?t=16");
                Process[] process = System.Diagnostics.Process.GetProcessesByName("gta_sa");
                if (process.Length > 0)
                {
                    for (int i = 0; i < process.Length; i++)
                    {
                        process[i].Kill();
                    }
                }

            }
        }

        private void clientupdater_Tick(object sender, EventArgs e)
        {
            clientupdater.Stop();

            try
            {
                Query.Query sQuery = new Query.Query(SunucuIP, sunucuPort);
                sQuery.Send('p');
            }
            catch
            {
                MessageBox.Show("Nova Roleplay Oyun Sunucusuna ulaşılamıyor", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(Environment.ExitCode);
            }

            WebClient client = new WebClient();
            try
            {
                
                client.Headers.Add(HttpRequestHeader.Referer, clientheader);
                byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
                Stream stream = client.OpenRead("http://" + SunucuIP + "/erdal/identity.php?clientname=" + this.EncryptString("Client_" + clientismi, Patch.anakey, iv) + "&windowshash=" + windowshashspecialkey);
                StreamReader reader = new StreamReader(stream);
                String content = reader.ReadToEnd();
                string namo = content.ToString();
                string decrypted = this.DecryptString(namo, Patch.anakey, iv);
                 if (decrypted == "yes")
                {
                    MessageBox.Show("ERR: Sunucudan Uzaklaştırıldınız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(Environment.ExitCode);
                    return;
                }
                if (decrypted != "no")
                {
                    Process.Start("https://youtu.be/EOn9rRSdBNU");
                    Environment.Exit(Environment.ExitCode);
                    return;
                }


               }
            catch
            {

            }
            
            clientupdater.Start();
            
            
        }


        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            DialogResult client = MessageBox.Show("Hdd Serial: " + GetHDDSerialNo() + "\n" + "Macid: " + GetMacAddress() + "\n" + "Cpuid: " + GetProcessorId(), "Nova Roleplay Client");
        }


        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void pictureBox2_Click_2(object sender, EventArgs e)
        {
            IntPtr ClientHwnd = this.Handle;
            ShowWindow(ClientHwnd, 6);
        }

        private void ModLoaderControl_Tick(object sender, EventArgs e)
        {
            ModLoaderControl.Stop();
            bool dizinVarmi = System.IO.Directory.Exists(oyunDizin + "\\" + modyarras);
            
          
            string hileIsimler = "Aşağıdaki Hileleri silin !\n\n";
            int hileCount = 0;
            try
            {
                RegistryKey localKey;
                if (Environment.Is64BitOperatingSystem)
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                else
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);

                string value = localKey.OpenSubKey("Software\\SAMP").GetValue("gta_sa_exe").ToString();

                string s = value.ToString();
                string v = s.Replace("gta_sa.exe", "");

                oyunDizin = v;
            }
            catch { }

            List<string> d3d9Icerik = new List<string>();
            if (dizinVarmi)
            {
                string[] d3d9ler = Directory.GetFiles(oyunDizin + "\\" + modyarras + "\\", "*d3d9.dll");
                foreach (string d3d9ekle in d3d9ler)
                {
                    string dosyaIsim = Path.GetFileName(d3d9ekle);
                    d3d9Icerik.Add(dosyaIsim);
                }
            }


            foreach (string d3d9kontrol in d3d9Icerik)
            {
                if (File.Exists(oyunDizin + "\\" + modyarras + "\\" + d3d9kontrol))
                {

                    long filesize = new FileInfo(oyunDizin + "\\" + modyarras + "\\" + d3d9kontrol).Length;
                    string ahmet = filesize.ToString();

                    try
                    {


                        byte[] iv2 = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

                        string decrypted2 = this.DecryptString(enblistesi, Patch.anakey, iv2);


                        if (decrypted2.ToLower().Contains(ahmet) == false)
                        {
                            hileIsimler += "\\modloader\\" + d3d9kontrol + "\n";
                            hileCount++;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Nova Roleplay sunucularına ulaşılamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(Environment.ExitCode);
                    }
                }
            }

            
            if (dizinVarmi)
            {
                string[] fileArray = Directory.GetFiles(oyunDizin + "\\" + modyarras + "\\", "*.cs");
                foreach (string file in fileArray)
                {
                    string fileName = Path.GetFileName(file);
                    long filesize = new FileInfo(oyunDizin + "\\" + modyarras + "\\" + fileName).Length;
                    string ahmet = filesize.ToString();

                    

                    try
                    {





                        byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };


                        string decrypted = this.DecryptString(cleolar, Patch.anakey, iv);


                        if (decrypted.ToLower().Contains(ahmet) == false)
                        {
                            hileIsimler += "\\" + modyarras + "\\" + fileName + "\n";
                            hileCount++;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Nova Roleplay sunucularına ulaşılamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(Environment.ExitCode);
                    }

                }
            }

            if (hileCount >= 1)
            {
                
                Process[] process = System.Diagnostics.Process.GetProcessesByName("gta_sa");
                if (process.Length > 0)
                {
                    for (int i = 0; i < process.Length; i++)
                    {
                        process[i].Kill();
                    }
                }
                DialogResult Soru = MessageBox.Show(hileIsimler, "Hata: " + hileCount + " Hile var !", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }


            
            ModLoaderControl.Start();

            
        }

        private void clientac_DoWork(object sender, DoWorkEventArgs e)
        {
            try
                {
                    string username = textBox2.Text;

                    byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
                    string urlAddress = "http://" + SunucuIP + "/erdal/olacakokadar.php";
                    using (WebClient client = new WebClient())
                    {
                        
                        client.Headers.Add(HttpRequestHeader.Referer, clientheader);
                        NameValueCollection postData = new NameValueCollection() 
       { 
              { "username", EncryptString(username, Patch.anakey, iv) },
              { "clientname", EncryptString("Client_" + clientismi, Patch.anakey, iv) },
              { "ipadress", EncryptString(ipadressofuser, Patch.anakey, iv) },
              { "osname", EncryptString(getOSInfo(), Patch.anakey, iv) },
              { "macid", EncryptString(GetMacAddress(), Patch.anakey, iv) },
              { "cpuid", EncryptString(GetProcessorId(), Patch.anakey, iv) },
              { "hddserial", EncryptString(GetHDDSerialNo(), Patch.anakey, iv) }
       };
                        string pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    };
                    textBox2.ReadOnly = false;
                }
                catch
                {
                    
                }
        }


        private void AntiSuspend_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (AntiSuspend.IsBusy == false)
            {
                wait(300);
                AntiSuspend.RunWorkerAsync();
            }
            

        }


        private void AntiSuspend_DoWork(object sender, DoWorkEventArgs e)
        {
            var firstTime = DateTime.Now;

            wait(250);
            var secondTime = DateTime.Now;
            int milliSeconds = (int)((TimeSpan)(secondTime - firstTime)).TotalMilliseconds;


            if (milliSeconds > 750)
            {
                MessageBox.Show("Suspend Tespit Edildi!");
                Process.Start("https://youtu.be/EOn9rRSdBNU");
                Environment.Exit(Environment.ExitCode);
            }
            AntiSuspend.RunWorkerCompleted += new RunWorkerCompletedEventHandler(AntiSuspend_RunWorkerCompleted);
          
        }


    }
}
