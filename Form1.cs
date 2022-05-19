using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceProcess;
using Microsoft.Win32;
using System.IO;

namespace Redshift
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] ServeName = { "mxredirect", "Red Giant Service" };
        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("https://id.maxon.net/register/?loginProcess=registerUser&encodedParams=cmVzcG9uc2VfdHlwZT10b2tlbitpZF90b2tlbiZjb250aW51ZVRvPSUyRm9hdXRoMiUyRmF1dGh6JTJGJTNGcmVzcG9uc2VfdHlwZSUzRHRva2VuJTJCaWRfdG9rZW4lMjZjbGllbnRfaWQlM0QyMTFhOTYwNy1jOTFhLTQ5MWEtOWQxMS0wZmYwNjNhMmIzM2YlMjZzdGF0ZSUzRHRyeSUyNnJlZGlyZWN0X3VyaSUzRGh0dHBzJTNBJTJGJTJGbXkubWF4b24ubmV0JTJGJTI2c3RhdGUlM0R0cnkmc3RhdGU9dHJ5JnN0YXRlPXRyeSZyZWRpcmVjdF91cmk9aHR0cHMlM0ElMkYlMkZteS5tYXhvbi5uZXQlMkYmY2xpZW50X2lkPTIxMWE5NjA3LWM5MWEtNDkxYS05ZDExLTBmZjA2M2EyYjMzZg");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (string s in ServeName)
            {
                ServiceController controller = new ServiceController(s);
                try
                {
                    ServiceControllerStatus scs = controller.Status;
                    if (scs == ServiceControllerStatus.Running)
                    {
                        try
                        {
                            controller.Stop();
                            MessageBox.Show(s + " 服务已停止", "信息：", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("服务停止失败" + ex.Message, "信息：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show(s + " 服务不存在", "信息：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ServiceController sc = new ServiceController("Red Giant Service");
            try
            {
                if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    RegistryKey r = Registry.LocalMachine;
                    RegistryKey k = r.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography");
                    string value = (string)k.GetValue("MachineGuid", null);

                    File.WriteAllText(Directory.GetCurrentDirectory() + @"\Guid.txt", value);
                    MessageBox.Show("原始Guid已备份到当前目录下", "信息：", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    k.SetValue("MachineGuid", Guid.NewGuid().ToString());
                    k.Close();
                    r.Close();
                    MessageBox.Show("修改成功", "信息：", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                sc.Close();
            }
            catch { return; }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (var item in ServeName)
            {
                ServiceController sc = new ServiceController(item);
                try
                {
                    ServiceControllerStatus ss = sc.Status;
                    if (ss == ServiceControllerStatus.Stopped)
                    {
                        try
                        {
                            sc.Start();
                            MessageBox.Show(item + " 服务已启动", "信息：", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("服务启动失败" + ex.Message, "信息：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show(item + " 服务不存在", "信息：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        string FilePath = @"C:\Program Files\Red Giant\Services\";
        private void button5_Click(object sender, EventArgs e)
        {
            if (File.Exists(FilePath + "Red Giant Service.exe") == true)
            {
                File.Move(FilePath + "Red Giant Service.exe", FilePath + "Red Giant Service.bak");
                File.Copy(Directory.GetCurrentDirectory() + @"\Red Giant Service.exe", FilePath + "Red Giant Service.exe");
                MessageBox.Show("文件替换完成", "信息：", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (File.Exists(FilePath + "Red Giant Service.bak") == true)
            {
                File.Delete(FilePath + "Red Giant Service.exe");
                File.Move(FilePath + "Red Giant Service.bak", FilePath + "Red Giant Service.exe");
                MessageBox.Show("文件还原完成", "信息：", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
