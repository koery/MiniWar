using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using VSZombie.Toobz;

namespace MiniWar.Toobz
{
    class RevolutionAnchor
    {
        public IntVector Vector { get; set; }
        public List<Wire> Wires { get; set; }
        public IntVector[] Points = new IntVector[4];
        public MGSprite Sprite { get; set; }
        public RevolutionAnchor(IntVector vector)
        {
            Wires = new List<Wire>();
            Sprite = MGSprite.MGSpriteWithFilename("point");
            WireShowLayer.SharedWireShow().AddChild(this);
            Vector = vector;
            Sprite.Position = Vector.ToUIVector2();
        }

        public void ClockwiseRotate()
        {
            var b = FoundFour();
            if (!b)
            {
                throw new Exception("4");
            }
            for (int i = 0; i < Wires.Count; i++)
            {
                if (Wires[i].Vector.X == Vector.X)
                {
                    Wires[i].Tag = Wires[i].Vector.X * 50 + Wires[i].Vector.Y * 2;
                }
                else
                {
                    Wires[i].Tag = Wires[i].Vector.X * 50 + Wires[i].Vector.Y * -2;
                }
            }

            BubbleSort(Wires);
            TrimFour();
        }

        private void TrimFour()
        {
            Points[0] = Wires[0].Vector;
            Points[1] = Wires[1].Vector;
            Points[2] = Wires[2].Vector;
            Points[3] = Wires[3].Vector;
            Wires[0].SetVector(Points[1]);
            Wires[1].SetVector(Points[2]);
            Wires[2].SetVector(Points[3]);
            Wires[3].SetVector(Points[0]);
        }

        private void BubbleSort(List<Wire> wires)
        {
            Wire temp;
            for (int i = 0; i < wires.Count - 1; i++)
            {
                for (int j = 0; j < wires.Count - 1 - i; j++)
                {
                    if (wires[j].Tag < wires[j + 1].Tag)
                    {
                        temp = wires[j];
                        wires[j] = wires[j + 1];
                        wires[j + 1] = temp;
                    }
                }
            }
        }

        private bool FoundFour()
        {
            Wires.Clear();
            var wires = WireShowLayer.SharedWireShow().Wires;
            for (int i = 0; i < wires.Count; i++)
            {
                var wire = wires[i];
                if (FindFour(wire))
                {
                    Wires.Add(wire);
                }
                if (Wires.Count == 4)
                {
                    return true;
                }
            }
            return false;
        }

        private bool FindFour(Wire wire)
        {
            if (XAdd(wire))
            {
                return true;
            }
            if (YAdd(wire))
            {
                return true;
            }
            if (XYAdd(wire))
            {
                return true;
            }
            if (XY(wire))
            {
                return true;
            }
            return false;
        }

        private bool XY(Wire wire)
        {
            if (Vector == wire.Vector)
            {
                return true;
            }
            return false;
        }

        private bool XAdd(Wire wire)
        {
            if (Vector == (wire.Vector + new IntVector(1, 0)))
            {
                return true;
            }
            return false;
        }

        private bool YAdd(Wire wire)
        {
            if (Vector == wire.Vector + new IntVector(0, 1))
            {
                return true;
            }
            return false;
        }

        private bool XYAdd(Wire wire)
        {
            if (Vector == wire.Vector + new IntVector(1, 1))
            {
                return true;
            }
            return false;
        }
    }
}
