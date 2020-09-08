using AOPCore.Decorator;
using AOPCore.ILayer;
using AOPCore.Layer;
using System;
using System.Threading;

namespace AOPCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order() { Id = 1, Name = "Lee", Count = 10, Price = 1000.00, Desc = "订单测试数据" };
            IOrderProcessor orderproc = new OrderProcessor();
            orderproc.Submit(order);
            Thread.Sleep(5000);
            IOrderProcessor orderprocessor = new OrderProcessorDecorator(new OrderProcessor());
            orderprocessor.Submit(order);
            Console.ReadLine();
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public string Desc { get; set; }
    }
}
