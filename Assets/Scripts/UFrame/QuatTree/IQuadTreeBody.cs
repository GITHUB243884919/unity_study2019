using UnityEngine;

namespace UFrame.QuadTree
{
    public interface IQuadTreeBody
    {
        Vector2 Position { get; }

        bool QuadTreeIgnore { get; }

        QuadTree quadTree { get; set; }

        int ID { get; }
    }
}

