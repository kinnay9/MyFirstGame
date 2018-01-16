using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager intance = null;

    public BoardManager boardScript;

    private int level = 3;



	// Use this for initialization
	void Awake ()
    {
        if (intance == null)
            intance = this;
        else if (intance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();

        InitGame();
		
	}

    void InitGame()
    {
        boardScript.SetupScene(level);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
