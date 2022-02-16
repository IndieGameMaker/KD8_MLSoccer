using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;

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
    private BehaviorParameters bps;

    void InitPlayer()
    {
        // Player의 색상 변경
        GetComponent<Renderer>().material = materials[(int)team];
        // Player의 위치, 각도 변경
        tr.localPosition = (team == Team.Blue) ? bluePos : redPos;
        tr.localRotation = (team == Team.Blue) ? blueRot : redRot;
        // Team 설정
        bps.TeamId = (int)team;
        // 물리엔진 초기화
        rb.velocity = rb.angularVelocity = Vector3.zero;
    }

    public override void Initialize()
    {
        tr = GetComponent<Transform>(); // tr = transform;
        rb = GetComponent<Rigidbody>();
        bps = GetComponent<BehaviorParameters>();

        InitPlayer();

        // 최대 시도횟수
        MaxStep = 10000;
    }

    public override void OnEpisodeBegin()
    {
        InitPlayer();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
    }

}
