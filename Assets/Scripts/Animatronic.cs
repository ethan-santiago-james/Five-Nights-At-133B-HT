using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatronic : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 4f;
    public float gravity = -9.81f * 2;

    public LayerMask groundMask, playerMask, wallMask;
    public Transform groundCheck;
    public Transform player;

    public bool canSeePlayer;
    public bool isAgainstWall;
    public bool isKnockedDown;
    public bool isUnderground;

    bool isGrounded;

    float frameCount;

    Vector3 velocity;   
    Vector3 desiredPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
        switch (gameObject.name)
        {
            case "rowan":
                desiredPosition = new Vector3(40.2f,0,38f);
                speed = 4f;
                break;

            case "jed":
                desiredPosition = new Vector3(116.4729f,0.127f,45.6f);
                speed = 2f;
                break;

            case "ethan":
                desiredPosition = new Vector3(137.76f,-1.88f,15.81f);
                speed = 3f;
                break;

            case "lester":
                desiredPosition = new Vector3(125.8f,-1.93f,87.92f);
                speed = 5f;
                break;

            case "caleb":
                desiredPosition = new Vector3(94.7f,-1.32f,35.87f);
                speed = 8f;
                break;
        }
    
        StartCoroutine(changeRotation());
        controller = GetComponent<CharacterController>();
        respawn();
        canSeePlayer = false;
    } 

    void setDesiredPosition(Vector3 desiredPosition) {

        this.desiredPosition = desiredPosition;
    }

    void respawn() {

        controller.enabled = false;
        switch (gameObject.name)
        {
            case "rowan":
                transform.localPosition = new Vector3(-43.79f,16.83f,-82.91f);
                break;

            case "jed":
                transform.localPosition = new Vector3(26.45f,17.07f,-75.2f);
                break;

            case "ethan":
                transform.localPosition = new Vector3(45.26f,16.91f,-92.44f);
                break;

            case "lester":
                transform.localPosition = new Vector3(7.4f,16.93f,-80.38f);
                break;

            case "caleb":
                transform.localPosition = new Vector3(25.4f,16.89f,-40.1f);
                break;
        }
        controller.enabled = true;

    }

    public IEnumerator changeRotation() {

        while(true) {

            if(!canSeePlayer && !isKnockedDown) {

                
                int num = Random.Range(0,4);
                
                if(num == 0) {
                    
                    transform.rotation = Quaternion.Euler(0f,0f,0f);
                }

                if(num == 1) {
                    
                    transform.rotation = Quaternion.Euler(0f,180f,0f);
                }


                if(num == 2) {
                    
                    transform.rotation = Quaternion.Euler(0f,90f,0f);
                }

                if(num == 3) {
                    
                    transform.rotation = Quaternion.Euler(0f,270f,0f);
                }  
            }
            
            yield return new WaitForSeconds(Random.Range(3f,10f));
        }
        

    }
    
    public IEnumerator getKnockedDown() {

        speed = 0f;
        isKnockedDown = true; 
        controller.height = 0f;
        for (int i = 0; i > -91; i--) {

            transform.rotation = Quaternion.Euler(i,transform.rotation.y,0f);
            yield return new WaitForSeconds(0.01f); 
        }
        yield return new WaitForSeconds(Random.Range(5f,10f));
        controller.height = 2f;
        for (int i = -90; i < 1; i++) {

            transform.rotation = Quaternion.Euler(i,transform.rotation.y,0f);
            yield return new WaitForSeconds(0.01f); 
        }
        speed = 4f; 
        isKnockedDown = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.y < -5f) {

           isUnderground = true;

        } else {

            isUnderground = false;
        }

        if(PlayerState.Instance.isRespawning) {

            respawn();

        }
        
        
        // DEFAULT BEHAVIOUR
        if (Physics.CheckSphere(transform.position, 20f, playerMask) && 
            (DoorScript.Instance.isOpen || !PlayerState.Instance.isInRangeOfCams) && !isKnockedDown && (!PlayerState.Instance.isUnderground || (isUnderground && PlayerState.Instance.isUnderground)))
        {
            

            // Direction from this object to the playerrtrhy
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            
            // Distance to player
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            Vector3 flatDirection = new Vector3(directionToPlayer.x, 0, directionToPlayer.z);

            if (flatDirection != Vector3.zero)  // Prevents LookRotation errors
            {
                Quaternion lookRotation = Quaternion.LookRotation(flatDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

           canSeePlayer = true;

        } else {

            canSeePlayer = false;
        }
        
        if(!isKnockedDown) {

            controller.Move(transform.forward * speed * Time.deltaTime);
        }
        
        // GRAVITY

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        frameCount++;
        if(frameCount > 100) {

            frameCount = 0;
        }

    }
}
