using System;
using System.Collections.Generic;
using System.Text;

namespace AOPCore.ILayer
{
    public interface IOrderProcessor
    {
        void Submit(Order order);
    }
}
