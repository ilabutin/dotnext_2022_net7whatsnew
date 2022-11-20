<Query Kind="Program">
  <Namespace>System.Runtime.InteropServices</Namespace>
  <RuntimeVersion>7.0</RuntimeVersion>
</Query>

void Main()
{
  for (int i = 0; i < 100; i++)
  {
    DoSomething(Logger, "t");
  }
}

void DoSomething(Func<string, int> action, string msg)
{
  action(msg);
}

int Logger(string s)
{
  return s.Length;
}

// open in LINQPad 5