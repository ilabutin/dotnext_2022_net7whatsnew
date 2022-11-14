<Query Kind="Program" />

using System.Numerics;

// Define our record with operators
record Point(int X, int Y) : IAdditionOperators<Point, Point, Point>
{
  public static Point operator +(Point left, Point right) =>
    new Point(left.X + right.X, left.Y + right.Y);

  public static Point operator checked +(Point left, Point right) =>
    new Point(checked(left.X + right.X), checked(left.Y + right.Y));
}

// Define extension method
static class Extensions
{
  public static T SumEx<T>(this IEnumerable<T> source) where T : IAdditionOperators<T, T, T>
  {
    using var enumerator = source.GetEnumerator();
    if (!enumerator.MoveNext()) throw new InvalidOperationException("Empty sequence");

    T total = enumerator.Current;

    while (enumerator.MoveNext())
    {
      total += enumerator.Current;
    }

    return total;
  }
}

// Demo:
void Main()
{
  var point1 = new Point(1, 1);
  var point2 = new Point(2, 2);
  var point3 = new Point(3, 3);

  new[] { point1, point2, point3 }.SumEx().Dump("sum of points");

  new[] { 1, 2, 3 }.SumEx().Dump("sum of numbers (int)");
  new[] { 1.1, 2.2, 3.3 }.SumEx().Dump("sum of numbers (double)");
}
