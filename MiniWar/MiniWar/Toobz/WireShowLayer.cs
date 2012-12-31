using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;
using VSZombie.Effect;
using VSZombie.Toobz;

namespace MiniWar.Toobz
{

    class WireShowLayer : MGLayer
    {
        private static WireShowLayer _sharedWireShow;
        public static WireShowLayer SharedWireShow()
        {
            if (_sharedWireShow == null)
            {
                _sharedWireShow = new WireShowLayer();
            }
            return _sharedWireShow;
        }

        public int[,] Chessboard;
        public List<Wire> Wires { get; set; }
        public Dictionary<IntVector, Wire> WireDictionary { get; set; }
        public List<Wire> NewWireList { get; set; }
        public List<RevolutionAnchor> RevolutionAnchors { get; set; }
        public List<Wire> Together { get; set; }
        public List<Wire> Clears { get; set; }
        private List<Lint> _rightLints;
        private List<Lint> _leftLints;

        private WireShowLayer()
        {
            //IsTouchEnable = true;
            Chessboard = new int[GameConfig.WidthX, GameConfig.HightY];
            Wires = new List<Wire>();
            WireDictionary = new Dictionary<IntVector, Wire>();
            NewWireList = new List<Wire>();
            RevolutionAnchors = new List<RevolutionAnchor>();
            Together = new List<Wire>();
            Clears = new List<Wire>();
            _rightLints = new List<Lint>();
            _leftLints = new List<Lint>();
            Thunders = new List<ThunderSprite>();
            for (int i = 0; i < 40; i++)
            {
                var sp = new ThunderSprite();
                AddChild(sp, 9);
                Thunders.Add(sp);
                sp.Visible = false;
            }
        }

        public void Init()
        {
            for (int i = 0; i < GameConfig.HightY; i++)
            {
                for (int j = 0; j < GameConfig.WidthX; j++)
                {
                    var wire = new Wire(new IntVector(j, -1));
                    wire.Drop(j);
                }
            }

            for (int i = 0; i < GameConfig.HightY; i++)
            {
                var lint = new Lint(new IntVector(GameConfig.WidthX, i));
                _rightLints.Add(lint);
            }

            for (int i = 0; i < GameConfig.HightY; i++)
            {
                var lint = new Lint(new IntVector(-1, i));
                _leftLints.Add(lint);
            }


            for (int i = 1; i < GameConfig.WidthX; i++)
            {
                for (int j = 1; j < GameConfig.HightY; j++)
                {
                    new RevolutionAnchor(new IntVector(i, j));
                }
            }

            RunAction(MGSequence.Actions(MGDelay.ActionWithDuration(1), MGCallFunc.ActionWithTarget(WireIsChange)));
        }

        public void Init(List<Wire> wires)
        {
            for (int i = wires.Count - 1; i >= 0; i--)
            {
                Wire wire = wires[i];
                wire.Drop(wire.Vector.X);
            }


            for (int i = 0; i < GameConfig.HightY; i++)
            {
                var lint = new Lint(new IntVector(GameConfig.WidthX, i));
                _rightLints.Add(lint);
            }

            for (int i = 0; i < GameConfig.HightY; i++)
            {
                var lint = new Lint(new IntVector(-1, i));
                _leftLints.Add(lint);
            }
            for (int i = 1; i < GameConfig.WidthX; i++)
            {
                for (int j = 1; j < GameConfig.HightY; j++)
                {
                    new RevolutionAnchor(new IntVector(i, j));
                }
            }
            RunAction(MGSequence.Actions(MGDelay.ActionWithDuration(1), MGCallFunc.ActionWithTarget(WireIsChange)));
        }

        public void WireIsChange()
        {
            OldList.Clear();
            StopAllAction();
            for (int j = 0; j < Wires.Count; j++)
            {
                Wires[j].Sprite.SetColor(255, 255, 255);
            }

            for (int i = 0; i < _rightLints.Count; i++)
            {
                _rightLints[i].FindLeft();
            }

            for (int i = 0; i < _leftLints.Count; i++)
            {
                _leftLints[i].FindRight();
            }
        }

        public List<Wire> OldList = new List<Wire>();
        public void ClearWire()
        {
            bool b = true;
            if (OldList.Count == Clears.Count)
            {
                for (int i = 0; i < OldList.Count; i++)
                {
                    if (OldList[i] != Clears[i])
                    {
                        b = false;
                        break;
                    }
                }
            }
            else
            {
                b = false;
            }
            if (!b)
            {
                OldList.Clear();
                OldList.AddRange(Clears);
                this.StopAllAction();
                _thunderindex = 0;
                {
                    Guid = Guid.NewGuid();
                    //for (int i = 0; i < Clears.Count; i++)
                    //{
                    //    Clears[i].Sprite.SetColor(100, 100, 100);
                    //}
                    RunAction(MGSequence.Actions(MGDelay.ActionWithDuration(1.04f), MGCallFunc.ActionWithTarget(() =>
                    {
                        for (int i = 0; i < Clears.Count; i++)
                        {
                            if (TouchLayer.Touch != null)
                            {
                                TouchLayer.Touch.IsTouchEnable = false;
                            }

                            //if (Clears[i].Vector.X == 0)
                            //{
                            //    if (Clears[i].WireData[1] != 0)
                            //    {
                            //        //zhao dao kai shi dian 
                            //        if (Clears[i].WireData[1] != 0)
                            {

                                Clears[i].Sprite.SetColor(66, 235, 246);
                                Clears[i].Sprite.Opacity = 155;
                                FindPath(Clears[i]);
                                //Clears[i].Sprite.RunAction(MGBlink.ActionWithDuration(.5f, 3));
                            }
                            //    }
                            //}
                        }
                        GameConfig.ChangeMoney(4);
                        if (ThunderCallBackStart != null)
                        {
                            ThunderCallBackStart(this);
                        }

                        //foreach (var w in Clears)
                        //{
                        //    w.Hide();
                        //}
                    }), MGDelay.ActionWithDuration(.8f), MGCallFunc.ActionWithTarget(() =>
                    {
                        foreach (var w in Clears)
                        {
                            w.Hide();
                        }
                        foreach (var thunder in Thunders)
                        {
                            thunder.Hide();
                            thunder.Visible = false;
                        }
                        if (TouchLayer.Touch != null)
                        {
                            TouchLayer.Touch.IsTouchEnable = true;
                        }
     
                        if (ThunderCallBackEnd != null)
                        {
                            ThunderCallBackEnd(this);
                        }
                    })));
                }
            }
        }

        private void FindPath(Wire wire)
        {
            if (wire.WireData[0] != 0)
            {
                YUp.Clear();
                if (!wire.BYAdd)
                {
                    YAdd(wire);
                }
            }
            if (wire.WireData[1] != 0)
            {
                XLeft.Clear();
                if (!wire.BXSub)
                {
                    XSub(wire);
                }
            }
            if (wire.WireData[2] != 0)
            {
                YDown.Clear();
                if (!wire.BYSub)
                {
                    YSub(wire);
                }
            }
            if (wire.WireData[3] != 0)
            {
                XRight.Clear();
                if (!wire.BXAdd)
                {
                    XAdd(wire);
                }
            }
        }
        List<Wire> YUp = new List<Wire>();
        List<Wire> YDown = new List<Wire>();
        List<Wire> XLeft = new List<Wire>();
        List<Wire> XRight = new List<Wire>();
        private List<ThunderSprite> Thunders;
        public ThunderCallBack ThunderCallBackStart;
        public ThunderCallBack ThunderCallBackEnd;
        public Guid Guid { get; set; }
        private int _thunderindex = 0;
        private void YAdd(Wire wire)
        {
            wire.BYAdd = true;
            if (wire.Vector.Y < GameConfig.HightY - 1)
            {
                var v = wire.Vector + new IntVector(0, 1);
                var w = WireDictionary[v];
                if (Clears.Contains(w) && w.WireData[2] != 0)
                {
                    YUp.Add(wire);
                    if (w.WireData[0] != 0)
                    {
                        if (!w.BYAdd)
                        {
                            YAdd(w);
                        }
                    }
                }
            }
            if (YUp.Count >= 1)
            {
                ThunderSprite sp;
                if (Thunders.Count - 1 > _thunderindex)
                {
                    sp = Thunders[_thunderindex];
                }
                else
                {
                    sp = new ThunderSprite();
                    AddChild(sp, 9);
                    Thunders.Add(sp);
                }
                _thunderindex++;
                sp.Points.Clear();
                sp.AddPoint(new Vector2(wire.Vector.X * 94 + 94 / 2 + GameConfig.RelativeOrigin.X, 768 - (wire.Vector.Y * 94 + 47 + GameConfig.RelativeOrigin.Y)));
                sp.EndPoint(new Vector2(wire.Vector.X * 94 + 94 / 2 + GameConfig.RelativeOrigin.X, 768 - (wire.Vector.Y * 94 + 47 + YUp.Count * 94 / 2 + GameConfig.RelativeOrigin.Y)));
                sp.Visible = true;
            }
        }

        private void XSub(Wire wire)
        {
            wire.BXSub = true;
            if (wire.Vector.X > 0)
            {
                var v = wire.Vector - new IntVector(1, 0);
                var w = WireDictionary[v];
                if (Clears.Contains(w) && w.WireData[3] != 0)
                {
                    XLeft.Add(wire);
                    if (w.WireData[1] != 0)
                    {
                        if (!w.BXSub)
                        {
                            XSub(w);
                        }
                    }
                }
            }
            if (XLeft.Count >= 1)
            {
                ThunderSprite sp;
                if (Thunders.Count - 1 > _thunderindex)
                {
                    sp = Thunders[_thunderindex];
                }
                else
                {
                    sp = new ThunderSprite();
                    AddChild(sp, 9);
                    Thunders.Add(sp);
                }
                _thunderindex++;
                sp.Points.Clear();
                sp.AddPoint(new Vector2(wire.Vector.X * 94 + 94 / 2 + GameConfig.RelativeOrigin.X, 768 - (wire.Vector.Y * 94 + 47 + GameConfig.RelativeOrigin.Y)));
                sp.EndPoint(new Vector2(wire.Vector.X * 94 + 94 / 2 + GameConfig.RelativeOrigin.X - XLeft.Count * 94 / 2, 768 - (wire.Vector.Y * 94 + 47 + GameConfig.RelativeOrigin.Y)));
                sp.Visible = true;
            }
        }

        private void YSub(Wire wire)
        {
            wire.BYSub = true;
            if (wire.Vector.Y > 0)
            {
                var v = wire.Vector - new IntVector(0, 1);
                var w = WireDictionary[v];
                if (Clears.Contains(w) && w.WireData[0] != 0)
                {
                    YDown.Add(wire);
                    if (w.WireData[2] != 0)
                    {
                        if (!w.BYSub)
                        {
                            YSub(w);
                        }
                    }
                }
            }

            if (YDown.Count >= 1)
            {
                ThunderSprite sp;
                if (Thunders.Count - 1 > _thunderindex)
                {
                    sp = Thunders[_thunderindex];
                }
                else
                {
                    sp = new ThunderSprite();
                    AddChild(sp, 9);
                    Thunders.Add(sp);
                }
                _thunderindex++;
                sp.Points.Clear();
                sp.AddPoint(new Vector2(wire.Vector.X * 94 + 94 / 2 + GameConfig.RelativeOrigin.X, 768 - (wire.Vector.Y * 94 + 47 + GameConfig.RelativeOrigin.Y)));
                sp.EndPoint(new Vector2(wire.Vector.X * 94 + 94 / 2 + GameConfig.RelativeOrigin.X, 768 - (wire.Vector.Y * 94 + 47 - YDown.Count * 94 / 2 + GameConfig.RelativeOrigin.Y)));
                sp.Visible = true;
            }
        }

        private void XAdd(Wire wire)
        {
            wire.BXAdd = true;
            if (wire.Vector.X < GameConfig.WidthX - 1)
            {
                var v = wire.Vector + new IntVector(1, 0);
                var w = WireDictionary[v];
                if (Clears.Contains(w) && w.WireData[1] != 0)
                {
                    XRight.Add(wire);
                    if (w.WireData[3] != 0)
                    {
                        if (!w.BXAdd)
                        {
                            XAdd(w);
                        }
                    }
                }
            }
            if (XRight.Count >= 1)
            {
                ThunderSprite sp;
                if (Thunders.Count - 1 > _thunderindex)
                {
                    sp = Thunders[_thunderindex];
                }
                else
                {
                    sp = new ThunderSprite();
                    AddChild(sp, 9);
                    Thunders.Add(sp);
                }
                _thunderindex++;
                sp.Points.Clear();
                sp.AddPoint(new Vector2(wire.Vector.X * 94 + 94 / 2 + GameConfig.RelativeOrigin.X, 768 - (wire.Vector.Y * 94 + 47 + GameConfig.RelativeOrigin.Y)));
                sp.EndPoint(new Vector2(wire.Vector.X * 94 + 94 / 2 + GameConfig.RelativeOrigin.X + XRight.Count * 94 / 2, 768 - (wire.Vector.Y * 94 + 47 + GameConfig.RelativeOrigin.Y)));
                sp.Visible = true;
            }
        }

        public void InData(Wire wire)
        {
            Chessboard[wire.Vector.X, wire.Vector.Y] = 1;
            if (!WireDictionary.ContainsKey(wire.Vector))
            {
                WireDictionary.Add(wire.Vector, wire);
                if (!ChildList.Contains(wire.Sprite))
                    AddChild(wire.Sprite);
                Wires.Add(wire);
            }
            else
            {
                throw new Exception("InData -_-");
            }
        }

        public void OutData(Wire wire)
        {
            Chessboard[wire.Vector.X, wire.Vector.Y] = 0;
            WireDictionary.Remove(wire.Vector);
            Wires.Remove(wire);
        }

        public override bool TouchesBegan(Microsoft.Xna.Framework.Input.MouseState touch,Point point)
        {
            for (int i = 0; i < Wires.Count; i++)
            {
                if (Wires[i].Sprite.InTapInside(point))
                {
                    Wires[i].Hide();

                    return false;
                }
            }

            return base.TouchesBegan(touch, point);
        }

        public void AddChild(RevolutionAnchor revolution)
        {
            if (!RevolutionAnchors.Contains(revolution))
            {
                AddChild(revolution.Sprite);
                RevolutionAnchors.Add(revolution);
            }
        }
    }

    public delegate void ThunderCallBack(object sender);

}
