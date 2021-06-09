using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OWO
{
    public class OWOUDPClient : OWOClient
    {
        private UdpClient client;
        private IPEndPoint endPoint;

        public override void Connect(string _ip)
        {
            try
            {
                endPoint = new IPEndPoint(IPAddress.Parse(_ip), port);
                client = new UdpClient(_ip, port);

                client.Connect(endPoint);
                OnConnected?.Invoke(_ip);
            }
            catch
            {
                OnConnectionFailed?.Invoke();
            }
        }

        public override void FindServersInLAN()
        {
            DeviceFinder deviceFinder = new DeviceFinder(this);

            deviceFinder.SearchForServers();
        }

        public override void SendMessageToOWOApp(string _message)
        {
            try
            {
                byte[] message = Encoding.ASCII.GetBytes(_message);

                client.Send(message, message.Length);
            }
            catch
            {
                OnDisconnected?.Invoke();
            }
        }

        public override void Disconnect()
        {
            if (client == null) return;

            SendMessageToOWOApp("Close");

            Task.Delay(1000).ContinueWith(task =>
            {
                client.Close();
                client = null;
            });

            OnDisconnected?.Invoke();
        }
    }
}