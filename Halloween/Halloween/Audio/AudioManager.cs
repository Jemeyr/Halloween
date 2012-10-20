using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Halloween.Audio
{
    public class AudioManager : GameComponent
    {
        internal readonly AudioListener AudioListener = new AudioListener();
        internal AudioEmitter AudioEmitter = new AudioEmitter();

        readonly AudioEngine _audioEngine;
        readonly WaveBank _soundEffectsBank;
        readonly WaveBank _musicBank;
        readonly SoundBank _soundBank;

        readonly List<Sound> _sounds = new List<Sound>();
        readonly Dictionary<string, Sound> _soundReferences = new Dictionary<string, Sound>();
        readonly Dictionary<string, Queue<Sound>> _soundQueues = new Dictionary<string, Queue<Sound>>();

        public AudioManager(Game game, string settingsFile, string soundbankFile, string soundEffectsWaveBankFile, string musicWaveBankFile)
            : base(game)
        {
            _audioEngine = new AudioEngine(settingsFile);
            _soundBank = new SoundBank(_audioEngine, soundbankFile);
            _soundEffectsBank = new WaveBank(_audioEngine, soundEffectsWaveBankFile);
            _musicBank = new WaveBank(_audioEngine, musicWaveBankFile, 0, 16);
        }

        public override void Initialize()
        {
            base.Initialize();
            _audioEngine.Update();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _audioEngine.Update();
            for (var x = 0; x < _sounds.Count; x++)
            {
                var sound = _sounds[x];
                if (!sound.Cue.IsStopped && !sound.Cue.IsDisposed)
                    continue;
                var queue = _soundQueues[sound.Name];
                if (queue.Count > 0)
                    _soundReferences[sound.Name] = queue.Dequeue();
                else
                    _soundReferences.Remove(sound.Name);
                sound.Recycle();
                _sounds.Remove(sound);
            }
        }

        void Play(Sound sound)
        {
            sound.Play();
            _sounds.Add(sound);
            if (!_soundReferences.ContainsKey(sound.Name))
            {
                _soundReferences.Add(sound.Name, sound);
                if (!_soundQueues.ContainsKey(sound.Name))
                    _soundQueues.Add(sound.Name, new Queue<Sound>());
            }
            else
                _soundQueues[sound.Name].Enqueue(sound);
        }

        public Sound Play(string soundName, bool forceNewInstance)
        {
            if (IsPlaying(soundName) && !forceNewInstance)
                return null;
            var sound = Sound.Create(soundName, _soundBank.GetCue(soundName));
            Play(sound);
            return sound;
        }

        public Sound Play(string soundName)
        {
            return Play(soundName, false);
        }

        public void Pause()
        {
            for (var x = 0; x < _sounds.Count; x++)
                _sounds[x].Pause();
        }

        public Sound Pause(string soundName)
        {
            if (!_soundReferences.ContainsKey(soundName))
                return null;
            var sound = _soundReferences[soundName];
            sound.Pause();
            return sound;
        }

        public void Resume()
        {
            for (var x = 0; x < _sounds.Count; x++)
                _sounds[x].Resume();
        }

        public Sound Resume(string soundName)
        {
            if (!_soundReferences.ContainsKey(soundName))
                return null;
            var sound = _soundReferences[soundName];
            sound.Resume();
            return sound;
        }

        public void Stop()
        {
            for (var x = 0; x < _sounds.Count; x++)
                _sounds[x].Stop();
        }

        public Sound Stop(string soundName)
        {
            if (!_soundReferences.ContainsKey(soundName))
                return null;
            var sound = _soundReferences[soundName];
            sound.Stop();
            return sound;
        }

        public bool IsPlaying(string soundName)
        {
            return _soundReferences.ContainsKey(soundName) && _soundReferences[soundName].IsPlaying;
        }
    }
}
