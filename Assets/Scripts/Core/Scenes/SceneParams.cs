using System;
using UnityEngine;

namespace Core.Scenes
{
    [Serializable]
    public struct SceneParams
    {
        public string Key;
        public ScreenOrientationParams OrientationParams;
    }

    [Serializable]
    public struct ScreenOrientationParams
    {
        public ScreenOrientation Orientation;
        public bool AutoRotateToPortrait;
        public bool AutoRotateToPortraitUpsideDown;
        public bool AutoRotateToLandscapeLeft;
        public bool AutoRotateToLandscapeRight;
    }
}
