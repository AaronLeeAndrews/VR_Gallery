﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollButton : MonoBehaviour {

    public float scrollAddValue;
    public Scrollbar scrollbar;

    public void AddValueToScrollbar()
    {
        scrollbar.value += scrollAddValue;
    }
}
