using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterPooling : MonoBehaviour
{
    //몬스터 풀링을 담당하는 스크립트입니다.
    
    
    //몬스터 프리팹을 받을 게임 오브젝트 선언
    [SerializeField] private GameObject _MonsterPrefab;
    //유니티 오브젝트 풀 선언
    public ObjectPool<GameObject> _pool;
    


    private void Start()
    {
        //ObjectPool 생성자
        //초기 10개 생성
        //최대 30개까지 풀에 저장
        _pool = new ObjectPool<GameObject>(CreatMonster, null, OnMonsterRelease, OnDestroyMonster,true, defaultCapacity: 10, maxSize: 30);
        //10개 생성
        for (int i = 0; i < 10; i++)
        {
            var monster = CreatMonster(); //몬스터풀에서 꺼내옴
            _pool.Release(monster); //바로 넣음
        }
        var monster1 = SpawnMonster();
    }
    //몬스터 생성
    private GameObject CreatMonster()
    {
        //프리팹을 인스턴스로 변환하여 반환
        return Instantiate(_MonsterPrefab);
    }


    //객체를 풀에 넣을 때
    public void OnMonsterRelease(GameObject monster)
    {
        monster.SetActive(false);
        var stat = monster.GetComponent<CharacterStat>();
        stat.StatReset();
    }
    
    //풀의 크기를 초과하였을 때 몬스터를 삭제
    /// <summary>
    /// 몬스터를 없앨때 사용하는 메서드
    /// </summary>
    /// <param name="monster"></param>
    private void OnDestroyMonster(GameObject monster)
    {
        Destroy(monster);
    }
    [ContextMenu("Creat Monster")]
    public GameObject SpawnMonster()
    {
        GameObject monster = _pool.Get();
        monster.SetActive(true);

        var stat = monster.GetComponent<CharacterStat>();
        if (stat != null)
        {
            stat.SetPool(this);
        }
        return monster;
    }




    /* 오브젝트 풀링: 오브젝트 풀링은 다음과 같은 부분에서 이점이 있다.
     *  1)힙 메모리 효율성: Instantiate는 호출과정에서 힙에 메모리를 할당하는데, 이때 오브젝트 풀링을 통해 필요한 메모리를 미리 할당함으로서 낭비되는 메모리 공간을 줄일 수 있다. 
     *  2)깊은 복사 방지: Instantiate는 단순 복사가 아니라 게임 오브젝트의 Transform, SpriteRenderer등의 모든 컴포넌트를 복사하여 완전히 새로운 객체를 만들게 된다. 이러한 깊은 복사 작업은 고비용 작업으로 많은 리소스를 필요로 함으로 오브젝트 풀링을 통해서 이러한 작업을 필요한 만큼 미리해두어 게임 실행 중에 드는 비용을 줄일 수 있다.
     *  3)Destroy는 렌더링이 끝난 후 객체를 삭제하게 되는데, 이때 Destroy할 오브젝트가 쌓이게 되면(지연삭제 큐가 쌓이게 되면) 프레임이 튀게 된다. 오브젝트 풀링은 오브젝트를 삭제할 때 오브젝트를 비활성화 하게 함으로써 비용을 절약한다.
     *  4)초기화 절차 생략: 오브젝트 풀링을 사용하지 않고 오브젝트를 파괴할 경우 각 오브젝트에 존재하는 AI, HP 등의 정보를 함께 파괴하게 되지만 오브젝트 풀링을 사용할 경우 객체 내부를 초기화하는 것만으로 리셋을 할 수 있어 비용을 줄일 수 있다.
     *  5)Garbage Collection 대상을 줄임: Garbage Collection은 더이상 사용하지 않는 객체를 자동으로 수거하여 메모리 공간에서 삭제하는 시스템으로 동기식으로 작동하기 때문에 쌓였다 삭제하게 되면 일시적으로 게임에 부담을 줄 수 있다. 때문에 객체를 삭제하지 않고 초기화 후 다시 사용하게 되면 게임의 부하를 줄일 수 있다.
     * 사용기술: 유니티 ObjectPool
     * 문제상황 및 해결과정: GC(Garbage Collector)의 간헐적 개입, 동적으로 생성된 몬스터들의 복잡한 초기화 로직 반복으로 인한 렉-->UnityEngine.Pool.ObjectPool<T> API를 학습 후 도입
     * 배운점
     *  1)ObjectPool<T>을 단순한 재사용 도구가 아닌, GC 발생 제어 등 최적화 수단의 필요성을 이해하고 이를 구현함 
     *  2)깊은 복사, 비동기식 처리 모델 등에 대한 이해를 확장하여 최적화의 필요성을 학습함
     * 성장한 부분: C#생성자에 대한 이해, 깊은 복사에 대한 이해
     * 향후 보완할 점: 현재 코드에서는 단순히 오브젝트를 활성화/비활성화만 하고 있다, 추후에는 이를 수정하여 활성화 시 함께 실행할 코드를 GetMonster()에 추가하여 코드를 더 효율적으로 만들 계획이다. 또한 
     * 
     */
}
