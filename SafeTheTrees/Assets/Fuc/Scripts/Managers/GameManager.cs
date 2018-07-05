using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public static int worldWidth = 1000;

    public Texture2D axeCursor;
    public Texture2D pickCursor;
    public Texture2D handCursor;
    public Texture2D interactCursor;
    void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
}
