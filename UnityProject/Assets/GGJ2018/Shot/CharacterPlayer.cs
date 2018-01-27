using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : MonoBehaviour {

    public float _maxSpeed = 0.2f;
    public float _aimingSensitivity = 0.5f;
    public float _shotSpeed = 4.0f;

    public Vector3 _walkingAmont;

    public float _recticleDistance = 0.5f;
    public GameObject _recticlePrefab;
    public GameObject _shotPrefab;

    private bool _isAiming;
    private bool _isFiring;
    private Vector3 _aimingDirection;
    private Vector3 _walkingDirection;
    private GameObject _recticleGameObject;

	// Use this for initialization
	void Start ()
    {
        _recticleGameObject = GameObject.Instantiate<GameObject>(_recticlePrefab);

    }
	
	// Update is called once per frame
	void Update ()
    {
        //this.transform.position += _maxSpeed * new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime;

        _aimingDirection = new Vector3(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
        float aimingMagnitude = _aimingDirection.magnitude;
        _isAiming = aimingMagnitude > _aimingSensitivity;

        _recticleGameObject.SetActive(_isAiming);
        _recticleGameObject.transform.position = this.transform.position + _recticleDistance * _aimingDirection;

        if (Input.GetButtonDown("Fire3"))
        {
            Vector3 shootDirection = _isAiming ? _aimingDirection : _walkingDirection;
            GameObject shotGO = GameObject.Instantiate<GameObject>(_shotPrefab, this.transform.position, Quaternion.identity);
            shotGO.GetComponent<Rigidbody2D>().position = this.transform.position + shootDirection * 0.7f;
            shotGO.GetComponent<Rigidbody2D>().velocity = shootDirection * _shotSpeed;

            _isFiring = false;
        }
    }

    private void FixedUpdate()
    {
        _walkingAmont = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        _aimingDirection = new Vector3(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
        float aimingMagnitude = _aimingDirection.magnitude;
        _isAiming = aimingMagnitude > _aimingSensitivity;


        if (_walkingAmont.sqrMagnitude > 0.01)
            _walkingDirection = _walkingAmont.normalized;


        this.GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + _maxSpeed * new Vector2( _walkingAmont.x, _walkingAmont.y));

       
    }
}
