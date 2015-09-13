using System;
using System.ServiceModel;

namespace De.Server
{
    public interface IServer
    {
        void StartServer();
        void StopServer();
    }

    public class Server<TService, TContract> : IServer
    {
        private readonly ServiceHost _serviceHost;

        public Server(string hostname, string portNum, string endpoint)
        {
            _serviceHost = new ServiceHost(typeof (TService));
            _serviceHost.AddServiceEndpoint(typeof (TContract), new NetTcpBinding(SecurityMode.None),
                new Uri($"net.tcp://{hostname}:{portNum}/{endpoint}"));
        }

        public void StartServer()
        {
            try
            {
                _serviceHost.Open();
            }
            catch (CommunicationException)
            {
                _serviceHost.Abort();
            }
        }

        public void StopServer()
        {
            _serviceHost.Close();
        }
    }
}