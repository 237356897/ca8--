using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desay
{
    public partial class FrmMesTest : Form
    {
        public FrmMesTest()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //// RabbitMQ连接
            //RabbitClient mqClient = new RabbitClient();
            //if (0 != mqClient.ConnectToBroker(connect_host, connect_port, connect_user, connect_pwd))
            //{
            //    Console.WriteLine("RabbitMQ Connect fail\n");
            //    return;
            //}
            //// 声明交换机
            //CRabbitmqExchange exchange = new CRabbitmqExchange(exchange_name, "topic");
            //if (0 != mqClient.ExchangeDeclare(exchange))
            //{
            //    Console.WriteLine("Create Exchange fail\n");
            //    return;
            //}
            //// 声明发送队列
            //CRabbitmqQueue queue = new CRabbitmqQueue("libing_test_topic1");
            //if (0 != mqClient.QueueDeclare(queue))
            //{
            //    Console.WriteLine("Create Queue fail\n");
            //    return;
            //}
            ////声明接受队列
            //CRabbitmqQueue queue_recv = new CRabbitmqQueue("TSR1234");
            //if (0 != mqClient.QueueDeclare(queue_recv))
            //{
            //    Console.WriteLine("Create Queue fail\n");
            //    return;
            //}
            //// 队列绑定到交换机上
            //if (0 != mqClient.BindingQueueToExchange(queue, exchange, routing_keys_name))
            //{
            //    Console.WriteLine("BindingQueueToExchange fail\n");
            //    return;
            //}

            //if (0 != mqClient.BindingQueueToExchange(queue_recv, exchange, routing_keys_name))
            //{
            //    Console.WriteLine("BindingQueueToExchange fail\n");
            //    return;
            //}
        }

        private void btnFracture_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }
    }
}
