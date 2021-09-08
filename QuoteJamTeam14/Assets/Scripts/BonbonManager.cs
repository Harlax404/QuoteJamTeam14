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

    private Bonbon currentBBJ1;
    private Bonbon currentBBJ2;

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
        bb.Init(inputs, playerId, emballageSprites[emballageSpriteIndex], bonbonSprites[bonbonSpriteIndex]);
        return bb;
    }

    public void DestroyBonbon(Bonbon bb)
    {
        if (bb == currentBBJ1)
        {
            // TO DO : add animation
            Destroy(bb.gameObject);
            currentBBJ1 = Spawnbonbon(posPlayer1, 1, posPlayerInput1);
        }
        else if (bb == currentBBJ2)
        {
            // TO DO : add animation
            Destroy(bb.gameObject);
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
            Destroy(inputPlayer1[0].gameObject);
            inputPlayer1.RemoveAt(0);
            if (inputPlayer1.Count == 0)
            {
                DestroyBonbon(currentBBJ1);
            }
        }
        else if (playerId == 2)
        {
            Destroy(inputPlayer2[0].gameObject);
            inputPlayer2.RemoveAt(0);
            if (inputPlayer2.Count == 0)
            {
                DestroyBonbon(currentBBJ2);
            }
        }
        else Debug.LogError("Wrong playerId");
    }
}