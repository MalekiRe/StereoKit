﻿using StereoKit;

class DemoMaterial : ITest
{
	Mesh meshSphere;

	Material matDefault;
	Material matWireframe;
	Material matCull;
	Material matTextured;
	Material matAlphaBlend;
	Material matAlphaAdd;
	Material matUnlit;
	Material matParameters;
	Material matPBR;
	Material matUIBox;

	Tex                oldSkyTex;
	SphericalHarmonics oldSkyLight;

	public void Initialize()
	{
		oldSkyTex         = Renderer.SkyTex;
		oldSkyLight       = Renderer.SkyLight;
		Renderer.SkyTex   = Tex.FromCubemapEquirectangular("old_depot.hdr", out SphericalHarmonics lighting);
		Renderer.SkyLight = lighting;

		meshSphere = Mesh.GenerateSphere(1,8);

		/// :CodeSample: Default.Material
		/// If you want to modify the default material, it's recommended to
		/// copy it first!
		matDefault = Default.Material.Copy();
		/// And here's what it looks like:
		/// ![Default Material example]({{site.screen_url}}/MaterialDefault.jpg)
		/// :End:

		/// :CodeSample: Material.Wireframe
		/// Here's creating a simple wireframe material!
		matWireframe = Default.Material.Copy();
		matWireframe.Wireframe = true;
		/// Which looks like this:
		/// ![Wireframe material example]({{site.screen_url}}/MaterialWireframe.jpg)
		/// :End:

		/// :CodeSample: Material.FaceCull Cull.Front
		/// Here's setting FaceCull to Front, which is the opposite of the
		/// default behavior. On a sphere, this is a little hard to see, but
		/// you might notice here that the lighting is for the back side of
		/// the sphere!
		matCull = Default.Material.Copy();
		matCull.FaceCull = Cull.Front;
		/// ![FaceCull material example]({{site.screen_url}}/MaterialCull.jpg)
		/// :End:

		/// :CodeSample: Material.Transparency Transparency.Add
		/// ## Additive Transparency
		/// Here's an example material with additive transparency. 
		/// Transparent materials typically don't write to the depth buffer,
		/// but this may vary from case to case. Note that the material's 
		/// alpha does not play any role in additive transparency! Instead, 
		/// you could make the material's tint darker.
		matAlphaAdd = Default.Material.Copy();
		matAlphaAdd.Transparency = Transparency.Add;
		matAlphaAdd.DepthWrite   = false;
		/// ![Additive transparency example]({{site.screen_url}}/MaterialAlphaAdd.jpg)
		/// :End:

		/// :CodeSample: Material.Transparency Transparency.Blend
		/// ## Alpha Blending
		/// Here's an example material with an alpha blend transparency. 
		/// Transparent materials typically don't write to the depth buffer,
		/// but this may vary from case to case. Here we're setting the alpha
		/// through the material's Tint value, but the diffuse texture's 
		/// alpha and the instance render color's alpha may also play a part
		/// in the final alpha value.
		matAlphaBlend = Default.Material.Copy();
		matAlphaBlend.Transparency = Transparency.Blend;
		matAlphaBlend.DepthWrite   = false;
		matAlphaBlend[MatParamName.ColorTint] = new Color(1, 1, 1, 0.75f);
		/// ![Alpha blend example]({{site.screen_url}}/MaterialAlphaBlend.jpg)
		/// :End:

		matTextured = Default.Material.Copy();
		matTextured[MatParamName.DiffuseTex] = Tex.FromFile("floor.png");

		/// :CodeSample: Default.MaterialUnlit
		matUnlit = Default.MaterialUnlit.Copy();
		matUnlit[MatParamName.DiffuseTex] = Tex.FromFile("floor.png");
		/// ![Unlit material example]({{site.screen_url}}/MaterialUnlit.jpg)
		/// :End:

		/// :CodeSample: Default.MaterialPBR
		/// Occlusion (R), Roughness (G), and Metal (B) are stored
		/// respectively in the R, G and B channels of their texture.
		/// Occlusion can be separated out into a different texture as per
		/// the GLTF spec, so you do need to assign it separately from the
		/// Metal texture.
		matPBR = Default.MaterialPBR.Copy();
		matPBR[MatParamName.DiffuseTex  ] = Tex.FromFile("metal_plate_diff.jpg");
		matPBR[MatParamName.MetalTex    ] = Tex.FromFile("metal_plate_metal.jpg", false);
		matPBR[MatParamName.OcclusionTex] = Tex.FromFile("metal_plate_metal.jpg", false);
		/// ![PBR material example]({{site.screen_url}}/MaterialPBR.jpg)
		/// :End:

		matUIBox = Default.MaterialUIBox.Copy();
		matUIBox["border_size_min"] = 0.01f;
		matUIBox["border_size_max"] = 0.02f;

		matParameters = Default.Material.Copy();
		matParameters[MatParamName.DiffuseTex] = Tex.FromFile("floor.png");
		matParameters[MatParamName.ColorTint ] = Color.HSV(0.6f, 0.7f, 1f);
		matParameters[MatParamName.TexScale  ] = 2;
	}

	int showCount;
	int showGrid = 3;
	void ShowMaterial(Material material, string screenshotName) 
	{
		float x = ((showCount % showGrid)-showGrid/2) * 0.2f;
		float y = ((showCount / showGrid)-showGrid/2) * 0.2f;
		showCount++;

		if (Tests.IsTesting)
			y += 20;

		meshSphere.Draw(material, Matrix.TS(x, y, -0.5f, 0.1f));
		Tests.Screenshot(400, 400, screenshotName, new Vec3(x, y, -0.42f), new Vec3(x,y,-0.5f));
	}

	public void Update()
	{
		showCount=0;
		ShowMaterial(matDefault,    "MaterialDefault.jpg");
		ShowMaterial(matWireframe,  "MaterialWireframe.jpg");
		ShowMaterial(matCull,       "MaterialCull.jpg");
		ShowMaterial(matTextured,   "MaterialTextured.jpg");
		ShowMaterial(matAlphaAdd,   "MaterialAlphaAdd.jpg");
		ShowMaterial(matAlphaBlend, "MaterialAlphaBlend.jpg");
		ShowMaterial(matUnlit,      "MaterialUnlit.jpg");
		ShowMaterial(matPBR,        "MaterialPBR.jpg");
		ShowMaterial(matParameters, "MaterialParameters.jpg");
		ShowMaterial(matUIBox,      "MaterialUIBox.jpg");
	}

	public void Shutdown()
	{
		Renderer.SkyTex   = oldSkyTex;
		Renderer.SkyLight = oldSkyLight;
	}
}