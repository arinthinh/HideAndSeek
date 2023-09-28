/*
An custom editor version of Auto9Slicer - kyubuns.
Customized by Pham Phuc (Arin) Thinh - Gadgame.com
*/
#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Toolkit.AutoNineSlice
{
    #region EditorWindow
    public class AutoNineSlices : EditorWindow
    {
        [MenuItem("Tools/Toolkit/Auto 9 Slices Tool")]
        public static void AutoNineSlicesTool() => EditorWindow.GetWindow<AutoNineSlices>("Auto 9 Slices Tool");
        public static bool isCreateBackup = true;
        public static UnityEngine.Object objectToSlice;
        public static int tolerate = 0;
        public static int centerSize = 2;
        public static int margin = 2;

        public static void Slice(UnityEngine.Object objectToSlice, SliceOptions sliceOption)
        {
            var directoryPath = AssetDatabase.GetAssetPath(objectToSlice);
            if (directoryPath == null) throw new Exception($"directoryPath == null");

            var importer = AssetImporter.GetAtPath(directoryPath);
            if (importer is TextureImporter textureImporter)
            {
                if (textureImporter.spriteBorder != Vector4.zero) return;
                var fullPath = Path.Combine(Path.GetDirectoryName(Application.dataPath) ?? "", directoryPath);
                var bytes = File.ReadAllBytes(fullPath);

                if (isCreateBackup)
                {
                    var fileName = Path.GetFileNameWithoutExtension(fullPath);
                    File.WriteAllBytes(Path.Combine(Path.GetDirectoryName(fullPath) ?? "", fileName + ".original" + Path.GetExtension(fullPath)), bytes);
                }

                var targetTexture = new Texture2D(2, 2);
                targetTexture.LoadImage(bytes);

                var slicedTexture = Slicer.Slice(targetTexture, sliceOption);
                textureImporter.textureType = TextureImporterType.Sprite;
                textureImporter.spriteBorder = slicedTexture.Border.ToVector4();
                if (fullPath.EndsWith(".png")) File.WriteAllBytes(fullPath, slicedTexture.Texture.EncodeToPNG());
                if (fullPath.EndsWith(".jpg")) File.WriteAllBytes(fullPath, slicedTexture.Texture.EncodeToJPG());
                if (fullPath.EndsWith(".jpeg")) File.WriteAllBytes(fullPath, slicedTexture.Texture.EncodeToJPG());

                Debug.Log($"Auto 9Slice {Path.GetFileName(directoryPath)} = {textureImporter.spriteBorder}");
            }
            AssetDatabase.Refresh();
        }

        private void OnGUI()
        {
            isCreateBackup = EditorGUILayout.Toggle("Create Backup?", isCreateBackup);
            tolerate = EditorGUILayout.IntField("Tolerate", tolerate);
            centerSize = EditorGUILayout.IntField("CenterSize", centerSize);
            margin = EditorGUILayout.IntField("Margin", margin);
            objectToSlice = EditorGUILayout.ObjectField("Texture To Slice", objectToSlice, typeof(Texture), true);

            if (objectToSlice == null) return;

            SliceOptions sliceOption = new(tolerate, centerSize, margin);

            if (GUILayout.Button("Slices"))
            {
                Slice(objectToSlice, sliceOption);
            }

        }
    }
    #endregion

    #region Helpers

    [Serializable]
    public class SliceOptions
    {
        public int Tolerate = 0;
        public int CenterSize = 2;
        public int Margin = 2;

        public SliceOptions (int tolerate, int centerSize, int margin)
        {
            Tolerate = tolerate;
            CenterSize = centerSize;
            Margin = margin;
        }
    }
    public class SlicedTexture
    {
        public SlicedTexture(Texture2D texture, Border border)
        {
            Texture = texture;
            Border = border;
        }

        public Texture2D Texture { get; }
        public Border Border { get; }
    }

    public struct Border
    {
        public Border(int left, int bottom, int right, int top)
        {
            Left = left;
            Bottom = bottom;
            Right = right;
            Top = top;
        }

        public Vector4 ToVector4()
        {
            return new Vector4(Left, Bottom, Right, Top);
        }

        public int Left { get; }
        public int Bottom { get; }
        public int Right { get; }
        public int Top { get; }
    }

    public static class Slicer
    {
        public static SlicedTexture Slice(Texture2D texture, SliceOptions options)
        {
            return (new Runner(texture, options).Run());
        }

        private class Runner
        {
            private readonly Texture2D _texture;
            private readonly SliceOptions _options;
            private int _width;
            private int _height;
            private Color32[] _pixels;

            public Runner(Texture2D texture, SliceOptions options)
            {
                _texture = texture;
                _options = options;
            }

            public SlicedTexture Run()
            {
                _width = _texture.width;
                _height = _texture.height;
                _pixels = _texture.GetPixels().Select(x => (Color32)x).ToArray();
                for (var i = 0; i < _pixels.Length; ++i) _pixels[i] = _pixels[i].a > 0 ? _pixels[i] : (Color32)Color.clear;

                var xDiffList = CalcDiffList(_width, _height, 1, _width);
                var (xStart, xEnd) = CalcLine(xDiffList);

                var yDiffList = CalcDiffList(1, _width, _width, _height);
                var (yStart, yEnd) = CalcLine(yDiffList);

                var skipX = (xStart == 0 && xEnd == 0);
                var skipY = (yStart == 0 && yEnd == 0);
                var output = GenerateSlicedTexture(xStart, xEnd, yStart, yEnd, skipX, skipY);

                var left = xStart;
                var bottom = yStart;
                var right = (_width - xEnd) - 1;
                var top = (_height - yEnd) - 1;

                if (skipX)
                {
                    left = 0;
                    right = 0;
                }

                if (skipY)
                {
                    top = 0;
                    bottom = 0;
                }

                return new SlicedTexture(output, new Border(left, bottom, right, top));
            }

            private ulong[] CalcDiffList(int lineDelta, int lineLength, int lineSeek, int length)
            {
                var diffList = new ulong[length];
                diffList[0] = ulong.MaxValue;

                for (var i = 1; i < length; ++i)
                {
                    ulong diff = 0;
                    var current = i * lineSeek;
                    for (var j = 0; j < lineLength; ++j)
                    {
                        var prev = current - lineSeek;
                        diff += (ulong)Diff(_pixels[prev], _pixels[current]);
                        current += lineDelta;
                    }
                    diffList[i] = diff;
                }

                return diffList;
            }

            private int Diff(Color32 a, Color32 b)
            {
                var rd = Mathf.Abs(a.r - b.r);
                var gd = Mathf.Abs(a.g - b.g);
                var bd = Mathf.Abs(a.b - b.b);
                var ad = Mathf.Abs(a.a - b.a);
                if (rd <= _options.Tolerate) rd = 0;
                if (gd <= _options.Tolerate) gd = 0;
                if (bd <= _options.Tolerate) bd = 0;
                if (ad <= _options.Tolerate) ad = 0;
                return rd + gd + bd + ad;
            }

            private (int Start, int End) CalcLine(ulong[] list)
            {
                var start = 0;
                var end = 0;
                var tmpStart = 0;
                var tmpEnd = 0;
                for (var i = 0; i < list.Length; ++i)
                {
                    if (list[i] == 0)
                    {
                        tmpEnd = i;
                        continue;
                    }

                    if (end - start < tmpEnd - tmpStart)
                    {
                        start = tmpStart;
                        end = tmpEnd;
                    }

                    tmpStart = i;
                    tmpEnd = i;
                }

                if (end - start < tmpEnd - tmpStart)
                {
                    start = tmpStart;
                    end = tmpEnd;
                }

                start += _options.Margin;
                end -= _options.Margin;

                if (end <= start)
                {
                    start = 0;
                    end = 0;
                }

                return (start, end);
            }

            private Texture2D GenerateSlicedTexture(int xStart, int xEnd, int yStart, int yEnd, bool skipX, bool skipY)
            {
                var outputWidth = _width - (xEnd - xStart) + (skipX ? 0 : _options.CenterSize - 1);
                var outputHeight = _height - (yEnd - yStart) + (skipY ? 0 : _options.CenterSize - 1);
                var outputPixels = new Color[outputWidth * outputHeight];
                for (int x = 0, originalX = 0; x < outputWidth; ++x, ++originalX)
                {
                    if (originalX == xStart && !skipX) originalX += (xEnd - xStart) - _options.CenterSize + 1;
                    for (int y = 0, originalY = 0; y < outputHeight; ++y, ++originalY)
                    {
                        if (originalY == yStart && !skipY) originalY += (yEnd - yStart) - _options.CenterSize + 1;
                        outputPixels[y * outputWidth + x] = Get(originalX, originalY);
                    }
                }

                var output = new Texture2D(outputWidth, outputHeight);
                output.SetPixels(outputPixels);
                return output;
            }

            private Color32 Get(int x, int y)
            {
                return _pixels[y * _width + x];
            }
        }
    }
    #endregion
}
#endif