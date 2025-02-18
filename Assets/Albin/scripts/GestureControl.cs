using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class GestureControl : MonoBehaviour
{
    private UdpClient udpClient;
    private IPEndPoint remoteEndPoint;

    void Start()
    {
        try
        {
            udpClient = new UdpClient(7070); // Ensure the port matches the Python script
            remoteEndPoint = new IPEndPoint(IPAddress.Any, 7070);
            Debug.Log("UDP Client initialized successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to initialize UDP Client: " + ex.Message);
        }
    }

    void Update()
    {
        if (udpClient == null)
        {
            Debug.LogError("UDP Client is not initialized!");
            return;
        }

        if (udpClient.Available > 0)
        {
            try
            {
                byte[] data = udpClient.Receive(ref remoteEndPoint);
                string gesture = Encoding.UTF8.GetString(data);
                Debug.Log("Received gesture: " + gesture);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error receiving data: " + ex.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        if (udpClient != null)
        {
            udpClient.Close();
            Debug.Log("UDP Client closed.");
        }
    }
}
