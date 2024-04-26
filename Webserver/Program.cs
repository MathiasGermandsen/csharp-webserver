using System;
using System.IO;
using System.Net;
using System.Diagnostics;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Starting server...");

    HttpListener listener = new HttpListener();
    listener.Prefixes.Add("http://127.0.0.1:8080/");
    listener.Start();

    Console.WriteLine("Server started on http://127.0.0.1:8080");

    // Open the index.html file in the default web browser
    Process.Start(new ProcessStartInfo("http://127.0.0.1:8080/index.html") { UseShellExecute = true });

    while (true)
    {
      HttpListenerContext context = listener.GetContext();
      HttpListenerRequest request = context.Request;
      HttpListenerResponse response = context.Response;

      string content = File.ReadAllText("index.html");
      byte[] buffer = System.Text.Encoding.UTF8.GetBytes(content);

      response.ContentLength64 = buffer.Length;
      Stream output = response.OutputStream;
      output.Write(buffer, 0, buffer.Length);
      output.Close();
    }
  }
}