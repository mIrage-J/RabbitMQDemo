using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTask
{
    class NewTask
    {
        static void Main(string[] args)
        {
            
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        var message = GetMessage(args);
                        var body = Encoding.UTF8.GetBytes(message);

                        var propertities = channel.CreateBasicProperties();
                        propertities.Persistent = true;

                    channel.BasicPublish(exchange: "",
                        routingKey: "task_queue",
                        basicProperties: propertities,
                        body: body);
                    }
                }
        }
        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }

    }
}
