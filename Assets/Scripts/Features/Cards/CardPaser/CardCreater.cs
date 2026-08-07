
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

public class CardCreater : MonoBehaviour
{
    //인풋 아웃풋 설정
    public TextAsset csvFile;
    public string outputFolder = "Assets/Cards";
    //인스펙터 창에서 실행할 수 있도록 함
    [ContextMenu("Import Cards From CSV")]

    public void ImportCards()
    {
        var cards = CardParser.ParseCSVToCards(csvFile); //읽어 온 CSV파일을 CardPaser클래스의 ParseCSVToCards메서드에 넣어서 카드 객체 리스트로 만들어 cards 카드 리스트형 변수에 대입
        //폴더가 없으면 생성
        if (!AssetDatabase.IsValidFolder(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        //cards리스트에 있는 모든 요소들을 하나씩 꺼내어 스크립터블 오브젝트 에셋으로 저장
        foreach (var card in cards)
        {
            AssetDatabase.CreateAsset(card, $"{outputFolder}/Card_{card.cardID}.asset");
        }
        //이상의 내용을 에디터에 저장
        AssetDatabase.SaveAssets();
    }
}
#endif
