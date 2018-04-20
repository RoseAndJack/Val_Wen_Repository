using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ;
using RabbitMQ.Client;

namespace ConsoleApplication2
{
    class Subscriber
    {
        private ConnectionFactory fact;
        private string Queue = "rose";
        /// <summary>
        /// 构造自动注册
        /// </summary>
        /// <param name="p"></param>
        public Subscriber(Publisher p)
        {
            this.fact = new ConnectionFactory();
            fact.HostName = "localhost";
            fact.UserName = "guest";
            fact.Password = "guest";
            p.TempEvent += new Publisher.TempHandler(this.SendMessage);
        }

        /// <summary>
        /// 反注册
        /// </summary>
        /// <param name="p"></param>
        public void CancelRegister(Publisher p)
        {
            p.TempEvent -= new Publisher.TempHandler(this.SendMessage);
        }

        /// <summary>
        /// 待注册的执行方法
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private void SendMessage(object o, MyEventArgs e)
        {
            using (var con = fact.CreateConnection())
            {
                using (var channel = con.CreateModel())
                {
                    channel.QueueDeclare(Queue, true, false, false, null);
                    IBasicProperties properites = channel.CreateBasicProperties();
                    properites.DeliveryMode = 2;
                    string message = "this is cpp1" + e.p1;
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", Queue, properites, body);
                    channel.BasicReturn += (object sender, RabbitMQ.Client.Events.BasicReturnEventArgs e1)=>
        {
            throw new NotImplementedException();
        };
                }
            }

        }

        
    }


}
