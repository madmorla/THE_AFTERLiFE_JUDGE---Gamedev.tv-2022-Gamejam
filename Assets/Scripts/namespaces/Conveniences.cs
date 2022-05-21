using UnityEngine;

namespace Conveniences
{
    public class Mouse
    { 
        public static void ToggleCursor(bool state)
        {
            if (state)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }


}
