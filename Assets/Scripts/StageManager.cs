using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[Serializable]
public class StageElementData
{
    public PolygonCollider2D collider;

    public Transform limitLeft, limitRight, limitTop, limitBottom;
}

public class StageManager : MonoBehaviour
{
    public static StageManager _instance;
    [SerializeField] PlayerController playerController;
    [SerializeField] CinemachineConfiner confiner;

    [SerializeField] List<StageElementData> stageElements = new List<StageElementData>();
    [SerializeField] int stageIndex = 0;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        UpdateConfiner();
    }

    public void BeatStage()
    {
        stageIndex = Mathf.Clamp(stageIndex + 1, 0, stageElements.Count - 1);
        UpdateConfiner();
    }

    void UpdateConfiner()
    {
        playerController.SetLimits(stageElements[stageIndex].limitLeft, stageElements[stageIndex].limitRight, stageElements[stageIndex].limitTop, stageElements[stageIndex].limitBottom);
        confiner.m_BoundingShape2D = stageElements[stageIndex].collider;
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.T))
    //     {
    //         BeatStage();
    //     }
    // }
}
