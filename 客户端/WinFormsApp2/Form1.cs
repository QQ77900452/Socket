using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static Socket ClientSocket;//声明负责通信的socket
        public static int SFlag = 0;//连接服务器的成功标志
        Thread th1;//声明一个线程

        private void cne_Click(object sender, EventArgs e)
        {
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            msgtb.Text += "正在连接...\n";

            IPAddress ip=IPAddress.Parse(iptb.Text);
            int port = int.Parse(porttb.Text);

            IPEndPoint ipe = new IPEndPoint(ip, port);

            try
            {
                ClientSocket.Connect(ipe);//用socket对象的connect方法连接上面建立的ipe对象
                SFlag = 1;//连接成功设置标志为1

                msgtb.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + iptb.Text + " 连接成功\n";
                cne.Enabled = false;//连接成功后禁止操作

                //开启一个线程接收数据
                th1 = new Thread(Recevie);
                th1.IsBackground = true;
                th1.Start(ClientSocket);

            }
            catch (Exception ex)
            {
                MessageBox.Show("服务器未打开");
            }
        }

        #region 接收服务器端数据
        void Recevie(object sk)
        {
            Socket socketRec = sk as Socket;

            while (true)
            {
                //5.接收数据
                byte[] receive=new byte[1024 * 1024];
                ClientSocket.Receive(receive);//调用receive接收字节数据

                //6.打印数据
                if(receive.Length > 0)
                {
                    msgtb.Text += DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss  ") + "接收：";   //打印接收时间
                    msgtb.Text += Encoding.UTF8.GetString(receive);  //将字节数据根据ASCII码转成字符串
                    msgtb.Text += "\r\n";
                }

            }


        }

        #endregion
        private void clo_Click(object sender, EventArgs e)
        {
            //保证是在连接状态下退出
            if(SFlag == 1)
            {
                byte[] send=new byte[1024 * 1024];
                send=Encoding.UTF8.GetBytes("*close*");//关闭客户端时给服务器发送一个退出标志
                ClientSocket.Send(send);

                th1.Abort();//关闭线程
                
                ClientSocket.Close();//关闭套接字
                cne.Enabled = true;
                SFlag = 0;//客户端退出后将连接成功的标志设置为0
                msgtb.Text += DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss  ")+ "客户端已关闭\n";
                MessageBox.Show("已关闭连接");
            }
        }

        private void sends_Click(object sender, EventArgs e)
        {
            //只有连接成功才能发送数据，接收部分是因为连接成功才开启线程，所以不需要使用标志
            if (SFlag == 1)
            {
                byte[] send = new byte[1024 * 1024];
                send = Encoding.UTF8.GetBytes(sendtb.Text);//将数据转换为字节进行传输
                ClientSocket.Send(send);//调用发送函数

                msgtb.Text += DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss  ") + "发送：";   //打印发送数据的时间
                msgtb.Text += sendtb.Text + "\n";   //打印发送的数据
                sendtb.Clear();   //清空发送框
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;//执行新线程时跨线程资源访问检查会提示报错，所以这里关闭检测
        }

        private void sendtb_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.sends_Click(sender, e); 
                e.Handled = true;
            }
        }
    }
}