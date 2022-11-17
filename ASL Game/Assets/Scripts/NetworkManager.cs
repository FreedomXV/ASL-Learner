using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Net;
using System.IO;
using UnityEngine.UI;
using System.Text;


public class NetworkManager : MonoBehaviour
{

    public Text ipaddress;
    public IPAddress serverIp = IPAddress.Parse("127.0.0.1");
    public Text text;
    TcpListener listener;
    public string messageToDisplay;
    private Material materialColored;

    bool newValue = true;
    Vector3 pos = Vector3.zero;
    Vector3 color = Vector3.zero;
    List<TcpClient> clientList = new List<TcpClient>();

    

    void Start()
    {
        //println(ipaddress.text.length());
        listener = new TcpListener(serverIp, port: Globals.port);
        listener.Start();
        listener.BeginAcceptTcpClient(OnServerConnect, null);
        print(listener.Server.ToString());


    }

    // Update is called once per frame
    void Update()
    {
        if (newValue)
        {
        text.text = messageToDisplay;
        newValue = false;
        }
    }

    protected void OnApplicationQuit()
    {
        listener?.Stop();
        for (int i = 0; i < clientList.Count; i++)
        {
            clientList[i].Close();
        }
    }


    void OnServerConnect(IAsyncResult ar)
    {
        TcpClient tcpClient = listener.EndAcceptTcpClient(ar);
        clientList.Add(tcpClient);
        NetworkStream nwStream = tcpClient.GetStream();
        byte[] buffer = new byte[tcpClient.ReceiveBufferSize];

        int bytesRead = nwStream.Read(buffer, 0, tcpClient.ReceiveBufferSize);
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        print(dataReceived);

        messageToDisplay = "Received : " + dataReceived;
        string type = dataReceived.Split('_')[0];
        int idx = int.Parse(dataReceived.Split('_')[1]);
        print(messageToDisplay);
        
       
        newValue = true;

        listener.BeginAcceptTcpClient(OnServerConnect, null);
    }

    public void OnDisconnect(TcpClient client)
    {
        clientList.Remove(client);
    }
}