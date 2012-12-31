using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public class MGPrimitives
    {
        public static void DrawPoint(Vector2 point, Color color)
        {
            var vertexData = new[]
                                 {
                                     new VertexPositionColor(new Vector3(point, 0f), color),
                                     new VertexPositionColor(new Vector3(point.X + 1f, point.Y, 0f), color),
                                     new VertexPositionColor(new Vector3(point.X, point.Y + 1f, 0f), color)
                                 };
            MGDirector director = MGDirector.SharedDirector();
            director.BasicEffect.CurrentTechnique.Passes[0].Apply();
            director.Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertexData, 0, 1);
        }

        public static void DrawLine(Vector2 point1, Vector2 point2, Color color)
        {
            var vertexData = new[]
                                 {
                                     new VertexPositionColor(new Vector3(point1.X, point1.Y, 0f), color),
                                     new VertexPositionColor(new Vector3(point2.X, point2.Y, 0f), color)
                                 };
            MGDirector director = MGDirector.SharedDirector();
            director.BasicEffect.CurrentTechnique.Passes[0].Apply();
            director.Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertexData, 0, 1);
        }

        public static void DrawPoly(Vector2[] points, Color color)
        {
            var array = new VertexPositionColor[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                array[i] = new VertexPositionColor(new Vector3(points[i], 0f), color);
            }
            MGDirector director = MGDirector.SharedDirector();
            foreach (EffectPass current in director.BasicEffect.CurrentTechnique.Passes)
            {
                current.Apply();
                director.Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, array, 0,
                                                                    points.Length - 2);
            }
        }

        public static void DrawCircle(Vector2 center, float r, float a, int segs, Color color)
        {
            float num = 6.28318548f/segs;
            var array = new VertexPositionColor[segs + 1];
            for (int i = 0; i <= segs; i++)
            {
                float num2 = i*num;
                float x = r*(float) Math.Cos((num2 + a)) + center.X;
                float y = r*(float) Math.Sin((num2 + a)) + center.Y;
                array[i] = new VertexPositionColor(new Vector3(x, y, 0f), color);
            }
            MGDirector director = MGDirector.SharedDirector();
            director.BasicEffect.CurrentTechnique.Passes[0].Apply();
            director.Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, array, 0, segs);
        }
    }
}