using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

[RequireComponent(typeof(DecisionRequester))]
public class PlayerAgent : Agent
{
    public enum Team
    {
        Blue, Red
    }

    public Team team = Team.Blue;

    public Material[] materials;

    // Player 초깃값 변수
    private Vector3 bluePos = new Vector3(-5.5f, 0.5f, 0.0f);
    private Vector3 redPos = new Vector3(5.5f, 0.5f, 0.0f);
    private Quaternion blueRot = Quaternion.Euler(Vector3.up * 90.0f);
    private Quaternion redRot = Quaternion.Euler(Vector3.up * -90.0f);

    private Transform tr;
    private Rigidbody rb;

    void InitPlayer()
    {
        // Player의 색상 변경
        GetComponent<Renderer>().material = materials[(int)team];
        // Player의 위치, 각도 변경
        tr.localPosition = (team == Team.Blue) ? bluePos : redPos;
        tr.localRotation = (team == Team.Blue) ? blueRot : redRot;
    }

    public override void Initialize()
    {
        tr = GetComponent<Transform>(); // tr = transform;
        rb = GetComponent<Rigidbody>();
        InitPlayer();
    }

}
