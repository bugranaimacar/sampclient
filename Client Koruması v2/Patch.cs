using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Security;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Specialized;
using System.Management;

namespace Client_Koruması_v2
{
    public partial class Patch : Form
    {
        public static string SunucuIP = "localhost";
        public static byte[] anakey;
        public static byte[] naim;

        public Patch()
        {
            InitializeComponent();
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


     public static string FromHexString(string hexString)
    {
        var bytes = new byte[hexString.Length / 2];
        for (var i = 0; i < bytes.Length; i++)
        {
            bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
        }

        return Encoding.Unicode.GetString(bytes);
    }

     public static string HextoString(string InputText)
     {

         byte[] bb = Enumerable.Range(0, InputText.Length)
                          .Where(x => x % 2 == 0)
                          .Select(x => Convert.ToByte(InputText.Substring(x, 2), 16))
                          .ToArray();
         return System.Text.Encoding.ASCII.GetString(bb);

     }

        private void Patch_Load(object sender, EventArgs e)
        {

            SHA256 mySHA256 = SHA256Managed.Create();
            // aes 256 key için salt aşağıda!
            anakey = mySHA256.ComputeHash(Encoding.ASCII.GetBytes("Boshret Kheir"));

            try
{
        WebClient client2 = new WebClient();
        client2.Headers.Add(HttpRequestHeader.Referer, "Client_57684286");
        Stream stream = client2.OpenRead("http://" + SunucuIP + "/erdal/cakir.php");
        StreamReader reader = new StreamReader(stream);
        String content = reader.ReadToEnd();
        string namo = content.ToString();

        byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
        

        string decrypted = this.DecryptString(namo, anakey, iv);


        if (decrypted != "PD0.1")
        {
            MessageBox.Show("Clientin yeni sürümü mevcut lütfen sitemizden indirin\nGüncel Sürüm: " + decrypted, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Environment.Exit(Environment.ExitCode);
        }
                }

            catch
                {
                MessageBox.Show("Nova Roleplay Sunucularına şu anda ulaşılamıyor! NO:01", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(Environment.ExitCode); 
                }
            try
                {
                RegistryKey localKey;
            if (Environment.Is64BitOperatingSystem)
                localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            else
                localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);

            string value = localKey.OpenSubKey("Software\\SAMP").GetValue("gta_sa_exe").ToString();
            if(value == "")
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

            RegistryKey yey;
            if (Environment.Is64BitOperatingSystem)
                yey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            else
                yey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);

            string yey2 = yey.OpenSubKey("Software\\SAMP").GetValue("gta_sa_exe").ToString();
            string s = yey2.ToString();
            string v = s.Replace("gta_sa.exe", "");
            string dizin = v;
            string naim = dizin + "\\SAMP\\";
            this.Hide();
            anaEkran f2 = new anaEkran();
            f2.ShowDialog();
            this.Close();﻿
            /*
            string filename = getFileName("");
            string url = "";
            
            if (File.Exists(naim + "custom.img"))
            {

                string size = "8366080";
                long filesize = new FileInfo(naim + "custom.img").Length;
                string ahmet = filesize.ToString();
                if (size != ahmet)
                {
                    File.Delete(naim + "custom.img");
                }
                else
                {
                    MessageBox.Show("İndirme Tamamlandı");
                    this.Hide();
                    anaEkran f2 = new anaEkran();
                    f2.ShowDialog();
                    this.Close();﻿
                }
            }
            label3.Text = "Dosya Adı: " + getFileName(url);
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFileCompleted += wc_completed;
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileAsync(new Uri(url), naim + filename);
            }
            */
        }
        private void opennewform(object obj)
        {
            Application.Run(new anaEkran());
        }
        private void wc_completed(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("İndirme Tamamlandı");
            this.Hide();
            anaEkran f2 = new anaEkran();
            f2.ShowDialog();
            this.Close();﻿
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;

            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
        }

        //Kaynak: http://stackoverflow.com/questions/13570839/get-filename-without-content-disposition
        private String getFileName(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            string fileName = "";
            try
            {

                HttpWebResponse res = (HttpWebResponse)request.GetResponse();
                using (Stream rstream = res.GetResponseStream())
                {
                    fileName = res.Headers["Content-Disposition"] != null ?
                       res.Headers["Content-Disposition"].Replace("attachment; filename=", "").Replace("\"", "") :
                       res.Headers["Location"] != null ? Path.GetFileName(res.Headers["Location"]) :
                       Path.GetFileName(url).Contains('?') || Path.GetFileName(url).Contains('=') ?
                       Path.GetFileName(res.ResponseUri.ToString()) : GetFileName(url);
                }
                res.Close();
            }
            catch { }

            return fileName;
        }


        private string GetFileName(string url)
        {
            string[] parts = url.Split('/');
            string fileName = "";

            if (parts.Length > 0)
                fileName = parts[parts.Length - 1];
            else
                fileName = url;
            return fileName;
        }
    }

}
