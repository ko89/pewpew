using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float _walkSpeed;
    public float _fastWalkSpeed;
    public int _health = 5;

    public GameObject _destructionPrefab;

    Vector2 walkDirection;
    float walkAmount;
    float waitTime;
    float currentTime = 0;

    Vector2 currentVelocity = new Vector2(0, 0);
    Vector2 currentWalkVector = new Vector2(0, 0);
    private GameManager _gameManager;
	// Use this for initialization
	void Start ()
    {
        _gameManager = GameManager.Instance;
        StartCoroutine(Behaviour());
    }


    //
    public IEnumerator Behaviour()
    {
        Vector3 targetPos;
        while (true)
        {
            Vector2 hollowDirection = transform.position - _gameManager.HollowPlayer.transform.position;


            if (Random.Range(0, 2) == 0)
                walkDirection = -hollowDirection;
            else
                walkDirection = Random.insideUnitCircle;

            walkDirection.Normalize();


            walkAmount = Random.Range(2, 3);
            waitTime = Random.Range(1, 3);
            currentTime = 0;
            Debug.Log("Walk");
            while (currentTime < waitTime)
            {
                yield return new WaitForFixedUpdate();
                //Debug.Log("CVW" + currentWalkVector);
                //Debug.Log(walkDirection * walkAmount);

               
                currentTime += Time.fixedDeltaTime;
                if (walkDirectionOverride)
                {
                    break;
                    //walkDirectionOverride = false;
                }
            }



            currentTime = 0;


            if (walkDirectionOverride)
            {
                waitTime = 0.0f;

            }
            else
            {
                waitTime = Random.Range(0.3f, 1.5f);

                walkAmount = 0.0f;
            }

                Debug.Log("Wait");
            while (currentTime < waitTime)
            {
                yield return new WaitForFixedUpdate();
                //Debug.Log("CVW" + currentWalkVector);
                //Debug.Log(walkDirection * walkAmount);


                currentTime += Time.fixedDeltaTime;
               // if (walkDirectionOverride)
                //{
                   // break;
                    //walkDirectionOverride = false;
               // }
            }
            /*
            while (currentTime < walkTime)
            {

                yield return new WaitForFixedUpdate();


                //Debug.Log("CVW" + currentWalkVector);
                //Debug.Log(walkDirection * walkAmount);

                currentWalkVector = Vector2.SmoothDamp(currentWalkVector, walkDirection * walkAmount, ref currentVelocity, 0.4f, 18.0f, Time.fixedDeltaTime);

                GetComponent<Rigidbody2D>().velocity = currentWalkVector;
                currentTime += Time.fixedDeltaTime;
                /*if (walkDirectionOverride)
                {
                    break;
                    //walkDirectionOverride = false;
                }
           


            currentWalkVector = Vector2.SmoothDamp(currentWalkVector, walkDirection * walkAmount, ref currentVelocity, 0.4f, 18.0f, Time.fixedDeltaTime);
            */



        }
    }

    private bool walkDirectionOverride = false;

    public void Update()
    {
        GetComponent<Animator>().SetBool("IsHurt", isHurt);
        GetComponent<Animator>().SetFloat("Speed", walkAmount);
        GetComponent<SpriteRenderer>().flipX = walkDirection.x > 0;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        currentWalkVector = collision.contacts[0].normal * walkAmount;
        walkDirection = collision.contacts[0].normal;
        walkDirectionOverride = true;
      
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        walkDirectionOverride = false;
    }

    public void FixedUpdate()
    {
        currentWalkVector = Vector2.SmoothDamp(currentWalkVector, walkDirection * walkAmount, ref currentVelocity, 0.7f, 28.0f, Time.fixedDeltaTime);

        if(!isHurt)
            GetComponent<Rigidbody2D>().velocity = currentWalkVector;

        hurtTimer += Time.fixedDeltaTime;

        if(hurtTimer > 1.3)
        {
            isHurt = false;
        }

    }









    bool isHit = false;
    bool isHurt = false;
    float hurtTimer = 0;
    public void Hit(object shot)
    {
        if (isHurt)
            return;
       


        GetComponent<Rigidbody2D>().AddForce(((Component)shot).GetComponent<Rigidbody2D>().velocity.normalized * 1000, ForceMode2D.Force);
        isHurt = true;
        _health--;
        if (_health <= 0)
        {
            StartCoroutine(Desintegrate());
        }




        hurtTimer = 0;
        //GetComponent<Rigidbody2D>().AddForce(Vector2.right * 100, ForceMode2D.Impulse);
        Debug.Log("PEW");
    }

    public IEnumerator Desintegrate()
    {
        GameObject.Instantiate<GameObject>(_destructionPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
