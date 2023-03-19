<Query Kind="Statements" />

var now = DateTime.Now;

var later_5ms = now.AddTicks(5 * 10);
var later_5ms_new = now.AddMicroseconds(5);

var networkingTime = new DateTime(2023, 03, 21, 18, 50, 00, 345, DateTimeKind.Local);

now.ToString("O").Dump("now");
later_5ms.ToString("O").Dump("later");
later_5ms_new.ToString("O").Dump("later_new");
networkingTime.ToString("O").Dump("networkingTime");

now.Add(TimeSpan.FromMicroseconds(5));
now.Nanosecond.Dump();
