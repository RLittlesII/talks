using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mediator.Demo.Services
{
    public class Logger : ILogger
    {
        public Task Log(string log)
        {
            Console.WriteLine(log);

            return Task.CompletedTask;
        }
    }
}