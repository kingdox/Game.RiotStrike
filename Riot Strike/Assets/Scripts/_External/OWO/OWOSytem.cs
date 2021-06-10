#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using OWO;
#endregion
/// <summary>
/// System to manage the "Conection" between the client and the jacket
/// </summary>
public class OWOSytem : MonoBehaviour
{
    #region Variables
    private static OWOSytem _;
    private const string LOCAL_IP = "127.0.0.1";
    private OWOUDPClient client;
    [Header("OWO System ~Feel the game~")]
    private string ip;
    public bool IsConnected { get; private set; } = false;
    public static Action<bool> OnConnectResult;
    #endregion
    #region Events
    private void Awake()
    {
        this.Singleton(ref _);
    }
    private void Start()
    {
        client = new OWOUDPClient();

        OnConnectResult += (bool result) => IsConnected = result;

        OWOClient.OnConnectionFailed += MsgFailedConection;
        OWOClient.OnConnected += MsgAgreedConection;
    }
    #endregion
    #region Methods

    /// <summary>
    /// Try to do the conection between the jacket and the game
    /// </summary>
    [ContextMenu("Connect (only in runPlay)")]
    public static void Connect(string _ip = default) => _._Connect(_ip);
    internal void _Connect(string _ip=default){
        if (ip == default) ip = LOCAL_IP;
        else ip = _ip;
        //$"Start connection with ip => {ip} ".Print("magenta");
        client.Connect(ip);
        
    }

    /// <summary>
    ///  Sended when fail the connection
    /// </summary>
    internal void MsgFailedConection() {
        "Conexión Fallida".Print("red");
        OnConnectResult.Invoke(false);
    }

    /// <summary>
    ///  Sended when is Success the connection
    /// </summary>
    internal void MsgAgreedConection(string info)
    {
        //FIXME Hacer un parche del cliente para saber si retorna?
        //por que nunca sabemos si realmente se comunica...?
        //ya que puede conectar y luego mandar el fallido..
        $"Conexión Lograda: {info}".Print("green");
        OnConnectResult.Invoke(true);
    }


    /// <summary>
    /// Send the sensation
    /// </summary>
    public static void SendSensation(ushort id, OWOMuscles muscle){
        if (!_.IsConnected) return;// 🛡
        $"Enviando {id} en {muscle}".Print("gray");
            
        _.client.SendSensation(id, muscle);
    }
    #endregion
}
