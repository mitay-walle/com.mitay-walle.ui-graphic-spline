using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

namespace Plugins
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	[RequireComponent(typeof(SplineExtrude))]
	[RequireComponent(typeof(CanvasRenderer))]
	[RequireComponent(typeof(SplineContainer))]
	public sealed class UISplineGraphic : MaskableGraphic
	{
		[SerializeField] private Texture texture;
		public override Texture mainTexture => texture ? texture : s_WhiteTexture;
		private MeshFilter meshFilter;
		private int lastHash;

		protected override void OnEnable()
		{
			if (meshFilter == null) meshFilter = GetComponent<MeshFilter>();
			if (meshFilter.sharedMesh != null) lastHash = meshFilter.sharedMesh.GetHashCode();
			Rebuild();
			base.OnEnable();
		}

		private void Update()
		{
			if (meshFilter == null) return;
			if (meshFilter.sharedMesh == null) return;

			if (lastHash != meshFilter.sharedMesh.GetHashCode())
			{
				Rebuild();
			}
		}

		private void Rebuild()
		{
			canvasRenderer.SetMesh(meshFilter.sharedMesh);
			canvasRenderer.SetMaterial(materialForRendering, texture);
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
		}

		protected override void Reset()
		{
			GetComponent<MeshRenderer>().enabled = false;
			base.Reset();
		}
	}
}
