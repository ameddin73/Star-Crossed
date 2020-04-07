using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int _score, _highScore, _delta;
    public Text scoreObject, highScoreObject;

    // Start is called before the first frame update
    void Start()
    {
        _score = _highScore = _delta = 0;
    }

    public void IncrementScore(int increment)
    {
        _score += increment;
    }

    public void EndGame()
    {
        _highScore = Mathf.Max(_score, _highScore);
        highScoreObject.GetComponent<Text>().text = "" + _highScore;
        _score = _delta = 0;
        scoreObject.GetComponent<Text>().text = "" + _score;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (_score != _delta)
        {
            _delta = _score;
            scoreObject.GetComponent<Text>().text = "" + _score;
        }
    }
}