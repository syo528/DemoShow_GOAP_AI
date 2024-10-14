using UnityEngine;
public class BerryController : MapObjectBase
{
    public MeshRenderer meshRenderer;
    public Material noramlMaterial;
    public Material ripeMaterial;
    public float time;
    private bool isRipe;
    private float timer;
    public float ripeSpeed = 1;

    public bool IsRipe
    {
        get => isRipe;
        set
        {
            isRipe = value;
            meshRenderer.material = isRipe ? ripeMaterial : noramlMaterial;
            if (isRipe) MapManager.Instance.OnBerryRipe(this);
            else MapManager.Instance.RemoveBerryRipe(this);
        }
    }
    private void Update()
    {
        if (!isRipe)
        {
            timer -= Time.deltaTime * ripeSpeed;
            if (timer <= 0)
            {
                IsRipe = true;
            }
        }
    }
    public override void Init(Vector2Int coord)
    {
        base.Init(coord);
        IsRipe = true;
    }
    public void OnPick()
    {
        IsRipe = false;
    }
}
