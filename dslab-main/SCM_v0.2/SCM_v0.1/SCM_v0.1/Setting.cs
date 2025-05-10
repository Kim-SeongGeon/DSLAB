using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xplane_data_test
{
    public partial class Setting : Form
    {
        private String cpk = null;
        private String directoryName = @"C:\Users\pa543\Desktop\c#\xplane_data_test\protocalData.txt";
        public Setting()
        {
            InitializeComponent();

            // 목록 생성 버튼
            CPB.SelectedIndex = 0;
            cpk = CPB.SelectedItem.ToString();

            // 설정 기본값
            if (File.Exists(directoryName))
            {
                using(StreamReader reader = new StreamReader(directoryName))
                {
                    String data = reader.ReadLine(); // 설정값을 읽어옴
                    String[] buf = data.Split(',');

                    if (buf[0].Equals("MAVLink1"))
                    {
                        VersionCB.SelectedIndex = 0;
                    }
                    else
                    {
                        VersionCB.SelectedIndex = 1;
                    }

                    if (buf[1].Equals("미사용"))
                    {
                        IncompatCB.SelectedIndex = 0;
                    }
                    else
                    {
                        IncompatCB.SelectedIndex = 1;
                    }

                    if (buf[2].Equals("미사용"))
                    {
                        CompatCB.SelectedIndex = 0;
                    }
                    else
                    {
                        CompatCB.SelectedIndex = 1;
                    }

                    if (buf[5].Equals("미사용"))
                    {
                        SignatureCB.SelectedIndex = 0;
                    }
                    else
                    {
                        SignatureCB.SelectedIndex = 1;
                    }
                    
                    SystemIDCB.SelectedIndex = (int.Parse(buf[3])-1);
                    CompIDCB.SelectedIndex = (int.Parse(buf[4])-1);
                }
            }
               
        }

        // 목록 생성 winform
        private void VehicleB_Click(object sender, EventArgs e)
        {
            if (cpk.Equals("STANAG 4586"))
            {
                CreateSTANAG(); // 나중에 생하기
            }
            else if (cpk.Equals("MAVLink"))
            {
                CreateMAVLINK();
            }
          
        }

        private void CPB_SelectedIndexChanged(object sender, EventArgs e) //생성할 프로토콜 선택시 cpk에 프로토콜 종료를 저장
        {
            cpk = CPB.SelectedItem.ToString();           
        }

        private void CreateMAVLINK() //MAVLINK 설정 GUI 생성
        {
            Label versionL = new Label();
            versionL.Text = "Version ";
            versionL.Location = new System.Drawing.Point(50, 300);

        }

        private void CreateSTANAG() //STANAG 설정 GUI 생성
        {

        }

        private void SaveB_Click(object sender, EventArgs e)
        {
            String data = VersionCB.SelectedItem.ToString() + "," + IncompatCB.SelectedItem.ToString() + ","
                + CompatCB.SelectedItem.ToString() + "," + SystemIDCB.SelectedItem.ToString() + ","
                 + CompIDCB.SelectedItem.ToString() + ","+ SignatureCB.SelectedItem.ToString();

            Console.WriteLine(data);

            if (File.Exists(directoryName))
            {
                using(StreamWriter writer = new StreamWriter(directoryName))
                {
                    Console.WriteLine("write");
                    writer.Write(data);
                }
            }
        }
    }
}
