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

    /// <summary>
    /// 테스트 메서드
    /// </summary>
    /// <returns></returns>
    [CloudCodeFunction("Test")]
    public string Test()
    {
        return "이것은 테스트 입니다";
    }

    [CloudCodeFunction("SaveDeck")]
    public void SaveDeck()
    {
        
    }
}


