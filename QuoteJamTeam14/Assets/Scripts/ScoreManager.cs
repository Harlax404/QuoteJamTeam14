using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text scoreP1;
    [SerializeField] Text scoreP2;

    int currentScoreP1;
    int currentScoreP2;

    [SerializeField] int lowScore = 50;
    [SerializeField] int mediumScore = 75;
    [SerializeField] int highScore = 100;

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
            currentScoreP1 += scoreToAdd;
            scoreP1.text = currentScoreP1.ToString();
        }
        else
        {
            currentScoreP2 += scoreToAdd;
            scoreP2.text = currentScoreP2.ToString();
        }
    }

    public void ResetScore()
    {
        currentScoreP1 = 0;
        scoreP1.text = currentScoreP1.ToString();

        currentScoreP2 = 0;
        scoreP2.text = currentScoreP2.ToString();
    }
}
