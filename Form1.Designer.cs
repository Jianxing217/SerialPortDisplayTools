using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form

    {



        float maxRate; //最大比例

        int run = 1;
        

        private bool listening = false;

        private bool PointOpen = true;
        private bool PointFlag = true;
        private bool SendFlag = false;

        private bool closing = false;



        Point lastPoint, nowPoint;
        Point xlastPoint, xnowPoint;
        Point ylastPoint, ynowPoint;
        Point zlastPoint, znowPoint;

        Point xtempPoint;
        Point ytempPoint;
        Point ztempPoint;

        List<int> x = new List<int>(); //存储串口接收的值
        List<int> y = new List<int>(); //存储串口接收的值
        List<int> z = new List<int>(); //存储串口接收的值
        List<int> count = new List<int>(); //存储串口接收的值
        Graphics g; //生成图形

        Pen drawPen = new Pen(Color.Red, 1);

        private StringBuilder builder = new StringBuilder();
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private ComboBox comboBox2;
        private Button button1;
        private TextBox textBox1;
        private TextBox txSend;
        private Button button2;
        private Button button3;
        private CheckBox checkBoxHexSend;
        private CheckBox checkBoxNewlineSend;
        private CheckBox checkBoxASCIISend;
        private SerialPort comm = new SerialPort();

        private PictureBox pictureBox1;
        private Button button4;
        private Button button5;

        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button button6;
        private Button button7;
        private Label label7;
        private Label label8;
        private Button button8;


        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txSend = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBoxHexSend = new System.Windows.Forms.CheckBox();
            this.checkBoxNewlineSend = new System.Windows.Forms.CheckBox();
            this.checkBoxASCIISend = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(74, 18);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(118, 23);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "端口";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(293, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "波特率";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "115200",
            "921600"});
            this.comboBox2.Location = new System.Drawing.Point(351, 18);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 23);
            this.comboBox2.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(514, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "连接";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Location = new System.Drawing.Point(34, 74);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(555, 138);
            this.textBox1.TabIndex = 6;
            // 
            // txSend
            // 
            this.txSend.Location = new System.Drawing.Point(34, 661);
            this.txSend.Name = "txSend";
            this.txSend.Size = new System.Drawing.Size(316, 25);
            this.txSend.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(386, 660);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 25);
            this.button2.TabIndex = 9;
            this.button2.Text = "发送";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(509, 662);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 25);
            this.button3.TabIndex = 10;
            this.button3.Text = "断开";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBoxHexSend
            // 
            this.checkBoxHexSend.AutoSize = true;
            this.checkBoxHexSend.Location = new System.Drawing.Point(34, 627);
            this.checkBoxHexSend.Name = "checkBoxHexSend";
            this.checkBoxHexSend.Size = new System.Drawing.Size(53, 19);
            this.checkBoxHexSend.TabIndex = 11;
            this.checkBoxHexSend.Text = "HEX";
            this.checkBoxHexSend.UseVisualStyleBackColor = true;
            // 
            // checkBoxNewlineSend
            // 
            this.checkBoxNewlineSend.AutoSize = true;
            this.checkBoxNewlineSend.Location = new System.Drawing.Point(265, 627);
            this.checkBoxNewlineSend.Name = "checkBoxNewlineSend";
            this.checkBoxNewlineSend.Size = new System.Drawing.Size(85, 19);
            this.checkBoxNewlineSend.TabIndex = 12;
            this.checkBoxNewlineSend.Text = "NewLine";
            this.checkBoxNewlineSend.UseVisualStyleBackColor = true;
            // 
            // checkBoxASCIISend
            // 
            this.checkBoxASCIISend.AutoSize = true;
            this.checkBoxASCIISend.Location = new System.Drawing.Point(140, 627);
            this.checkBoxASCIISend.Name = "checkBoxASCIISend";
            this.checkBoxASCIISend.Size = new System.Drawing.Size(69, 19);
            this.checkBoxASCIISend.TabIndex = 13;
            this.checkBoxASCIISend.Text = "ASCII";
            this.checkBoxASCIISend.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(42, 260);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(547, 353);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(386, 619);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(84, 27);
            this.button4.TabIndex = 15;
            this.button4.Text = "开始";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.start_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(505, 619);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(84, 27);
            this.button5.TabIndex = 16;
            this.button5.Text = "停止";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.stop_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(42, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "X轴:红色 ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 598);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 18;
            this.label4.Text = "-7FF";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 428);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 15);
            this.label5.TabIndex = 19;
            this.label5.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 260);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 15);
            this.label6.TabIndex = 20;
            this.label6.Text = "+7FF";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(400, 218);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(72, 35);
            this.button6.TabIndex = 21;
            this.button6.Text = "清除";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.clear_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(517, 218);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(72, 33);
            this.button7.TabIndex = 22;
            this.button7.Text = "保存";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.save_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(154, 231);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 15);
            this.label7.TabIndex = 23;
            this.label7.Text = "Y轴：绿色";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(262, 231);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 15);
            this.label8.TabIndex = 24;
            this.label8.Text = "Z轴：蓝色";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(198, 19);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(64, 21);
            this.button8.TabIndex = 25;
            this.button8.Text = "刷新";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.refresh_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(609, 698);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkBoxASCIISend);
            this.Controls.Add(this.checkBoxNewlineSend);
            this.Controls.Add(this.checkBoxHexSend);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txSend);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "Form1";
            this.Text = "串口波形显示";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            string[] PortName = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string port in PortName)
            {
                comboBox1.Items.Add(port);
            }

        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)

        {

            g = e.Graphics;
            pictureBox1.Width = count.Count();

            xnowPoint.X = run;
            ynowPoint.X = run;
            znowPoint.X = run;

            float tmpX = x[0];
            float tmpY = y[0];
            float tmpZ = z[0];

            if (tmpX > 0)
            {
                xnowPoint.Y = Convert.ToInt32(pictureBox1.Height - (tmpX / maxRate) * pictureBox1.Height- pictureBox1.Height/2);  //tempX 跟 maxRate必须要浮点数才能有值出来

            }
            else
            {
                xnowPoint.Y = Convert.ToInt32(pictureBox1.Height - (pictureBox1.Height / 2 - (Math.Abs(tmpX) / maxRate) * pictureBox1.Height) );  //tempX 跟 maxRate必须要浮点数
            }

            if (tmpY > 0)
            {
                ynowPoint.Y = Convert.ToInt32(pictureBox1.Height - (tmpY / maxRate) * pictureBox1.Height - pictureBox1.Height / 2);

            }
            else
            {
                ynowPoint.Y = Convert.ToInt32(pictureBox1.Height - (pictureBox1.Height / 2 - (Math.Abs(tmpY) / maxRate) * pictureBox1.Height));
            }

            if (tmpZ > 0)
            {
                znowPoint.Y = Convert.ToInt32(pictureBox1.Height - (tmpZ / maxRate) * pictureBox1.Height - pictureBox1.Height / 2);
            }
            else
            {
                znowPoint.Y = Convert.ToInt32(pictureBox1.Height - (pictureBox1.Height/2-(Math.Abs(tmpZ) / maxRate) * pictureBox1.Height));
            }


            if (PointFlag == true)
            {


                xtempPoint = new Point(0, xnowPoint.Y);
                ytempPoint = new Point(0, ynowPoint.Y);
                ztempPoint = new Point(0, znowPoint.Y);
                PointFlag = false;
            }
            else
            {
                xlastPoint = xtempPoint;
                ylastPoint = ytempPoint;
                zlastPoint = ztempPoint;


                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; //消除锯齿状
                drawPen.Color = Color.Red;
                g.DrawLine(drawPen, xlastPoint, xnowPoint);

                drawPen.Color = Color.Green;
                g.DrawLine(drawPen, ylastPoint, ynowPoint);

                drawPen.Color = Color.Blue;
                g.DrawLine(drawPen, zlastPoint, znowPoint);


                xlastPoint = xnowPoint;
                ylastPoint = ynowPoint;
                zlastPoint = znowPoint;
            }

        }



        void comm_dataReceived(object sender, SerialDataReceivedEventArgs e)

        {
            if (SendFlag == false) return; 
            if (closing==false) return; //防止关闭时锁死
            maxRate = 65535;
            string tmpS = comm.ReadExisting();
            builder.Append(tmpS);

            string s = builder.ToString();

            builder.Clear();//清除字符串构造器

            string[] arr = s.Split('\n', '\r').Where(t => t.Trim() != "").ToArray();
            var lastInt = arr.Last();
            var mid  = arr.Length/2;



            ////因为要访问ui资源，所以需要使用invoke方式同步ui。

            this.Invoke((EventHandler)(delegate

            {
                char[] delimiterChars = { ':', ' ' };
                string[] state = lastInt.Split(delimiterChars);
                //List<int> x = new List<int>();
                if (state[0] == "OK" || state[0] == "ERR")
                {
                    textBox1.AppendText(state[0] + "\r\n");
                }
                else
                {
                    string[] words = arr[mid].Split(delimiterChars);
                    if (words[0] == "+ACC" && words.Length > 6)
                    {
                        listening = true;
                        string xString = words[2] + words[1];
                        string yString = words[4] + words[3];
                        string zString = words[6] + words[5];
                        //textBox1.AppendText("表头：" + words[0] + "\r\n");
                        textBox1.AppendText("X轴：" + xString + "\r\n");
                        textBox1.AppendText("Y轴：" + yString + "\r\n");
                        textBox1.AppendText("Z轴：" + zString + "\r\n");
                        textBox1.AppendText("\r\n");
                        //textBox1.AppendText(tmpS + "\r\n");

                        x.Clear();
                        y.Clear();
                        z.Clear();

                        //将十六进制字符串转换为十进制i
                        int xint = Convert.ToInt32(xString, 16);
                        int yint = Convert.ToInt32(yString, 16);
                        int zint = Convert.ToInt32(zString, 16);

                        if (xint > 32767)
                        {
                            //正数和0的补码就是该数字本身。负数的补码则是将其对应正数按位取反再加1。
                            xint = -((xint ^ 0xFFFF) + 1);
                        }
                        if (yint > 32767)
                        {
                            //正数和0的补码就是该数字本身。负数的补码则是将其对应正数按位取反再加1。
                            yint = -((yint ^ 0xFFFF) + 1);
                        }
                        if (zint > 32767)
                        {
                            //正数和0的补码就是该数字本身。负数的补码则是将其对应正数按位取反再加1。
                            zint = -((zint ^ 0xFFFF) + 1);
                        }
                        //textBox1.AppendText("Z轴：" + zint + "\r\n");

                        //Regex regex = new Regex("");
                        //string[] substrings = regex.Split(xString);

                        //byte[] b = new byte[substrings.Length];

                        //b = Convert.ToByte(substrings);


                        //逐个字符变为16进制字节数据
                        //for (int i = 0; i < substrings.Length; i++)
                        //{
                        //    b[i] = Convert.ToByte(substrings[i], 16);
                        //}

                        //按照指定编码将字节数组变为字符串
                        //return encode.GetString(b);



                        x.Add(xint);
                        y.Add(yint);
                        z.Add(zint);

                        run = run + 1;
                        count.Add(run);

                        if (run >= 580)
                        {
                            run = 1;
                            count.Clear();
                            PointFlag = true;
                            //textBox1.AppendText("循环数量置位" + "\r\n");
                            //this.pictureBox1.Image = null; //清屏
                            //pictureBox1.Invalidate();
                            //this.pictureBox1.Refresh();
                            //pictureBox1.Width = count.Count();
                        }
                        listening = false;
                        //run = run + 1;
                        //pictureBox1.Width = run;
                        pictureBox1.Width = count.Count();
                        if (PointOpen == true)
                        {
                            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
                            PointOpen = false;
                        }


                    }
                    else
                    {
                        textBox1.AppendText(words[0] + "\r\n");
                    }
                }

                

               

            }));



        }

        private void button1_Click(object sender, EventArgs e)

        {

            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
            {
                textBox1.AppendText("请选择好端口跟波特率！！！" +"\r\n");
            }
            else
            {
                if (closing == false)
                {
                    comm.PortName = comboBox1.SelectedItem.ToString();

                    comm.BaudRate = Convert.ToInt32(comboBox2.SelectedItem.ToString());

                    textBox1.AppendText("COM: " + Convert.ToString(comm.PortName) + "\r\n");
                    textBox1.AppendText("Baud Rate: " + Convert.ToString(comm.BaudRate) + "\r\n");

                    comm.Open();
                    closing = true;

                    comm.DataReceived += comm_dataReceived;
                    //comm.DataReceived += pictureBox1_Paint;
                    textBox1.AppendText("成功打开端口！！！  \r\n");
                    

                }
                else
                {
                    textBox1.AppendText("端口已经打开了！！！   " + "COM: " + Convert.ToString(comm.PortName) + "\r\n");
                }

            }




        }



        private void button2_Click(object sender, EventArgs e)

        {


            closing = true;
            comm.Close();
            closing = false;
            textBox1.AppendText("端口已关闭！！！" + "\r\n");
            while (listening) Application.DoEvents();

            // MessageBox.Show("ok");




        }



        private void button4_Click(object sender, EventArgs e)

        {
            SendFlag = false;
            if (closing == false)
            {
                textBox1.AppendText("请先启动串口！！！" + "\r\n");
            }
            else
            {
                //16进制发送

                if (checkBoxHexSend.Checked == true && checkBoxASCIISend.Checked == false)

                {
                    try
                    {
                        if (checkBoxNewlineSend.Checked)

                        {
                            string hexString = null;
                            string txdata = null;
                            textBox1.AppendText(txSend.Text + "\r\n");
                            //txSend.AppendText("\r\n");
                            txdata = txSend.Text.Trim();
                            hexString = txdata.Replace(" ", "");
                            if ((hexString.Length % 2) != 0) hexString += " ";
                            byte[] returnBytes = new byte[hexString.Length / 2];
                            for (int i = 0; i < returnBytes.Length; i++)
                                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Replace(" ", ""), 16);
                            comm.Write(returnBytes, 0, returnBytes.Length);
                            SendFlag = true;

                        }

                        else//不包含换行符

                        {
                            string hexString = null;
                            string txdata = null;
                            textBox1.AppendText(txSend.Text + "\r\n");
                            txdata = txSend.Text.Trim();
                            hexString = txdata.Replace(" ", "");
                            if ((hexString.Length % 2) != 0) hexString += " ";
                            byte[] returnBytes = new byte[hexString.Length / 2];
                            for (int i = 0; i < returnBytes.Length; i++)
                                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Replace(" ", ""), 16);
                            comm.Write(returnBytes, 0, returnBytes.Length);
                            SendFlag = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        textBox1.AppendText("请输入正确的十六进制数！！！\r\n");
                    }

                }

                else if (checkBoxHexSend.Checked == false && checkBoxASCIISend.Checked == true)//ascii编码直接发送

                {

                    //包含换行符

                    if (checkBoxNewlineSend.Checked)

                    {
                        byte[] sendData = null;
                        textBox1.AppendText(txSend.Text + "\r\n");
                        sendData = Encoding.ASCII.GetBytes(txSend.Text.Trim() + "\r\n"); //Trim()是去掉里面开头和末尾的空格
                        comm.Write(sendData, 0, sendData.Length);
                        SendFlag = true;
                    }

                    else//不包含换行符

                    {
                        byte[] sendData = null;
                        textBox1.AppendText(txSend.Text + "\r\n");
                        sendData = Encoding.ASCII.GetBytes(txSend.Text.Trim()); //Trim()是去掉里面开头和末尾的空格
                        comm.Write(sendData, 0, sendData.Length);
                        SendFlag = true;
                    }

                }
                else 
                {
                    textBox1.AppendText("请选正确中其中一个编码方式！！！\r\n");
                }

            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void stop_Click(object sender, EventArgs e)
        {
            SendFlag = false;
            if (closing == false)
            {
                textBox1.AppendText("请先启动串口！！！" + "\r\n");
            }
            else
            {

                byte[] sendData = null;
                sendData = Encoding.ASCII.GetBytes("AT+SEN=0" + "\r\n"); //Trim()是去掉里面开头和末尾的空格
                textBox1.AppendText("AT+SEN=0" + "\r\n");
                comm.Write(sendData, 0, sendData.Length);
                textBox1.AppendText("停止接收收据！！！" + "\r\n");
                SendFlag = true;
            }
        }

        private void start_Click(object sender, EventArgs e)
        {
            SendFlag = false;
            if (closing == false)
            {
                textBox1.AppendText("请先启动串口！！！" + "\r\n");
            }
            else
            {
                byte[] sendData = null;
                sendData = Encoding.ASCII.GetBytes("AT+SEN=1" + "\r\n"); //Trim()是去掉里面开头和末尾的空格
                textBox1.AppendText("AT+SEN=1" + "\r\n");
                comm.Write(sendData, 0, sendData.Length);
                textBox1.AppendText("开始接收收据！！！" + "\r\n");
                SendFlag = true;
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void save_Click(object sender, EventArgs e)
        {
            String path = System.Environment.CurrentDirectory;
            String name = "SensorData";
            string Log = textBox1.Text;
            //生成目录
            //创建文件夹
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }

            // 判断文件是否存在，不存在则创建，否则读取值显示到txt文档
            if (!System.IO.File.Exists(path + "/" + name + "_Log" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt"))
            {
                FileStream fs1 = new FileStream(path + "/" + name + "_Log" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(Log);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(path + "/" + name + "_Log" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt" + "", FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(Log);//开始写入值
                sr.Close();
                fs.Close();
            }
  
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            string[] PortName = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string port in PortName)
            {
                comboBox1.Items.Add(port);
            }
          
        }


    }
}

