using AOPCore.ILayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace AOPCore.Decorator
{
    public class OrderProcessorDecorator : IOrderProcessor
    {
        public IOrderProcessor OrderProcessor { get; set; }
        public OrderProcessorDecorator(IOrderProcessor orderprocessor)
        {
            OrderProcessor = orderprocessor;
        }

        public void Submit(Order order)
        {
            PreProcessed(order);
            OrderProcessor.Submit(order);
            PostProcessed(order);
        }

        public void PreProcessed(Order order)
        {
            Console.WriteLine("提交订单前，进行订单数据校验....");
            if (order.Price < 0)
            {
                Console.WriteLine("订单总价有误，请重新核对订单");
            }
        }

        public void PostProcessed(Order order)
        {
            Console.WriteLine("提交订单后，进行订单日志记录....");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "提交订单，订单名称：" + order.Name + "，订单价格：" + order.Price);
        }
    }
}
