using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSMes
{
    class CRabbitmqExchange
    {
        /**
        * m_name 交换机名称
        *
        **/
        public String m_name;

        /**
        * m_type 指定exchange类型，"fanout"  "direct" "topic"三选一
        *	"fanout" 广播的方式，发送到该exchange的所有队列上(不需要进行bind操作)
        *	"direct" 通过路由键发送到指定的队列上(把消息发送到匹配routing key的队列中。)
        *	"topic" 通过匹配路由键的方式获取，使用通配符*，#
        *    "headers" 通过消息内容中的 headers 属性进行匹配，性能差，很少使用
        **/
        public String m_type;

        /**
        * m_durable 交换机是否持久化(当mq服务端断开重启后，交换机是否还存在)
        **/
        public bool m_bDurable;

        /**
        * m_auto_delete 连接断开时，交换机是否自动删除
        *
        **/
        public bool m_bAutoDelete;

        /**
        * m_internal 默认为0，没有使用到
        *
        **/
        public bool m_bInternal;

        /**
        * passive 检测exchange是否存在
        *	设为true，
        *		若exchange存在则命令成功返回（调用其他参数不会影响exchange属性），
        *		若不存在不会创建exchange，返回错误。
        *	设为false，
        *		如果exchange不存在则创建exchange，调用成功返回。
        *		如果exchange已经存在，并且匹配现在exchange的话则成功返回，如果不匹配则exchange声明失败。
        **/
        public bool m_bPassive;

        public CRabbitmqExchange(String name, String type = "direct",
             bool durable = true, bool passive = true, bool auto_delete = false, bool internals = false)
        {

            this.m_name = name;

            this.m_type = type;

            this.m_bDurable = durable;

            this.m_bPassive = passive;

            this.m_bAutoDelete = auto_delete;

            this.m_bInternal = internals;

        }

        CRabbitmqExchange(CRabbitmqExchange other)
        {
            this.m_name = other.m_name;
            this.m_bDurable = other.m_bDurable;
            this.m_type = other.m_type;
            this.m_bAutoDelete = other.m_bAutoDelete;
            this.m_bInternal = other.m_bInternal;
            this.m_bPassive = other.m_bPassive;
        }
    }
}
