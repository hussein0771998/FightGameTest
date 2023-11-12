using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    private Vector3 _input;
    public Animator playerAnimator;
    public GameObject camera1;
    public ManagerJoystic MJ;
    public bool Sword , arrow , arrowWalk , swordWalk;
    public Button gunIcon;
    public Sprite swordSprite, arowSprite;
    private void Start()
    {
        Sword = false;
        arrow = false;
       
    }
    private void Update()
    {
        
        camera1.transform.position = new Vector3(transform.position.x,
            transform.position.y, transform.position.z);
        GatherInput();
        Look();
    }
    public void GunAnimation(int gunNumber)
    {
        playerAnimator.SetInteger("GunNumber", gunNumber);

        if (gunNumber == 1)
        {
           
              Sword = !Sword;
          
            playerAnimator.SetBool("sword idle", Sword);
            playerAnimator.SetBool("arrowIdle", !Sword);
            arrow = false;
        }
        if (gunNumber == 2)
        {
            
            arrow = !arrow;
           
            playerAnimator.SetBool("arrowIdle", arrow);
            playerAnimator.SetBool("sword idle", !arrow);

            Sword = false;
        }

        if (playerAnimator.GetBool("arrowIdle") == true)
            arrowWalk = true;
        else
            arrowWalk = false;
        
        if (playerAnimator.GetBool("sword idle") == true)
            swordWalk = true;
        else
            swordWalk = false;

        if (Sword)
            gunIcon.GetComponent<Image>().sprite = swordSprite;
        
        
        if (arrow)
            gunIcon.GetComponent<Image>().sprite = arowSprite;


    }
    public void ShootArrow()
    {
        if (Sword)
        {
            string[] swordAnimations = { "shootsword1", "shootsword2", "shootsword3" };

            // Randomly select an animation from the array
            string randomAnimation = swordAnimations[Random.Range(0, swordAnimations.Length)];

            // Set the selected animation to true
            playerAnimator.SetBool(randomAnimation, true);
        }
       
        if (arrow && ArrowProjectTile.instance.canShoot)
        {
            playerAnimator.SetBool("shootArrow", true);
            ArrowProjectTile.instance.ShootArow();
        }
       
       
        StartCoroutine(EndAnimation());
    }
    IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(1f);
        playerAnimator.SetBool("shootArrow", false);
        playerAnimator.SetBool("shootsword1", false); 
        playerAnimator.SetBool("shootsword2", false);
        playerAnimator.SetBool("shootsword3", false);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GatherInput()
    {
        _input = new Vector3(MJ.inputHorizantal(), 0, MJ.inputVertical());
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;

        var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        Vector3 movement = transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime;
        _rb.MovePosition(transform.position + movement);

        // Check if the player is moving
        bool isMoving = _input != Vector3.zero;

        // Set the "walk" parameter based on whether the player is moving
        if (!arrowWalk || !swordWalk)
        {
            playerAnimator.SetBool("walk", isMoving);
            _speed = 5;
        }
        if(arrow)
        {
            playerAnimator.SetBool("walkArrow", isMoving);
            _speed = 2;
        }
        if (Sword)
        {
            playerAnimator.SetBool("walkSword", isMoving);
            _speed = 4;
        }

       // Debug.Log(" isMoving : " + isMoving);
    }
}



