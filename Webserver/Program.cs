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
      TcpClient client = listener.AcceptTcpClient();
      Console.WriteLine("TcpClient accepted");

      byte[] buffer = new byte[10240];
      NetworkStream stream = client.GetStream();
      Console.WriteLine("Got Client Stream obj");

      int length = stream.Read(buffer, 0, buffer.Length);
      Console.WriteLine("Read stream to buffer {0} bytes", length);

      string incomingMessage = Encoding.UTF8.GetString(buffer, 0, length);
      Console.WriteLine("Buffer decoded to string:");
      Console.WriteLine("---------------------------------------------------------------");
      Console.WriteLine("Incoming message:");
      Console.WriteLine(incomingMessage);

      string httpBody = string.Empty;
      string httpResonse = string.Empty;

      if (incomingMessage.StartsWith("POST"))
      {
        string[] inputs = incomingMessage.Split(new[] { "\r\n" }, StringSplitOptions.None);
        string expression = inputs[inputs.Length - 1].Split('=')[1];
        int result = CalculateResult(expression);

        httpBody = $"<html><form method='post' action='http://127.0.0.1:13000'>" +
                    $"<input type='text' id='result' name='result' value='{result}' readonly><br>" +
                  "<input type='button' value='1' onclick=\"document.getElementById('result').value += '1'\">" +
                   "<input type='button' value='2' onclick=\"document.getElementById('result').value += '2'\">" +
                   "<input type='button' value='3' onclick=\"document.getElementById('result').value += '3'\"><br>" +
                   "<input type='button' value='4' onclick=\"document.getElementById('result').value += '4'\">" +
                   "<input type='button' value='5' onclick=\"document.getElementById('result').value += '5'\">" +
                   "<input type='button' value='6' onclick=\"document.getElementById('result').value += '6'\"><br>" +
                   "<input type='button' value='7' onclick=\"document.getElementById('result').value += '7'\">" +
                   "<input type='button' value='8' onclick=\"document.getElementById('result').value += '8'\">" +
                   "<input type='button' value='9' onclick=\"document.getElementById('result').value += '9'\"><br>" +
                   "<input type='button' value='+' onclick=\"document.getElementById('result').value += '+'\">" +
                   "<input type='button' value='-' onclick=\"document.getElementById('result').value += '-'\">" +
                   "<input type='button' value='*' onclick=\"document.getElementById('result').value += '*'\"><br>" +
                   "<input type='button' value='/' onclick=\"document.getElementById('result').value += '/'\">" +
                   "<input type='button' value='C' onclick=\"document.getElementById('result').value = ''\"><br>" +
                   "<input type='button' value='=' onclick='this.form.submit()'>" +
                   "</form></html>";
      }
      else
      {
        httpBody = "<html><form method='post' action='http://127.0.0.1:13000'>" +
                   "<input type='text' id='result' name='result' readonly><br>" +
                   "<input type='button' value='1' onclick=\"document.getElementById('result').value += '1'\">" +
                   "<input type='button' value='2' onclick=\"document.getElementById('result').value += '2'\">" +
                   "<input type='button' value='3' onclick=\"document.getElementById('result').value += '3'\"><br>" +
                   "<input type='button' value='4' onclick=\"document.getElementById('result').value += '4'\">" +
                   "<input type='button' value='5' onclick=\"document.getElementById('result').value += '5'\">" +
                   "<input type='button' value='6' onclick=\"document.getElementById('result').value += '6'\"><br>" +
                   "<input type='button' value='7' onclick=\"document.getElementById('result').value += '7'\">" +
                   "<input type='button' value='8' onclick=\"document.getElementById('result').value += '8'\">" +
                   "<input type='button' value='9' onclick=\"document.getElementById('result').value += '9'\"><br>" +
                   "<input type='button' value='+' onclick=\"document.getElementById('result').value += '+'\">" +
                   "<input type='button' value='-' onclick=\"document.getElementById('result').value += '-'\">" +
                   "<input type='button' value='*' onclick=\"document.getElementById('result').value += '*'\"><br>" +
                   "<input type='button' value='/' onclick=\"document.getElementById('result').value += '/'\">" +
                   "<input type='button' value='C' onclick=\"document.getElementById('result').value = ''\"><br>" +
                   "<input type='button' value='=' onclick='this.form.submit()'>" +
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

      Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

      Thread.Sleep(100);
    }
  }

  private int CalculateResult(string expression)
  {
    expression = System.Net.WebUtility.UrlDecode(expression);
    string[] tokens = expression.Split(new[] { '+', '-', '*', '/' });
    int result = int.Parse(tokens[0]);
    int j = tokens[0].Length;

    for (int i = 1; i < tokens.Length; i++)
    {
      switch (expression[j])
      {
        case '+':
          result += int.Parse(tokens[i]);
          break;
        case '-':
          result -= int.Parse(tokens[i]);
          break;
        case '*':
          result *= int.Parse(tokens[i]);
          break;
        case '/':
          result /= int.Parse(tokens[i]);
          break;
      }
      j += tokens[i].Length + 1;
    }

    return result;
  }
}