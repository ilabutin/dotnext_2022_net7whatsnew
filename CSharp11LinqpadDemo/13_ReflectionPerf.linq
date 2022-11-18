<Query Kind="Program">
  <Namespace>BenchmarkDotNet.Attributes</Namespace>
</Query>

#load "BenchmarkDotNet"

void Main()
{
  RunBenchmark(true);
}

public class InvokeTest
{
  private MethodInfo? _method;
  private object[] _args = new object[1] { 42 };

  [GlobalSetup]
  public void Setup()
  {
    _method = typeof(InvokeTest).GetMethod(nameof(InvokeMe), BindingFlags.Public | BindingFlags.Static)!;
  }

  [Benchmark]
  // *** This went from ~116ns to ~39ns or 3x (66%) faster.***
  public void InvokeSimpleMethod() => _method!.Invoke(obj: null, new object[] { 42 });

  [Benchmark]
  // *** This went from ~106ns to ~26ns or 4x (75%) faster. ***
  public void InvokeSimpleMethodWithCachedArgs() => _method!.Invoke(obj: null, _args);

  public static int InvokeMe(int i) => i;
}