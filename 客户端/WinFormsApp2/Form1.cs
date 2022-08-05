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

        public static Socket ClientSocket;//��������ͨ�ŵ�socket
        public static int SFlag = 0;//���ӷ������ĳɹ���־
        Thread th1;//����һ���߳�

        private void cne_Click(object sender, EventArgs e)
        {
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            msgtb.Text += "��������...\n";

            IPAddress ip=IPAddress.Parse(iptb.Text);
            int port = int.Parse(porttb.Text);

            IPEndPoint ipe = new IPEndPoint(ip, port);

            try
            {
                ClientSocket.Connect(ipe);//��socket�����connect�����������潨����ipe����
                SFlag = 1;//���ӳɹ����ñ�־Ϊ1

                msgtb.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + iptb.Text + " ���ӳɹ�\n";
                cne.Enabled = false;//���ӳɹ����ֹ����

                //����һ���߳̽�������
                th1 = new Thread(Recevie);
                th1.IsBackground = true;
                th1.Start(ClientSocket);

            }
            catch (Exception ex)
            {
                MessageBox.Show("������δ��");
            }
        }

        #region ���շ�����������
        void Recevie(object sk)
        {
            Socket socketRec = sk as Socket;

            while (true)
            {
                //5.��������
                byte[] receive=new byte[1024 * 1024];
                ClientSocket.Receive(receive);//����receive�����ֽ�����

                //6.��ӡ����
                if(receive.Length > 0)
                {
                    msgtb.Text += DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss  ") + "���գ�";   //��ӡ����ʱ��
                    msgtb.Text += Encoding.UTF8.GetString(receive);  //���ֽ����ݸ���ASCII��ת���ַ���
                    msgtb.Text += "\r\n";
                }

            }


        }

        #endregion
        private void clo_Click(object sender, EventArgs e)
        {
            //��֤��������״̬���˳�
            if(SFlag == 1)
            {
                byte[] send=new byte[1024 * 1024];
                send=Encoding.UTF8.GetBytes("*close*");//�رտͻ���ʱ������������һ���˳���־
                ClientSocket.Send(send);

                th1.Abort();//�ر��߳�
                
                ClientSocket.Close();//�ر��׽���
                cne.Enabled = true;
                SFlag = 0;//�ͻ����˳������ӳɹ��ı�־����Ϊ0
                msgtb.Text += DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss  ")+ "�ͻ����ѹر�\n";
                MessageBox.Show("�ѹر�����");
            }
        }

        private void sends_Click(object sender, EventArgs e)
        {
            //ֻ�����ӳɹ����ܷ������ݣ����ղ�������Ϊ���ӳɹ��ſ����̣߳����Բ���Ҫʹ�ñ�־
            if (SFlag == 1)
            {
                byte[] send = new byte[1024 * 1024];
                send = Encoding.UTF8.GetBytes(sendtb.Text);//������ת��Ϊ�ֽڽ��д���
                ClientSocket.Send(send);//���÷��ͺ���

                msgtb.Text += DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss  ") + "���ͣ�";   //��ӡ�������ݵ�ʱ��
                msgtb.Text += sendtb.Text + "\n";   //��ӡ���͵�����
                sendtb.Clear();   //��շ��Ϳ�
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;//ִ�����߳�ʱ���߳���Դ���ʼ�����ʾ������������رռ��
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