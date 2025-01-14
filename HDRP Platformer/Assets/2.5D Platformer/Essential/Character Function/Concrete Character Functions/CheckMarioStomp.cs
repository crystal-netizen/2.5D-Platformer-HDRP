﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CheckMarioStomp : CharacterFunction
    {
        List<CharacterControl> targets;

        public override void RunFunction()
        {
            targets = control.DATASET.BLOCKING_DATA.MarioStompTargets;

            if (control.DATASET.BLOCKING_DATA.MarioStompTargets.Count > 0)
            {
                DealDamageToTarget();
            }
            else
            {
                AddsTargetToList();
            }
        }

        void DealDamageToTarget()
        {
            control.RIGID_BODY.velocity = Vector3.zero;
            control.RIGID_BODY.AddForce(Vector3.up * 250f);

            foreach (CharacterControl c in targets)
            {
                AttackCondition info = new AttackCondition();
                info.CopyInfo(c.DATASET.DAMAGE_DATA.MarioStompAttack, control);

                int index = Random.Range(0, c.DATASET.RAGDOLL_DATA.ArrBodyParts.Length);

                TriggerDetector randomPart =
                    c.DATASET.RAGDOLL_DATA.ArrBodyParts[index].GetComponent<TriggerDetector>();

                c.DATASET.DAMAGE_DATA.damageTaken = new DamageTaken(
                    control,
                    c.DATASET.DAMAGE_DATA.MarioStompAttack,
                    randomPart,
                    control.RIGHT_FOOT_ATTACK,
                    Vector3.zero);

                c.RunFunction(typeof(DamageReaction), info);
            }

            targets.Clear();
        }

        void AddsTargetToList()
        {
            if (control.DATASET.BLOCKING_DATA.DownBlockingObjs.Count > 0)
            {
                foreach (KeyValuePair<GameObject, List<GameObject>> data in
                    control.DATASET.BLOCKING_DATA.DownBlockingObjs)
                {
                    foreach (GameObject obj in data.Value)
                    {
                        _Add(obj);
                    }
                }
            }
        }

        void _Add(GameObject obj)
        {
            CharacterControl c = CharacterManager.Instance.GetCharacter(obj.transform.root.gameObject);

            if (c != null)
            {
                if (c.BOX_COLLIDER.center.y + c.transform.position.y <
                    control.transform.position.y)
                {
                    if (c != control)
                    {
                        if (!targets.Contains(c))
                        {
                            targets.Add(c);
                        }
                    }
                }
            }
        }
    }
}