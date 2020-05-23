using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour
{
	public bool mWillGrowPool = true;
	//Prefab to Object pool
	private GameObject mPrefab;
	//Is it able to Grow the Pool

	//Define Pool Size
	private int mPoolSize = 5;
	
	private List <GameObject> mPooledObjects;

	private GameObject mObject;

	//Instantiate the Prefabs and assign to the List for Pooling
	public void InitializePool (GameObject inPrefab, int inPoolSize = 5)
	{
		mPoolSize = inPoolSize;
		
		mPrefab = inPrefab;

		mPooledObjects = new List<GameObject> ();

		for (int index = 0; index < mPoolSize; index++) {
			mObject = GameObject.Instantiate (mPrefab)as GameObject;
			mPooledObjects.Add (mObject);
			mObject.SetActive (false);
		}

		mObject = null;
	}

	///  <summary>To Get the curretly Diactivated Object in the Hierarchy  </summary>
	public GameObject ReleseReusable ()
	{
		for (int index = 0; index < mPooledObjects.Count; index++) 
		{
			if (!mPooledObjects [index].activeInHierarchy) 
			{
				mPooledObjects [index].SetActive (true);
				return mPooledObjects [index];
			}
		}

		if (mWillGrowPool) 
		{
			mObject = GameObject.Instantiate (mPrefab) as GameObject;

			mPooledObjects.Add (mObject);

			return mObject;
		}
		return null;
	}
}