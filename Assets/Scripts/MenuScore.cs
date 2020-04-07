using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScore : MonoBehaviour
{

    public Text highScoreObject;
    // Start is called before the first frame update
    void Start()
    {
        highScoreObject.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("highScore");
    }

    // Update is called once per frame
    void Update()
    {
        // highScoreObject.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("highScore");
    }
}
