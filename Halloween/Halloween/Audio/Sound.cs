using System;
using Microsoft.Xna.Framework.Audio;

namespace Halloween.Audio
{
    public class Sound : IRecyclable
    {
        protected internal Cue Cue { get; set; }
        public string Name { get; set; }
        public bool IsPlaying
        {
            get { return Cue.IsPlaying; }
        }

        float _volume;
        public float Volume
        {
            get { return _volume; }
            set
            {
                if (!(Math.Abs(_volume - value) > float.Epsilon))
                    return;
                if (value > 1) 
                    value = 1;
                if (value < 0)
                    value = 0;
                _volume = value;
                Cue.SetVariable("Volume", value);
            }
        }

        internal Sound()
        {
        }

        internal static Sound Create(string name, Cue cue)
        {
            var sound = Pool.Acquire<Sound>();
            sound.Name = name;
            sound.Cue = cue;
            sound.Volume = sound.Cue.GetVariable("Volume");
            return sound;
        }

        public void Play()
        {
            Cue.Play();
        }

        public void Pause()
        {
            Cue.Pause();
        }


        public void Resume()
        {
            Cue.Resume();
        }

        public void Stop()
        {
            Cue.Stop(AudioStopOptions.Immediate);
        }

        public void Stop(AudioStopOptions options)
        {
            Cue.Stop(options);
        }

        protected internal virtual void Recycle()
        {
            Name = string.Empty;
            Cue.Dispose();
        }

        void IRecyclable.Recycle()
        {
            Recycle();
        }
    }
}
