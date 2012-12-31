using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;

namespace MiniWar.Zombie
{
    class AnimationMgr
    {
        private static AnimationMgr _shardAnimationMgr;
        public static AnimationMgr ShardAnimationMgr()
        {
            return _shardAnimationMgr ?? (_shardAnimationMgr = new AnimationMgr());
        }

        private List<MGAnimation> _num1001Zombie;
        private List<MGAnimation> _num4001Zombie;
        private List<MGAnimation> _num3001Zombie;
        private List<MGAnimation> _num2001Zombie;
        private List<MGAnimation> _num5001Zombie;
        private List<MGAnimation> _num6001Zombie;

        private MGAnimation _electric;
        private MGAnimation _electric2;
        private MGAnimation _rocket;
        private MGAnimation _ice;
        private MGAnimation _ice2;
        private MGAnimation _bomb;
        private MGAnimation _bomb2;
        private MGAnimation _cloud;
        private AnimationMgr()
        {
            _num1001Zombie = new List<MGAnimation>();
            _num4001Zombie = new List<MGAnimation>();
            _num3001Zombie = new List<MGAnimation>();
            _num2001Zombie = new List<MGAnimation>();
            _num5001Zombie = new List<MGAnimation>();
            _num6001Zombie = new List<MGAnimation>();

            List<MGSpriteFrame> frames = new List<MGSpriteFrame>();
            var cache = MGSpriteFrameCache.SharedSpriteFrameCache();
            frames.Clear();
            for (int i = 0; i < 10; i++)
            {
                frames.Add(cache.SpriteFrameByName("1001" + i + ".png"));
            }
            var stand1 = MGAnimation.animationWithFrames(frames, .2f);

            frames.Clear();
            for (int i = 10; i < 26; i++)
            {
                frames.Add(cache.SpriteFrameByName("1001" + i + ".png"));
            }
            var run1 = MGAnimation.animationWithFrames(frames, .1f);

            frames.Clear();
            for (int i = 26; i < 34; i++)
            {
                frames.Add(cache.SpriteFrameByName("1001" + i + ".png"));
            }
            var die1 = MGAnimation.animationWithFrames(frames, .1f);

            frames.Clear();
            frames.Add(cache.SpriteFrameByName("dj1.png"));
            frames.Add(cache.SpriteFrameByName("dj2.png"));
            var dj1 = MGAnimation.animationWithFrames(frames, .1f);
            _num1001Zombie.Add(run1);
            _num1001Zombie.Add(stand1);
            _num1001Zombie.Add(die1);
            _num1001Zombie.Add(dj1);

            frames.Clear();
            for (int i = 0; i < 12; i++)
            {
                frames.Add(cache.SpriteFrameByName("4001" + i + ".png"));
            }
            var stand4 = MGAnimation.animationWithFrames(frames, .2f);

            frames.Clear();
            for (int i = 12; i < 17; i++)
            {
                frames.Add(cache.SpriteFrameByName("4001" + i + ".png"));
            }
            var run4 = MGAnimation.animationWithFrames(frames, .1f);

            frames.Clear();
            for (int i = 17; i < 22; i++)
            {
                frames.Add(cache.SpriteFrameByName("4001" + i + ".png"));
            }
            var die4 = MGAnimation.animationWithFrames(frames, .1f);

            frames.Clear();
            frames.Add(cache.SpriteFrameByName("dj5.png"));
            frames.Add(cache.SpriteFrameByName("dj6.png"));
            var dj4 = MGAnimation.animationWithFrames(frames, .1f);
            _num4001Zombie.Add(run4);
            _num4001Zombie.Add(stand4);
            _num4001Zombie.Add(die4);
            _num4001Zombie.Add(dj4);

            frames.Clear();
            for (int i = 0; i < 9; i++)
            {
                frames.Add(cache.SpriteFrameByName("2001" + i + ".png"));
            }
            var stand2 = MGAnimation.animationWithFrames(frames, .2f);

            frames.Clear();
            for (int i = 9; i < 15; i++)
            {
                frames.Add(cache.SpriteFrameByName("2001" + i + ".png"));
            }
            var run2 = MGAnimation.animationWithFrames(frames, .1f);

            frames.Clear();
            for (int i = 16; i < 21; i++)
            {
                frames.Add(cache.SpriteFrameByName("2001" + i + ".png"));
            }
            var die2 = MGAnimation.animationWithFrames(frames, .2f);

            frames.Clear();
            frames.Add(cache.SpriteFrameByName("dj3.png"));
            frames.Add(cache.SpriteFrameByName("dj4.png"));
            var dj2 = MGAnimation.animationWithFrames(frames, .1f);
            _num2001Zombie.Add(run2);
            _num2001Zombie.Add(stand2);
            _num2001Zombie.Add(die2);
            _num2001Zombie.Add(dj2);


            frames.Clear();
            for (int i = 0; i < 9; i++)
            {
                frames.Add(cache.SpriteFrameByName("3001" + i + ".png"));
            }
            var stand3 = MGAnimation.animationWithFrames(frames, .2f);

            frames.Clear();
            for (int i = 9; i < 20; i++)
            {
                frames.Add(cache.SpriteFrameByName("3001" + i + ".png"));
            }
            var run3 = MGAnimation.animationWithFrames(frames, .1f);

            frames.Clear();
            for (int i = 20; i < 26; i++)
            {
                frames.Add(cache.SpriteFrameByName("3001" + i + ".png"));
            }
            var die3 = MGAnimation.animationWithFrames(frames, .1f);

            frames.Clear();
            frames.Add(cache.SpriteFrameByName("dj7.png"));
            frames.Add(cache.SpriteFrameByName("dj8.png"));
            var dj3 = MGAnimation.animationWithFrames(frames, .1f);

            _num3001Zombie.Add(run3);
            _num3001Zombie.Add(stand3);
            _num3001Zombie.Add(die3);
            _num3001Zombie.Add(dj3);

            frames.Clear();
            for (int i = 0; i < 4; i++)
            {
                frames.Add(cache.SpriteFrameByName("5001" + i + ".png"));
            }
            var run5 = MGAnimation.animationWithFrames(frames, .2f);
            frames.Clear();
            for (int i = 4; i < 9; i++)
            {
                frames.Add(cache.SpriteFrameByName("5001" + i + ".png"));
            }
            var die5 = MGAnimation.animationWithFrames(frames, .2f);
            frames.Clear();
            frames.Add(cache.SpriteFrameByName("dj9.png"));
            frames.Add(cache.SpriteFrameByName("dj10.png"));
            var dj5 = MGAnimation.animationWithFrames(frames, .1f);
            _num5001Zombie.Add(run5);
            _num5001Zombie.Add(dj5);
            _num5001Zombie.Add(die5);

            frames.Clear();
            for (int i = 0; i < 9; i++)
            {
                frames.Add(cache.SpriteFrameByName("6001" + i + ".png"));
            }
            var stand6 = MGAnimation.animationWithFrames(frames, .2f);

            frames.Clear();
            for (int i = 9; i < 20; i++)
            {
                frames.Add(cache.SpriteFrameByName("6001" + i + ".png"));
            }
            var run6 = MGAnimation.animationWithFrames(frames, .1f);

            frames.Clear();
            for (int i = 20; i < 26; i++)
            {
                frames.Add(cache.SpriteFrameByName("6001" + i + ".png"));
            }
            var die6 = MGAnimation.animationWithFrames(frames, .1f);
            frames.Clear();
            frames.Add(cache.SpriteFrameByName("dj11.png"));
            frames.Add(cache.SpriteFrameByName("dj12.png"));
            var dj6 = MGAnimation.animationWithFrames(frames, .1f);
            _num6001Zombie.Add(run6);
            _num6001Zombie.Add(stand6);
            _num6001Zombie.Add(die6);
            _num6001Zombie.Add(dj6);

            frames.Clear();
            frames.Add(cache.SpriteFrameByName("sd1.png"));
            frames.Add(cache.SpriteFrameByName("sd2.png"));
            _electric = MGAnimation.animationWithFrames(frames, .2f);
            frames.Clear();
            frames.Add(cache.SpriteFrameByName("sd3.png"));
            frames.Add(cache.SpriteFrameByName("sd4.png"));
            _electric2 = MGAnimation.animationWithFrames(frames, .2f);


            frames.Clear();
            frames.Add(cache.SpriteFrameByName("201.png"));
            frames.Add(cache.SpriteFrameByName("202.png"));
            frames.Add(cache.SpriteFrameByName("203.png"));
            _rocket = MGAnimation.animationWithFrames(frames, .2f);

            frames.Clear();
            frames.Add(cache.SpriteFrameByName("dbing1.png"));
            frames.Add(cache.SpriteFrameByName("dbing2.png"));
            frames.Add(cache.SpriteFrameByName("dbing3.png"));
            frames.Add(cache.SpriteFrameByName("dbing4.png"));
            frames.Add(cache.SpriteFrameByName("dbing5.png"));
            frames.Add(cache.SpriteFrameByName("dbing6.png"));
            _ice = MGAnimation.animationWithFrames(frames, .2f);

            frames.Clear();
            frames.Add(cache.SpriteFrameByName("xb1.png"));
            frames.Add(cache.SpriteFrameByName("xb2.png"));
            frames.Add(cache.SpriteFrameByName("xb3.png"));
            frames.Add(cache.SpriteFrameByName("xb4.png"));
            frames.Add(cache.SpriteFrameByName("xb5.png"));
            _ice2 = MGAnimation.animationWithFrames(frames, .2f);



            frames.Clear();
            for (int i = 1; i < 8; i++)
            {
                frames.Add(cache.SpriteFrameByName("sample" + i + ".png"));
            }
            _bomb = MGAnimation.animationWithFrames(frames, .2f);

            frames.Clear();
            frames.Add(cache.SpriteFrameByName("sk1.png"));
            frames.Add(cache.SpriteFrameByName("sk2.png"));
            frames.Add(cache.SpriteFrameByName("sk3.png"));
            frames.Add(cache.SpriteFrameByName("sk4.png"));
            frames.Add(cache.SpriteFrameByName("sk5.png"));
            _bomb2 = MGAnimation.animationWithFrames(frames, .1f);


            frames.Clear();
            for (int i = 1; i < 5; i++)
            {
                frames.Add(cache.SpriteFrameByName("Cloud_white_0" + i + ".png"));
            }
            _cloud = MGAnimation.animationWithFrames(frames, .2f);

            frames.Clear();
        }

        public List<MGAnimation> GetNumZombie(int id)
        {
            switch (id)
            {
                case 1001:
                    return _num1001Zombie;
                case 2001:
                    return _num2001Zombie;
                case 3001:
                    return _num3001Zombie;
                case 4001:
                    return _num4001Zombie;
                case 5001:
                    return _num5001Zombie;
                case 6001:
                    return _num6001Zombie;
            }
            return null;
        }

        public MGAnimation GetElectricAnimation()
        {
            return _electric;
        }

        public MGAnimation GetElectric2Animation()
        {
            return _electric2;
        }

        public MGAnimation GetRocketAnimation()
        {
            return _rocket;
        }

        public MGAnimation GetBigIceAnimation()
        {
            return _ice;
        }

        public MGAnimation GetIceAnimation()
        {
            return _ice2;
        }

        public MGAnimation GetBombAnimation()
        {
            return _bomb;
        }

        public MGAnimation GetBombAnimation2()
        {
            return _bomb2;
        }

        public MGAnimation GetCloudAnimation()
        {
            return _cloud;
        }


    }
}
