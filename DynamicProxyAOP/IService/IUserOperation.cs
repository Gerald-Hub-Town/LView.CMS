using DynamicProxyAOP.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicProxyAOP.IService
{
    public interface IUserOperation
    {
        void Test(User oUser);
        void Test(User oUser, User oUser2);
    }
}
