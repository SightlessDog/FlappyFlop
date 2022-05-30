namespace Managers
{
    public class Properties
    {
        //Distance between the lines
        public const float DISTANCE_LEFT = 25f;
        public const float DISTANCE_RIGHT = 25f;
        
        //Enemy xPOs
        public const float ENEMY_LEFT = 12.5f;
        public const float ENEMY_RIGHT = 12.5f;

        //Frequency to send objects
        public const float TERRAIN_FREQUENCY = 10f;
        public const float OBSTACLE_FREQUENCY = 4f;
        public const float ENEMY_FREQUENCY = 15f;
        public const int BLOCK_OBSTACLE_FREQUENCY = 2;

        //Obstacle yPos
        public const float OBSTACLE_MIN_RANGE = 47f;
        public const float OBSTACLE_MAX_RANGE = 63f;
        public const float OBSTACLE_MID_RANGE = 55f;
        
        //Enemy yPos
        public const float ENEMY_YPOS_MAX = 50f;
        public const float ENEMY_YPOS_MIN = 70f;
        
        //Object life time
        public const float OBJECT_LIFE_TIME = 20f;
    }
}