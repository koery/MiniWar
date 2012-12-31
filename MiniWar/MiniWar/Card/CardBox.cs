using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;
using Microsoft.Xna.Framework;

namespace MiniWar.Card
{
    class CardBox
    {
        public MGSprite ICOSprite { get; set; }
        public MGSprite Sprite { get; set; }
        public int Id { get; set; }
        public CardBox(int id)
        {
            Id = id;
            //ICOSprite = MGSprite.MGSpriteWithFilename("Card/1_1");
            //Sprite = MGSprite.MGSpriteWithFilename("Card/1_2");

            for (int i = 0; i < CardConfig.PaperInfos.Count; i++)
            {
                var card = CardConfig.PaperInfos[i];
                if (card.Id == id)
                {
                    ICOSprite = MGSprite.MGSpriteWithSpriteFrameName(card.Img);
                    Sprite = MGSprite.MGSpriteWithSpriteFrameName(card.Img2);
                    break;
                }
            }
            Sprite.Visible = false;
        }

        public void SetPoint(Vector2 vector2)
        {
            Sprite.Position = vector2;
            ICOSprite.Position = vector2;
        }

        public void SetPoint(int index)
        {
            SetPoint(new Vector2(132 + index * 76, 713));
        }

        public void MouseClick()
        {
            ICOSprite.Opacity = 155;
        }

        public void MouseMove()
        {
            Sprite.Visible = true;
        }

        public void MouseCancel()
        {
            ICOSprite.Opacity = 255;
            Sprite.RunAction(MGSequence.Actions(MGMoveTo.ActionWithDuration(.2f, ICOSprite.Position), new MGHide()));
        }

        public void MouseEnd()
        {
            ICOSprite.Visible = false;
            Sprite.Visible = false;
        }
    }
}
