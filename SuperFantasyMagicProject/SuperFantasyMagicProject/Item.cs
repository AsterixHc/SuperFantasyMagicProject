﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject
{
    public enum ItemEffect { Heal }

    class Item : GameObject
    {
        public Item()
        {

        }

        protected int price;

        protected ItemEffect effect;

    }
}
