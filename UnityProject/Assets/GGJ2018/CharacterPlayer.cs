﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : MonoBehaviour {


    public bool _hasSoul = false;

    public float _normalSpeed = 0.2f;
    public float _hollowSpeed = 0.1f;

    public float _aimingSensitivity = 0.5f;
    public float _shotSpeed = 4.0f;

    public Vector3 _walkingAmont;
    public int _health = 3;


    public float _recticleDistance = 0.5f;
    public GameObject _recticlePrefab;
    public GameObject _shotPrefab;
    public GameObject _mouthCenter;
    public GameObject _destructionPrefab;
    public PointEffector2D _sucker;
    public Color _soulColor;

    public ControlScheme _controlScheme;
    public string _characterID;
    private bool _isAiming;

    private bool _isFiring;
    private Vector3 _aimingDirection;
    private Vector3 _walkingDirection;
    private GameObject _recticleGameObject;
    private Animator _animator;

    private bool _isHurt;
    private float _hurtTimer;
    // Use this for initialization
    void Start()
    {
        _recticleGameObject = GameObject.Instantiate<GameObject>(_recticlePrefab);
        _animator = GetComponent<Animator>();
        if (!_hasSoul)
            _animator.SetTrigger("IsFiring");
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position += _maxSpeed * new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime;

        _aimingDirection = new Vector3(Input.GetAxis(_controlScheme.AimHorzizontalAxis), Input.GetAxis(_controlScheme.AimVerticalAxis));
        float aimingMagnitude = _aimingDirection.magnitude;
        _isAiming = aimingMagnitude > _aimingSensitivity;

        _recticleGameObject.SetActive(_isAiming && _hasSoul);
        _recticleGameObject.transform.position = _mouthCenter.transform.position + _recticleDistance * _aimingDirection;

        // FireLogic
        if (Input.GetButtonDown(_controlScheme.FireButton) && _hasSoul)
        {
            _animator.SetBool("IsFiring", true);

            Vector3 shootDirection = _isAiming ? _aimingDirection : _walkingDirection;
            GameObject shotGO = GameObject.Instantiate<GameObject>(_shotPrefab, _mouthCenter.transform.position, Quaternion.identity);
            Shot shotScript = shotGO.GetComponent<Shot>();
            shotScript.CharacterID = _characterID;
            shotScript._color = _soulColor;
            shotGO.GetComponent<Rigidbody2D>().position = _mouthCenter.transform.position;

            Vector2 shotSpeed = shootDirection.normalized * _shotSpeed;



            /*
            if (shotSpeed.magnitude < _shotSpeed)
                shotSpeed = shotSpeed.normalized * _shotSpeed;
                */
            shotGO.GetComponent<Rigidbody2D>().velocity = shotSpeed;

            _hasSoul = false;
            _isFiring = true;
        }

        _sucker.gameObject.SetActive(Input.GetButton(_controlScheme.FireButton) && !_hasSoul && !_isFiring);

        if (Input.GetButtonUp(_controlScheme.FireButton))
            _isFiring = false;
    }

    private void FixedUpdate()
    {
        _walkingAmont = new Vector3(Input.GetAxis(_controlScheme.HorzizontalAxis), Input.GetAxis(_controlScheme.VerticalAxis));
        _animator.SetFloat("Speed", _walkingAmont.magnitude);

        if (_walkingAmont.sqrMagnitude > 0.01)
            _walkingDirection = _walkingAmont.normalized;

        bool isBack = GetComponent<CharacterSprites>()._isBack = _walkingDirection.y > 0;



        if (Mathf.Abs(_walkingDirection.x) > 0.02 && _walkingDirection.x > 0)
            this.transform.localScale = new Vector3(isBack ? 1 : -1, 1, 1);
        else
            this.transform.localScale = new Vector3(isBack ? -1 : 1, 1, 1);


        this.GetComponent<Rigidbody2D>().velocity = (_hasSoul ? _normalSpeed : _hollowSpeed) * new Vector2(_walkingAmont.x, _walkingAmont.y);


        _hurtTimer += Time.fixedDeltaTime;

        if (_hurtTimer > 1.3)
        {
            _isHurt = false;
            _animator.SetBool("IsHurt", false);
        }


    }

    public void RecieveSoul()
    {
        _animator.SetBool("TriggerReturn", true);
        _hasSoul = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isHurt)
            return;
        _hurtTimer = 0;
        _isHurt = true;
        _animator.SetBool("IsHurt", true);
        _health--;

        if (_health <= 0)
        {
            StartCoroutine(Desintegrate());
        }
    }

    public IEnumerator Desintegrate()
    {
        GameObject.Instantiate<GameObject>(_destructionPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);

        GameManager.DoGameOver();
    }


}