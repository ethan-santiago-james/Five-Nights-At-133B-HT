using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    public Text titleText;
    public Button playButton;
   
    public Button skipIntroButton;

    public GameObject rowanIntro;
    public Text rowanText;
    public GameObject jedIntro;
    public Text jedText;
    public GameObject lesterIntro;
    public Text lesterText;
    public GameObject ethanIntro;
    public Text ethanText;
    public GameObject calebIntro;
    public Text calebText;

    public Text introText;

    public static MenuScript Instance { get; private set; }
    public bool gameStarted;

    string[] introLines = {
        "Mathias remembered when Lester mentioned 133B Harbour Terrace long ago...",
        "He plans to journey there to attain energy drinks for a late study session.",
        "Ethan has fell into a deep depression, and is now bald and fat.",
        "Caleb gave up on rowing, and has put on 30kg.",
        "Jed has changed his haircut to increase his Twitch audience.",
        "Lester has some wrinkles as he got caught up in a fire!",
        "Rowan was inspired by Ethan to shave his head as well."
    };

    void Awake()
    {
        
        rowanIntro.SetActive(false);
        jedIntro.SetActive(false);
        lesterIntro.SetActive(false);
        ethanIntro.SetActive(false);
        calebIntro.SetActive(false);

        gameStarted = false;
        gameObject.SetActive(true);
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
        skipIntroButton.onClick.AddListener(SkipIntro);
        StartCoroutine(playIntro());
        
    }

    void startGame() {

        PlayerState.Instance.monsterText.gameObject.SetActive(false);
        PlayerState.Instance.monsterBG.SetActive(false);
        
        skipIntroButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
        playButton.onClick.AddListener(OnButtonClicked);
        titleText.gameObject.SetActive(true);

    }

    void SkipIntro() {

        //StopAllCoroutines();
        introText.gameObject.SetActive(false);
        ethanIntro.gameObject.SetActive(false);
        calebIntro.gameObject.SetActive(false);
        jedIntro.gameObject.SetActive(false);
        lesterIntro.gameObject.SetActive(false);
        rowanIntro.gameObject.SetActive(false);
        skipIntroButton.gameObject.SetActive(false);
        startGame();
    }

    void OnButtonClicked() {

        gameStarted = true;
        gameObject.SetActive(false);
        PlayerState.Instance.monsterText.gameObject.SetActive(true);
        PlayerState.Instance.monsterBG.SetActive(true);
        PlayerState.Instance.underwearImage.gameObject.SetActive(true);
        PlayerState.Instance.underwearText.gameObject.SetActive(true);
    }

    public IEnumerator playIntro() {

        introText.gameObject.SetActive(true);

        for (int i = 0; i < introLines[0].Length; i++) {
            introText.text = introLines[0].Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        } 
        yield return new WaitForSeconds(0.5f);  
        for (int i = 0; i < introLines[1].Length; i++) {
            introText.text = introLines[1].Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        introText.gameObject.SetActive(false);

        ethanIntro.gameObject.SetActive(true);
        for (int i = 0; i < introLines[2].Length; i++) {
            ethanText.text = introLines[2].Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.5f);
        ethanIntro.gameObject.SetActive(false);

        calebIntro.gameObject.SetActive(true);
        for (int i = 0; i < introLines[3].Length; i++) {
            calebText.text = introLines[3].Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.5f);
        calebIntro.gameObject.SetActive(false);

        jedIntro.gameObject.SetActive(true);
        for (int i = 0; i < introLines[4].Length; i++) {
            jedText.text = introLines[4].Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.5f);
        jedIntro.gameObject.SetActive(false);

        lesterIntro.gameObject.SetActive(true);
        for (int i = 0; i < introLines[5].Length; i++) {
            lesterText.text = introLines[5].Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.5f);
        lesterIntro.gameObject.SetActive(false);

        rowanIntro.gameObject.SetActive(true);
        for (int i = 0; i < introLines[6].Length; i++) {
            rowanText.text = introLines[6].Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.5f);
        rowanIntro.gameObject.SetActive(false);
        startGame();

    }

}
