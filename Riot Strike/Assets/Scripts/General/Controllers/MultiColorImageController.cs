#region imports
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Get;
using XavHelpTo.Know;
#endregion
/// <summary>
/// Cambia el "Color deseado" de <see cref="ImageController"/>
/// Cada cierto tiempo
/// <para>Dependencia con <see cref="ImageController"/></para>
/// </summary>
[RequireComponent(typeof(ImageController))]
public class MultiColorImageController : MonoBehaviour
{
    #region Variable
    private int index = 0;
    [Header("Settings")]
    public ImageController imgController;
    [Space]
    public Color[] colors;
    [Header("Time")]
    public float tick = 1;
    private float count = 0;

    //guardamos el wanted inicial
    private Color wantedInit;
    public bool isPlaying = true;

    #endregion
    #region Events
    private void Start(){
        index = 0;
        if (!imgController) this.Component(out imgController,true);
        wantedInit = imgController.color_want;
    }
    private void Update()
    {

        if (isPlaying && tick.TimerIn(ref count))
        {
            ChangeColor();
        }
        else if (!isPlaying && imgController.color_want != wantedInit)
        {
            //asigna el valor del color_want
            imgController.color_want = wantedInit;
        }

    }
    #endregion
    #region Methods
    /// <summary>
    /// Cambiamos a uno de los colores escogidos
    /// </summary>
    public void ChangeColor(){
        index = colors.Length.DifferentIndex(index);
        imgController.color_want = colors[index];
    }
    #endregion
}
