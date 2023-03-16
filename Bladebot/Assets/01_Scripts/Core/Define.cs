using UnityEngine;

namespace Core
{
    public enum StateType
    {
        Normal = 0,
        Attack = 1,
        // OnHit = 2,
        Rolling = 3,
    }

    public class Define
    {
        private static Camera _mainCam = null;
        public static Camera MainCam
        {
            get
            {
                if (_mainCam == null)
                {
                    _mainCam = Camera.main;
                }
                return _mainCam;
            }
        }
    }
}
