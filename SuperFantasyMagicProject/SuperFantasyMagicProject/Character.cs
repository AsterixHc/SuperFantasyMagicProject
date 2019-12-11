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

        protected Texture2D texture;
        protected string texturePath;
        protected Vector2 position;
        protected Vector2 origin;

        protected int baseHealth;
        protected int baseMana;
        protected double baseCritical;

        protected int strength;
        protected int agility;
        protected int intelligence;

        protected int maxHealth;
        protected int currentHealth;

        protected int maxMana;
        protected int currentMana;

        protected int damage;
        protected double critical;
        protected int turnSpeed;

        protected bool ranged;
        protected bool isAlive = true;
        protected bool isPoisoned = false;
        protected bool isScratched = false;
        protected bool isGusted = false;
        protected bool isParalyzed = false;

        #region Properties

        public string TexturePath { get; protected set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; } //TODO: Make set protected once animations have been moved.

        public virtual int Strength
        {
            get { return strength; }
            set
            {
                strength = value;
                MaxHealth = baseHealth + strength * 10;
                Damage = strength * 2;
            }
        }

        public virtual int Agility
        {
            get { return agility; }
            set
            {
                agility = value;
                TurnSpeed = agility / 2;
            }
        }
        public virtual int Intelligence
        {
            get { return intelligence; }
            set
            {
                intelligence = value;
                MaxMana = baseMana + Intelligence * 10;
                Critical = baseCritical + intelligence / 10;
            }
        }

        public virtual int MaxHealth
        {
            get { return maxHealth; }
            protected set
            {
                maxHealth = value;

                if (CurrentHealth > value)
                {
                    CurrentHealth = value;
                }
            }
        }

        public virtual int CurrentHealth 
        {
            get { return currentHealth; }
            set
            {
                if(value <= MaxHealth && value >= 0)
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
            } 
        }

        public virtual int MaxMana
        {
            get { return maxMana; }
            protected set
            {
                maxMana = value;

                if (CurrentMana > value)
                {
                    CurrentMana = value;
                }
            }
        }

        public virtual int CurrentMana
        {
            get { return currentMana; }
            set
            {
                if (value <= MaxMana && value >= 0)
                {
                    currentMana = value;
                }
                else if (value < 0)
                {
                    currentMana = 0;
                }
                else if (value > MaxMana)
                {
                    currentMana = MaxMana;
                }
            }
        }

        public virtual int Damage
        {
            get { return damage; }
            protected set
            {
                if (value >= 0)
                {
                    damage = value;
                }
            }
        }
        
        public virtual double Critical { get; protected set; }
        public virtual int TurnSpeed { get; protected set; }

        //Read-only property that sets isAlive when read.
        public bool IsAlive
        {
            get
            {
                if (currentHealth > 0)
                {
                    isAlive = true;
                }
                else
                {
                    isAlive = false;
                }
                return isAlive;
            }
        }

        #endregion

        public abstract int Attack();

        public abstract void SpecialAttack();

        public virtual void TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            CurrentHealth -= dmg;
        }

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

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            texture = content.Load<Texture2D>(texturePath);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            //Because IsAlive checks current health and updates itself, this also serves as an update to IsAlive.
            if (IsAlive)
            {
                //update logic
            }
        }
    }
}
