using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPalette : MonoBehaviour {

    public List<Color> _colors = new List<Color>();

    public static ColourPalette Instance;

    private void Awake()
    {
        Instance = this;
    }
}
