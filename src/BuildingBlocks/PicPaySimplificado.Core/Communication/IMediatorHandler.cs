using PicPaySimplificado.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Core.Communication
{
    public interface IMediatorHandler
    {
        Task<bool> SendCommand<T>(T command) where T : Command;

    }
}
