using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.PublishSubscribe.Redis;

public interface IRedisMessageConsumer
{
    Task Register();

    Task Unregister();
}
