<Query Kind="Program" />

// Define interface with Parse method
interface IParseable<T> where T : IParseable<T>
{
  static abstract T Parse(string s);
}

// Define record which implements the interface
record Point(int X, int Y) : IParseable<Point>
{
  public static Point Parse(string s)
  {
    var match = Regex.Match(s, @"Point { X = (\d+), Y = (\d+) }");
    if (!match.Success) throw new ArgumentException("Cannot parse point - invalid string");
    return new Point(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
  }
}

// Define helper function for easier calling
T ParseAny<T>(string s) where T : IParseable<T> => T.Parse(s);

void Main()
{
  string pointString = new Point(2, 3).ToString().Dump("Point string");

  // Call the Parse method directly:
  Point.Parse(pointString).Dump();

  // Call the polymorphic ParseAny method:
  ParseAny<Point>(pointString).Dump();
}

/*

----
interface IAddable<TSelf> where TSelf : IAddable<TSelf>
{
	static abstract TSelf operator + (TSelf left, TSelf right);
}

----
	public static Point operator + (Point left, Point right) =>
		new Point (left.X + right.X, left.Y + right.Y);

----
T AddAny<T> (T value1, T value2) where T : IAddable<T> => value1 + value2;

----
	var point1 = new Point (1, 1);
	var point2 = new Point (2, 2);
	
	// Call the addition operator directly:
	(point1 + point2).Dump();
	
	// Call the polymorphic AddAny method:
	AddAny (point1, point2).Dump();

*/