<Query Kind="Statements">
  <Namespace>System.ComponentModel</Namespace>
</Query>


string nowDateStr = "2022-11-20";
var converter = TypeDescriptor.GetConverter(typeof(DateOnly));
DateOnly? nowDate = converter.ConvertFromInvariantString(nowDateStr) as DateOnly?;

nowDate.Dump("converted");