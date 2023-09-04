using Edgar.Unity;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ProjectLumina.Dungeon
{
    [CreateAssetMenu(menuName = "Project Lumina/Dungeon/Tilemap Layers Handler", fileName = "Tilemap Layers Handler")]
    public class TilemapLayersHandler : TilemapLayersHandlerBaseGrid2D
    {
        public override void InitializeTilemaps(GameObject gameObject)
        {
            gameObject.AddComponent<Grid>();

            var backgroundUnder = CreateTilemapGameObject("Background Under", gameObject, "Background", 0);
            var background = CreateTilemapGameObject("Background", gameObject, "Background", 5);
            var backgroundOverlay = CreateTilemapGameObject("Background Overlay", gameObject, "Background", 6);
            var walls = CreateTilemapGameObject("Walls", gameObject, "Wall", 0);
            var platforms = CreateTilemapGameObject("Platforms", gameObject, "Platform", 0);
            var foreground = CreateTilemapGameObject("Foreground", gameObject, "Foreground", 0);
            var collideable = CreateTilemapGameObject("Collideable", gameObject, "Foreground", 1);
            var other1 = CreateTilemapGameObject("Other 1", gameObject, "Foreground", 2);
            var other2 = CreateTilemapGameObject("Other 2", gameObject, "Foreground", 3);

            AddCompositeCollider(walls);
            AddCompositeCollider(platforms);
            AddPlatformEffector(platforms);
            AddCompositeCollider(collideable);
        }

        protected GameObject CreateTilemapGameObject(string name, GameObject parentObject, string sortingLayer, int sortingOrder)
        {
            var tilemapObject = new GameObject(name);
            tilemapObject.transform.SetParent(parentObject.transform);
            tilemapObject.layer = LayerMask.NameToLayer(sortingLayer);
            var tilemap = tilemapObject.AddComponent<Tilemap>();
            var tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
            tilemapRenderer.sortingLayerName = sortingLayer;
            tilemapRenderer.sortingOrder = sortingOrder;

            return tilemapObject;
        }

        protected void AddCompositeCollider(GameObject gameObject, bool isTrigger = false)
        {
            var tilemapCollider2D = gameObject.AddComponent<TilemapCollider2D>();
            tilemapCollider2D.usedByComposite = true;

            var compositeCollider2d = gameObject.AddComponent<CompositeCollider2D>();
            compositeCollider2d.geometryType = CompositeCollider2D.GeometryType.Polygons;
            compositeCollider2d.isTrigger = isTrigger;

            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

        protected void AddPlatformEffector(GameObject gameObject)
        {
            gameObject.GetComponent<CompositeCollider2D>().usedByEffector = true;
            gameObject.AddComponent<PlatformEffector2D>();
        }
    }
}