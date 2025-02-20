﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/WallJumpPrep")]
    public class WallJumpPrep : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.control.MoveLeft = false;
            characterState.control.MoveRight = false;
            characterState.control.RIGID_BODY.velocity = Vector3.zero;

            characterState.DATASET.MOVE_DATA.Momentum = 0f;

            if (characterState.control.GetBool(typeof(FacingForward)))
            {
                characterState.control.RunFunction(typeof(FaceForward), false);
            }
            else
            {
                characterState.control.RunFunction(typeof(FaceForward), true);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}