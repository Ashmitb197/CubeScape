using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class GesturePlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    private Rigidbody playerRb;

    private UdpClient udpClient;
    private int listenPort = 8080;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        try
        {
            udpClient = new UdpClient(listenPort); // Initialize UDP client on port 7070
            Debug.Log($"UDP Client started on port {listenPort}");
        }
        catch (SocketException ex)
        {
            Debug.LogError($"Failed to initialize UDP Client: {ex.Message}");
        }
    }

    private void Update()
    {
        if (udpClient != null)
        {
            try
            {
                if (udpClient.Available > 0)
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, listenPort);
                    byte[] receivedBytes = udpClient.Receive(ref remoteEndPoint);
                    string gesture = Encoding.ASCII.GetString(receivedBytes);

                    HandleGesture(gesture);
                }
            }
            catch (SocketException ex)
            {
                Debug.LogError($"UDP Receive error: {ex.Message}");
            }
        }
    }

    private void HandleGesture(string gesture)
    {
        if (gesture == "W")
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else if (gesture == "A")
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (gesture == "S")
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        else if (gesture == "D")
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }

    private void OnApplicationQuit()
{
    if (udpClient != null)
    {
        udpClient.Close();
        Debug.Log("UDP Client closed.");
    }
}

}
