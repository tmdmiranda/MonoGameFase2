namespace Projeto2_MG.Main

{
    public class Entity : AnimatedSprite
    {

        
        public Entity(Vector2 pos) : base(pos) 
        {
            framesPerSecond = 10;
        } 
        public string Name { get; set; }

        public int Health { get; set; }

        public float Speed { get; set; }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}