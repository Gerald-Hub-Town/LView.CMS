using DynamicProxyAOP.Entity;
using DynamicProxyAOP.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicProxyAOP.Service
{
    public class UserOperation : MarshalByRefObject, IUserOperation
    {
        public void Test(User oUser)
        {
            Console.WriteLine("Test方法执行了");
        }

        public void Test(User oUser, User oUser2)
        {
            Console.WriteLine("Test2方法执行了");
        }
    }
}
