using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGFramework
{
    public interface IMGButtonDelegate
    {
        void ButtonTouchBegan(MGButton b);
        void ButtonMoveOut(MGButton b);
        void ButtonClickedBegan(MGButton b);
        void ButtonClicked(MGButton b);
    }
}
