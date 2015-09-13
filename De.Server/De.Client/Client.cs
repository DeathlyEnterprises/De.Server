using System.ServiceModel;

namespace De.Client
{
    public interface IClient<out TService>
    {
        void Connect();
        void Disconnect();
        TService GetService();
    }

    public class Client<TService> : IClient<TService>
    {
        private readonly ChannelFactory<TService> _channelFactory;
        private TService _service;

        public Client(string hostname, string portNum, string endpoint)
        {
            _channelFactory = new ChannelFactory<TService>(new NetTcpBinding(SecurityMode.None),
                new EndpointAddress($"net.tcp://{hostname}:{portNum}/{endpoint}"));
        }

        public void Connect()
        {
            _service = _channelFactory.CreateChannel();
        }

        public void Disconnect()
        {
            _channelFactory.Close();
        }

        public TService GetService()
        {
            return _service;
        }
    }
}