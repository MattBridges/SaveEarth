﻿using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
    private float speed;
    private Vector3 rotDir;
    
    public void CreateShip(Sprite ShipSprite, float MoveSpeed)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ShipSprite;
        speed = MoveSpeed;        
    }
    public void MoveLeft(Rigidbody2D rb)
    {
        rb.AddForce(-Vector2.right * speed);
    }
    public void MoveRight(Rigidbody2D rb)
    {
        rb.AddForce(Vector2.right * speed);
    }
    public void MoveUp(Rigidbody2D rb)
    {
        rb.AddForce( Vector2.up * speed);
    }
    public void MoveDown(Rigidbody2D rb)
    {
        rb.AddForce(-Vector2.up * speed);       
    }
    public void JoystickMove(Rigidbody2D rb, Vector3 dir)
    {
        rb.AddForce(dir * speed);
    }
    public void Rotate(Vector3 stickPos)
    {
        Vector3 newDir = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Atan2(stickPos.x, stickPos.y) * -1 * Mathf.Rad2Deg + 90);
        if (newDir.z == 90 && stickPos.y == 0)
            rotDir = this.transform.eulerAngles;
        else
            rotDir = newDir;

        transform.eulerAngles = rotDir;

    }
    public void FireCannon(GameObject projectle, float bulletSpeed, AudioSource audioSrc, AudioClip shotSound, Transform shotPos, Vector3 shotDirection )
    {
        audioSrc.clip = shotSound;
        audioSrc.Play();
        GameObject projectile = (GameObject)Instantiate(projectle, shotPos.position, Quaternion.identity);
        projectile.transform.parent = GameObject.Find("BulletCollector").transform;
        projectile.GetComponent<Rigidbody>().velocity = shotDirection * bulletSpeed;
    }
}
