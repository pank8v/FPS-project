using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{

  public static BulletPool Instance { get; private set; }
  
  [SerializeField] private Bullet bulletPrefab;
  [SerializeField] private int poolSize = 20;
  
  private Queue<Bullet> availilibleBullets = new Queue<Bullet>();

  private void Awake() {
    if (Instance == null) {
      Instance = this;
    }
    else {
      Destroy(gameObject);
      return;
    }
    for (int i = 0; i < poolSize; i++) {
      Bullet bullet = Instantiate(bulletPrefab, transform);
      bullet.SetPool(this);
      bullet.gameObject.SetActive(false);
      availilibleBullets.Enqueue(bullet);
    }
  }



  public Bullet GetBullet() {
    Bullet bullet;
    if (availilibleBullets.Count > 0) {
      bullet = availilibleBullets.Dequeue();
    }
    else {
      bullet = Instantiate(bulletPrefab, transform);
      bullet.SetPool(this);
    }
    bullet.gameObject.SetActive(true);
    return bullet;
  }

  public void ReturnBullet(Bullet bullet) {
    bullet.gameObject.SetActive(false);
    availilibleBullets.Enqueue(bullet);
  }
  
}
