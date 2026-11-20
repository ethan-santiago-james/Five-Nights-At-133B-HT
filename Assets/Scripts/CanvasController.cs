using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{

    public float secondsElapsed;
    public Text timerText;
    public GameObject successScreen;
    public GameObject failureScreen;
    public Text failureText;

    bool isShowingSuccessScreen = false;

    public static NewBehaviourScript Instance { get; private set; } 
    
    private string[] deathMessages = {
        "The monster addiction was too strong, better luck next time!",
        "You asked Rowan if you could come over one too many times...",
        "Never trust Ethan when he's been drinking...",
        "Stay away from Lester's family, never bother them again!",
        "No Jed does not want to go sledding with you...",
        "Don't tell Caleb that Japan are the best rugby team..."
    };

    public Text successText;
    private static int night = 1;

    void Awake() {

        if (Instance == null)
        {
            Instance = this;

        }
        else if (Instance != this)
        {

            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        secondsElapsed = 0f;
        successScreen.SetActive(false);
        failureScreen.SetActive(false);
        timerText.gameObject.SetActive(true);
    }

    void resetTimer() {
        secondsElapsed = 0f;
    }   

    // Update is called once per frame
    void Update()
    {
        if(MenuScript.Instance.gameStarted == false) {
            return;
        }
        secondsElapsed += Time.deltaTime;

        switch (secondsElapsed) {

            case < 15f:
                timerText.text = "12am";
                break;

            case < 30f:

                timerText.text = "1am";
                break;

            case < 45f:

                timerText.text = "2am";
                break;

             case < 60f:

                timerText.text = "3am";
                break;

            case < 75f:

                timerText.text = "4am";
                break;

            case < 90f:

                timerText.text = "5am";
                break;

            default:

                timerText.text = "6am";
                resetTimer();
                StartCoroutine(showSuccessScreen());
                break;

            

        }
    }

    public IEnumerator showSuccessScreen() {

        if(PlayerState.Instance.isDead) {
            yield break;
        }
        PlayerState.Instance.isRespawning = true;
        PlayerState.Instance.player.GetComponent<CharacterController>().enabled = false;
        Debug.Log("Showing success screen");
        successScreen.SetActive(true);
        timerText.gameObject.SetActive(false);
        successText.text = "Congratulations, you survived Night " + night + "!";
        PlayerState.Instance.monsterText.gameObject.SetActive(false);
        PlayerState.Instance.monsterBG.SetActive(false);
        PlayerState.Instance.underwearImage.gameObject.SetActive(false);
        PlayerState.Instance.underwearText.gameObject.SetActive(false);
        PlayerState.Instance.player.localPosition = new Vector3(187.15f,-0.23f,28.95f);
        yield return new WaitForSeconds(1.5f);
        PlayerState.Instance.player.GetComponent<CharacterController>().enabled = true;
        PlayerState.Instance.isRespawning = false;

        if(night < 5) {

            night += 1;
            successScreen.SetActive(false);
            timerText.gameObject.SetActive(true);
            PlayerState.Instance.monsterText.gameObject.SetActive(true);
            PlayerState.Instance.monsterBG.SetActive(true);
            PlayerState.Instance.underwearImage.gameObject.SetActive(true);
            PlayerState.Instance.underwearText.gameObject.SetActive(true);

        } else {

            successText.text = "Mathias has successfully beat 133B Harbour Terrace!";

        }
        

    }

    public int getDeathMessageIndex(string animatronicName) {

        switch(animatronicName) {

            case "rowan":
                return 1;

            case "jed":
                return 4;

            case "lester":
                return 3;

            case "ethan":
                return 2;

            default:
                return 5;

        }
    }

    public IEnumerator showFailureScreen(string reason) {

        PlayerState.Instance.isDead = true;
        failureScreen.SetActive(true);
        timerText.gameObject.SetActive(false);
        PlayerState.Instance.monsterText.gameObject.SetActive(false);
        PlayerState.Instance.monsterBG.SetActive(false);
        PlayerState.Instance.underwearImage.gameObject.SetActive(false);
        PlayerState.Instance.underwearText.gameObject.SetActive(false);

        if(reason == "monster") {

            failureText.text = deathMessages[0];

        } else {

            failureText.text = deathMessages[getDeathMessageIndex(reason)];
        }

        
        yield return new WaitForSeconds(2f);
    }   
}
