using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using TMPro;

public class BallCtrl : MonoBehaviour
{
    public Agent[] players;
    private Rigidbody rb;
    public int blueScore, redScore;

    public TMP_Text blueTeamScore;
    public TMP_Text redTeamScore;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void InitBall()
    {
        rb.velocity = rb.angularVelocity = Vector3.zero;
        transform.localPosition = new Vector3(0, 1, 0);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("RED_GOAL"))
        {
            // BULE TEAM +1 REWARD
            players[0].AddReward(+1.0f);
            // RED TEAM -1 REWARD
            players[1].AddReward(-1.0f);

            // Ball 초기화
            InitBall();

            // Player 초기화
            players[0].EndEpisode();
            players[1].EndEpisode();

            blueTeamScore.text = (++blueScore).ToString("000");
        }

        if (coll.collider.CompareTag("BLUE_GOAL"))
        {
            // BULE TEAM -1 REWARD
            players[0].AddReward(-1.0f);
            // RED TEAM +1 REWARD
            players[1].AddReward(+1.0f);

            // Ball 초기화
            InitBall();

            // Player 초기화
            players[0].EndEpisode();
            players[1].EndEpisode();

            redTeamScore.text = $"{++redScore:000}";
        }
    }

}
