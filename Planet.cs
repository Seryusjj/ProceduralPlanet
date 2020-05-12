using Godot;
using Godot.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[Tool]
public class Planet : Spatial
{
	[Export]
	public ColorSettings colorSettings;

	[Export]
	public ShapeSettings shapeSettings;

	ShapeGenerator shapeGenerator;


	TerrainFace[] terrainFaces;
	MeshInstance[] instances;
	//MeshInstance instance;
	bool PropertyChanged = true;
	const int numFaces = 6;
	Task<ArrayMesh>[] AllTasks = new Task<ArrayMesh>[numFaces + 1];


	public override void _Ready()
	{

		// ready is not call from editor then we have to connect this
		shapeSettings.Connect(nameof(ShapeSettings.PropertyChanged), this, nameof(OnPropertyChanged));
		colorSettings.Connect(nameof(ColorSettings.PropertyChanged), this, nameof(OnPropertyChanged));

		GeneratePlanet();
	}



	public void OnPropertyChanged()
	{
		PropertyChanged = true;
		GeneratePlanet();
		//GD.Print(nameof(OnPropertyChanged));
	}


	public override void _Process(float delta)
	{

	}

	void Init()
	{
		shapeGenerator = new ShapeGenerator(shapeSettings);

		Vector3[] directions = { Vector3.Up, Vector3.Down, Vector3.Left, Vector3.Right, Vector3.Forward, Vector3.Back };
		terrainFaces = new TerrainFace[numFaces];
		if (instances == null)
		{
			instances = new MeshInstance[numFaces];
			for (int i = 0; i < numFaces; ++i)
			{
				instances[i] = new MeshInstance();				
				AddChild(instances[i]);
			}
		}
		for (int i = 0; i < numFaces; ++i)
		{
			terrainFaces[i] = new TerrainFace(shapeGenerator, directions[i]);
		}
	}

	public void OnColorSettingsUpdated()
	{
		GeneratePlanet();
	}

	public void OnShapeSettingsChanged()
	{
		GeneratePlanet();
	}


	public void GeneratePlanet()
	{
		if (PropertyChanged)
		{
			PropertyChanged = false;

			Init();

			//easier but slower api
			//GenerateMeshWithSurfaceTool(); 
			GenerateMeshWithArrayMesh();
		}
	}

	void GenerateMeshWithSurfaceTool()
	{
		for (int i = 0; i < terrainFaces.Length; i++)
		{
			var terrain = terrainFaces[i];

			var mat = new SpatialMaterial();
			mat.AlbedoColor = colorSettings.PlanetColour;
			using (var st = new SurfaceTool())
			{
				st.Begin(Mesh.PrimitiveType.Triangles);
				st.AddSmoothGroup(true);
				st.SetMaterial(mat);
				terrain.ConstructMesh(st);

				st.GenerateNormals();
				var mesh = st.Commit();
				instances[i].Mesh = mesh;
			}
		}
	}

	void GenerateMeshWithArrayMesh()
	{
		for (int i = 0; i < terrainFaces.Length; i++)
		{
			var terrain = terrainFaces[i];
			terrain.ConstructMesh(out Vector3[] vertex, out int[] indexes, out Vector3[] normals);
			var mat = new SpatialMaterial();
			mat.AlbedoColor = colorSettings.PlanetColour;

			var mesh = new ArrayMesh();
			object[] arr = new object[(int)Mesh.ArrayType.Max];
			arr[(int)Mesh.ArrayType.Vertex] = vertex;
			arr[(int)Mesh.ArrayType.Index] = indexes;
			arr[(int)Mesh.ArrayType.Normal] = normals;

			mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, new Godot.Collections.Array(arr));			
			// looks like not working thats why we gen the normals ...
			//mesh.RegenNormalmaps();
			// only one surface in this mesh
			mesh.SurfaceSetMaterial(0, mat);				

			instances[i].Mesh = mesh;
		}
	}
}

