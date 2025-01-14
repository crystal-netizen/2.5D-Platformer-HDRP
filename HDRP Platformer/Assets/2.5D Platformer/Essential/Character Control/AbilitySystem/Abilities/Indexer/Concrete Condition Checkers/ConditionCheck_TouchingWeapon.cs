﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_TouchingWeapon : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.DATASET.COLLIDING_OBJ_DATA.CollidingWeapons.Count == 0)
            {
                if (control.DATASET.WEAPON_DATA.HoldingWeapon == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}