﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Creatures
{
    class DemonFlower : Character
    {
        public DemonFlower(Random rnd, int maxHealth, int currentHealth, int mana, int strenght, int agillity, int intelligence)
        {

        }

        public override void Attack()
        {
            //Attack at random against Player
            //Attack random enemy in an array

        }

        public override void SpecialAttack()
        {
            //Applies Posien cloud (100% chance!)
            //apply poisen
            //DMG should be about 2-4% of the max health of the character
        }

        public override void TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            throw new NotImplementedException();
        }
    }
}
