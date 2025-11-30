using System;
using Unity.Services.CloudCode.Core;

namespace HelloWorld;

public class MyModule
{
    [CloudCodeFunction("SayHello")]
    public string Hello(string name)
    {
        return $"Hello, {name}!";
    }

    [CloudCodeFunction("GetRandom")]
    public int GetRandomDice(int max)
    {
        Random random = new Random();
        return random.Next(max);
    }
}


