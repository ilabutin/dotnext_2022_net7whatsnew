<Query Kind="Statements" />

#region Raw strings
string oldJson = "{ \"key\": \"author\", \"value\": \"Igor\" }";
Util.FixedFont(oldJson).Dump();

Util.ReadLine();

string betterJson = """{ "key": "author", "value": "Igor" }""";
Util.FixedFont(betterJson).Dump();

Util.ReadLine();

string newJson = """
   {
     "key": "author",
     "value": "Igor"
   }
   """;

Util.FixedFont(newJson).Dump();

Util.ReadLine();

string newJsonNoIndent = """
   {
     "key": "author",
     "value": "Igor"
   }
""";

Util.FixedFont(newJsonNoIndent).Dump();

#endregion

#region String interpolation
string author = "Igor";

string oldResult = $"{{ \"key\": \"author\", \"value\": \"{(author != null ? author.ToUpper() : "no author" )}\" }}";

Util.FixedFont(oldResult).Dump();

string newResult = $$"""{ "key": "author", "value": "{{
  (author != null 
  ? author.ToUpper()
  : "no author") }}" }""";
  
Util.FixedFont(newResult).Dump();

#endregion