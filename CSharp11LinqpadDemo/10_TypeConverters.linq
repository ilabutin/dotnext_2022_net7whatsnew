<Query Kind="Statements">
  <Namespace>System.ComponentModel</Namespace>
</Query>

DateOnly date = DateOnly.FromDateTime(DateTime.Now);
string nowDateStr = "2022-11-20";
var converter = TypeDescriptor.GetConverter(typeof(DateOnly));
DateOnly? nowDate = converter.ConvertFromInvariantString(nowDateStr) as DateOnly?;

date.Dump("date");
nowDate.Dump("converted");