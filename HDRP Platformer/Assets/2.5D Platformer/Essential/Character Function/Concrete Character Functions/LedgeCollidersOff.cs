﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class LedgeCollidersOff : CharacterFunction
    {
        public override void RunFunction()
        {
            control.DATASET.LEDGE_GRAB_DATA.collider1.gameObject.SetActive(false);
            control.DATASET.LEDGE_GRAB_DATA.collider2.gameObject.SetActive(false);
        }
    }
}