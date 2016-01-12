using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public abstract class MainGameLogic : MonoBehaviour {
    [SerializeField]
    protected Text waveText;

    [SerializeField]
    protected GameObject GameOverStuff;

    [SerializeField]
    protected    Text highScoreText;

    [SerializeField]
    protected Text currentScoreText;

    [SerializeField]
    protected bool gameOver = false;
	// Use this for initialization
	public virtual void Start () {
        Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    public virtual void GameOver()
    {
        GameOverStuff.SetActive(true);
        UpdateHighScore();
        
    }

    public abstract void UpdateHighScore();


}
