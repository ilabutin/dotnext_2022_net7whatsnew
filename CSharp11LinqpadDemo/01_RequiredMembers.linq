<Query Kind="Statements" />


var pointWithInitializer = new Point() { X = 1, Y = 2 };
var pointWithPartialInit = new Point() { X = 3 };
var pointWithCtor = new Point(5, 6);

Util.HorizontalRun(true, pointWithInitializer, pointWithPartialInit, pointWithCtor).Dump();

public class Point
{
  public Point()
  {
  }

  public Point(int x, int y)
  {
    X = x;
    Y = y;
  }

  public int X { get; init; }
  public int Y { get; init; }
}

