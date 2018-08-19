using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshShortingLayer : MonoBehaviour {
	[SerializeField] string SortingLayerName = "Default";
	[SerializeField] int OrderInLayer = 0;
	// Use this for initialization
	MeshRenderer meshRenderer;
	void Awake () {
		meshRenderer = GetComponent<MeshRenderer>();
	}

	void Start()
	{
		UpdateSortingLayer();
	}
	
	void UpdateSortingLayer()
	{
		meshRenderer.sortingLayerName = this.SortingLayerName;
		meshRenderer.sortingOrder = OrderInLayer;
	}

	void OnValidate()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		UpdateSortingLayer();
	}
}
