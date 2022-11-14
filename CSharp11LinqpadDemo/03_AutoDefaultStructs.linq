<Query Kind="Statements" />

// Auto-default structs (switch target .NET)

var s = new MyStruct(5);
s.Dump();

struct MyStruct
{
  public MyStruct(int x)
  {
    X = x;
  }
  
  public int X;
  public int Y;
}