﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/Turn180")]
    public class Turn180 : CharacterAbility
    {
        public bool TurnOnEnter;
        public bool TurnOnExit;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (TurnOnEnter)
            {
                if (characterState.control.GetBool(typeof(FacingForward)))
                {
                    characterState.control.RunFunction(typeof(FaceForward), false);
                }
                else
                {
                    characterState.control.RunFunction(typeof(FaceForward), true);
                }
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (TurnOnExit)
            {
                if (characterState.control.GetBool(typeof(FacingForward)))
                {
                    characterState.control.RunFunction(typeof(FaceForward), false);
                }
                else
                {
                    characterState.control.RunFunction(typeof(FaceForward), true);
                }
            }
        }
    }
}