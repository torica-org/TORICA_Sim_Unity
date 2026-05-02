using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.XR.Hands;

public class XRHandsTracker : MonoBehaviour
{
    private XRHandSubsystem handSubsystem;
    private bool isGameStarted = false;

    void Start()
    {
        List<XRHandSubsystem> subsystems = new List<XRHandSubsystem>();
        SubsystemManager.GetInstances(subsystems);

        if (subsystems.Count > 0)
        {
            handSubsystem = subsystems[0];
        }
    }

    void Update()
    {
        if (handSubsystem == null || !handSubsystem.running || isGameStarted) return;

        XRHand rightHand = handSubsystem.rightHand;
        if (!rightHand.isTracked) return;

        // 手首、親指、人差し指、小指のジョイント情報を取得します
        XRHandJoint wrist = rightHand.GetJoint(XRHandJointID.Wrist);
        XRHandJoint thumbTip = rightHand.GetJoint(XRHandJointID.ThumbTip);
        XRHandJoint indexTip = rightHand.GetJoint(XRHandJointID.IndexTip);
        XRHandJoint littleTip = rightHand.GetJoint(XRHandJointID.LittleTip);

        // すべての関節の座標（Pose）が取得できているか確認します
        if (wrist.TryGetPose(out Pose wristPose) &&
            thumbTip.TryGetPose(out Pose thumbPose) &&
            indexTip.TryGetPose(out Pose indexPose) &&
            littleTip.TryGetPose(out Pose littlePose))
        {
            // 手首から各指先までの距離を計算します
            float thumbDistance = Vector3.Distance(wristPose.position, thumbPose.position);
            float indexDistance = Vector3.Distance(wristPose.position, indexPose.position);
            float littleDistance = Vector3.Distance(wristPose.position, littlePose.position);

            // 親指が遠く（10cm以上）、他の指が近い（7cm未満）状態を判定します
            if (thumbDistance > 0.1f && indexDistance < 0.07f && littleDistance < 0.07f)
            {
                isGameStarted = true;
                OnGameStartGesture();
            }
        }
    }

    private void OnGameStartGesture()
    {
        Debug.Log("「親指を立てる」ジェスチャーを検知しました");
    }
}