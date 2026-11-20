using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
 
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 2f;

    public bool isGoingUnderBed;
 
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask doorMask;
    public LayerMask animatronicMask;
    public LayerMask monsterMask;
    public LayerMask katanaMask;
    public LayerMask trapdoorMask;
    public LayerMask ladderMask;
    public LayerMask bedMask;
    public LayerMask underwearMask;

    public Image rowanImage;
    public Image jedImage;
    public Image ethanImage;
    public Image lesterImage;
    public Image calebImage;

    private Transform animatronic;
    public GameObject audioManagerObject;

    private AudioManager audioManager;
 
    Vector3 velocity;
     bool isGrounded;
     bool canOpenDoor;
     bool isGettingJumpScared;
    // Start is called before the first frame update
    void Start()
    {
         isGettingJumpScared = false;
         transform.rotation = Quaternion.Euler(0f,0f,0f);
         StartCoroutine(getJumpScared());
         audioManager = audioManagerObject.GetComponent<AudioManager>();
    }

    public IEnumerator climb() {

        if(!PlayerState.Instance.isUnderground) {

            yield return null;
        } else {

             PlayerState.Instance.isClimbingLadder = true;
                float initY = transform.position.y;
                for(int i = 0; i < 100; i++) { 

                    transform.position = new Vector3(transform.position.x, initY + (10f*(i/100f)), transform.position.z);
                    yield return new WaitForSeconds(0.01f);
            
                }
                PlayerState.Instance.isClimbingLadder = false;
        }
       

    }

    public IEnumerator goUnderBed(Collider bed) {

        controller.enabled = false;
        isGoingUnderBed = true;
        if(bed.gameObject.name == "HidableBed1") {

            transform.localPosition = new Vector3(126.12f,-8.28f,58f);

        } else if(bed.gameObject.name == "HidableBed2") {

            transform.localPosition = new Vector3(140.5f,-8.28f,58f); // 187.1539f, -0.23, 28.95

        }
        yield return null;
        isGoingUnderBed = false;
        controller.enabled = true;
        
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -5f) {

            PlayerState.Instance.isUnderground = true;

        } else {

            PlayerState.Instance.isUnderground = false;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Collider[] doorColliders = Physics.OverlapSphere(transform.position, 20f, doorMask);
        Collider[] enemiesThatCanJumpScare = Physics.OverlapSphere(transform.position, 6f, animatronicMask);
        Collider[] enemiesAttackable = Physics.OverlapSphere(transform.position, 8f, animatronicMask);
        Collider[] monsterColliders = Physics.OverlapSphere(transform.position, 10f, monsterMask);
        Collider[] katanaColliders = Physics.OverlapSphere(transform.position, 10f, katanaMask);
        Collider[] trapdoorColliders = Physics.OverlapSphere(transform.position, 13f, trapdoorMask);
        Collider[] ladderColliders = Physics.OverlapSphere(transform.position, 5f, ladderMask);
        Collider[] bedColliders = Physics.OverlapSphere(transform.position, 10f, bedMask);
        Collider[] underwearColliders = Physics.OverlapSphere(transform.position, 10f, underwearMask);

        foreach (Collider col in underwearColliders) {

            UnderwearScript script = col.gameObject.GetComponent<UnderwearScript>();

            if(!script.collected && Input.GetMouseButtonDown(0)) {

                script.collected = true;
                PlayerState.Instance.underwears += 1;

            }

        }   
        foreach (Collider col in bedColliders) {

            if(Input.GetKeyDown(KeyCode.B) && !PlayerState.Instance.isUnderground) {
  
                StartCoroutine(goUnderBed(col));

            }

        }
        foreach(Collider col in ladderColliders) {

            LadderScript script = col.gameObject.GetComponent<LadderScript>();

            if(Input.GetKeyDown(KeyCode.C) && (script.upperTrapdoor == null || script.upperTrapdoor.GetComponent<TrapdoorScript>().isOpen)) {

                StartCoroutine(climb());    

            }


        }

        foreach (Collider col in katanaColliders) {

            KatanaScript script = col.gameObject.GetComponent<KatanaScript>();

            if(!script.isPickedUp && Input.GetKeyDown(KeyCode.P)) {

                script.pickUp();

            }

        }

        foreach (Collider col in trapdoorColliders) {

            
            TrapdoorScript script = col.gameObject.GetComponent<TrapdoorScript>();

            if(Input.GetKeyDown(KeyCode.T)) {

                if(!script.isOpen && !script.isOpeningOrClosing) {

                    StartCoroutine(script.openTrapdoor());
                } else if(!script.isOpeningOrClosing) {

                    StartCoroutine(script.closeTrapdoor());
                }

            }

        }
        foreach (Collider col in monsterColliders) {

            MonsterScript script = col.gameObject.GetComponent<MonsterScript>();

            if(Input.GetMouseButtonDown(0) && !PlayerState.Instance.isUnderground) {

                PlayerState.Instance.monsterCountDown = 120;
                script.needsNewPosition = true;

            }

        }

        foreach (Collider col in enemiesAttackable) {

            Animatronic script = col.gameObject.GetComponent<Animatronic>();

            if(PlayerState.Instance.isAttacking && !script.isKnockedDown) {

                StartCoroutine(script.getKnockedDown());

            }

        }

        foreach (Collider col in enemiesThatCanJumpScare) {

            Animatronic script = col.gameObject.GetComponent<Animatronic>();

            if(script.canSeePlayer) {

                animatronic = col.gameObject.transform;
                isGettingJumpScared = true;

            }

        }

        foreach (Collider col in doorColliders)
        {

            if(Input.GetKey(KeyCode.O) && col.gameObject.name == "door") {


                LockerScript script = col.gameObject.GetComponent<LockerScript>();

                if(!script.isOpen && !script.isClosingOrOpening) {


                    StartCoroutine(script.openDoor());
                } else if(!script.isClosingOrOpening) {


                    StartCoroutine(script.closeDoor());
                }
                

            } else if(Input.GetKey(KeyCode.O) && col.gameObject.name == "Main Door") {

                DoorScript script = col.gameObject.GetComponent<DoorScript>();

                if(!script.isOpen && !script.isClosingOrOpening) {


                    StartCoroutine(script.openDoor());
                } else if(!script.isClosingOrOpening) {


                    StartCoroutine(script.closeDoor());
                }

            }

        }

                if (isGrounded && velocity.y < 0)
                {
                    velocity.y = -2f;
                }
        
                float x = (Input.GetAxis("Horizontal")/4);
                float z = Input.GetAxis("Vertical");
        
                //right is the red Axis, foward is the blue axis
                Vector3 move = transform.right * x + transform.forward * z;
        
                if(Input.GetKey(KeyCode.LeftControl) && !isGoingUnderBed && !PlayerState.Instance.isRespawning) {

                    controller.Move(move * speed * Time.deltaTime);

                } else if(!isGoingUnderBed && !PlayerState.Instance.isRespawning) {

                    controller.Move(move * (speed/2) * Time.deltaTime);
                }
        
                //check if the player is on the ground so he can jump
                if (Input.GetButtonDown("Jump") && isGrounded && !PlayerState.Instance.isClimbingLadder)
                {
                    //the equation for jumping
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

                if(!PlayerState.Instance.isClimbingLadder) {

                    velocity.y += gravity * Time.deltaTime;
                }   

                if(!isGoingUnderBed) {

                    controller.Move(velocity * Time.deltaTime);
                }
                
    }

    public IEnumerator showJumpScareImage() {

        switch(animatronic.gameObject.name) {

                    case "rowan":
                        rowanImage.gameObject.SetActive(true);
                        yield return new WaitForSeconds(1f);
                        rowanImage.gameObject.SetActive(false);
                        break;
                    case "jed":
                        jedImage.gameObject.SetActive(true);
                        yield return new WaitForSeconds(1f);
                        jedImage.gameObject.SetActive(false);
                        break;
                    case "lester":
                        lesterImage.gameObject.SetActive(true);
                        yield return new WaitForSeconds(1f);
                        lesterImage.gameObject.SetActive(false);
                        break;
                    case "ethan":
                        ethanImage.gameObject.SetActive(true);
                        yield return new WaitForSeconds(1f);
                        ethanImage.gameObject.SetActive(false);
                        break;
                    default:
                        calebImage.gameObject.SetActive(true);
                        yield return new WaitForSeconds(1f);
                        calebImage.gameObject.SetActive(false);
                        break;

        }

    }

    public IEnumerator getJumpScared() {

        while(true) {

            if(animatronic != null && animatronic.gameObject.GetComponent<Animatronic>().canSeePlayer && !animatronic.gameObject.GetComponent<Animatronic>().isKnockedDown
            && !animatronic.gameObject.GetComponent<Animatronic>().isAgainstWall) {
                
                if(PlayerState.Instance.underwears > 0) {

                    PlayerState.Instance.underwears -= 1;
                    isGettingJumpScared = false;
                    yield return new WaitForSeconds(5f);

                } else if(!PlayerState.Instance.isDead) {

                     audioManager.PlaySFX(audioManager.attack);
                    StartCoroutine(showJumpScareImage());
                    transform.LookAt(new Vector3(animatronic.position.x,animatronic.position.y + 6f,animatronic.position.z));
                    Cursor.lockState = CursorLockMode.None;
                    StartCoroutine(die());
                    break;
                }
               
                
            }
            yield return new WaitForSeconds(0.05f);
            

        }
        

    }

    public IEnumerator die() {

        for (int i = 0; i > -90; i--) {

            transform.rotation = Quaternion.Euler(i,-180f,0f);
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(NewBehaviourScript.Instance.showFailureScreen(animatronic.gameObject.name));

    }
}
