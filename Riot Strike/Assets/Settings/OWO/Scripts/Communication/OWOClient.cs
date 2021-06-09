using System;

namespace OWO
{
    public enum OWOMuscles
    {
        Pectoral_R, Pectoral_L,
        Abdominal_R, Abdominal_L,
        Arm_R, Arm_L,
        Dorsal_R, Dorsal_L,
        Lumbar_R, Lumbar_L
    };

    public abstract class OWOClient
    {
        protected const int port = 54010;

        public static Action<string> OnConnected = delegate { };
        public static Action OnDisconnected = delegate { };
        public static Action OnConnectionFailed = delegate { };
        public static Action<ushort, OWOMuscles> OnSensationSend = delegate { };

        /// <summary>
        /// Starts connection with OWO client.
        /// </summary>
        /// <param name="_ip"></param>
        public abstract void Connect(string _ip);

        /// <summary>
        /// Finds servers in LAN and connects to the first one.
        /// </summary>
        public abstract void FindServersInLAN();

        /// <summary>
        /// Sends a sensation to OWO App specifying the sensation id
        /// and the desired muscle.
        /// </summary>
        /// <param name="_sensationId"></param>
        /// <param name="_muscle"></param>
        public void SendSensation(ushort _sensationId, OWOMuscles _muscle)
        {
            SendMessageToOWOApp($"owo/{_sensationId}/{(ushort)_muscle}/eof");
            OnSensationSend?.Invoke(_sensationId, _muscle);
        }

        /// <summary>
        /// Communicates with OWO App to send messages.
        /// </summary>
        /// <param name="_message"></param>
        public abstract void SendMessageToOWOApp(string _message);

        /// <summary>
        /// Closes communication with OWO App.
        /// </summary>
        public abstract void Disconnect();
    }
}