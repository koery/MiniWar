using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;

namespace MiniWar
{
    public delegate void Callback();
    public delegate void CallbackN(MGNode sender);
    class GameConfig
    {
        public static int HightY = 6;
        public static int WidthX = 9;
        public static int GirdSize = 94;
        public static Vector2 RelativeOrigin = new Vector2(87, 52);
        public static int MAX_X_COUNT = 8;
        public static int MAX_T_COUNT = 15;
        public static int MAX_L_COUNT = 18;
        public static int MAX_I_COUNT = 20;
        public static int MAX_i_COUNT = 5;
        public static int CardCount = 6;
        private static int _money = 5500;
        public static int Money { get { return _money; } private set { _money = value; } }
        public static Callback ChangeMoneyCallBack;
        public static bool ChangeMoney(int value)
        {
            _money += value;
            if (_money < 0)
            {
                _money = 0;
                return false;
            }
            if (_money > 9999)
            {
                _money = 9999;
                return false;
            }
            if (ChangeMoneyCallBack != null)
            {
                ChangeMoneyCallBack.Invoke();
            }
            return true;
        }
    }
}
