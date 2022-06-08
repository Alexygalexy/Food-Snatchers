using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RayaBotClone : RayaBot
{
    // Start is called before the first frame update
    protected override void Start()
    {
        ScoreBoard = transform.Find("ScoreBoard_Raya_Clone").gameObject;//Instantiate(myPrefabScoreBoard, transform.position, Quaternion.identity);
        player1_scoreText = ScoreBoard.GetComponentInChildren<TextMeshProUGUI>();

        audioRaya = GetComponents<AudioSource>();
        hit = audioRaya[0];
        collect = audioRaya[1];
    }

    // Update is called once per frame
    protected override void Update()
    {
        ClosestFood = FindClosestFood();
        movePositionTransform = ClosestFood;
        base.GoToPosition();
    }
}
