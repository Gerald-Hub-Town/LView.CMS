using AOPCore.ILayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace AOPCore.Layer
{
    public class OrderProcessor : IOrderProcessor
    {
        public void Submit(Order order)
        {
            Console.WriteLine("提交订单");
        }
    }
}
