using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationAxis{X,Y,Z};
public enum Typee{Rotation,Pendulum}

public class RotateObject : MonoBehaviour 
{
	public Typee r_type;
	public RotationAxis axis = RotationAxis.Z;

	[Header("Pendulum Props")]
	public float pendulum_dist = 1f;
	public float pendumum_time = 1f;

	[Header("Rotation Props")]
	public float speed = 0f;

	void Start()
	{
		
		if(r_type==Typee.Pendulum)
		{
			if(axis==RotationAxis.X){
				Vector3 ang = transform.localEulerAngles;
				ang.x-=pendulum_dist;
				transform.localEulerAngles = ang;
				float tang = ang.x;
				iTween.RotateTo(gameObject,iTween.Hash("x",(tang+(pendulum_dist*2)),"easeType",iTween.EaseType.easeInOutSine,"loopType",iTween.LoopType.pingPong,"time",pendumum_time,"islocal",true));
			}
			else if(axis==RotationAxis.Y){
				Vector3 ang = transform.localEulerAngles;
				ang.y-=pendulum_dist;
				transform.localEulerAngles = ang;
				float tang = ang.y;
				iTween.RotateTo(gameObject,iTween.Hash("y",(tang+(pendulum_dist*2)),"easeType",iTween.EaseType.easeInOutSine,"loopType",iTween.LoopType.pingPong,"time",pendumum_time,"islocal",true));
			}
			else if(axis==RotationAxis.Z){
				Vector3 ang = transform.localEulerAngles;
				ang.z-=pendulum_dist;
				transform.localEulerAngles = ang;
				float tang = ang.z;
				iTween.RotateTo(gameObject,iTween.Hash("z",(tang+(pendulum_dist*2)),"easeType",iTween.EaseType.easeInOutSine,"loopType",iTween.LoopType.pingPong,"time",pendumum_time,"islocal",true));
			}
		}

	}

	void Update () 
	{
		if(r_type==Typee.Rotation){
			if(speed==0) return;
			if(axis==RotationAxis.X){	transform.Rotate(speed,0,0); }
			if(axis==RotationAxis.Y){	transform.Rotate(0,speed,0); }
			if(axis==RotationAxis.Z){	transform.Rotate(0,0,speed); }
		}
	}
}
