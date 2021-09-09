using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text scoreP1;
    [SerializeField] Text scoreP2;

    [SerializeField] Text multiplierTextP1;
    [SerializeField] Text multiplierTextP2;

    int currentScoreP1;
    int currentScoreP2;

    int multiplierP1;
    int multiplierP2;

    bool blockMultiplierP1 = false;
    bool blockMultiplierP2 = false;

    public int lowScore = 50;
    public int mediumScore = 75;
    public int highScore = 100;
    public int goldMultiplier = 3;

    void Start() {
        ResetScore();
    }

    public static ScoreManager Get;
    private void Awake()
    {
        if (Get == null)
        {
            Get = this;
        }
        else Destroy(this.gameObject);
    }

    public void AddScrore(int scoreToAdd, int playerId)
    {
        if (playerId == 1)
        {
            currentScoreP1 += multiplierP1 * scoreToAdd;
            scoreP1.text = currentScoreP1.ToString();
        }
        else
        {
            currentScoreP2 += multiplierP2 * scoreToAdd;
            scoreP2.text = currentScoreP2.ToString();
        }
    }

    public void ResetScore()
    {
        ResetMultiplier();

        currentScoreP1 = 0;
        scoreP1.text = currentScoreP1.ToString();

        currentScoreP2 = 0;
        scoreP2.text = currentScoreP2.ToString();
    }

    private void ResetMultiplier() {
        multiplierP1 = 1;
        multiplierP2 = 1;
    }

    public void ResetMultiplier(bool isP1) {    // Overload used to be called from PlayerInput Script when a wrong input happens
        if(isP1) {
            multiplierP1 = 1;
            multiplierTextP1.text = "x" + multiplierP1;
        } else {
            multiplierP2 = 1;
            multiplierTextP2.text = "x" + multiplierP2;
        }
    }
    
    public void IncrementMultiplier(bool isP1) {
        if(isP1)
        {
            if (!blockMultiplierP1)
            {
                multiplierP1++;
                multiplierTextP1.text = "x" + multiplierP1;
            }
        } 
        else
        {
            if (!blockMultiplierP2)
            {
                multiplierP2++;
                multiplierTextP2.text = "x" + multiplierP2;
            }
        }
    }

    public void SetBlockMultiplier(bool isP1, bool state)
    {
        if (isP1)
        {
            blockMultiplierP1 = state;
        }
        else
        {
            blockMultiplierP2 = state;
        }
    }
}
