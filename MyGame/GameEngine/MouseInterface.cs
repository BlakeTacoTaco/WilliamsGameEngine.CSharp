using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine
{
    internal interface MouseInterface
    {
        public abstract void Hover();

        //left mouse
        public abstract void ReleaseLeft();
        public abstract void PressLeft();
        public abstract void HoldLeft();

        //right mouse
        public abstract void ReleaseRight();
        public abstract void PressRight();
        public abstract void HoldRight();
    }
}
