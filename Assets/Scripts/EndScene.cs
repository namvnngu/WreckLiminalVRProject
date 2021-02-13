using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.Core;
using Liminal.Core.Fader;

public class EndScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var fader = ScreenFader.Instance;
        fader.FadeTo(Color.black, 10f);
        ExperienceApp.End();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
