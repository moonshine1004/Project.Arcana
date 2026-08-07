using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUIRenderer : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private UsingCardList _usingCardList;

    [SerializeField] private CardView[] _cardViewPrefab;
    [SerializeField] private CardManager _cardManager;


    private bool CanAnimation = true;
    
    private void Update()
    {
        
    }

    public void CardRender()
    {
        _cardViewPrefab[0].Initialize(_usingCardList.hand[0]);
        _cardViewPrefab[1].Initialize(_usingCardList.hand[1]);
        _cardViewPrefab[2].Initialize(_usingCardList.hand[2]);
        _cardViewPrefab[3].Initialize(_usingCardList.hand[3]);
        _cardViewPrefab[4].Initialize(_usingCardList.hand[4]);
        for(int i=0; i<5; i++)
        {
            _cardViewPrefab[i].Initialize(_usingCardList.hand[i]);
        }

        _cardManager.ImageRenderer(0, _usingCardList.hand[0].cardID);
        _cardManager.ImageRenderer(1, _usingCardList.hand[1].cardID);
        _cardManager.ImageRenderer(2, _usingCardList.hand[2].cardID);
        _cardManager.ImageRenderer(3, _usingCardList.hand[3].cardID);
        _cardManager.ImageRenderer(4, _usingCardList.hand[4].cardID);
    }

    public void cardAnimation(int i)
    {
        StartCoroutine(MoveCardUpAndDown(_cardViewPrefab[i].transform, 100f, 0.2f));
        
    }

    private IEnumerator MoveCardUpAndDown(Transform cardTransform, float distance, float duration)
    {
        if (CanAnimation)
        {
            CanAnimation = false;

            Vector3 startPos = cardTransform.position;
            Vector3 targetPos = startPos + new Vector3(0, distance, 0);

            // 위로 이동
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / duration;
                cardTransform.position = Vector3.Lerp(startPos, targetPos, t);
                yield return null;
            }

            // 잠깐 대기
            yield return new WaitForSeconds(0.1f);

            // 다시 아래로
            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / duration;
                cardTransform.position = Vector3.Lerp(targetPos, startPos, t);
                yield return null;
            }

            // 보정
            cardTransform.position = startPos;

            CanAnimation = true;
        }
        
    }
}

