using GameServer.Player;
using GameServer.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Network.Server
{
    public class UdpServer
    {
        public const int PORT = 8000;

        private UdpClient _server;
        private PlayerManager _playerManager;

        private bool _isRunning;
        private Thread _listenThread;

        public UdpServer()
        {
            _playerManager = new PlayerManager();
        }

        public void Start()
        {
            _isRunning = true;

            _server = new UdpClient(PORT);
            Logger.Info($"server is running on port {PORT}.");

            _listenThread = new Thread(new ThreadStart(Listen));
            _listenThread.Start();
        }

        public void Stop()
        {
            _isRunning = false;

            _listenThread.Abort();
            _server.Close();
        }

        private void Listen()
        {
            try
            {
                while (_isRunning)
                {
                    IPEndPoint clientIp = new IPEndPoint(IPAddress.Any, 0);
                    byte[] data = _server.Receive(ref clientIp);
                    Logger.Info(clientIp.Address.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Server exception: {ex.Message}");
            }
        }
    }
}
