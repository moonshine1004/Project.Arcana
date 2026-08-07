using UnityEngine;

public class ProjectileLifetime : MonoBehaviour
{
    public float _lifetime = 6;
    private float _timer;
    private ProjectileLauncher _pool;

    private void Start()
    {
        _lifetime = 6;
    }
    private void OnEnable()
    {
        _timer = 0f;
    }

    public void SetLifetime(float time)
    {
        _lifetime = time;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _lifetime)
        {
            _pool.ReturnToPool(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _pool.ReturnToPool(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _pool.ReturnToPool(this.gameObject);
    }
    public void ProjectileDestroy(ProjectileLauncher launcher)
    {
        _pool = launcher;
    }
}
