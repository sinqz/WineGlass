using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetWorkAndData;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System;
using NetWorkAndData.APIS;
using LitJson;

public class NetworkController : MonoSingletonTemplateScript<NetworkController>
{

    public void OnLoad()
    {
        Connect(APIS.SERVER_IP, APIS.SERVER_PORT);
        Response res = new Response();
        onRecv = new System.Action<ProtocolTypes, Hashtable>(res.OnRecvMethod);
    }

    public void onDispatch(ProtocolTypes type, byte[] json)
    {
        string jsonStr = Encoding.UTF8.GetString(json);
        Hashtable res = (Hashtable)MiniJSON.jsonDecode(jsonStr);
        if (res == null || res.Count <= 0)
            return;
        onRecv.Invoke(type, res);
    }

    public Action onConnect = new Action(() =>
    {

        Debug.Log("connected");

    });

    public Action onDisconnect = new Action(() =>
    {
        Debug.Log("disconnected");
    });

    public Action<ProtocolTypes, Hashtable> onRecv = new Action<ProtocolTypes, Hashtable>((ProtocolTypes type, Hashtable result) =>
     {

     });

    public Action<Packet> onEvent = new Action<Packet>((Packet packet) =>
    {

        switch (packet.type)
        {
            case PacketType.connect:
                {
                    Instance.onConnect?.Invoke();
                }
                break;
            case PacketType.disconnect:
                {
                    Instance.onDisconnect?.Invoke();
                }
                break;
            case PacketType.recv:
                {
                    Instance.onDispatch(packet.proto, packet.data);
                }
                break;
        }

    });

    void Update()
    {
        Packet packet;
        lock (msgQueue)
        {
            if (msgQueue.Count < 1)
            {
                return;
            }
            packet = msgQueue.Dequeue();
        }

        if (null == packet)
        {
            return;
        }

        if (null == onEvent)
        {
            return;
        }

        onEvent(packet);
    }

    TcpClient tcpClient;

    Thread recvThread;

    Queue<Packet> msgQueue = new Queue<Packet>();

    public bool Connect(string ip, int port)
    {
        tcpClient = new TcpClient(ip, port);

        if (!tcpClient.Connected)
        {
            return false;
        }

        onEvent(new Packet { type = PacketType.connect });

        recvThread = new Thread(Run);
        recvThread.Start();

        return tcpClient.Connected;
    }

    public void Close()
    {
        if (null == tcpClient || !tcpClient.Connected)
        {
            return;
        }

        isClose = true;

        lock (tcpClient)
        {
            tcpClient.Close();
            tcpClient = null;
        }

        recvThread.Join();
    }

    public void Send<T>(T packet) where T : ProtocolBase
    {
        var json = JsonUtility.ToJson(packet);
        var data = Encoding.UTF8.GetBytes(json);
        Send(packet.type, data);
    }


    const int bodySize = 2;
    const int typeSize = 2;
    const int headerSize = bodySize + typeSize;

    private bool Send(ProtocolTypes type, byte[] data)
    {
        try
        {
            lock (tcpClient)
            {
                if (null == tcpClient)
                {
                    return false;
                }

                var stream = tcpClient.GetStream();
                if (!stream.CanWrite)
                {
                    return false;
                }

                var lengthByte = BitConverter.GetBytes((UInt16)data.Length);
                var typeByte = BitConverter.GetBytes((UInt16)type);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(lengthByte);
                    Array.Reverse(typeByte);
                }

                stream.Write(lengthByte, 0, lengthByte.Length);
                stream.Write(typeByte, 0, typeByte.Length);
                stream.Write(data, 0, data.Length);
            }
        }
        catch (SocketException ex)
        {
            Debug.LogError(ex.ToString());
            return false;
        }

        return true;
    }

    private bool isClose = false;

    void Post(Packet packet)
    {
        lock (msgQueue)
        {
            msgQueue.Enqueue(packet);
        }
    }

    void Run()
    {
        byte[] recvBuffer = new byte[1024000];
        int readPos = 0;

        NetworkStream stream = tcpClient.GetStream();

        while (!isClose)
        {
            int remainSize = recvBuffer.Length - readPos;
            if (remainSize <= 0)
            {
                Post(new Packet { type = PacketType.disconnect });
                return;
            }

            try
            {
                int read = stream.Read(recvBuffer, readPos, remainSize);
                if (read <= 0)
                {
                    Post(new Packet { type = PacketType.disconnect });
                    return;
                }
                readPos += read;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
                Post(new Packet { type = PacketType.disconnect });
                return;
            }

            if (readPos < headerSize)
            {
                continue;
            }

            var sizeByte = new byte[2];
            var typeByte = new byte[2];

            Array.Copy(recvBuffer, 0, sizeByte, 0, 2);
            Array.Copy(recvBuffer, 2, typeByte, 0, 2);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(sizeByte);
                Array.Reverse(typeByte);
            }

            int bodyLen = BitConverter.ToUInt16(sizeByte, 0);
            int protoType = BitConverter.ToUInt16(typeByte, 0);

            var packetSize = headerSize + bodyLen;
            if (readPos < packetSize)
            {
                continue;
            }

            byte[] packet = new byte[bodyLen];
            Array.Copy(recvBuffer, headerSize, packet, 0, bodyLen);
            Array.Copy(recvBuffer, packetSize, recvBuffer, 0, readPos - packetSize);
            readPos -= packetSize;

            Post(new Packet { type = PacketType.recv, proto = (ProtocolTypes)protoType, data = packet });
        }
    }

    private void OnDestroy()
    {
        Close();
    }

}
