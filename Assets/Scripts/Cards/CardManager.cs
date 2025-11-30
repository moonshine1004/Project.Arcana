using System.Net;
using UnityEngine;
using UnityEngine.UI;


public class CardManager : MonoBehaviour
{
    [SerializeField] public Sprite[] _sprites;

    [SerializeField] public UnityEngine.UI.Image[] _images;

    public void ImageRenderer(int cardslot, int cardID)
    {
        _images[cardslot].sprite = _sprites[cardID];
        
    }

    void Start()
    {
        
    }

}
