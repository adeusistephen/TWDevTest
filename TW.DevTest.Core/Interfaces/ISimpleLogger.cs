using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.DevTest.Core.Interfaces
{
    public interface ISimpleLogger
    {
        void Debug(string message);
        void Info(string message);
        void Error(string message);
        void Warning(string message);
    }
}
