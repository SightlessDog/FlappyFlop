namespace Managers
{
    public class Properties
    {
        //Distance between the lines
        public const float DISTANCE_LEFT = 25f;
        public const float DISTANCE_RIGHT = 25f;

        //Frequency to send objects
        public const float TERRAIN_FREQUENCY = 10f;
        public const float OBSTACLE_FREQUENCY = 4f;
        public const int BLOCK_OBSTACLE_FREQUENCY = 2;

        //Obstacle yPos
        public const float OBSTACLE_MIN_RANGE = 47f;
        public const float OBSTACLE_MAX_RANGE = 63f;
        public const float OBSTACLE_MID_RANGE = 55f;

        //Enemy yPos
        public const float ENEMY_MIN_RANGE = 50f;
        public const float ENEMY_MAX_RANGE = 60f;
        public const float ENEMY_MID_RANGE = 55f;

        //Object life time
        public const float OBJECT_LIFE_TIME = 20f;
    }
}