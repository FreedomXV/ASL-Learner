using UnityEngine;
using Unity.Barracuda;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine.UI;
using System;

public class Classification : MonoBehaviour

{
	[SerializeField] GameObject nextPanel;
	[SerializeField] GameObject tick;

	public AudioSource audioSource;
	public AudioClip tickSound;


	const int IMAGE_SIZE = 640;
	const string INPUT_NAME = "images";
	const string OUTPUT_NAME = "output";

	[Header("Model Stuff")]
	public NNModel modelFile;
	public TextAsset labelAsset;

	[Header("Scene Stuff")]
	public CameraView CameraView;
	public Preprocess preprocess;
	public Text uiText;

	string[] labels;
	IWorker worker;

	int pass = 0;

	void Start()
	{
		Model model = ModelLoader.Load(modelFile);
		//Debug.Log("loaded model");
		//Debug.Log("IRSource");
		//Debug.Log(model.IrSource);
		//Debug.Log("irversion");
		//Debug.Log(model.IrVersion);
		//Debug.Log("layout");
		//Debug.Log(model.layout);
		worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, model);
		//foreach (var layer in model.layers)
		//	Debug.Log(layer.name + " does " + layer.type);
		LoadLabels();
	}

	void LoadLabels()
	{
		////get only items in quotes
		//var stringArray = labelAsset.text.Split('"').Where((item, index) => index % 2 != 0);
		////get every other item
		//labels = stringArray.Where((x, i) => i % 2 != 0).ToArray();
		string[] temp = {"No Detection", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "No Detection", "No Detection" , "No Detection" , "No Detection"};
		labels = temp;
		//Debug.Log("old labels loaded");
		//Debug.Log(labels);
	}

	void Update()
	{

        WebCamTexture webCamTexture = CameraView.GetCamImage();

        if (webCamTexture.didUpdateThisFrame && webCamTexture.width > 100 && pass == 0)
        {
            preprocess.ScaleAndCropImage(webCamTexture, IMAGE_SIZE, RunModel);
        }

        //string camName = WebCamTexture.devices[0].name;
        //      WebCamTexture webCamTexture;
        //      webCamTexture = new WebCamTexture(camName);

        //preprocess.ScaleAndCropImage(webCamTexture, IMAGE_SIZE, RunModel);
    }

	void RunModel(byte[] pixels)
	{
		//Debug.Log("attempting to run model");
		StartCoroutine(RunModelRoutine(pixels));
	}

	IEnumerator RunModelRoutine(byte[] pixels)
	{

		if (pass == 0)
        {
			Tensor tensor = TransformInput(pixels);
			//Debug.Log("tensor fed pixels");

			var inputs = new Dictionary<string, Tensor> {
				{ INPUT_NAME, tensor }
			};
			
			//Debug.Log("worker attempting execute");
			worker.Execute(inputs);
			Tensor outputTensor = new Tensor(1, 1, 31, 25200);
			outputTensor = worker.PeekOutput(OUTPUT_NAME);
			//Debug.Log("worker execute success, output tensor initialised");

			////get largest output
			List<float> temp = outputTensor.ToReadOnlyArray().ToList();
			float max = temp.Max();
			int index = temp.IndexOf(max);
			//Debug.Log("output tensor to list");
			//Debug.Log(max);
			//Debug.Log(index);
			//Debug.Log("size of array");
			//Debug.Log(temp.Count);

			var result = index;
			////set UI text
			uiText.text = result.ToString();

			if (result > 70000)
			{
				LevelPass();
				pass = 1;
				tensor.Dispose();
				outputTensor.Dispose();
			};

			tensor.Dispose();
			outputTensor.Dispose();
		};

		yield return null;
	}

	//transform from 0-255 to -1 to 1
	Tensor TransformInput(byte[] pixels)
	{
		float[] transformedPixels = new float[pixels.Length];

		for (int i = 0; i < pixels.Length; i++)
		{
			transformedPixels[i] = (pixels[i] - 127f) / 128f;
		}
		return new Tensor(1, IMAGE_SIZE, IMAGE_SIZE, 3, transformedPixels);
	}

	public void LevelPass()
	{
		StartCoroutine(LevelFinish());
	}

	IEnumerator LevelFinish()
	{
		tick.SetActive(true);
		audioSource.clip = tickSound;
		audioSource.Play();
		yield return new WaitForSeconds(1f);
		nextPanel.SetActive(true);
	}
}