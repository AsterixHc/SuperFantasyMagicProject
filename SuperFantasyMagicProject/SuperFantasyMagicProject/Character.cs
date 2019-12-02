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
        protected string path;

        protected int maxHealth;
        protected int currentHealth;
        protected int mana;
        protected int strenght;
        protected int agility;
        protected int intelligence;

        protected bool ranged;

        public Vector2 Position { get => position; set => position = value; }
        public string Path { get => path; protected set => path = value; }

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

        public virtual void CalculateStrength()
        {
            //Increase DMG for the warrior
            //Increase Health for all chars

            //1 point = 2 DMG
            //1 point = 10health
        }

        public virtual void CalculateAgility()
        {
            //Increase DMG for the Rogue
            //Increase Speed for all chars

            //1 point = 2 DMG
            //1 point = 5 speed ??
        }

        public virtual void CalculateIntelligence()
        {
            //Increase DMG for the Mage
            //Increase Crit chance for all chars

            //1 point = 2 DMG
            //1 point = 0.10% crit chance??
        }

    }
}
