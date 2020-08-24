﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/AnimatorStateTransition")]
    public class AnimatorStateTransition : CharacterAbility
    {
        static bool DebugTransitionTiming = true;

        [SerializeField] TransitionTarget transitionTo;
        int TargetStateNameHash = 0;

        public List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>();
        public List<TransitionConditionType> notConditions = new List<TransitionConditionType>();
        public float CrossFade;
        public float Offset;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            TargetStateNameHash = transitionTo.GetHashID();
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!Interfered(characterState.characterControl))
            {
                if (IndexChecker.MakeTransition(characterState.characterControl, transitionConditions))
                {
                    if (!IndexChecker.NotCondition(characterState.characterControl, notConditions))
                    {
                        characterState.ANIMATION_DATA.InstantTransitionMade = true;
                        MakeInstantTransition(characterState.characterControl);
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.ANIMATION_DATA.InstantTransitionMade = false;
        }

        void MakeInstantTransition(CharacterControl control)
        {
            if (CrossFade <= 0f)
            {
                control.characterSetup.SkinnedMeshAnimator.Play(TargetStateNameHash, 0);
            }
            else
            {
                if (DebugTransitionTiming)
                {
                    //Debug.Log("Instant transition to: " + TransitionTo.ToString() + " - CrossFade: " + CrossFade);
                }

                if (Offset <= 0f)
                {
                    control.characterSetup.SkinnedMeshAnimator.CrossFade(TargetStateNameHash, CrossFade, 0);
                }
                else
                {
                    control.characterSetup.SkinnedMeshAnimator.CrossFade(TargetStateNameHash, CrossFade, 0, Offset);
                }
            }
        }

        bool Interfered(CharacterControl control)
        {
            if (control.ANIMATION_DATA.LockTransition)
            {
                return true;
            }

            if (control.ANIMATION_DATA.InstantTransitionMade)
            {
                return true;
            }

            if (control.characterSetup.SkinnedMeshAnimator.GetInteger(
                HashManager.Instance.ArrMainParams[(int)MainParameterType.TransitionIndex]) != 0)
            {
                return true;
            }

            AnimatorStateInfo nextInfo = control.characterSetup.SkinnedMeshAnimator.GetNextAnimatorStateInfo(0);

            if (nextInfo.shortNameHash == TargetStateNameHash)
            {
                return true;
            }

            return false;
        }
    }
}