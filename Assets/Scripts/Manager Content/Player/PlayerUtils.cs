using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public static class PlayerUtils
    {
        public static Player Instantiate()
        {
            float x = 0.0f;
            float y = 0.0f;

            GameObject gameObject = GameObject.FindGameObjectWithTag(IDs.TAG_ID__PLAYER_LOCATION);

            if (gameObject != null)
            {
                x = gameObject.transform.position.x;
                y = gameObject.transform.position.y;
            }

            return PlayerUtils.Instantiate(x, y);
        }

        public static Player Instantiate(float x, float y)
        {
            if (GameObjectUtils.Instantiate<Player>(IDs.PREFAB_ID__PLAYER, out Player player))
            {
                player.transform.position = new Vector3(x, y, 0.0f);

                return player;
            }

            return null;
        }
    }
}
