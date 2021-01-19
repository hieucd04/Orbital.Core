using System.Net;
using System.Net.Sockets;
using JetBrains.Annotations;

namespace Orbital.Core
{
    internal class Network : INetwork
    {
        public string LocalIpAddress { get; }

        [UsedImplicitly]
        public Network()
        {
            using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0);
            socket.Connect("8.8.8.8", 65530);
            var endPoint = (IPEndPoint) socket.LocalEndPoint;
            LocalIpAddress = endPoint?.Address.ToString();
        }
    }
}