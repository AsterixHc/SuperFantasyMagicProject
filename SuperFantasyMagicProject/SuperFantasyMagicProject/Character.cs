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

        private int maxHealth;
        private int currentHealth;
        private int mana;
        private int strenght;
        private int agility;
        private int intelligence;
        private int damage;

        protected bool ranged;
        protected bool isPoisoned = false;
        protected bool isPlayerAlive = true;
        protected bool isScratched = false;
        protected bool isGusted = false;
        protected bool isParalysed = false;

        public Vector2 Position { get => position; set => position = value; }
        public string Path { get => path; protected set => path = value; }
        public int CurrentHealth 
        { 
            get => currentHealth; 
            set
            {
                if(value >= 0 && MaxHealth >= value)
                {
                    currentHealth = value;
                }
                else if(value < 0)
                {
                    currentHealth = 0;
                }
                else if(value > MaxHealth)
                {
                    currentHealth = MaxHealth;
                }
                else
                {
#if DEBUG
                    Console.WriteLine("Yaps current health er dum");
#endif                
                }
            } 
        }

        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int Mana { get => mana; set => mana = value; }
        public int Strenght { get => strenght; set => strenght = value; }
        public int Agility { get => agility; set => agility = value; }
        public int Intelligence { get => intelligence; set => intelligence = value; }
        public int Damage { get => damage; set => damage = value; }

        public abstract void Attack();

        public abstract void SpecialAttack();

        public abstract void TakeDamage(int dmg);

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

        //    //1 point = 2 DMG
        //    //1 point = 10health
        //}

        public virtual void CalculateAgility()
        {
            //Increase DMG for the Rogue
            //Increase Speed for all chars

        //    //1 point = 2 DMG
        //    //1 point = 5 speed ??
        //}

        public virtual void CalculateIntelligence()
        {
            //Increase DMG for the Mage
            //Increase Crit chance for all chars

        //    //1 point = 2 DMG
        //    //1 point = 0.10% crit chance??
        //}

    }
}
