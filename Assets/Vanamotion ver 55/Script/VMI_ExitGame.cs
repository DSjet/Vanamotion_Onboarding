using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanamotion
{
    public class VMI_ExitGame : MonoBehaviour
    {
        bool isPoseValidRight = false;
        bool isPoseValidLeft = false;
    
        public void SetPoseValidRight(bool valid)
        {
            isPoseValidRight = valid;
        }
    
        public void SetPoseValidLeft(bool valid)
        {
            isPoseValidLeft = valid;
        }
    
        public void Exit()
        {
            if (isPoseValidRight && isPoseValidLeft)
            {
                Application.Quit();
            }
        }
    
        public void normalExit()
        {
            Application.Quit();
        }
    }
}

