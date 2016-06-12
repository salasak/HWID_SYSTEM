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
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        UdpClient server = new UdpClient();
        WebClient client = new WebClient();
        string URL = "http://localhost/projects/tools/";
        public Form1()
        {
            InitializeComponent();
        }



        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            try
            {
                string thisguid = HWDI.GetMachineGuid();
                server.Connect("localhost", 80);
                string loginusername = textEdit1.Text;
                string loginpassword = textEdit2.Text;
                if (client.DownloadString(URL + "api.php?type=login&username=" + textEdit1.Text + "&password=" + textEdit2.Text + "&hwid=" + thisguid).Contains("0x05"))
                {
                    XtraMessageBox.Show("Successfully logged in.", "Success!");
                    //Open form, or do whatever people do here.
                }
                else if (client.DownloadString(URL + "api.php?type=login&username=" + textEdit1.Text + "&password=" + textEdit2.Text + "&hwid=" + thisguid).Contains("0x01"))
                {
                    XtraMessageBox.Show("HWID doesn't match.", "ERROR");
                }
                else if (client.DownloadString(URL + "api.php?type=login&username=" + textEdit1.Text + "&password=" + textEdit2.Text + "&hwid=" + thisguid).Contains("0x02"))
                {
                    XtraMessageBox.Show("Please fill in all fields.", "ERROR");
                }
                else if (client.DownloadString(URL + "api.php?type=login&username=" + textEdit1.Text + "&password=" + textEdit2.Text + "&hwid=" + thisguid).Contains("0x03"))
                {
                    XtraMessageBox.Show("Username or password are invalid.", "ERROR");
                }
                else if (client.DownloadString(URL + "api.php?type=login&username=" + textEdit1.Text + "&password=" + textEdit2.Text + "&hwid=" + thisguid).Contains("0x04"))
                {
                    XtraMessageBox.Show("You are banned.", "ERROR");
                }
            }
            catch (WebSocketException)
            {
                XtraMessageBox.Show("Server is unavailable", "Server ERROR", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            /*
            Register Reg = new Register();
            Reg.Show();
             * */

            // #1. Make second form
            // If you want to make equivalent one, then change Form2 -> Form1
            Register form2 = new Register();

            // #2. Set second form's size
            form2.Width = this.Width;

            // #4. Set parent form's visible to false
            this.Visible = false;

            // #5. Open second dialog
            form2.ShowDialog();

            // #6. Set parent form's visible to true
            this.Visible = true;
        }
    }
}
