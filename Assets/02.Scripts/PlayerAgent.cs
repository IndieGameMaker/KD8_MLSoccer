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
        var action = actions.DiscreteActions;

        Vector3 dir = Vector3.zero;
        Vector3 rot = Vector3.zero;

        switch (action[0])
        {
            case 1: dir = tr.forward; break;
            case 2: dir = -tr.forward; break;
        }
        switch (action[1])
        {
            case 1: rot = -tr.up; break;
            case 2: rot = tr.up; break;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        /* Discrete 방식
            Branch 0 - 전/후 이동 (0:정지, 1:전진, 2:후진)
            Branch 1 - 좌/우 회전 (0:정지, 1:왼쪽, 2:오른쪽)
        */

        var actions = actionsOut.DiscreteActions;
        actions.Clear();

        // Branch 0
        if (Input.GetKey(KeyCode.W)) actions[0] = 1;
        if (Input.GetKey(KeyCode.S)) actions[0] = 2;

        // Branch 1
        if (Input.GetKey(KeyCode.A)) actions[1] = 1;
        if (Input.GetKey(KeyCode.D)) actions[1] = 1;
    }

}
