<Query Kind="Statements">
  <RuntimeVersion>7.0</RuntimeVersion>
</Query>


var utf16 = "abc";     // string
var utf8 = "abc"u8;   // ReadOnlySpan<byte>

utf16.Dump("UTF-16");
utf8.Dump("UTF-8");

byte[] asArray = utf8.ToArray();

ReadOnlySpan<char> utf16span = utf16.AsSpan();
if (utf16span is "abc") 
{
  Console.WriteLine("Match!");
}
else
{
  Console.WriteLine("No match!");
}