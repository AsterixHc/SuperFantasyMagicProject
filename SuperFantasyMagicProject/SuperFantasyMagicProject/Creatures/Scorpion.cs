﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Creatures
{
    class Scorpion : Character
    {

        public override void Attack()
        {
            //Attack at random against Player
        }

        public override void SpecialAttack()
        {
            //Attack at random against Player with "Poisen sting"
            //Apply Poisen on Target
        }

        public override bool TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            throw new NotImplementedException();
        }
    }
}
