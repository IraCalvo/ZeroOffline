using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    //temp
    [SerializeField] GameObject playerToSpawn;
    public float DelvingTimer;
    bool playerIsDelving = false;


    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        { 
            Destroy(Instance);
        }
    }

    private void Update()
    {
        if (playerIsDelving)
        {
            DelvingTimer += Time.deltaTime;
        }
    }

    public void BeginDelve()
    {
        playerIsDelving = true;

        //find a way to 'process' the load and wait for the room to generate then turn everything on?
        PlayerBattleUIManager.instance.SetupBatleUI(true);
    }
}
