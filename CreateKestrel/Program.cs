using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO.Pipelines;
using System.Buffers;

TcpListener listener = new TcpListener(IPAddress.Any, 5000);

listener.Start();

Console.WriteLine("Servidor iniciado na porta 5000");

while (true)
{
    var client = await listener.AcceptTcpClientAsync();

    _ = Task.Run(() => HandleClient(client));
}

async Task FillPipeAsync(TcpClient client, PipeWriter writer)
{
    NetworkStream stream = client.GetStream();

    while(true)
    {
        Memory<byte> memory = writer.GetMemory(4096);

        int bytesRead = await stream.ReadAsync(memory);

        if (bytesRead == 0)
            break;

        writer.Advance(bytesRead);

        FlushResult result = await writer.FlushAsync();

        if (result.IsCompleted) 
            break;
    }

    await writer.CompleteAsync();
}

async Task ReadPipeAsync(PipeReader reader, TcpClient client)
{
    NetworkStream stream = client.GetStream();

    while(true)
    {
        ReadResult result = await reader.ReadAsync();
        ReadOnlySequence<byte> buffer = result.Buffer;

        if (TryReadRequest(ref buffer, out var request))
        {
            Console.WriteLine($"Método: {request.Method}");
            Console.WriteLine($"Path: {request.Path}");

            // Aqui, preciso responder o cliente
            string response = 
                "HTTP/1.1 200 OK\r\n" +
                "Content-Length: 12\r\n" +
                "Content-Type: text/plain\r\n" +
                "\r\n" +
                "Hello World!";

            byte[] responseBytes = Encoding.UTF8.GetBytes(response);

            await stream.WriteAsync(responseBytes);
        }

        reader.AdvanceTo(buffer.Start, buffer.End);

        if (result.IsCompleted)
            break;
    }

    await reader.CompleteAsync();
}

bool TryReadRequest(ref ReadOnlySequence<byte> buffer, out HttpRequest request)
{
    SequenceReader<byte> reader = new SequenceReader<byte>(buffer);

    if(!reader.TryReadTo(out ReadOnlySequence<byte> headerSequence, Encoding.ASCII.GetBytes("\r\n\r\n"), advancePastDelimiter: true))
    {
        request = null!;
        return false;
    }

    string headerText = Encoding.UTF8.GetString(headerSequence.ToArray());

    string[] lines = headerText.Split("\r\n");

    string[] parts = lines[0].Split(" ");

    request = new HttpRequest
    {
        Method = parts[0],
        Path = parts[1]
    };

    buffer = buffer.Slice(reader.Position);

    return true;
}

async Task HandleClient(TcpClient client)
{
    Pipe pipe = new Pipe();

    Task writing = FillPipeAsync(client, pipe.Writer);
    Task reading = ReadPipeAsync(pipe.Reader, client);

    await Task.WhenAll(writing, reading);
}