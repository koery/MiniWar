using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSZombie.Toobz;

namespace MiniWar.Toobz
{
    class Lint
    {
        public IntVector Vector { get; private set; }
        public Lint(IntVector vector)
        {
            Vector = vector;
        }

        public void FindRight()
        {
            WireShowLayer.SharedWireShow().Together.Clear();
            XAdd();

            bool isClear = false;
            var w = WireShowLayer.SharedWireShow().Together;
            foreach (var wire in w)
            {

                if (wire.Vector.X == GameConfig.WidthX - 1)
                {
                    if (wire.WireData[3] != 0)
                    {
                        foreach (var wr in w)
                        {
                            if (wr.Vector.X == 0 && wr.WireData[1] != 0)
                            {
                                foreach (var wire1 in w)
                                {
                                    wire1.Sprite.SetColor(148, 148, 148);
                                    wire1.Sprite.RunAction(MGFramework.MGSequence.Actions(MGFramework.MGDelay.ActionWithDuration(.5f), MGFramework.MGBlink.ActionWithDuration(1f, 4)));
                                }
                                isClear = true;
                                WireShowLayer.SharedWireShow().Clears.Clear();
                                WireShowLayer.SharedWireShow().Clears.AddRange(w);
                                WireShowLayer.SharedWireShow().ClearWire();
                                break;
                            }
                        }
                        isClear = true;
                        //WireShowLayer.SharedWireShow().Clears.Clear();
                        //WireShowLayer.SharedWireShow().Clears.AddRange(w);
                        //WireShowLayer.SharedWireShow().ClearWire();
                        break;
                    }
                }
            }
        }

        public void FindLeft()
        {
            WireShowLayer.SharedWireShow().Together.Clear();
            XSub();
            bool isClear = false;
            var w = WireShowLayer.SharedWireShow().Together;
            foreach (var wire in w)
            {
                if (wire.Vector.X == 0)
                {
                    //if (wire.WireData[1] != 0)
                    //{
                    //    foreach (var wr in w)
                    //    {
                    //        wr.Sprite.SetColor(0, 0, 0);
                    //    }
                    //}
                }
            }

        }

        private void XAdd()
        {
            var v = Vector + new IntVector(1, 0);
            if (WireShowLayer.SharedWireShow().WireDictionary.ContainsKey(v))
            {
                Wire wire = WireShowLayer.SharedWireShow().WireDictionary[v];
                if (wire.WireData[1] != 0)
                {
                    wire.FundPrince();
                    int i = WireShowLayer.SharedWireShow().Together.Count;
                    for (int j = 0; j < i; j++)
                    {
                        var w = WireShowLayer.SharedWireShow().Together[j];
                        w.Sprite.SetColor(255, 136, 49);
                    }
                }
            }
        }

        private void XSub()
        {
            var v = Vector - new IntVector(1, 0);
            if (WireShowLayer.SharedWireShow().WireDictionary.ContainsKey(v))
            {
                Wire wire = WireShowLayer.SharedWireShow().WireDictionary[v];
                if (wire.WireData[3] != 0)
                {
                    wire.FundPrince();
                    int i = WireShowLayer.SharedWireShow().Together.Count;
                    for (int j = 0; j < i; j++)
                    {
                        var w = WireShowLayer.SharedWireShow().Together[j];
                        w.Sprite.SetColor(251, 213, 68);
                    }
                }
            }
        }
    }
}
