#if !WINDOWS_PHONE

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;

namespace Halloween.Input
{
    public sealed class PlayerInputCollection : ReadOnlyCollection<PlayerInput>
    {
        public PlayerInput this[PlayerIndex index] 
        { 
            get { return base[(int) index]; } 
        }

        internal new PlayerInput this[int index]
        {
            get { return base[index]; }
        }

        internal PlayerInputCollection()
            : base(new List<PlayerInput>(new[] { new PlayerInput(PlayerIndex.One), new PlayerInput(PlayerIndex.Two), new PlayerInput(PlayerIndex.Three), new PlayerInput(PlayerIndex.Four)}))
        {
        }

        internal void Update(GameTime gameTime)
        {
            for (var x = 0; x < Count; x++)
                base[x].Update(gameTime);
        }
    }
}

#endif