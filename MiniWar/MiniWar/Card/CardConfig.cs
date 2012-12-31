using System.Collections.Generic;
using MGFramework;
using Microsoft.Xna.Framework;

namespace MiniWar.Card
{
    class CardConfig
    {
        public static List<PaperInfo> PaperInfos = new List<PaperInfo>
        {
            new PaperInfo
                {Id = 1, Img = "4_1.png",Img2 = "4_2.png",Sprite=MGSprite.MGSpriteWithSpriteFrameName ("4_1.png"), Pos = new Vector2(59, 546),Str="随机交换", Price = 100},
            new PaperInfo
                {Id = 2, Img = "8_1.png",Img2 = "8_2.png",Sprite=MGSprite.MGSpriteWithSpriteFrameName ("8_1.png"), Pos = new Vector2(59+65, 546),Str = "小冰",Price = 100},
            new PaperInfo                      
                {Id = 3, Img = "2_1.png",Img2 = "2_2.png",Sprite=MGSprite.MGSpriteWithSpriteFrameName ("2_1.png"), Pos = new Vector2(59+65*2, 546),Str="火箭",Price = 100},
            new PaperInfo                      
                {Id = 4, Img = "1_1.png",Img2 = "1_2.png",Sprite=MGSprite.MGSpriteWithSpriteFrameName ("1_1.png"), Pos = new Vector2(59+65*3, 546),Str = "十字",Price = 150},
            new PaperInfo                     
                {Id = 5, Img = "7_1.png",Img2 = "7_2.png",Sprite=MGSprite.MGSpriteWithSpriteFrameName ("7_1.png"), Pos = new Vector2(59+65*4, 546),Str = "小地雷",Price = 150},
            new PaperInfo                     
                {Id = 6, Img = "9_1.png",Img2 = "9_2.png",Sprite=MGSprite.MGSpriteWithSpriteFrameName ("9_1.png"), Pos = new Vector2(59, 546+90),Str = "大冰",Price = 150},
            new PaperInfo                       
                {Id = 7, Img = "6_1.png",Img2 = "6_2.png",Sprite=MGSprite.MGSpriteWithSpriteFrameName ("6_1.png"), Pos = new Vector2(59+65, 546+90),Str = "大地雷",Price = 200},
            new PaperInfo                  
                {Id = 8, Img = "5_1.png",Img2 = "5_2.png",Sprite=MGSprite.MGSpriteWithSpriteFrameName ("5_1.png"), Pos = new Vector2(59+65*2, 546+90),Str = "闪电",Price = 200},
        };
    }

    internal class PaperInfo
    {
        public string Img;
        public string Img2;
        public MGSprite Sprite;
        public Vector2 Pos;
        public string Str;
        public int Id;
        public int Price;
    }
}