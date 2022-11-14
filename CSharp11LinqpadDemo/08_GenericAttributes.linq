<Query Kind="Program" />

interface IValidator { }
class StringValidator : IValidator { }
class IntValidator : IValidator { }

class ValidateAttribute<T> : Attribute
    where T : IValidator
{
  private string name;
  
  public ValidateAttribute(string name) => this.name = name;
}

[Validate<StringValidator>(nameof(param))]
void MyMethod(string param)
{
  Console.WriteLine(param);
}

void Main()
{
  MyMethod("Test");
}



