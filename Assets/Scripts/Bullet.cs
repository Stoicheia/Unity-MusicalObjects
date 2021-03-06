﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
    public float damage = 1;

	float speed;
	Vector3 moveVector;
	bool friendly = false;

	MeshCollider stage;
    public string poolTag = "";
    ObjectPooler pooler;
	SpriteRenderer render;


    void Start()
    {
        if(poolTag=="")
            poolTag = name;
        stage = GameObject.FindGameObjectWithTag("Stage").GetComponent<MeshCollider>();
        pooler = FindObjectOfType(typeof(ObjectPooler)) as ObjectPooler;
        render = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        float moveDistance = speed*Time.deltaTime;
    	moveVector = new Vector3(0,1,0)*moveDistance;
        Move(moveVector);
    }

    void Move(Vector3 v){
    	transform.Translate(v);
    	if(Mathf.Abs(transform.position.x-stage.transform.position.x)>stage.bounds.size.x/2+render.bounds.size.x/2||
    		Mathf.Abs(transform.position.y-stage.transform.position.y)>stage.bounds.size.y/2+render.bounds.size.y/2){
    		pooler.Despawn(gameObject, poolTag);
    	}
    }

    void OnCollisionEnter2D(Collision2D collision){
        OnHit(collision);
    }

    void OnHit(Collision2D hit){
        IDamageable hitObject = hit.gameObject.GetComponent<IDamageable>();
        if(hitObject!=null){
            hitObject.TakeHit(damage, hit);
        }
    }

    public void SetSpeed(float s){
    	speed = s;
    }

    public void Rotate(float r){
    	transform.rotation *= Quaternion.Euler(0,0,r);
    }

    public void SetFriendly(bool f){
    	friendly = f;
    }


}
