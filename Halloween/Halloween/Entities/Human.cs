﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Halloween.Entities
{
    class Human : Pawn
    {
        public bool armed;
        
        public Human() {
            health = 2;
            friendly = false;
        }

    }
}
