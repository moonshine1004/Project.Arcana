using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Mono.Cecil.Cil;

public static class CardShuffleUtil
{
    /// <summary>
    /// Fisher–Yates Shuffle의 구현체
    /// </summary>
    /// <typeparam name="T">섞일 리스트의 타입</typeparam>
    /// <param name="list">섞일 리스트</param>
    /// <param name="suffled">리스트로 반환</param>
    public static void Shuffle<T>(List<T> list, out List<T> suffled)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int j = Random.Range(i, list.Count);

            (list[i], list[j]) = (list[j], list[i]);
        }
        suffled = list;
    }
    
    
}