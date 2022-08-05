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

        public static Socket ServerSocket;//�������ڼ������׽���
        public static Socket socketAccept;//�����󶨿ͻ��˵��׽���
        public static Socket socket;//����������ĳһ�ͻ���ͨ�ŵ��׽���

        public static int SFlag = 0;//���ӳɹ���־
        public static int CFlag = 0;//�ͻ��˹رձ�־

        Thread th1;//�߳�1
        Thread th2;//�߳�2

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls=false;//ִ�����߳�ʱ���߳���Դ���ʼ�����ʾ�������Թرռ��
        }

        private void cne_Click(object sender, EventArgs e)
        {
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            msgtb.Text += "��������...\n";
            cne.Enabled = false;//�����н�ֹ�������Ӱ�ť

            //1.����IP��Port
            IPAddress ip = IPAddress.Parse(iptb.Text);
            int port = int.Parse(prottb.Text);

            IPEndPoint ipe = new IPEndPoint(ip, port);

            try
            {
                //2.ʹ��bind()��
                ServerSocket.Bind(ipe);
                //3.�򿪼���
                //listing(int backlog);backlog:��������
                ServerSocket.Listen(10);
                /*
                 * tip:
                 * Accept���谭���̵߳����У�һֱ�ڵȴ��ͻ��˵�����
                 * �ͻ�����������룬���ͻ�һֱ�ȴ������߳̿���
                 * ���Կ���һ�����߳̽��տͻ�������
                 */

                //�����߳�Accept����ͨ�ŵĿͻ���socket
                th1 = new Thread(Listen);//�̰߳�listen����
                th1.IsBackground = true;//�����߳��ں�ִ̨��
                th1.Start(ServerSocket);//start����Ĳ�����listen��������Ҫ�Ĳ��������ﴫ�͵�������ͨ�ŵ�Socket����
                Console.WriteLine("1");

            }
            catch(SocketException ex)
            {
                MessageBox.Show("��������������");
            }
        }

        #region ������ͻ��˵�����
        void Listen(object sk)
        {
            socketAccept = sk as Socket;//����һ����ͻ��˶Խӵ��׽���

            try
            {
                while (true)
                {
                    //GNFlag���Ϊ1�ͽ����ж����ӣ�ʹ���ڱ�־��Ϊ�˱�֤�ܹ�˳���رշ�������
                    /*
                     * �����̹ر�һ�κ����û�������־λ��ʹ�÷�����һֱ�����жϵ�λ��
                     * �����������һֱ�����ж�λ�þͲ���˳���ر�
                   */
                    //4.������client����
                    socket = socketAccept.Accept();

                    CFlag = 0;//���ӳɹ������ͻ��˹رձ�־����Ϊ0
                    SFlag = 1;//�����ӳɹ��������ӳɹ���־����Ϊ1

                    msgtb.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + iptb.Text + " ���ӳɹ�\r\n";

                    //�����ڶ����߳̽��տͻ�������
                    th2 = new Thread(Recevie);//�̰߳�Recevice����
                    th2.IsBackground = true;//�����߳��ں�ִ̨��
                    th2.Start(socket);//Start����Ĳ�����Listen��������Ҫ�Ĳ��������ﴫ�͵�������ͨ�ŵ�Socket����
                }
                
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region ���տͻ�������
        void Recevie(object sk)
        {
            socket = sk as Socket;

            while (true)
            {
                try
                {
                    if(CFlag == 0 && SFlag == 1)
                    {
                        //5.��������
                        byte[] recieve = new byte[1024 * 1024];
                        int len = socket.Receive(recieve);

                        //6.��ӡ��������
                        if(recieve.Length > 0)
                        {
                            //������յ��ͻ��˵�ֹͣ��־
                            if (Encoding.UTF8.GetString(recieve, 0, len) == "*close*")
                            {
                                msgtb.Text += DateTime.Now.ToString("yy-MM-dd hh:mm:ss  ") + "�ͻ������˳�" + "\n";
                                CFlag= 1;//���ͻ��˹رձ�־����Ϊ1

                                break;//�˳�ѭ��
                            }
                        }

                        //��ӡ��������
                        msgtb.Text += DateTime.Now.ToString("yy-MM-dd hh:mm:ss  ") + "���գ�";
                        msgtb.Text += Encoding.UTF8.GetString(recieve, 0, len);  //���ֽ����ݸ���ASCII��ת���ַ���
                        msgtb.Text += "\r\n";

                    }
                    else
                    {
                        break;//�˳�ѭ��
                    }
                }
                catch(SocketException ex)
                {
                    MessageBox.Show("���ղ�����Ϣ,�쳣���£�"+ex.Message);
                }
            }
        }


        #endregion

        #region �ر�����
        private void clo_Click(object sender, EventArgs e)
        {
            //���������˿ͻ�����Ҫ�ر��߳�2��socket��û�������Ͽͻ���ֱ�ӹر��߳�1�������׽���
            if (CFlag == 1)
            {
                th2.Abort();//�ر��߳�2
                socket.Close();//�ر�����ͨ�ŵ��׽���
            }

            ServerSocket.Close();//�ر��������ӵ��׽���
            socketAccept.Close();//�ر���ͻ��˰󶨵��׽���
            th1.Abort();//�ر��߳�1

            CFlag = 0;//���ͻ��˱�־��������Ϊ0���ڽ�������ʱ�Ǵ򿪵�״̬
            SFlag = 0;//�����ӳɹ���־��������Ϊ0����ʾ�˳�����
            cne.Enabled = true;
            msgtb.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ")+"�������ѹر�\n";
            MessageBox.Show("�������ѹر�");
        }
        #endregion

        #region ��ͻ��˷�������
        private void sends_Click(object sender, EventArgs e)
        {
            //SFlag��־�ɹ����ӣ�ͬʱ���ͻ����Ǵ򿪵�ʱ����ܷ�������
            if(SFlag == 1 && CFlag == 0)
            {
                byte[] send=new byte[1024 * 1024];
                send =Encoding.UTF8.GetBytes(sendtb.Text);//���ַ���ת���ֽڸ�ʽ����
                socket.Send(send);

                //��ӡ����ʱ��ͷ��͵�����
                msgtb.Text += DateTime.Now.ToString("yy-MM-dd hh:mm:ss  ") + "���ͣ�";
                msgtb.Text += sendtb.Text + "\n";
                sendtb.Clear();  //������Ϳ�
            }
        }
        #endregion

        #region ���enter��������
        private void sendtb_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)//���������Ļس���
            {
                this.sends_Click(sender, e);
                e.Handled = true;
            }
        }
        #endregion
    }
}