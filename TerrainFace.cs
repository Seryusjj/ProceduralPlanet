using Godot;
using System;

public class TerrainFace
{


    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;
    ShapeGenerator shapeGenerator;


    public TerrainFace(ShapeGenerator shapeGenerator, Vector3 upVector)
    {
        this.resolution = shapeGenerator.Settings.Resolution;
        this.shapeGenerator = shapeGenerator;
        localUp = upVector;
        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = axisA.Cross(localUp);
    }


    public void ConstructMesh(SurfaceTool st)
    {
        int i = 0;
        for (int y = 0; y < resolution; ++y)
        {
            for (int x = 0; x < resolution; ++x)
            {
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                // fit point on plane (the plane is always same size but different number of points)
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.Normalized();

                st.AddVertex(shapeGenerator.CalculatePopintOnPlanet(pointOnUnitSphere));

                // calculate the vertex that will draw the triangle, clockwise
                if (x != resolution - 1 && y != resolution - 1)
                {
                    st.AddIndex(i);
                    st.AddIndex(i + resolution + 1);
                    st.AddIndex(i + resolution);

                    st.AddIndex(i);
                    st.AddIndex(i + 1);
                    st.AddIndex(i + resolution + 1);
                }
                i++;
            }
        }
    }

    public void ConstructMesh(out Vector3[] vertices, out int[] triangles, out Vector3[] vertexNormals)
    {
        vertices = new Vector3[resolution * resolution];
        vertexNormals = new Vector3[resolution * resolution];
        triangles = new int[(resolution - 1) * (resolution - 1) * 6];

        int triIndex = 0;
        int i = 0;
        for (int y = 0; y < resolution; ++y)
        {
            for (int x = 0; x < resolution; ++x)
            {
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                // fit point on plane (the plane is always same size but different number of points)
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.Normalized();

                vertices[i] = shapeGenerator.CalculatePopintOnPlanet(pointOnUnitSphere);

                // calculate the vertex that will draw the triangle, clockwise
                if (x != resolution - 1 && y != resolution - 1)
                {
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i + resolution + 1;
                    triangles[triIndex + 2] = i + resolution;

                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;
                    triIndex += 6;
                }
                i++;
            }
        }

        // Normals calculation
        // Zero-out our normal buffer to start from a clean slate.
        for (int v = 0; v < vertices.Length; v++)
            vertexNormals[v] = Vector3.Zero;
        // For each face, compute the face normal, and accumulate it into each vertex
        for (int index = 0; index < triangles.Length; index += 3)
        {
            int vertexA = triangles[index];
            int vertexB = triangles[index + 1];
            int vertexC = triangles[index + 2];

            var edgeAB = vertices[vertexB] - vertices[vertexA];
            var edgeAC = vertices[vertexC] - vertices[vertexA];

            // The cross product is perpendicular to both input vectors (normal to the plane).
            // Flip the argument order if you need the opposite winding.    
            //var areaWeightedNormal = edgeAB.Cross(edgeAC);
            var areaWeightedNormal = edgeAC.Cross(edgeAB);
            vertexNormals[vertexA] += areaWeightedNormal;
            vertexNormals[vertexB] += areaWeightedNormal;
            vertexNormals[vertexC] += areaWeightedNormal;
        }

        // Finally, normalize all the sums to get a unit-length, area-weighted average.
        for (int v = 0; v < vertices.Length; v++)
            vertexNormals[v] = vertexNormals[v].Normalized();

    }


}
