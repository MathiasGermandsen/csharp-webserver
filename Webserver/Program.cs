using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public static class Program
{
  public static void Main(string[] args)
  {
    IHttpServer server = new HttpServer(13000);
    server.Start();
  }
}

public interface IHttpServer
{
  void Start();
}

public class HttpServer : IHttpServer
{
  private readonly TcpListener listener;

  public HttpServer(int port)
  {
    listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
  }

  public void Start()
  {
    int i = 0;
    listener.Start();

    while (true)
    {
      Console.WriteLine("Waiting for client {0}", i++);
      var client = listener.AcceptTcpClient();
      Console.WriteLine("TcpClient accepted");

      var buffer = new byte[10240];
      var stream = client.GetStream();
      Console.WriteLine("Got Client Stream obj");

      var length = stream.Read(buffer, 0, buffer.Length);
      Console.WriteLine("Read stream to buffer {0} bytes", length);

      var incomingMessage = Encoding.UTF8.GetString(buffer, 0, length);
      Console.WriteLine("Buffer decoded to string:");
      Console.WriteLine("---------------------------------------------------------------");
      Console.WriteLine("Incoming message:");
      Console.WriteLine(incomingMessage);

      var httpBody = string.Empty;
      var httpResonse = string.Empty;

      if (incomingMessage.StartsWith("POST"))
      {
        var inputs = incomingMessage.Split(new[] { "\r\n" }, StringSplitOptions.None);
        var values = inputs[inputs.Length - 1].Split('&');
        var num1 = int.Parse(values[0].Split('=')[1]);
        var num2 = int.Parse(values[1].Split('=')[1]);
        var sum = num1 + num2;

        httpBody = $"<html><h1>Result: {sum}</h1></html>";
      }
      else
      {
        httpBody = "<html><form method='post' action='http://127.0.0.1:13000'>" +
                    "Number 1: <input type='text' name='num1'><br>" +
                    "Number 2: <input type='text' name='num2'><br>" +
                    "<input type='submit' value='Add'>" +
                    "</form></html>";
      }

      httpResonse = "HTTP/1.0 200 OK" + Environment.NewLine
                  + "Content-Length: " + httpBody.Length + Environment.NewLine
                  + "Content-Type: " + "text/html" + Environment.NewLine
                  + Environment.NewLine
                  + httpBody
                  + Environment.NewLine + Environment.NewLine;

      Console.WriteLine("===============================================================");
      Console.WriteLine("Response message:");
      Console.WriteLine(httpResonse);
      Console.WriteLine("---------------------------------------------------------------");

      stream.Write(Encoding.UTF8.GetBytes(httpResonse));
      Console.WriteLine("Response networkstream written");

      stream.Flush();
      Console.WriteLine("Response networkstream Flushed");

      stream.Close();
      Console.WriteLine("Response networkstream Closed");

      client.Close();
      Console.WriteLine("TcpClient closed");

      Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

      Thread.Sleep(100);
    }
  }
}