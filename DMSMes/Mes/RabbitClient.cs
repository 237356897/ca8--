using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using RabbitMQ.Client.Events;

namespace DMSMes
{
    class RabbitClient
    {
        /*
        * m_strHostName amqp主机名称
        */
        String m_strHostName;

        /*
        * m_strUser 服务端用户名
        */
        String m_strUser;

        /*
        *m_strPassWd 服务器的登陆密码
        */
        String m_strPassWd;


        int m_iPort;
        int m_iChannel;

        ConnectionFactory factory;
        IConnection connection;

        /*
        @brief ConnectToBroker   连接到 Rabbit-MQ Broker 
        @param [in] strHostName
        @param [in] iPort
        @param [in] strUser
        @param [in] strPassWd
        @return 0-表示连接上Rabbit-MQ Broker成功；负值表示连接到 Rabbit-MQ Broker 失败；
        */
        public int ConnectToBroker(String strHostName, int iPort, String strUser, String strPassWd)
        {

            m_strHostName = strHostName;
            m_iPort = iPort;
            m_strUser = strUser;
            m_strPassWd = strPassWd;

            factory = new ConnectionFactory();
            factory.HostName = strHostName;
            factory.Port = iPort;
            factory.UserName = strUser;
            factory.Password = strPassWd;

            //创建一个连接点
            try
            {
                connection = factory.CreateConnection();
                if (!connection.IsOpen)
                {
                    Console.WriteLine("amqp new connection failed.\n");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CreateConnection() throw exception.meassage = {0} \n", ex.Message);
            }

            return 0;
        }

        /*
        @brief DisConnectToBroker 断开连接到 Broker
        @return 0-表示断开与Broker的连接；负值表示函数执行异常
        */
        int DisConnectToBroker()
        {
            connection.Close();
            return 0;
        }

        /*
        @brief ExchangeDeclare   声明Exchange
        @param [in] exchange     交换器 Obj
        @return 0-表示Exchange创建成功；<0-表示创建Exchange失败
        */
        public int ExchangeDeclare(CRabbitmqExchange exchange)
        {
            try
            {
                IModel ch = connection.CreateModel();
                if (ch.IsClosed)
                {
                    Console.WriteLine("CreateModel() create channel fail");
                    return -1;
                }

                IDictionary<String, object> dic = null;
                ch.ExchangeDeclare(exchange.m_name, exchange.m_type, exchange.m_bDurable, exchange.m_bAutoDelete, dic);

                ch.ExchangeDeclarePassive(exchange.m_name);//检测路由是否存在，如果存在正常返回，不存在则抛出异常

                ch.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ExchangeDeclare() throw exception.meassage = {0} \n", ex.Message);
                return -2;
            }
            return 0;
        }

        /*
        @brief QueueDeclare      声明消息队列
        @param [in] queue        消息队列 Obj
        @return 0-表示队列创建成功；小于0表示创建队列失败
        */
        public int QueueDeclare(CRabbitmqQueue queue)
        {
            try
            {
                IModel ch = connection.CreateModel();
                if (ch.IsClosed)
                {
                    Console.WriteLine("CreateModel() create channel fail");
                    return -1;
                }

                IDictionary<String, object> dic = null;
                ch.QueueDeclare(queue.m_name, queue.m_durable, queue.m_bExclusive, queue.m_bAutoDelete, dic);
                ch.QueueDeclarePassive(queue.m_name);//检测队列是否存在，如果存在正常返回，不存在则抛出异常

                ch.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("QueueDeclare() throw exception.meassage = {0} \n", ex.Message);
                return -2;
            }
            return 0;

        }

        // KeyInfo BindingKey: Rabbit-MQ中通过绑定交换器与队列关联起来，在 Binding的时候一般会指定一个绑定键 BindingKey。通过 BindingKey Rabbit-MQ就知道如何将
        // KeyInfo             Message 正确的路由到 Queue了。

        // KeyInfo RoutingKey: 生产者（Publisher）将消息发布给 Exchange 的时候一般会指定一个 RoutiingKey，用来指定这个消息的路由规则。

        // KeyInfo Publisher 发布消息到 Exchange,下一步路由到 Queue 时，BindingKey 和 RoutingKey 相匹配时，消息才会被路由到相应的队列中；
        // KeyInfo BindingKey 在某些 Exchange 类型下是不生效的，比如在 "fanout"类型的交换器就会 无视 BindingKey；
        // KeyInfo "Direct" 类型的 Exchange ，BindingKey 和 RoutingKey 需要完全匹配；"topic" 类型的 Exchange，BindingKey 和 RoutingKey 需要模糊匹配；

        /*
        @brief QueueBinding        将队列，交换机和绑定规则绑定起来形成路由
        @param [in] queue          消息队
        @param [in] exchange       交换机
        @param [in] strBindingKey  路由名称----- "msg.#".eg."msg.weather.**"
        @return 0-表示成功绑定；小于0代表错误
        */
        public int BindingQueueToExchange(CRabbitmqQueue queue, CRabbitmqExchange exchange, String strBindingKey)
        {
            try
            {
                IModel ch = connection.CreateModel();
                if (ch.IsClosed)
                {
                    Console.WriteLine("CreateModel() create channel fail");
                    return -1;
                }
                ch.ConfirmSelect();
                ch.QueueBind(queue.m_name, exchange.m_name, strBindingKey);

                ch.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("BindingQueueToExchange() throw exception.meassage = {0} \n", ex.Message);
                return -2;
            }
            return 0;
        }

        /*
        @brief QueueUnbingding     解除绑定队列
        @param [in] queue          消息队列名称
        @param [in] exchange       交换机名称
        @param [in] strBindingKet  绑定路由名称
        @return 0-表示成功解除绑定；小于0表示解除绑定失败
        */
        int UnbindingQueueToExchange(CRabbitmqQueue queue, CRabbitmqExchange exchange, String strBindingKey)
        {
            try
            {
                IModel ch = connection.CreateModel();
                if (ch.IsClosed)
                {
                    Console.WriteLine("CreateModel() create channel fail");
                    return -1;
                }

                IDictionary<String, object> dic = null;
                ch.QueueUnbind(queue.m_name, exchange.m_name, strBindingKey, dic);

                ch.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("BindingQueueToExchange() throw exception.meassage = {0} \n", ex.Message);
                return -2;
            }
            return 0;
        }

        /*
        @brief QueueDelete         删除队列
        @param [in] strQueueName   消息队列名称
        @param [bool] iIfUnused      消息队列是否在用：1--无论是否在用都删除
        @return 0-表示成功删除；小于0表示错误
        */
        int QueueDelete(CRabbitmqQueue queue, bool iIfUnused)
        {
            try
            {
                IModel ch = connection.CreateModel();
                if (ch.IsClosed)
                {
                    Console.WriteLine("CreateModel() create channel fail");
                    return -1;
                }

                IDictionary<String, object> dic = null;
                ch.QueueDelete(queue.m_name, iIfUnused, false);

                ch.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(" QueueDelete() throw exception.meassage = {0} \n", ex.Message);
                return -2;
            }
            return 0;
        }

        /*
        @brief PublishMessage      发布消息
        @param [in] message        消息实体
        @param [in] exchange       交换机名称
        @param [in] strRouteKey    分发路由的规则:1:DirectExchange;2.Fanout Exchange;3.Topic Exchange;详见 https://zhuanlan.zhihu.com/p/48779080
        @return 0-表示消息发布成功； 小于0表示消息发布失败
        */
        public int PublishMessage(byte[] message, CRabbitmqExchange exchange, String strRouteKey)
        {

            try
            {
                IModel ch = connection.CreateModel();
                if (ch.IsClosed)
                {
                    Console.WriteLine("CreateModel() create channel fail");
                    return -1;
                }

                //设置传输参数
                IBasicProperties properties = ch.CreateBasicProperties();

                //写入  使用confirm机制
                ch.ConfirmSelect();
                ch.BasicPublish(exchange.m_name, strRouteKey, properties, message);
                TimeSpan time = new TimeSpan(0, 0, 0, 0, 100);      //等待延迟时间
                if (!ch.WaitForConfirms(time))
                    Console.WriteLine("send message failed.");
                else
                    Console.WriteLine("send message succeed.");

                ch.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(" PublishMessage() throw exception.meassage = {0} \n", ex.Message);
                return -2;
            }

            return 0;
        }

        /*
        @brief PubulishMessages    批量发布消息
        @param [in] messages       待发布消息池
        @param [in] exchange       交换机名称
        @param [in] strRoutrKey    发布路由规则
        @return 0-表示消息发布成功； 小于0表示消息发布失败
        */
        int PublishMessages(List<byte[]> messages, CRabbitmqExchange exchange, String strRouteKey)
        {
            for (int i = 0; i < messages.Count; i++)
            {
                int ret = PublishMessage(messages[i], exchange, strRouteKey);
                if (ret != 0)
                {
                    Console.WriteLine(" 第{0}条数据传输失败", i);
                    return ret;
                }
            }
            return 0;
        }

        /*
        @brief ConsumerMessage      消费消息
        @param [in] strQueueName    队列名称
        @param [out] messageArray   获取的消息实体
        @param [in] getNum          需要获取的消息个数
        @param [bool] isBlock       阻塞状态,true为阻塞获取，false为非阻塞
        @return 0-表示获取消息成功；  小于0表示错误 -- 错误消息从ErrorReturn 返回
        */
        public int ConsumerMessage(CRabbitmqQueue queue, ref List<String> messageArray, int getNum = 1, bool isBlock = false)
        {
            try
            {
                IModel ch = connection.CreateModel();
                if (ch.IsClosed)
                {
                    Console.WriteLine("CreateModel() create channel fail");
                    return -1;
                }
                //输入1，那如果接收一个消息，但是没有应答，则客户端不会收到下一个消息
                ch.BasicQos(0, 1, false);

                //消费
                if (isBlock)
                {
                    //在队列上定义一个消费者
                    QueueingBasicConsumer consumer = new QueueingBasicConsumer(ch);
                    // EventingBasicConsumer consumer = new EventingBasicConsumer(ch);
                    while (getNum > 0)
                    {

                        //消费队列，并设置应答模式为程序主动应答
                        ch.BasicConsume(queue.m_name, false, consumer);
                        //阻塞函数，获取队列中的消息
                        BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        byte[] bytes = ea.Body;
                        string str = Encoding.UTF8.GetString(bytes);
                        messageArray.Add(str);

                        //回复确认
                        ch.BasicAck(ea.DeliveryTag, false);
                        getNum--;
                    }
                }
                else
                {
                    while (getNum > 0)
                    {
                        BasicGetResult res = ch.BasicGet(queue.m_name, false/*noAck*/);
                        if (res != null)
                        {
                            messageArray.Add(System.Text.UTF8Encoding.UTF8.GetString(res.Body));
                            ch.BasicAck(res.DeliveryTag, false);

                        }
                        else
                        {
                            Console.WriteLine("无内容!!");
                        }
                        getNum--;
                    }
                }
                ch.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(" PublishMessage() throw exception.meassage = {0} \n", ex.Message);
                return -2;
            }

            return 0;
        }

    }
}
