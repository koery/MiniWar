using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;
using VSZombie.Toobz;

namespace MiniWar.Toobz
{
    class Wire
    {
        public bool BYAdd { get; set; }
        public bool BXAdd { get; set; }
        public bool BYSub { get; set; }
        public bool BXSub { get; set; }

        private WireDirection _wireDir;
        private WireType _wireState;
        public List<int> WireData;
        public MGSprite Sprite { get; set; }
        public IntVector Vector { get; set; }
        public int Tag { get; set; }
        public bool IsChecked { get; set; }
        public WireType WireState
        {
            get { return _wireState; }
            private set
            {
                _wireState = value;
                InWireData();
            }
        }

        public WireDirection WireDir
        {
            get { return _wireDir; }
            private set
            {
                _wireDir = value;
                InWireData();
            }
        }

        public Wire(IntVector vector)
        {
            WireData = new List<int>();
            WireState = GetRandomType();
            WireDir = GetRandomDir();
            Vector = vector;
            UpdataImage();
            WireShowLayer.SharedWireShow().NewWireList.Add(this);
        }

        public Wire(IntVector vector, WireType wireType, WireDirection wireDir)
        {
            WireData = new List<int>();
            WireState = wireType;
            WireDir = wireDir;
            Vector = vector;
            UpdataImage();
            WireShowLayer.SharedWireShow().NewWireList.Add(this);
        }

        public void UpdataImage()
        {
            WireDate.WireSprite data = WireDate.GetWireSprite(_wireState, _wireDir);
            if (Sprite == null)
            {
                Sprite = MGSprite.MGSpriteWithSpriteFrameName(data.Name);
            }
            Sprite.Rotation = data.Rotate;
            Sprite.Position = Vector.ToUIVector2() + new Vector2(GameConfig.GirdSize, GameConfig.GirdSize) * .5f;
        }

        private void InWireData()
        {
            WireData.Clear();
            switch (_wireDir)
            {
                case WireDirection.WireUp:
                    switch (WireState)
                    {
                        #region Up

                        case WireType.Wirei:
                            WireData.Add((int)WireDirection.WireUp);
                            WireData.Add(0);
                            WireData.Add(0);
                            WireData.Add(0);
                            break;
                        case WireType.WireI:
                            WireData.Add((int)WireDirection.WireUp);
                            WireData.Add(0);
                            WireData.Add((int)WireDirection.WireDown);
                            WireData.Add(0);
                            break;
                        case WireType.WireL:
                            WireData.Add((int)WireDirection.WireUp);
                            WireData.Add((int)WireDirection.WireLeft);
                            WireData.Add(0);
                            WireData.Add(0);
                            break;
                        case WireType.WireT:
                            WireData.Add((int)WireDirection.WireUp);
                            WireData.Add((int)WireDirection.WireLeft);
                            WireData.Add(0);
                            WireData.Add((int)WireDirection.WireRight);
                            break;
                        case WireType.WireX:
                            WireData.Add((int)WireDirection.WireUp);
                            WireData.Add((int)WireDirection.WireLeft);
                            WireData.Add((int)WireDirection.WireDown);
                            WireData.Add((int)WireDirection.WireRight);
                            break;

                        #endregion
                    }
                    break;
                case WireDirection.WireRight:
                    switch (WireState)
                    {
                        #region Right

                        case WireType.Wirei:
                            WireData.Add(0);
                            WireData.Add(0);
                            WireData.Add(0);
                            WireData.Add((int)WireDirection.WireRight);
                            break;
                        case WireType.WireI:
                            WireData.Add(0);
                            WireData.Add((int)WireDirection.WireLeft);
                            WireData.Add(0);
                            WireData.Add((int)WireDirection.WireRight);
                            break;
                        case WireType.WireL:
                            WireData.Add((int)WireDirection.WireUp);
                            WireData.Add(0);
                            WireData.Add(0);
                            WireData.Add((int)WireDirection.WireRight);
                            break;
                        case WireType.WireT:
                            WireData.Add((int)WireDirection.WireUp);
                            WireData.Add(0);
                            WireData.Add((int)WireDirection.WireDown);
                            WireData.Add((int)WireDirection.WireRight);
                            break;
                        case WireType.WireX:
                            WireData.Add((int)WireDirection.WireUp);
                            WireData.Add((int)WireDirection.WireLeft);
                            WireData.Add((int)WireDirection.WireDown);
                            WireData.Add((int)WireDirection.WireRight);
                            break;

                        #endregion
                    }
                    break;
                case WireDirection.WireDown:
                    switch (WireState)
                    {
                        #region Down

                        case WireType.Wirei:
                            WireData.Add(0);
                            WireData.Add(0);
                            WireData.Add((int)WireDirection.WireDown);
                            WireData.Add(0);
                            break;
                        case WireType.WireI:
                            WireData.Add((int)WireDirection.WireUp);
                            WireData.Add(0);
                            WireData.Add((int)WireDirection.WireDown);
                            WireData.Add(0);
                            break;
                        case WireType.WireL:
                            WireData.Add(0);
                            WireData.Add(0);
                            WireData.Add((int)WireDirection.WireDown);
                            WireData.Add((int)WireDirection.WireRight);
                            break;
                        case WireType.WireT:
                            WireData.Add(0);
                            WireData.Add((int)WireDirection.WireLeft);
                            WireData.Add((int)WireDirection.WireDown);
                            WireData.Add((int)WireDirection.WireRight);
                            break;
                        case WireType.WireX:
                            WireData.Add((int)WireDirection.WireUp);
                            WireData.Add((int)WireDirection.WireLeft);
                            WireData.Add((int)WireDirection.WireDown);
                            WireData.Add((int)WireDirection.WireRight);
                            break;

                        #endregion
                    }
                    break;
                case WireDirection.WireLeft:
                    switch (WireState)
                    {
                        #region Left

                        case WireType.Wirei:
                            WireData.Add(0);
                            WireData.Add((int)WireDirection.WireLeft);
                            WireData.Add(0);
                            WireData.Add(0);
                            break;
                        case WireType.WireI:
                            WireData.Add(0);
                            WireData.Add((int)WireDirection.WireLeft);
                            WireData.Add(0);
                            WireData.Add((int)WireDirection.WireRight);
                            break;
                        case WireType.WireL:
                            WireData.Add(0);
                            WireData.Add((int)WireDirection.WireLeft);
                            WireData.Add((int)WireDirection.WireDown);
                            WireData.Add(0);
                            break;
                        case WireType.WireT:
                            WireData.Add((int)WireDirection.WireUp);
                            WireData.Add((int)WireDirection.WireLeft);
                            WireData.Add((int)WireDirection.WireDown);
                            WireData.Add(0);
                            break;
                        case WireType.WireX:
                            WireData.Add((int)WireDirection.WireUp);
                            WireData.Add((int)WireDirection.WireLeft);
                            WireData.Add((int)WireDirection.WireDown);
                            WireData.Add((int)WireDirection.WireRight);
                            break;

                        #endregion
                    }
                    break;
            }
        }

        public void SetVector(IntVector v)
        {
            Vector = v;
            UpdataImage();
            if (WireShowLayer.SharedWireShow().WireDictionary.ContainsKey(Vector))
            {
                WireShowLayer.SharedWireShow().WireDictionary[Vector] = this;
            }
            WireShowLayer.SharedWireShow().WireIsChange();
        }

        public void Drop(int x)
        {
            int y = -1;
            for (int i = 0; i < GameConfig.HightY; i++)
            {
                if (WireShowLayer.SharedWireShow().Chessboard[x, i] == 0)
                {
                    y = i;
                    WireShowLayer.SharedWireShow().Chessboard[x, y] = 1;
                    Vector = new IntVector(x, y);
                    WireShowLayer.SharedWireShow().InData(this);
                    break;
                }
            }

            if (y == -1)
            {
                throw new Exception("Drop  y = -1");
            }

            MGMoveTo move = MGMoveTo.ActionWithDuration(.7f, (new IntVector(x, y).ToUIVector2() + new Vector2(.5f, .5f) * 94));
            Sprite.RunAction(MGSequence.Actions(move, MGCallFunc.ActionWithTarget(() =>
            {
                //WireShowLayer.SharedWireShow().InData(this);
                //??
                WireShowLayer.SharedWireShow().WireIsChange();
            })));
        }

        public void InfromTop()
        {
            int x = Vector.X;
            int y = Vector.Y;
            for (int i = y; i < GameConfig.HightY; i++)
            {
                if (!WireShowLayer.SharedWireShow().WireDictionary.ContainsKey(new IntVector(x, i)))
                {
                    continue;
                }
                if (WireShowLayer.SharedWireShow().WireDictionary[new IntVector(x, i)] != null)
                {
                    var box = WireShowLayer.SharedWireShow().WireDictionary[new IntVector(x, i)];
                    {
                        WireShowLayer.SharedWireShow().OutData(box);
                        box.Drop(x);
                    }
                }
            }
        }

        public virtual void Hide()
        {
            WireShowLayer.SharedWireShow().NewWireList.Remove(this);
            WireShowLayer.SharedWireShow().OutData(this);
            WireShowLayer.SharedWireShow().RemoveChild(this.Sprite);
            InfromTop();
            var w = new Wire(new IntVector(this.Vector.X, -1));
            w.Drop(this.Vector.X);
        }

        public List<int> ChangeWireDirection()
        {
            //顺时针
            switch (WireDir)
            {
                case WireDirection.WireUp:
                    WireDir = WireDirection.WireRight;
                    break;
                case WireDirection.WireRight:
                    WireDir = WireDirection.WireDown;
                    break;
                case WireDirection.WireDown:
                    WireDir = WireDirection.WireLeft;
                    break;
                case WireDirection.WireLeft:
                    WireDir = WireDirection.WireUp;
                    break;
            }
            UpdataImage();
            WireShowLayer.SharedWireShow().WireIsChange();
            return WireData;
        }


        public List<int> ChangeWireState(WireType type)
        {
            WireState = type;
            WireShowLayer.SharedWireShow().RemoveChild(Sprite);
            Sprite = null;
            UpdataImage();
            WireShowLayer.SharedWireShow().AddChild(Sprite);
            WireShowLayer.SharedWireShow().WireIsChange();
            return WireData;
        }

        public void FundPrince()
        {
            WireShowLayer.SharedWireShow().Together.Clear();
            if (!IsChecked)
            {
                FindPrince();
            }
            foreach (var wire in WireShowLayer.SharedWireShow().Together)
            {
                wire.IsChecked = false;
            }
        }

        public void FindPrince()
        {
            XAdd();
            XSub();
            YAdd();
            YSub();
            IsChecked = false;
        }

        private bool XAdd()
        {
            if (Vector.X < GameConfig.WidthX - 1)
            {
                var v = Vector + new IntVector(1, 0);
                if (WireShowLayer.SharedWireShow().WireDictionary.ContainsKey(v))
                {
                    var wire = WireShowLayer.SharedWireShow().WireDictionary[v];
                    return IsPrince(wire);
                }
            }
            return false;
        }

        private bool XSub()
        {
            if (Vector.X > 0)
            {
                var v = Vector - new IntVector(1, 0);
                if (WireShowLayer.SharedWireShow().WireDictionary.ContainsKey(v))
                {
                    var wire = WireShowLayer.SharedWireShow().WireDictionary[v];
                    return IsPrince(wire); ;
                }
            }
            return false;
        }

        private bool YAdd()
        {
            if (Vector.Y < GameConfig.HightY - 1)
            {
                var v = Vector + new IntVector(0, 1);
                if (WireShowLayer.SharedWireShow().WireDictionary.ContainsKey(v))
                {
                    var wire = WireShowLayer.SharedWireShow().WireDictionary[v];
                    return IsPrince(wire); ;
                }
            }
            return false;
        }

        private bool YSub()
        {
            if (Vector.Y > 0)
            {
                var v = Vector - new IntVector(0, 1);
                if (WireShowLayer.SharedWireShow().WireDictionary.ContainsKey(v))
                {
                    var wire = WireShowLayer.SharedWireShow().WireDictionary[v];
                    return IsPrince(wire); ;
                }
            }
            return false;
        }

        private bool IsPrince(Wire wire)
        {
            if (wire.Vector.X < Vector.X)
            {
                if (wire.WireData[3] + WireData[1] > WireData[1] && wire.WireData[3] + WireData[1] > wire.WireData[3])
                {
                    {
                        if (!WireShowLayer.SharedWireShow().Together.Contains(this))
                            WireShowLayer.SharedWireShow().Together.Add(this);
                        IsChecked = true;
                        if (!wire.IsChecked)
                        {
                            wire.FindPrince();
                        }
                    }
                    return true;
                }
            }
            if (wire.Vector.X > Vector.X)
            {
                if (wire.WireData[1] + WireData[3] > WireData[3] && wire.WireData[1] + WireData[3] > wire.WireData[1])
                {
                    if (!WireShowLayer.SharedWireShow().Together.Contains(this))
                        WireShowLayer.SharedWireShow().Together.Add(this);
                    IsChecked = true;
                    if (!wire.IsChecked)
                    {
                        wire.FindPrince();
                    }
                    return true;
                }
            }
            if (wire.Vector.Y > Vector.Y)
            {
                if (wire.WireData[2] + WireData[0] > WireData[0] && wire.WireData[2] + WireData[0] > wire.WireData[2])
                {
                    if (!WireShowLayer.SharedWireShow().Together.Contains(this))
                        WireShowLayer.SharedWireShow().Together.Add(this);
                    IsChecked = true;
                    if (!wire.IsChecked)
                    {
                        wire.FindPrince();
                    }
                    return true;
                }
            }
            if (wire.Vector.Y < Vector.Y)
            {
                if (wire.WireData[0] + WireData[2] > wire.WireData[0] && wire.WireData[0] + WireData[2] > WireData[2])
                {
                    if (!WireShowLayer.SharedWireShow().Together.Contains(this))
                        WireShowLayer.SharedWireShow().Together.Add(this);
                    IsChecked = true;
                    if (!wire.IsChecked)
                    {
                        wire.FindPrince();
                    }
                    return true;
                }
            }
            return false;
        }

        public static WireDirection GetRandomDir()
        {
            return (WireDirection)Control.SharedControl().GetRandom(1, 4);
        }

        public static WireType GetRandomType()
        {
            WireType type = (WireType)Control.SharedControl().GetRandom(5);
            if (WireType.Wirei == type)
            {
                int i = 0;
                foreach (var wire in WireShowLayer.SharedWireShow().NewWireList)
                {
                    if (wire.WireState == WireType.Wirei)
                    {
                        i++;
                    }
                }
                if (i > GameConfig.MAX_i_COUNT)
                {
                    type = GetRandomType();
                }
            }
            if (WireType.WireI == type)
            {
                int i = 0;
                foreach (var wire in WireShowLayer.SharedWireShow().NewWireList)
                {
                    if (wire.WireState == WireType.WireI)
                    {
                        i++;
                    }
                }
                if (i > GameConfig.MAX_I_COUNT)
                {
                    type = GetRandomType();
                }
            }
            if (WireType.WireL == type)
            {
                int i = 0;
                foreach (var wire in WireShowLayer.SharedWireShow().NewWireList)
                {
                    if (wire.WireState == WireType.WireL)
                    {
                        i++;
                    }
                }
                if (i > GameConfig.MAX_L_COUNT)
                {
                    type = GetRandomType();
                }
            }

            if (WireType.WireT == type)
            {
                int i = 0;
                foreach (var wire in WireShowLayer.SharedWireShow().NewWireList)
                {
                    if (wire.WireState == WireType.WireT)
                    {
                        i++;
                    }
                }
                if (i > GameConfig.MAX_T_COUNT)
                {
                    type = GetRandomType();
                }
            }

            if (WireType.WireX == type)
            {
                int i = 0;
                foreach (var wire in WireShowLayer.SharedWireShow().NewWireList)
                {
                    if (wire.WireState == WireType.WireX)
                    {
                        i++;
                    }
                }
                if (i > GameConfig.MAX_X_COUNT)
                {
                    type = GetRandomType();
                }
            }
            return type;
        }
    }

    public class WireDate
    {
        public static WireSprite GetWireSprite(WireType type, WireDirection direction)
        {
            var value = new WireSprite();
            switch (type)
            {
                case WireType.Wirei:
                    value.Name = "brick_4.png";
                    switch (direction)
                    {
                        case WireDirection.WireLeft:
                            value.Rotate = 0;
                            break;
                        case WireDirection.WireUp:
                            value.Rotate = 90;
                            break;
                        case WireDirection.WireRight:
                            value.Rotate = 180;
                            break;
                        case WireDirection.WireDown:
                            value.Rotate = -90;
                            break;
                    }
                    break;
                case WireType.WireI:
                    value.Name = "brick_2.png";
                    switch (direction)
                    {
                        case WireDirection.WireLeft:
                            value.Rotate = 0;
                            break;
                        case WireDirection.WireUp:
                            value.Rotate = 90;
                            break;
                        case WireDirection.WireRight:
                            value.Rotate = 180;
                            break;
                        case WireDirection.WireDown:
                            value.Rotate = -90;
                            break;
                    }
                    break;
                case WireType.WireL:
                    value.Name = "brick_3.png";
                    switch (direction)
                    {
                        case WireDirection.WireLeft:
                            value.Rotate = 0;
                            break;
                        case WireDirection.WireUp:
                            value.Rotate = 90;
                            break;
                        case WireDirection.WireRight:
                            value.Rotate = 180;
                            break;
                        case WireDirection.WireDown:
                            value.Rotate = -90;
                            break;
                    }
                    break;
                case WireType.WireT:
                    value.Name = "brick_0.png";
                    switch (direction)
                    {
                        case WireDirection.WireLeft:
                            value.Rotate = 90;
                            break;
                        case WireDirection.WireUp:
                            value.Rotate = 180;
                            break;
                        case WireDirection.WireRight:
                            value.Rotate = -90;
                            break;
                        case WireDirection.WireDown:
                            value.Rotate = 0;
                            break;
                    }
                    break;
                case WireType.WireX:
                    value.Name = "brick_1.png";
                    switch (direction)
                    {
                        case WireDirection.WireLeft:
                            value.Rotate = 90;
                            break;
                        case WireDirection.WireUp:
                            value.Rotate = 180;
                            break;
                        case WireDirection.WireRight:
                            value.Rotate = -90;
                            break;
                        case WireDirection.WireDown:
                            value.Rotate = 0;
                            break;
                    }
                    break;
            }
            return value;
        }

        #region Nested type: WireSprite

        public struct WireSprite
        {
            public string Name;
            public int Rotate;
        }

        #endregion
    }

    public enum WireType
    {
        WireT,
        WireX,
        WireI,
        WireL,
        Wirei
    }

    public enum WireDirection
    {
        WireLeft = 1,
        WireUp = 2,
        WireDown = 3,
        WireRight = 4,
    }
}
