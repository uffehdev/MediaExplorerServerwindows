using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace MediaExplorerServer
{
    class MainServer
    {
        Thread serverThread, connsThread;
        private int _port;
        private Socket _listener;
        private TcpServiceProvider _provider;
        private List<tcpConnection> _connections;
        private int _maxConnections = 100;

        private AsyncCallback ConnectionReady;
        private WaitCallback AcceptConnection;
        private AsyncCallback ReceivedDataReady;

        public MainServer(TcpServiceProvider provider, int port)
        {
            _provider = provider;
            _port = port;
            _listener = new Socket(AddressFamily.InterNetwork,
                            SocketType.Stream, ProtocolType.Tcp);
            _connections = new List<tcpConnection>();
            ConnectionReady = new AsyncCallback(ConnectionReady_Handler);
            AcceptConnection = new WaitCallback(AcceptConnection_Handler);
            ReceivedDataReady = new AsyncCallback(ReceivedDataReady_Handler);
        }


        public bool Start()
        {
            try
            {
                _listener.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), _port));
                _listener.Listen(100);
                _listener.BeginAccept(ConnectionReady, null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ConnectionReady_Handler(IAsyncResult ar)
        {
            lock (this)
            {
                if (_listener == null) return;
                Socket conn = _listener.EndAccept(ar);
                if (_connections.Count >= _maxConnections)
                {
                    //Max number of connections reached.
                    string msg = "SE001: Server busy";
                    conn.Send(Encoding.UTF8.GetBytes(msg), 0,
                              msg.Length, SocketFlags.None);
                    conn.Shutdown(SocketShutdown.Both);
                    conn.Close();
                }
                else
                {
                    //Start servicing a new connection
                    tcpConnection st = new tcpConnection();
                    st._conn = conn;
                    st._server = this;
                    st._provider = (TcpServiceProvider)_provider.Clone();
                    st._buffer = new byte[4];
                    _connections.Add(st);
                    //Queue the rest of the job to be executed latter
                    ThreadPool.QueueUserWorkItem(AcceptConnection, st);
                }
                //Resume the listening callback loop
                _listener.BeginAccept(ConnectionReady, null);
            }
        }

        private void AcceptConnection_Handler(object state)
        {
            tcpConnection st = state as tcpConnection;
            try { st._provider.OnAcceptConnection(st); }
            catch
            {
                //report error in provider... Probably to the EventLog
            }
            //Starts the ReceiveData callback loop
            if (st._conn.Connected)
                st._conn.BeginReceive(st._buffer, 0, 0, SocketFlags.None,
                  ReceivedDataReady, st);
        }

        private void ReceivedDataReady_Handler(IAsyncResult ar)
        {
            tcpConnection st = ar.AsyncState as tcpConnection;
            st._conn.EndReceive(ar);
            //Im considering the following condition as a signal that the
            //remote host droped the connection.
            if (st._conn.Available == 0) DropConnection(st);
            else
            {
                try { st._provider.OnReceiveData(st); }
                catch
                {
                    //report error in the provider
                }
                //Resume ReceivedData callback loop
                if (st._conn.Connected)
                    st._conn.BeginReceive(st._buffer, 0, 0, SocketFlags.None,
                      ReceivedDataReady, st);
            }
        }

        public void Stop()
        {
            lock (this)
            {
                _listener.Close();
                _listener = null;
                //Close all active connections
                foreach (object obj in _connections)
                {
                    tcpConnection st = obj as tcpConnection;
                    try { st._provider.OnDropConnection(st); }
                    catch
                    {
                        //some error in the provider
                    }
                    st._conn.Shutdown(SocketShutdown.Both);
                    st._conn.Close();
                }
                _connections.Clear();
            }
        }
        internal void DropConnection(tcpConnection st)
        {
            lock (this)
            {
                st._conn.Shutdown(SocketShutdown.Both);
                st._conn.Close();
                if (_connections.Contains(st))
                    _connections.Remove(st);
            }
        }


        public int MaxConnections
        {
            get
            {
                return _maxConnections;
            }
            set
            {
                _maxConnections = value;
            }
        }


        public int CurrentConnections
        {
            get
            {
                lock (this) { return _connections.Count; }
            }
        }
    }
}
