using UnityEngine;

public class ShieldAnimation : MonoBehaviour {

    private SpriteRenderer _spriteRenderer;
    private Animator _anim;

    // Use this for initialization
    void Start () {
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
