using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class BallCtrl : MonoBehaviour
{
    public Agent[] players;
    private Rigidbody rb;

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
        }
    }

}
