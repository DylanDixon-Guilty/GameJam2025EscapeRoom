using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;
    public bool isTimerStart;

    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TextMeshProUGUI timerTextUI;
    [SerializeField] private string loseScene;

    
    private float elapsedTime = 20f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(isTimerStart)
        {
            if (elapsedTime >= 0)
            {
                elapsedTime -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(elapsedTime / 60);
                int seconds = Mathf.FloorToInt(elapsedTime % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                timerTextUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                Debug.Log("You did not complete the puzzle in time");
                SceneManager.LoadScene(loseScene);
            }
        }
    }
}
