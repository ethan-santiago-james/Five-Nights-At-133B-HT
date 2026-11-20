using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public bool isInCamMenu;
    public bool isInRangeOfCams;
    public bool isAttacking;
    public bool isUnderground;
    public bool isClimbingLadder;
    public bool isRespawning;
    public bool isDead;

    public Transform player;
    public LayerMask camLayer;

    public Text monsterText;
    public GameObject monsterBG;
    
    public Image underwearImage;
    public Text underwearText; 

    public int monsterCountDown = 120;
    public int underwears = 0;

    public static PlayerState Instance { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(updateMonsterCount());
        isInCamMenu = false;
        if (Instance == null)
        {
            Instance = this;

        }
        else if (Instance != this)
        {

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        monsterText.text = "Needs Energy Drink In: " + monsterCountDown + "s";
        underwearText.text = underwears + "";
        isInRangeOfCams = Physics.CheckSphere(player.position,15f,camLayer);

        if(isInRangeOfCams) {

            if(Input.GetKeyDown(KeyCode.C)) {

                isInCamMenu = true;

            }

             if(Input.GetKeyUp(KeyCode.C)) {

                isInCamMenu = false;

            }
 
        }
        
    }

    public IEnumerator updateMonsterCount() {

        while(true) {

            if(MenuScript.Instance.gameStarted) {
                monsterCountDown = Mathf.Max(0, monsterCountDown - 1);
                if(monsterCountDown == 0) {

                    // DIE
                    StartCoroutine(dieFromNoMonster());
                    break;
                }    
            }
            
            yield return new WaitForSeconds(1f);
        }
        

    }

    public IEnumerator updateCamMenu() {

        isInCamMenu = !isInCamMenu;
        yield return new WaitForSeconds(0.1f);

    }

    public IEnumerator dieFromNoMonster() {

        for (int i = 0; i > -91; i--) {

            player.rotation = Quaternion.Euler(i,0f,0f);
            yield return new WaitForSeconds(0.05f);
        }
        NewBehaviourScript.Instance.StartCoroutine(NewBehaviourScript.Instance.showFailureScreen("monster"));

    }
}
