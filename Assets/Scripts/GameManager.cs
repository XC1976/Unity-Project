using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // SINGLETON : An object with a single instance that initiates itself, impossible to create other instance

    // To use a game manager from a public script : <nameSingleton>.Instance.<functionName>()
    // specify static makes it a single instance, accessible everywhere without any linking
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI textScore;   // affichage du score en cours

    // UI Game Over
    public TextMeshProUGUI textFinalScore;
    public GameObject PanelGameOver;

    // UI HP of player
    public TextMeshProUGUI textPlayerHP;
    public int playerHP;    // Set to public because we want the ability to change the number of lives in the editor directly

    // UI starting screen
    public GameObject PanelStart;
    
    // ========= Private ========= //
    private int _score = 0;
    private bool _isGameOver = false;
    
    // ========= Les fonctions ========= //

    // Start before the execution of the first frame
    private void Awake()
    {
        Instance = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Freeze the game until the player press "o" to start the game
        Time.timeScale = 0f;
        
        // Make the Game Over panel disappear
        PanelGameOver.SetActive(false);

        // Make the Start panel appear to matter what
        PanelStart.SetActive(true);

        // Show the player initial amount of lives
        textPlayerHP.text = "HP : " + playerHP;
    }

    // Update is called once per frame
    void Update()
    {
        // Unfreeze the initially frozen game if the player press "o"
        if(Keyboard.current != null && Keyboard.current.oKey.wasPressedThisFrame) {
            Time.timeScale = 1f;
            // Make the start panel disappear
            PanelStart.SetActive(false);
        }
        
        // Restart the scene (game) if the r key is pressed
        if(Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame) {
            // Unfreeze the scene, we froze it in the GameOver() function
            Time.timeScale = 1f;
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Function
    public void AddScore()
    {
        _score++;
        textScore.text = "Score : " + _score;
    }

    public void GameOver() {

        // Ensures the game over can be triggered only once per scene since it switches to true
        // if the player loses
        if (_isGameOver)
        {
            return;
        }

        if(playerHP > 1) {
            playerHP--;
            textPlayerHP.text = "HP : " + playerHP;
        } else {
            _isGameOver = true;
    
            // Freeze the game so the user is unable to move
            Time.timeScale = 0f;
    
            PanelGameOver.SetActive(true);
            textFinalScore.text = "Votre score est : " + _score;
        }
    }
}