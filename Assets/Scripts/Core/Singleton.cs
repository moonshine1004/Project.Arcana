using UnityEngine;

/// <summary>
/// 싱글턴 오브젝트를 생성하기 위한 서브 클래스입니다.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T>: MonoBehaviour where T : MonoBehaviour
{
    //정적 인스턴스 선언
    protected static T _instance;
    //생성자는 protected으로 숨기기
    protected Singleton(){}

    /// <summary>
    /// 싱글턴 프로퍼티만 공개
    /// 동적으로 새 싱글턴 인스턴스를 만들 때, 싱글턴 객체가 null일 때 getter 참조 시 객체 생성
    /// </summary>
    /// <returns></returns>
    public static T Instance
    {
        get
        {
            //오브젝트가 있으면 지나가고 없으면 생성
        if(_instance == null)
        {
            //기존에 존재하는 인스턴스 찾기
            _instance = FindAnyObjectByType<T>();
            //그래도 없다면
            if(_instance == null)
            {
                //게임 오브젝트 생성 후 객체 인스턴스를 컴포넌트를 추가
                GameObject newGameobject = new GameObject(typeof(T).Name);
                _instance = newGameobject.AddComponent<T>();
            }
        }

        return _instance;
        }
    }
    

    
    




}
