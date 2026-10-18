using UnityEngine;
using DG.Tweening;
public class PlayingCardObject : MonoBehaviour
{
    [SerializeField]Renderer _renderer;
    int _cardValue = 0;
    MatchTwoGame _manager;
    bool _isFlipped = false;
    
    public int cardValue => _cardValue;
    public bool isFlipped => _isFlipped;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUp()
    {
        if (!_isFlipped && !_manager.waiting)
        {
            transform.DOLocalRotate(new Vector3(-90, 0, 180), .25f);
            _manager.Flip(this);
            _isFlipped = true;
        }

    }

    public void Unflip()
    {
        _isFlipped = false;
        transform.DOLocalRotate(new Vector3(-90, 0, 0), .15f);
    }

    public void SetManager(MatchTwoGame manager)
    {
        _manager = manager;
    }

    public void SetValue(int newValue)
    {
        _cardValue = newValue;
    }

    public void SetTexture(Material material)
    {
        _renderer.material = material;
    }

}
