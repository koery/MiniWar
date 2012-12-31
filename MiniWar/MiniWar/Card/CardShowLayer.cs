using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;
using VSZombie.Toobz;

namespace MiniWar.Card
{
    class CardShowLayer : MGLayer
    {
        private static CardShowLayer _sharedCardShow;
        public static CardShowLayer SharedCardShow()
        {
            if (_sharedCardShow == null)
            {
                _sharedCardShow = new CardShowLayer();
            }
            return _sharedCardShow;
        }

        public List<Paper> Papers;
        public List<CardBox> Boxs;
        private CardShowLayer()
        {
            Papers = new List<Paper>();
            Boxs = new List<CardBox>();
        }

        public bool[] IsHas = new bool[6];
        public void Init()
        {
            for (int i = 0; i < 6; i++)
            {
                var paper = new HorizontalPaper();
                AddChild(paper);
                paper.SetVector(-1, i);
                paper.Sprite.Position += new Vector2(30 - 3 * i, 0);
                paper.Tag = 1;
                IsHas[i] = true;
            }

            //for (int i = 0; i < GameConfig.CardCount; i++)
            //{
            //    var card = new CardBox();
            //    AddChild(card);
            //    card.SetPoint(new Vector2(132 + i * 76, 713));
            //}
        }

        public override void Update(float time)
        {
            for (int i = 0; i < Papers.Count; i++)
            {
                Papers[i].Launch(time);
            }
            base.Update(time);
        }

        public bool AddChild(CardBox box)
        {
            if (Boxs.Count >= GameConfig.CardCount)
            {
                return false;
            }
            MainGameScene.ShardMainGame().CardShow.AddChild(box.ICOSprite);
            AddChild(box.Sprite);
            box.SetPoint(Boxs.Count);
            Boxs.Add(box);
            return true;
        }

        public void RemoveChild(CardBox box)
        {
            MainGameScene.ShardMainGame().CardShow.RemoveChild(box.ICOSprite);
            RemoveChild(box.Sprite);
            Boxs.Remove(box);
        }

        public void AddChild(Paper paper)
        {
            AddChild(paper.Sprite);
            Papers.Add(paper);
        }

    }
}
