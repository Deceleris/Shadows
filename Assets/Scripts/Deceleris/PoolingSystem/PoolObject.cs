using UnityEngine;
using System.Collections;

public class PoolObject : MonoBehaviour
{

	Pool pool;

	public virtual void OnCreate (Pool pool)
	{
		this.pool = pool;
		pool.inactives.Insert(0, this);
		pool.instances.Insert(0, this);
		gameObject.SetActive(false);
	}

	public virtual void Activate()
	{
		pool.inactives.Remove(this);
		gameObject.SetActive(true);
	}

	public virtual void Desactivate()
	{
		pool.inactives.Add(this);
		gameObject.SetActive(false);
	}
}