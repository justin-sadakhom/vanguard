using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsMenu : MonoBehaviour {

	private Component[] dropdowns;
	private Resolution[] res;
	private Dropdown resolution;
	private Dropdown aliasing;
	private Dropdown filtering;
	private Dropdown vsync;
	private Dropdown particle;
	private Dropdown shadow1;
	private Dropdown shadow2;

	void Start() {

		dropdowns = GetComponentsInChildren<Dropdown>();
		resolution = (Dropdown)dropdowns[0];
		resolution.ClearOptions();
		res = Screen.resolutions;

		for (int i = 0; i < res.Length; i++) {
			resolution.options.Add(new Dropdown.OptionData(ResToString(res[i])));
			resolution.value = i;
			resolution.onValueChanged.AddListener(delegate{Screen.SetResolution(res[resolution.value].width, res[resolution.value].height, true);});
		}

		List<string> AAOptions = new List<string>();
		AAOptions.Add("None");
		AAOptions.Add("2x FXAA");
		AAOptions.Add("4x FXAA");
		AAOptions.Add("8x FXAA");

		aliasing = (Dropdown)dropdowns[1];
		aliasing.ClearOptions();
		aliasing.AddOptions(AAOptions);

		List<string> AnisOptions = new List<string>();
		AnisOptions.Add("Enabled");
		AnisOptions.Add("Disabled");

		filtering = (Dropdown)dropdowns[2];
		filtering.ClearOptions();
		filtering.AddOptions(AnisOptions);

		List<string> VsyncOptions = new List<string>();
		VsyncOptions.Add("Don't Sync");
		VsyncOptions.Add("One Sync Per Frame");
		VsyncOptions.Add("Two Syncs Per Frame");

		vsync = (Dropdown)dropdowns[3];
		vsync.ClearOptions();
		vsync.AddOptions(VsyncOptions);

		List<string> ParticleOptions = new List<string>();
		ParticleOptions.Add("Soft Particles");
		ParticleOptions.Add("Hard Particles");

		particle = (Dropdown)dropdowns[4];
		particle.ClearOptions();
		particle.AddOptions(ParticleOptions);

		List<string> ShadowOptions1 = new List<string>();
		ShadowOptions1.Add("Soft & Hard Shadows");
		ShadowOptions1.Add("Hard Shadows Only");
		ShadowOptions1.Add("No Shadows");

		shadow1 = (Dropdown)dropdowns[5];
		shadow1.ClearOptions();
		shadow1.AddOptions(ShadowOptions1);

		List<string> ShadowOptions2 = new List<string>();
		ShadowOptions2.Add("Very High");
		ShadowOptions2.Add("High");
		ShadowOptions2.Add("Medium");
		ShadowOptions2.Add("Low");

		shadow2 = (Dropdown)dropdowns[6];
		shadow2.ClearOptions();
		shadow2.AddOptions(ShadowOptions2);
	}

	void FixedUpdate() {

		QualitySettings.antiAliasing = aliasing.value;

		if (filtering.value == 0)
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
		else if (filtering.value == 1)
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;

		QualitySettings.vSyncCount = vsync.value;

		if (particle.value == 0)
			QualitySettings.softParticles = true;
		else if (particle.value == 1)
			QualitySettings.softParticles = false;

		if (shadow1.value == 0)
			QualitySettings.shadows = ShadowQuality.All;
		else if (shadow1.value == 1)
			QualitySettings.shadows = ShadowQuality.HardOnly;
		else if (shadow1.value == 2)
			QualitySettings.shadows = ShadowQuality.Disable;

		if (shadow2.value == 0)
			QualitySettings.shadows = (ShadowQuality)ShadowResolution.VeryHigh;
		else if (shadow2.value == 1)
			QualitySettings.shadows = (ShadowQuality)ShadowResolution.High;
		else if (shadow2.value == 2)
			QualitySettings.shadows = (ShadowQuality)ShadowResolution.Medium;
		else if (shadow2.value == 3)
			QualitySettings.shadows = (ShadowQuality)ShadowResolution.Low;
	}

	private string ResToString(Resolution res) {
		return res.width + " x " + res.height;
	}
}