using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class DistanceTracker : NetworkBehaviour
{
    private Vector3 lastPosition;

    [Networked]
    public float TotalDistance { get; set; }

    public override void Spawned()
    {
        lastPosition = transform.position;
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) return; // Chỉ StateAuthority tính

        float distanceThisFrame = Vector3.Distance(transform.position, lastPosition);

        // Tuỳ bạn đặt điều kiện lọc, ví dụ: ignore nếu nhảy xa bất thường
        if (distanceThisFrame < 5f)
        {
            TotalDistance += distanceThisFrame;
            GetComponent<PlayerRaceData>().Distance = TotalDistance;
        }

        lastPosition = transform.position;
    }
}
