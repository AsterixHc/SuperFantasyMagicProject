using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SuperFantasyMagicProject
{
    abstract class Character : GameObject
    {

        protected Random rnd;

        protected Vector2 position;

        protected int maxHealth;
        protected int currnetHealth;
        protected int mana;
        protected int strenght;
        protected int agility;
        protected int intelligence;

        protected bool ranged;

        public abstract void Attack();

        public abstract void SpecialAttack();

        public abstract bool TakeDamage(int dmg);

        public virtual void Flee()
        {
            Console.WriteLine("Im Escaping Battle");
        }

        public virtual void UseItem(Item item)
        {
            Console.WriteLine("I Used an Item");
        }

        public virtual void LevelUp()
        {
            Console.WriteLine("I leveled up");
        }
    }
}
