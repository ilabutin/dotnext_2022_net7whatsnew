<Query Kind="Program">
  <Namespace>System.Threading.Channels</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
  Channel<byte> channel = Channel.CreateBounded<byte>(100);

  var writer = channel.Writer;
  var reader = channel.Reader;

  Task.Run(async () => await StartWriting(writer));

  //Thread.Sleep(10000);

  var readStream = new ChannelStream(reader);

  // Read stream the old way
  int requiredLength = 4;
  byte[] buffer = new byte[requiredLength];

  while (true)
  {
    int offset = 0;
    
    while (offset != requiredLength)
    {
      int read = readStream.Read(buffer, offset, requiredLength - offset);
      if (read == -1)
      {
        Console.WriteLine(UTF8Encoding.UTF8.GetString(buffer, 0, offset));
        return;        
      }
      Console.WriteLine($"Read {read} bytes at offset {offset}");
      
      offset += read;
    }
    Console.WriteLine(UTF8Encoding.UTF8.GetString(buffer, 0, requiredLength));
  }

  // Read the stream the new way
  //while (true)
  //{
  //  try
  //  {
  //    readStream.ReadExactly(buffer, 0, requiredLength);
  //    Console.WriteLine(UTF8Encoding.UTF8.GetString(buffer, 0, requiredLength));
  //  }
  //  catch (Exception e)
  //  {
  //    break;
  //  }
  //}
}

// You can define other methods, fields, classes and namespaces here
async Task StartWriting(ChannelWriter<byte> writer)
{
  byte[] message = "Hello, world"u8.ToArray();  
  for (int i = 0; i < message.Length; i++)
  {
    await writer.WriteAsync(message[i]);
    if (i % 2 == 0)
    {
      await Task.Delay(TimeSpan.FromSeconds(1));
    }
  }
  writer.Complete();
}

class ChannelStream : Stream
{
  private readonly ChannelReader<byte> channelReader;
  
  public ChannelStream(ChannelReader<byte> channelReader)
  {
    this.channelReader = channelReader;    
  }
  
  public override bool CanRead => true;

  public override bool CanSeek => throw new NotImplementedException();

  public override bool CanWrite => throw new NotImplementedException();

  public override long Length => throw new NotImplementedException();

  public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

  public override void Flush()
  {
    throw new NotImplementedException();
  }

  public override int Read(byte[] buffer, int offset, int count)
  {
    int read = 0;
    while (read < count)
    {
      var waitToReadTask = channelReader.WaitToReadAsync();
      // If we have to wait, then return immediately if buffer has something already
      if (!waitToReadTask.IsCompleted && read > 0)
      {
        return read;
      }
      // Now wait for something to appear
      var task = waitToReadTask.AsTask();
      task.Wait();
      bool result = task.Result;
      if (!result)
      {
        return read > 0 ? read : -1;
      }
      
      // Now read and fill the buffer
      if (channelReader.TryRead(out byte b))
      {
        buffer[offset + read] = b;
        read++;
        continue;
      }
    }
    
    return read;
  }

  public override long Seek(long offset, SeekOrigin origin)
  {
    throw new NotImplementedException();
  }

  public override void SetLength(long value)
  {
    throw new NotImplementedException();
  }

  public override void Write(byte[] buffer, int offset, int count)
  {
    throw new NotImplementedException();
  }
}
