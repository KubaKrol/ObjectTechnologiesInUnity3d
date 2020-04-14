using UnityEngine;

public class HexGridPerlinNoiseGenerator
{
    #region Public Variables

    public float[][] perlinNoise;
    
    #endregion Public Variables


    #region Public Methods

    public HexGridPerlinNoiseGenerator(HexGridSettings hexGridSettings)
    {
        _HexGridSettings = hexGridSettings;
    }
    
    public float[][] GenerateNoise(float seed, float xMultiplier, float yMultiplier)
    {
        perlinNoise = new float[_HexGridSettings.height][];

        for (var i = 0; i < _HexGridSettings.height; i++)
        {
            perlinNoise[i] = new float[_HexGridSettings.width];
            
            for (var j = 0; j < _HexGridSettings.width; j++)
            {
                perlinNoise[i][j] = Mathf.PerlinNoise((i + seed) * xMultiplier, (j + seed) * yMultiplier);
            }
        }

        return perlinNoise;
    }
    
    #endregion Public Methods
    

    #region Private Variables
    
    private HexGridSettings _HexGridSettings;

    #endregion Private Variables
}
