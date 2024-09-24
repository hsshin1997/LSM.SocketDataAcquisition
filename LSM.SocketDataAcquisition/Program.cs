using System;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;

class LsmSocketDataAcquisitionCon
{
    static void Main(string[] args)
    {
        // LSM Motor IP Address and Port
        string ipAddress = "192.168.1.11";
        int port = 800; // port 799

        try
        {
            // Create a TCP Client
            TcpClient client = new TcpClient();
            Console.WriteLine($"Connecting to LSM motor at {ipAddress}:{port}");

            // connect to the LSM motor
            client.Connect(ipAddress, port);

            Console.WriteLine("Connected to LSM motor");

            // Get the network stream for reading and writing
            NetworkStream stream = client.GetStream();

            // Set read and write timeouts (in milliseconds)
            stream.ReadTimeout = 5000;
            stream.WriteTimeout = 5000;

            // Example command to send to the motor
            // string command = "Status_Request";
            byte[] status_request = new byte[5];
            status_request[0] = 0xB5; // Command Identifier
            status_request[1] = 0x05; // Request Type
            status_request[2] = 0x00; // Path ID (Use 0 to select all path)
            status_request[3] = 0x00; // Motor ID (Use 0 to select all motor)
            status_request[4] = 0x00; // Additional data or checksum 

            // Send the command to the motor
            Console.WriteLine("Sending command to LSM motor...");
            // Write the command bytes to the network stream
            stream.Write(status_request, 0, status_request.Length);

            // Create a list to store the full response
            List<byte> fullResponse = new List<byte>();


            client.Close();
            Console.WriteLine("Connection Closed");
        }
        catch (SocketException se)
        {
            Console.WriteLine($"SocketException: {se.SocketErrorCode} - {se.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }

        Console.WriteLine("\nPress Enter to exit...");
        Console.ReadLine();
    }
}