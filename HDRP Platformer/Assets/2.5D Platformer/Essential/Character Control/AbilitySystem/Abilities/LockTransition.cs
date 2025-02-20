﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/LockTransition")]
    public class LockTransition : CharacterAbility
    {
        public float UnlockTime;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.DATASET.TRANSITION_DATA.LockTransition = true;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime > UnlockTime)
            {
                characterState.DATASET.TRANSITION_DATA.LockTransition = false;
            }
            else
            {
                characterState.DATASET.TRANSITION_DATA.LockTransition = true;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}