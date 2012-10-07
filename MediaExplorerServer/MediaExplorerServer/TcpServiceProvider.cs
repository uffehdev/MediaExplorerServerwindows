using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MediaExplorerServer
{
    class TcpServiceProvider
    {
        public abstract class TcpServiceProvider : ICloneable
        {
            public virtual object Clone()
            {
                throw new Exception("Derived clases" +
                          " must override Clone method.");
            }

            public abstract void OnAcceptConnection(tcpConnection state);

            public abstract void OnReceiveData(tcpConnection state);

            public abstract void OnDropConnection(tcpConnection state);
        }
    }
}
