<Query Kind="Statements">
  <Namespace>System.Diagnostics.CodeAnalysis</Namespace>
</Query>


var pointWithInitializer = new Point() { X = 1, Y = 2 };
var pointWithPartialInit = new Point() { X = 3, Y = 4 };
var pointWithCtor = new Point(5, 6);

Util.HorizontalRun(true, pointWithInitializer, pointWithPartialInit, pointWithCtor).Dump();

public class Point
{
  public Point()
  {
  }

  [SetsRequiredMembers]
  public Point(int x, int y)
  {
    X = x;
    Y = y;
  }

  public required int X { get; init; }
  public required int Y { get; init; }
}

