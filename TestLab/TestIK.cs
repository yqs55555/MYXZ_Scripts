/*
 * FileName             : TestIK.cs
 * Author               : 
 * Creat Date           : 
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIK : MonoBehaviour
{
    public Transform lookAtTarget;

    public Transform leftHandTarget;
    public Transform rightHandTarget;
    public Transform leftFootTarget;
    public Transform rightFootTarget;

    private Animator _animator;

    void Start()
    {
        _animator = this.GetComponent<Animator>();
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (_animator != null)
        {
            //仅仅是头部跟着变动
            _animator.SetLookAtWeight(1);
            //身体也会跟着转, 弧度变动更大
            //_animator.SetLookAtWeight(1, 1, 1, 1);
            if (lookAtTarget != null)
            {
                _animator.SetLookAtPosition(lookAtTarget.position);
            }

            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            if (leftHandTarget != null)
            {
                _animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
                _animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
            }

            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            if (leftHandTarget != null)
            {
                _animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
                _animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
            }

            _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
            if (leftHandTarget != null)
            {
                _animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootTarget.position);
                _animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootTarget.rotation);
            }

            _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
            if (leftHandTarget != null)
            {
                _animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootTarget.position);
                _animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootTarget.rotation);
            }
        }
    }
}
