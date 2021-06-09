using System.Net;
using System.Net.Sockets;
using System.Text;

namespace OWO
{
    public class DeviceFinder
    {
        private readonly OWOClient owoClient;
        private readonly UdpClient client;
        private readonly IPEndPoint endPoint;

        private const string broadcastMessage = "ping";
        private const string expectedResponse = "pong";

        public DeviceFinder(OWOClient _client)
        {
            owoClient = _client;

            endPoint = new IPEndPoint(IPAddress.Broadcast, 54010);
            client = new UdpClient();

            client.EnableBroadcast = true;
        }

        /// <summary>
        /// Sends a broadcast message to detect if a server is listening.
        /// </summary>
        public void SearchForServers()
        {
            byte[] message = Encoding.ASCII.GetBytes(broadcastMessage);
            client.Send(message, message.Length, endPoint);

            WaitForServerResponseAndConnect();
        }

        /// <summary>
        /// Waits for a response of the broadcast and connects to the first server detected.
        /// </summary>
        private async void WaitForServerResponseAndConnect()
        {
            bool responseReceived = false;

            UdpReceiveResult result;
            string serverResponse;

            while (!responseReceived)
            {
                result = await client.ReceiveAsync();
                serverResponse = Encoding.ASCII.GetString(result.Buffer);

                if (serverResponse.Equals(expectedResponse))
                {
                    responseReceived = true;
                }
            }

            owoClient.Connect(result.RemoteEndPoint.Address.ToString());
        }
    }
}