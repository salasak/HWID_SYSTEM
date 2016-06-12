using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.WebSockets;
using System.Net.Sockets;
using DevExpress.XtraEditors;
using System.Management;
using HWID_SYSTEM;

namespace HWID_SYSTEM
{
    public partial class Register : DevExpress.XtraEditors.XtraForm
    {
        UdpClient server = new UdpClient();
        WebClient client = new WebClient();
        public Register()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string thisguid = HWDI.GetMachineGuid();
                server.Connect("localhost", 80);
                string loginusername = textEdit1.Text;
                string loginpassword = textEdit2.Text;
                if (client.DownloadString("http://localhost/projects/tools/api.php?type=register&username=" + textEdit1.Text + "&password=" + textEdit2.Text + "&rpassword=" + textEdit3.Text + "&email=" + textEdit4.Text + "&hwid=" + thisguid).Contains("0x6"))
                {
                    XtraMessageBox.Show("User is registered.", "SUCCESS");
                    this.Close();
                }
                else if (client.DownloadString("http://localhost/projects/tools/api.php?type=register&username=" + textEdit1.Text + "&password=" + textEdit2.Text + "&rpassword=" + textEdit3.Text + "&email=" + textEdit4.Text + "&hwid=" + thisguid).Contains("0x1"))
                {
                    XtraMessageBox.Show("Please fill in all fields.", "ERROR");
                }
                else if (client.DownloadString("http://localhost/projects/tools/api.php?type=register&username=" + textEdit1.Text + "&password=" + textEdit2.Text + "&rpassword=" + textEdit3.Text + "&email=" + textEdit4.Text + "&hwid=" + thisguid).Contains("0x2"))
                {
                    XtraMessageBox.Show("Username is already in use.", "ERROR");
                }
                else if (client.DownloadString("http://localhost/projects/tools/api.php?type=register&username=" + textEdit1.Text + "&password=" + textEdit2.Text + "&rpassword=" + textEdit3.Text + "&email=" + textEdit4.Text + "&hwid=" + thisguid).Contains("0x3"))
                {
                    XtraMessageBox.Show("Username must be alphanumberic and 4-15 characters in length.", "ERROR");
                }
                else if (client.DownloadString("http://localhost/projects/tools/api.php?type=register&username=" + textEdit1.Text + "&password=" + textEdit2.Text + "&rpassword=" + textEdit3.Text + "&email=" + textEdit4.Text + "&hwid=" + thisguid).Contains("0x4"))
                {
                    XtraMessageBox.Show("Email is not a valid email address.", "ERROR");
                }
                else if (client.DownloadString("http://localhost/projects/tools/api.php?type=register&username=" + textEdit1.Text + "&password=" + textEdit2.Text + "&rpassword=" + textEdit3.Text + "&email=" + textEdit4.Text + "&hwid=" + thisguid).Contains("0x5"))
                {
                    XtraMessageBox.Show("Passwords do not match.", "ERROR");
                }
            }
            catch (WebSocketException)
            {
                XtraMessageBox.Show("Server is unavailable", "Server ERROR", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
        }
    }
}