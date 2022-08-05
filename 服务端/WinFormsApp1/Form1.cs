using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static Socket ServerSocket;//声明用于监听的套接字
        public static Socket socketAccept;//声明绑定客户端的套接字
        public static Socket socket;//声明用于与某一客户端通信的套接字

        public static int SFlag = 0;//连接成功标志
        public static int CFlag = 0;//客户端关闭标志

        Thread th1;//线程1
        Thread th2;//线程2

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls=false;//执行新线程时跨线程资源访问检查会提示报错，所以关闭检擦
        }

        private void cne_Click(object sender, EventArgs e)
        {
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            msgtb.Text += "正在连接...\n";
            cne.Enabled = false;//连接中禁止操作连接按钮

            //1.连接IP和Port
            IPAddress ip = IPAddress.Parse(iptb.Text);
            int port = int.Parse(prottb.Text);

            IPEndPoint ipe = new IPEndPoint(ip, port);

            try
            {
                //2.使用bind()绑定
                ServerSocket.Bind(ipe);
                //3.打开监听
                //listing(int backlog);backlog:监听数量
                ServerSocket.Listen(10);
                /*
                 * tip:
                 * Accept会阻碍主线程的运行，一直在等待客户端的请求，
                 * 客户端如果不接入，它就会一直等待，主线程卡死
                 * 所以开启一个新线程接收客户端请求
                 */

                //开启线程Accept进行通信的客户端socket
                th1 = new Thread(Listen);//线程绑定listen函数
                th1.IsBackground = true;//运行线程在后台执行
                th1.Start(ServerSocket);//start里面的参数是listen函数所需要的参数，这里传送的是用于通信的Socket对象
                Console.WriteLine("1");

            }
            catch(SocketException ex)
            {
                MessageBox.Show("服务器出问题了");
            }
        }

        #region 建立与客户端的连接
        void Listen(object sk)
        {
            socketAccept = sk as Socket;//创建一个与客户端对接的套接字

            try
            {
                while (true)
                {
                    //GNFlag如果为1就进行中断连接，使用在标志是为了保证能够顺利关闭服务器端
                    /*
                     * 当护短关闭一次后，如果没有这个标志位会使得服务器一直卡在中断的位置
                     * 如果服务器端一直卡在中断位置就不能顺利关闭
                   */
                    //4.阻塞到client连接
                    socket = socketAccept.Accept();

                    CFlag = 0;//连接成功，将客户端关闭标志设置为0
                    SFlag = 1;//当连接成功，将连接成功标志设置为1

                    msgtb.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + iptb.Text + " 连接成功\r\n";

                    //开启第二个线程接收客户端数据
                    th2 = new Thread(Recevie);//线程绑定Recevice函数
                    th2.IsBackground = true;//运行线程在后台执行
                    th2.Start(socket);//Start里面的参数是Listen函数所需要的参数，这里传送的是用于通信的Socket对象
                }
                
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 接收客户端数据
        void Recevie(object sk)
        {
            socket = sk as Socket;

            while (true)
            {
                try
                {
                    if(CFlag == 0 && SFlag == 1)
                    {
                        //5.接收数据
                        byte[] recieve = new byte[1024 * 1024];
                        int len = socket.Receive(recieve);

                        //6.打印接收数据
                        if(recieve.Length > 0)
                        {
                            //如果接收到客户端的停止标志
                            if (Encoding.UTF8.GetString(recieve, 0, len) == "*close*")
                            {
                                msgtb.Text += DateTime.Now.ToString("yy-MM-dd hh:mm:ss  ") + "客户端已退出" + "\n";
                                CFlag= 1;//将客户端关闭标志设置为1

                                break;//退出循环
                            }
                        }

                        //打印接收数据
                        msgtb.Text += DateTime.Now.ToString("yy-MM-dd hh:mm:ss  ") + "接收：";
                        msgtb.Text += Encoding.UTF8.GetString(recieve, 0, len);  //将字节数据根据ASCII码转成字符串
                        msgtb.Text += "\r\n";

                    }
                    else
                    {
                        break;//退出循环
                    }
                }
                catch(SocketException ex)
                {
                    MessageBox.Show("接收不到消息,异常如下："+ex.Message);
                }
            }
        }


        #endregion

        #region 关闭连接
        private void clo_Click(object sender, EventArgs e)
        {
            //若连接上了客户端需要关闭线程2和socket，没有连接上客户端直接关闭线程1和其他套接字
            if (CFlag == 1)
            {
                th2.Abort();//关闭线程2
                socket.Close();//关闭用于通信的套接字
            }

            ServerSocket.Close();//关闭用于连接的套接字
            socketAccept.Close();//关闭与客户端绑定的套接字
            th1.Abort();//关闭线程1

            CFlag = 0;//将客户端标志重新设置为0，在进行连接时是打开的状态
            SFlag = 0;//将连接成功标志程序设置为0，表示退出连接
            cne.Enabled = true;
            msgtb.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ")+"服务器已关闭\n";
            MessageBox.Show("服务器已关闭");
        }
        #endregion

        #region 向客户端发送数据
        private void sends_Click(object sender, EventArgs e)
        {
            //SFlag标志成功连接，同时当客户端是打开的时候才能发送数据
            if(SFlag == 1 && CFlag == 0)
            {
                byte[] send=new byte[1024 * 1024];
                send =Encoding.UTF8.GetBytes(sendtb.Text);//将字符串转成字节格式发送
                socket.Send(send);

                //打印发送时间和发送的数据
                msgtb.Text += DateTime.Now.ToString("yy-MM-dd hh:mm:ss  ") + "发送：";
                msgtb.Text += sendtb.Text + "\n";
                sendtb.Clear();  //清除发送框
            }
        }
        #endregion

        #region 点击enter发送数据
        private void sendtb_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)//如果是输入的回车键
            {
                this.sends_Click(sender, e);
                e.Handled = true;
            }
        }
        #endregion
    }
}