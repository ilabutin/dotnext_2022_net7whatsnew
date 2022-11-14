<Query Kind="Statements" />

int[] numbers = { 0, 1, 2, 3, 4 };

if (numbers is [0, 1, 2, 3, 4])
	"Numbers is { 0, 1, 2, 3, 4 }".Dump();

if (numbers is [0, ..])
	"Numbers starts with 0".Dump();

if (numbers is [0, _, _, _, _])
	"Numbers starts with 0, and has 5 elements".Dump();

if (numbers is [_, _, _, _, var last])
	last.Dump ("Last number in the series of 5");

if (numbers is [.., var last2])
  last2.Dump("Last number in the series");

if (numbers is [0, .. var rest])
	rest.Dump ("Numbers after the zero");

if (numbers is [0, .. var middle, 4])       // "slice"
  middle.Dump("Numbers in the middle");
  
if (numbers is [_, _, >2, ..])
  "Third number above 2".Dump();