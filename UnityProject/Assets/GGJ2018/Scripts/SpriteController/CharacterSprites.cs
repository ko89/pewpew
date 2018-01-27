using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode(), RequireComponent(typeof(SpriteRenderer))]
public class CharacterSprites : MonoBehaviour
{
    public int _walkPhase;
    public bool _isBack;
    public bool _isHollow;
    public bool _isWalking;

    //public Sprite[] _idleSprites;
    public Sprite[] _walkSprites;
    public Sprite[] _backSprites;
    public Sprite[] _hollowSprites;
    public Sprite[] _hollowBackSprites;

    private SpriteRenderer _spriteRenderer;
    // Use this for initialization
    void Start ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!_isBack && !_isHollow)
            _spriteRenderer.sprite = _walkSprites[_walkPhase];
        if (!_isBack && _isHollow)
            _spriteRenderer.sprite = _hollowSprites[_walkPhase];
        if (_isBack && !_isHollow)
            _spriteRenderer.sprite = _backSprites[_walkPhase];
        if (_isBack && _isHollow)
            _spriteRenderer.sprite = _hollowBackSprites[_walkPhase];
    }
}
