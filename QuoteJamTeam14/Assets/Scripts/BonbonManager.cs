using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class BonbonManager : MonoBehaviour
{
    [SerializeField, Min(1)] int minInput;
    [SerializeField, Min(1)] int maxInput;

    [SerializeField] Bonbon bonbonPrefab;
    [SerializeField] InputObject inputPrefab;

    [SerializeField] Transform posPlayer1;
    [SerializeField] Transform posPlayer2;

    [SerializeField] Transform posPlayerInput1;
    [SerializeField] Transform posPlayerInput2;
    [SerializeField] float height = 10;

    [SerializeField] float fallDuration = 0.5f;

    private Bonbon currentBBJ1;
    private bool isEmballageP1 = true;
    private Bonbon currentBBJ2;
    private bool isEmballageP2 = true;

    private List<InputObject> inputPlayer1 = new List<InputObject>();
    private List<InputObject> inputPlayer2 = new List<InputObject>();

    [SerializeField] List<Sprite> inputSprites = new List<Sprite>();
    [SerializeField] List<Sprite> bonbonSprites = new List<Sprite>();
    [SerializeField] List<Sprite> emballageSprites = new List<Sprite>();

    public static BonbonManager Get;
    private void Awake()
    {
        if (Get == null)
        {
            Get = this;
        }
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentBBJ1 = Spawnbonbon(posPlayer1, 1, posPlayerInput1);
        currentBBJ2 = Spawnbonbon(posPlayer2, 2, posPlayerInput2);
    }

    Bonbon Spawnbonbon(Transform pos, int playerId, Transform posInput)
    {
        Bonbon bb = Instantiate(bonbonPrefab, pos);
        ArrayList inputs = new ArrayList();

        //to do : Meilleur algo pour le nombre d'input a faire en fonction des scores ?
        int nbInput = Random.Range(minInput, maxInput);
        for (int i = 0; i < nbInput; ++i)
        {
            int rand = Random.Range(0, 4);
            inputs.Add(rand);

            InputObject obj = Instantiate(inputPrefab, posInput);
            obj.transform.position += i * height * Vector3.up;
            obj.Init(rand, inputSprites[rand]);

            if (playerId == 1)
                inputPlayer1.Add(obj);
            else if (playerId == 2)
                inputPlayer2.Add(obj);
            else Destroy(obj.gameObject);
        }
        
        int bonbonSpriteIndex = Random.Range(0, bonbonSprites.Count);
        int emballageSpriteIndex = Random.Range(0, emballageSprites.Count);
        if (playerId == 1)
        {
            if (isEmballageP1)
            {
                bb.Init(emballageSprites[emballageSpriteIndex], 50);
            }
            else
            {
                bb.Init(bonbonSprites[bonbonSpriteIndex], 50);
            }
        }
        else
        {
            if (isEmballageP2)
            {
                bb.Init(emballageSprites[emballageSpriteIndex], 50);
            }
            else
            {
                bb.Init(bonbonSprites[bonbonSpriteIndex], 50);
            }
        }
        PlayerInput.Get.SetListInput(playerId == 1, inputs);
        return bb;
    }

    public void DestroyBonbon(Bonbon bb, bool forReset = false)
    {
        if (bb == currentBBJ1)
        {
            // TO DO : add animation
            Destroy(bb.gameObject);
            if (!forReset && !isEmballageP1)
            {
                ScoreManager.Get.AddScrore(currentBBJ1.score, 1);
            }
            if (forReset) ResetEmballageStatus();
            else SwapEmballageStatus(1);
            currentBBJ1 = Spawnbonbon(posPlayer1, 1, posPlayerInput1);
        }
        else if (bb == currentBBJ2)
        {
            // TO DO : add animation
            Destroy(bb.gameObject);
            if (!forReset && !isEmballageP2)
            {
                ScoreManager.Get.AddScrore(currentBBJ2.score, 2);
            }
            if (forReset) ResetEmballageStatus();
            else SwapEmballageStatus(2);
            currentBBJ2 = Spawnbonbon(posPlayer2, 2, posPlayerInput2);
        }
        else
        {
            Destroy(bb.gameObject);
        }
    }

    public void DestroyInput(int playerId)
    {
        if (playerId == 1)
        {
            foreach(InputObject obj in inputPlayer1)
            {
                obj.StartFall(height, fallDuration);
            }
            Destroy(inputPlayer1[0].gameObject);
            inputPlayer1.RemoveAt(0);
            if (inputPlayer1.Count == 0)
            {
                DestroyBonbon(currentBBJ1);
            }
        }
        else if (playerId == 2)
        {
            foreach (InputObject obj in inputPlayer2)
            {
                obj.StartFall(height, fallDuration);
            }
            Destroy(inputPlayer2[0].gameObject);
            inputPlayer2.RemoveAt(0);
            if (inputPlayer2.Count == 0)
            {
                DestroyBonbon(currentBBJ2);
            }
        }
        else Debug.LogError("Wrong playerId");
    }

    public void DestroyAllInput()
    {
        foreach (InputObject obj in inputPlayer1)
        {
            Destroy(obj.gameObject);
        }
        inputPlayer1.Clear();
        DestroyBonbon(currentBBJ1, true);

        foreach (InputObject obj in inputPlayer2)
        {
            Destroy(obj.gameObject);
        }
        inputPlayer2.Clear();
        DestroyBonbon(currentBBJ2, true);
    }

    private void SwapEmballageStatus(int playerId)
    {
        if (playerId == 1)
        {
            isEmballageP1 = !isEmballageP1;
        }
        else if (playerId == 2)
        {
            isEmballageP2 = !isEmballageP2;
        }
        else Debug.LogError("Wrong playerId");
    }

    private void ResetEmballageStatus()
    {
        isEmballageP1 = true;
        isEmballageP2 = true;
    }
}
