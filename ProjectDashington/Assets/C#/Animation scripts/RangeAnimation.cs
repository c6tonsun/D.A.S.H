using UnityEngine;

public class RangeAnimation : MonoBehaviour {

    private SpriteRenderer _spriteRenderer;
    private Animator _anim;
    
    protected virtual void Start()
    {
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
