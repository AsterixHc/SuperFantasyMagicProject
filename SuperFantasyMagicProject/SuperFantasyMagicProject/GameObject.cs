using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SuperFantasyMagicProject
{
    abstract class GameObject
    {
        //Graphics
        protected Texture2D texture;
        protected Texture2D[] textures;
        protected string texturePath;
        protected Vector2 origin;
        public Vector2 Position { get; set; }

        //Animation
        protected float fps = 4;
        private float timeElasped;
        private int currentIndex;

        public GameObject()
        {
            Position = Vector2.Zero;
            origin = Vector2.Zero;
        }

        public abstract void LoadContent(ContentManager content);

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        protected void Animate(GameTime gameTime)
        {
            //Calculate and set current texture index.
            timeElasped += (float)gameTime.ElapsedGameTime.TotalSeconds;
            currentIndex = (int)(timeElasped * fps);
            texture = textures[currentIndex];

            //Reset animation if end of sprites array has been reached.
            if (currentIndex >= textures.Length - 1)
            {
                timeElasped = 0;
                currentIndex = 0;
            }
        }
    }
}
