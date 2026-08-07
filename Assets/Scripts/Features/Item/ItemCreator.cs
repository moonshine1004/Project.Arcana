#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

public class ItemCreator : MonoBehaviour
{
    
    
    public string outputFolder = "Assets/Items";

    /// <summary>
    /// 요약 설명 작성
    /// </summary>
    /// <typeparam name="T">리스트 요소 타입</typeparam>
    /// <param name="name">매개변수 설명</param>
    /// <returns>리턴 값 설명</returns>
    /// 
    /// <example>
    /// <code>
    /// 예시 작성
    /// </code>
    /// </example>
    [ContextMenu("Item Creat")]
    public void CreatItem()
    {
        
        if (!AssetDatabase.IsValidFolder(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        
        for(int i = 0; i < 10; i++)
        {
            var newItem = ScriptableObject.CreateInstance<ItemData>();
            newItem.id = i;
            newItem.itemType = 0;

            AssetDatabase.CreateAsset(newItem, $"{outputFolder}/Item_{i}.asset");

        }
        AssetDatabase.SaveAssets();
    }
}
#endif