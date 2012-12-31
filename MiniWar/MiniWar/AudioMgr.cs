using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using MGFramework;
using Microsoft.Xna.Framework.Audio;

namespace MiniWar
{
    class AudioMgr
    {

        public static Song BMG;
        public static List<SoundEffect> Explo;
        public static void LoadAudio()
        {
            BMG = MGDirector.SharedDirector().Content.Load<Song>("Audio/675");
            Explo = new List<SoundEffect>();
            Explo.Add(MGDirector.SharedDirector().Content.Load<SoundEffect>("Audio/Explo2"));
            Explo.Add(MGDirector.SharedDirector().Content.Load<SoundEffect>("Audio/UI_Clicks01"));
            Explo.Add(MGDirector.SharedDirector().Content.Load<SoundEffect>("Audio/UIdivebeep"));
        }

        public static void PlayBMG()
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(BMG);
        }

        public static void PlayAudio(int i)
        {
            Explo[i].Play();
        }
    }
}
