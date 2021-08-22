using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Transform cameraTransform;
    public bool infiniteHorizontal;
    public bool infiniteVertical;
    private Vector3 lastCameraPosition;
    [SerializeField] private Vector2 parallaxFactor;
    private float textureUnitSizeX;
    private float textureUnitSizeY;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        var sprite = GetComponent<SpriteRenderer>().sprite;
        var texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }


    private void Update()

    {
        var deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position -= new Vector3(deltaMovement.x * parallaxFactor.x, deltaMovement.y * parallaxFactor.y);
        lastCameraPosition = cameraTransform.position;

        if (infiniteHorizontal)
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
            {
                var offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
                transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
            }

        if (infiniteVertical)
            if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY)
            {
                var offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
                transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetPositionY);
            }
    }
}