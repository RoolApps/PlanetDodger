using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MenuOrientationController : MonoBehaviour {

    public Canvas PortraitCanvas;
    public Canvas LandscapeCanvas;

    ScreenOrientation Current = ScreenOrientation.Unknown;
    ScreenOrientation[] Landscape = new ScreenOrientation [] { ScreenOrientation.Landscape, ScreenOrientation.LandscapeLeft, ScreenOrientation.LandscapeRight };
    ScreenOrientation[] Portrait = new ScreenOrientation[] { ScreenOrientation.Portrait, ScreenOrientation.PortraitUpsideDown };

    private void Update()
    {
        var orientation = Screen.orientation;
        if(Current != orientation)
        {
            if(Landscape.Contains(orientation))
            {
                LandscapeCanvas.gameObject.SetActive(true);
                PortraitCanvas.gameObject.SetActive(false);
            }
            else if (Portrait.Contains(orientation))
            {
                PortraitCanvas.gameObject.SetActive(true);
                LandscapeCanvas.gameObject.SetActive(false);
            }
            Current = orientation;
        }
    }

}
