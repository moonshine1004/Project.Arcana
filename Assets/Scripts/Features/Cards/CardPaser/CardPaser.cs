using UnityEngine;
using UnityEditor;
//리스트를 사용하기 위한 네임스페이스
using System.Collections.Generic;

//정적 클래스-->인스턴스 없이 호출 가능
public static class CardParser
{
    //CSV파일을 받아 카드 타입 객체의 리스트를 반환하는 스크립트
    public static List<CardData> ParseCSVToCards(TextAsset csvFile)
    {
        var result = new List<CardData>(); //카드 리스트 생성
        var rows = ParseCSV(csvFile.text); //CSV파일의 텍스트를 ParseCSV메서드에 넣어 문자열 배열의 리스트로 만들어 대입-->인덱스가 바뀔 때마다 행이 바뀜
        //데이터가 적어 의미 없는 경우 종료
        if (rows.Count < 4)
        {
            Debug.LogError("[CardParser] CSV 줄 수가 너무 적습니다.");
            return result;
        }


        string[] headers = rows[1]; //문자열 배열 리스트의 2번째 요소(데이터의 2번째 행)를 문자열 배열에 대입
        //줄별로 파싱
        for (int i = 3; i < rows.Count; i++) //데이터 행은 4번째부터 시작해서 마지막 행까지
        {
            string[] row = rows[i]; //i번째 행(현재 행)의 데이터를 가져옴
            //현재 행의 길이가 0이거나 행의 0번째 요소가 null이면 빈줄 스킵함(넘어감)
            if (row.Length == 0 || string.IsNullOrWhiteSpace(row[0]))
            {
                Debug.LogWarning($"[CardParser] 빈 줄 스킵: row {i}");
                continue;
            }
            //카드 스크립터블 오브젝트(인스턴스)를 메모리에 생성
            CardData card = ScriptableObject.CreateInstance<CardData>();
            //각 열을 파싱하는 내부 루프
            //이거 왜 &&?
            for (int j = 0; j < headers.Length && j < row.Length; j++)
            {
                string header = headers[j].Trim(); //.Trim(): 앞뒤 공백 제거 후 헤더 변수에 저장, 첫번째 루프 밖에 있는 headers배열을 대입함으로서 엑셀 데이터의 행 이동 없이 열만 이동
                string value = row[j].Trim(); //첫 루프 내의 rows배열을 대입 받아 루프가 반복 될 때마다 엑셀 대이터의 다음 행으로 이동하여 행 내의 j번째 행의 값에 공백 제거 후 값 변수에 저장

                if (string.IsNullOrEmpty(value)) continue; // 빈 값 스킵

                try //예외를 확인
                {
                    switch (header) //문자열 변수인 헤더의 값에 따라 스위치문
                    {
                        case "CardID":
                            if (int.TryParse(value, out var id)) //value문자열을 int로 변환 시도 성공 시 id에 대입됨, 실패 시 false반환
                                card.cardID = id;
                            break;
                        case "Cost":
                            if (int.TryParse(value, out var cost))
                                card.cost = cost;
                            break;
                        case "Damage":
                            if (float.TryParse(value, out var dmg))
                                card.damage = dmg;
                            break;
                        case "Element":
                            if (int.TryParse(value, out var elem))
                                card.element = (Enum.Element)elem;
                            break;
                        
                        case "TargetType":
                            if (int.TryParse(value, out var target))
                                card.targetType = (Enum.TargetType)target;
                            break;
                        
                    }
                }
                catch (System.Exception ex) //try가 예외를 확인하면 실행
                {
                    Debug.LogWarning($"[CardParser] '{header}' 값 '{value}' 처리 실패 (row {i}): {ex.Message}");
                }
            }

            result.Add(card); //카드 리스트에 추가
        }

        return result; //카드 리스트 반환
    }

    //CSV파일을 받아와 문자열 배열 리스트를 반환하는 스크립트
    //구분자를 기준으로 텍스트를 잘라 문자열 배열 리스트로 만듦
    private static List<string[]> ParseCSV(string csvText)
    {
        List<string[]> result = new List<string[]>(); //문자열 배열 리스트 result선언
        //csvText.Split: 문자열을 특정 구분자로 나누어 문자열을 분리하는 C# 표준 라이브러리에 포함된 메서드
        var lines = csvText.Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.RemoveEmptyEntries); //빈 문자열을 제외하고 특정 구분자("\r\n", "\n"-->줄바꿈) 단위로 잘라 행(line)에 저장

        foreach (var line in lines)//위에서 얻은 lines행의 요소를 하나씩 꺼내어 ParseCSVLine메서드를 통해 문자열 배열로 바꿈 (i.e.문자열을 잘라 문자열 배열로 바꿈)
        {
            result.Add(ParseCSVLine(line));//result 리스트에 line을 매게변수로 받는 ParseCSVLine메서드 실행하여 얻은 값을 대입
        }

        return result;
    }
    //문자열을 받아와 문자열 배열을 반환하는 메서드
    //ParseCSV에서 구분자를 기준으로 자른 각 단어를 
    private static string[] ParseCSVLine(string line)
    {
        List<string> values = new List<string>(); //values라는 문자열 리스트 선언
        bool inQuotes = false;
        string current = ""; //문자를 담아둘 문자열 

        for (int i = 0; i < line.Length; i++) //문자열의 길이 만큼 반복
        {
            char c = line[i]; //문자열의 i번째 문자 저장하는 문자 변수

            if (c == '"') //i번째 문자가 공백이면 
            {
                inQuotes = !inQuotes; //inQuotes를 트루로
            }
            else if (c == ',' && !inQuotes) //i번째 문사가 콤마거나 inQuotes가 트루면
            {
                values.Add(current.Trim()); //values 배열에 문자들을 저장해둔 current를 앞뒤 공백 제거후 넣음
                current = ""; //current는 다시 공백으로 초기화
            }
            else
            {
                current += c; //문자열에 i번째 문자 저장
            }
        }

        values.Add(current.Trim()); 
        return values.ToArray(); //리스트를 배열로 바꾸어 반환
    }

    /* 개발 기능: 엑셀 데이터 테이블을 파싱하여 스크립터블 오브젝트를 동적으로 생성할 수 있는 파이프라인 개발 --> 이는 게임 특성 상 반복적인 밸런싱과 많은 양의 오브젝트를 다룸에 있어 큰 이점을 가짐
     * 사용 기술: 유니티 스크립터블 오브젝트, using System.IO, AssetDatabase, [ContextMenu]
     *  1)유니티 스크립터블 오브젝트: 유니티의 스크립터블 오브젝트는 구조가 같은 대량의 서로 다른 오브젝트를 다루는데에 용이한 유니티의 데이터 컨테이너이다. 
     *                                이 기능을 사용하면 복수의 데이터를 빠르게 에셋으로 만드어 저장하여 사용할 수 있다는 이점이 있다.
     *                                본 프로젝트는 이를 이용하여 프로젝트의 핵심 시스템인 '카드 시스템'에 필요한 카드를 간편하고 빠르게 제작했다.
     *                                스크립터블 오브젝트 기능은 기획자와 협업하여 데이터 테이블을 통해 사용하면 더욱 효과적인 기능으로, 본 프로젝트 또한 기획자가 제작한 카드 데이터 테이블을 통해 카드 스크립터블 오브젝트를 제작했다.
     *  2)System.IO: System.IO은 .NET 라이브러리가 제공하는 네임 스페이스로 파일, 스트림의 입출력을 돕는다. 본 프로젝트에서는 System.IO의 Directory를 이용하여 AssetDatabase의 폴더를 다루었다.
     *  3)AssetDatabase: 
     * 문제상황 및 해결과정
     *  1)파싱해 온 데이터에 대한 데이터 타입 정의에 있어서의 문제점
     *   -->파싱해 온 데이터를 변수에 대입하는 과정에 있어 모든 값을 문자열로 가져왔기 때문에 문자열이 아닌 데이터를 받아드리지 못하는 문제 발생
     *   -->이를 수정하고자 명시적 형변환을 이용하여 데이터를 받아왔으나 유효하지 않은 enum을 받아오는 문제를 확인함
     *   -->이를 해결하기 위해 int.Parse(), enum.Parse()등 Parse를 사용하였지만 Parse는 존재하지 않는 enum값, 포맷 오류 등의 예외 상황에 대한 대처가 어려움
     *   -->따라서 TryParse()를 사용하여 이러한 문제 상황을 개선함
     * 
     * 
     * 배운점
     *  1)협업의 있어서의 필요성 및 중요성: 오늘날의 게임은 하드웨어 스펙의 비약적 상승과 함께 점점 더 복잡해져 가고 있다. 이에 따라 기획자와 프로그래머가 다뤄야하는 데이터의 양 또한 늘어가고 있다.
     *                                      본 프로젝트에서 사용한 카드(오브젝트)의 개수는 약 30개였음에도 카드의 이름을 정의하고 이를 활용하는데에 있어 많은 어려움을 겪었다.
     *                                      즉, 고차원적으로 진화해 가는 현대 게임에서의 오브젝트를 다루는 데는 이보다 더한 수고스러움이 들기 때문에, 이러한 수고를 덜기 위해 만들어지는 데이터 테이블은 
     *                                      게임 제작에 필수 요소가 되었으며, 프로그래머에게 있어서는 이를 제작하는 기획자와의 소통이 더욱 중요해졌다고 볼 수 있다.
     *                                      본 프로젝트를 제작함에 있어서 이러한 협업의 필요성을 느꼈으며, 프로그래머와 기획자의 소통과 이러한 소통의 다리가 되어주는 문서의 중요성 또한 뼈저리게 실감할 수 있었다.
     *  
     * 성장한 부분: 
     * 
     * 
     * 향후 보완할 점
     *  1)현재는 모든 값을 String으로 파싱해 온 후 명시적 형변환하는 방식을 사용하였지만, 이는 방대한 자료를 다룰 때의 비용 문제와 각 데이터를 재정의하는 코드를 따로 작성해야 되는 비효율성에 있어 개선이 요구됨
     *      따라서 향후에는 데이터 테이블의 헤더를 인식하여 데이터를 파싱할 때부터 해당 자료형으로 데이터를 받아올 수 있도록 개선할 예정임
     * 
     * 
     * 
     * 
     * 
     */
}
