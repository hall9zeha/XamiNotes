using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace XamiNotes.Clases
{
    public class Metodos
    {
        ///<summary>
        ///Este método cambia el tipo de letra del contenido de la nota seleccionada, al mostrarse en detalles de nota
        ///</summary>
        /// <remarks>
        /// Este método cambia el tipo de letra del contenido de la nota seleccionada, al mostrarse en detalles de nota
        /// </remarks> 
        /// <example>
        /// <code>
        /// CambiarFont(int idFont, Activity myActivity , EditText texto, EditText titulo);
        /// </code>
        /// </example>
        /// <param name="context">Representa la activity donde se llama al método, se colocoará cono "this" </param>
        /// <param name="detalleNota">Representa el objeto de nuestro EditText, para nuestro caso </param>
        /// <param name="idFont">Representa el entero que traeremos desde una base de datos SQLite, con el id del Font </param>

        public static void CambiarFont(int idFont, Activity context, EditText detalleNota, EditText tituloNota)
        {
            
            switch (idFont) {
                 
                case 1:
                   Typeface xamiFont = context.Resources.GetFont(Resource.Font.lora_italic);
                    detalleNota.Typeface = xamiFont;
                    tituloNota.Typeface = xamiFont;
                    break;
                case 2:
                    Typeface xamiFont1 = context.Resources.GetFont(Resource.Font.bangers);
                    detalleNota.Typeface = xamiFont1;
                    tituloNota.Typeface = xamiFont1;
                    break;
                case 3:
                    Typeface xamiFont2 = context.Resources.GetFont(Resource.Font.homemade);
                    detalleNota.Typeface = xamiFont2;
                    tituloNota.Typeface = xamiFont2;
                    break;
                case 4:
                    Typeface xamiFont3 = context.Resources.GetFont(Resource.Font.Indie_flower);
                    detalleNota.Typeface = xamiFont3;
                    tituloNota.Typeface = xamiFont3;
                    break;
                case 5:
                    Typeface xamiFont4 = context.Resources.GetFont(Resource.Font.lobster);
                    detalleNota.Typeface = xamiFont4;
                    tituloNota.Typeface = xamiFont4;
                    break;
                case 6:
                    Typeface xamiFont5 = context.Resources.GetFont(Resource.Font.lora_italic);
                    detalleNota.Typeface = xamiFont5;
                    tituloNota.Typeface = xamiFont5;
                    break;
                case 7:
                    Typeface xamiFont6 = context.Resources.GetFont(Resource.Font.lora_variable);
                    detalleNota.Typeface = xamiFont6;
                    tituloNota.Typeface = xamiFont6;
                    break;
                case 8:
                    Typeface xamiFont7 = context.Resources.GetFont(Resource.Font.monoton);
                    detalleNota.Typeface = xamiFont7;
                    tituloNota.Typeface = xamiFont7;
                    break;
                case 9:
                    Typeface xamiFont8 = context.Resources.GetFont(Resource.Font.pacifico);
                    detalleNota.Typeface = xamiFont8;
                    tituloNota.Typeface = xamiFont8;
                    break;
                case 10:
                    Typeface xamiFont9 = context.Resources.GetFont(Resource.Font.permanent_maker);
                    detalleNota.Typeface = xamiFont9;
                    tituloNota.Typeface = xamiFont9;
                    break;
                case 11:
                    Typeface xamiFont10 = context.Resources.GetFont(Resource.Font.presregular);
                    detalleNota.Typeface = xamiFont10;
                    tituloNota.Typeface = xamiFont10;
                    break;
                default:
                    Typeface xamiFont11 = context.Resources.GetFont(Resource.Font.lora_italic);
                    detalleNota.Typeface = xamiFont11;
                    tituloNota.Typeface = xamiFont11;
                    break;
            }
        }
        //Aplicando propiedad de sobrecarga a un mismo método
        public static void CambiarFont(int idFont, Activity context, TextView detalleNota, TextView tituloNota)
        {

            switch (idFont)
            {

                case 1:
                    Typeface xamiFont = context.Resources.GetFont(Resource.Font.lora_italic);
                    detalleNota.Typeface = xamiFont;
                    tituloNota.Typeface = xamiFont;
                    break;
                case 2:
                    Typeface xamiFont1 = context.Resources.GetFont(Resource.Font.bangers);
                    detalleNota.Typeface = xamiFont1;
                    tituloNota.Typeface = xamiFont1;
                    break;
                case 3:
                    Typeface xamiFont2 = context.Resources.GetFont(Resource.Font.homemade);
                    detalleNota.Typeface = xamiFont2;
                    tituloNota.Typeface = xamiFont2;
                    break;
                case 4:
                    Typeface xamiFont3 = context.Resources.GetFont(Resource.Font.Indie_flower);
                    detalleNota.Typeface = xamiFont3;
                    tituloNota.Typeface = xamiFont3;
                    break;
                case 5:
                    Typeface xamiFont4 = context.Resources.GetFont(Resource.Font.lobster);
                    detalleNota.Typeface = xamiFont4;
                    tituloNota.Typeface = xamiFont4;
                    break;
                case 6:
                    Typeface xamiFont5 = context.Resources.GetFont(Resource.Font.lora_italic);
                    detalleNota.Typeface = xamiFont5;
                    tituloNota.Typeface = xamiFont5;
                    break;
                case 7:
                    Typeface xamiFont6 = context.Resources.GetFont(Resource.Font.lora_variable);
                    detalleNota.Typeface = xamiFont6;
                    tituloNota.Typeface = xamiFont6;
                    break;
                case 8:
                    Typeface xamiFont7 = context.Resources.GetFont(Resource.Font.monoton);
                    detalleNota.Typeface = xamiFont7;
                    tituloNota.Typeface = xamiFont7;
                    break;
                case 9:
                    Typeface xamiFont8 = context.Resources.GetFont(Resource.Font.pacifico);
                    detalleNota.Typeface = xamiFont8;
                    tituloNota.Typeface = xamiFont8;
                    break;
                case 10:
                    Typeface xamiFont9 = context.Resources.GetFont(Resource.Font.permanent_maker);
                    detalleNota.Typeface = xamiFont9;
                    tituloNota.Typeface = xamiFont9;
                    break;
                case 11:
                    Typeface xamiFont10 = context.Resources.GetFont(Resource.Font.presregular);
                    detalleNota.Typeface = xamiFont10;
                    tituloNota.Typeface = xamiFont10;
                    break;
                default:
                    Typeface xamiFont11 = context.Resources.GetFont(Resource.Font.lora_italic);
                    detalleNota.Typeface = xamiFont11;
                    tituloNota.Typeface = xamiFont11;
                    break;
            }
        }
        public static int RegistrarFont(Activity context, IMenuItem item, EditText tituloNota, EditText detalleNota,  int idFont)
        {
            int font = 0;
            switch (item.TitleFormatted.ToString()) {

                case  "Bangers":
                    font = 1;
                    Typeface xamiFont = context.Resources.GetFont(Resource.Font.lora_italic);
                    detalleNota.Typeface = xamiFont;
                    tituloNota.Typeface = xamiFont;
                    break;
                case "Dosis":
                    font = 2;
                    Typeface xamiFont1 = context.Resources.GetFont(Resource.Font.bangers);
                    detalleNota.Typeface = xamiFont1;
                    tituloNota.Typeface = xamiFont1;
                    break;
                case "HomeMade":
                    font = 3;
                    Typeface xamiFont2 = context.Resources.GetFont(Resource.Font.homemade);
                    detalleNota.Typeface = xamiFont2;
                    tituloNota.Typeface = xamiFont2;
                    break;
                case "Indie":
                    font = 4;
                    Typeface xamiFont3 = context.Resources.GetFont(Resource.Font.Indie_flower);
                    detalleNota.Typeface = xamiFont3;
                    tituloNota.Typeface = xamiFont3;
                    break;
                case "Lobster":
                    font = 5;
                    Typeface xamiFont4 = context.Resources.GetFont(Resource.Font.lobster);
                    detalleNota.Typeface = xamiFont4;
                    tituloNota.Typeface = xamiFont4;
                    break;
                case "LoraItalic":
                    font = 6;
                    Typeface xamiFont5 = context.Resources.GetFont(Resource.Font.lora_italic);
                    detalleNota.Typeface = xamiFont5;
                    tituloNota.Typeface = xamiFont5;
                    break;
                case "LoraVariable":
                    font = 7;
                    Typeface xamiFont6 = context.Resources.GetFont(Resource.Font.lora_variable);
                    detalleNota.Typeface = xamiFont6;
                    tituloNota.Typeface = xamiFont6;
                    break;
                case "Monoton":
                    font = 8;
                    Typeface xamiFont7 = context.Resources.GetFont(Resource.Font.monoton);
                    detalleNota.Typeface = xamiFont7;
                    tituloNota.Typeface = xamiFont7;
                    break;
                case "Pacifico":
                    font = 9;
                    Typeface xamiFont8 = context.Resources.GetFont(Resource.Font.pacifico);
                    detalleNota.Typeface = xamiFont8;
                    tituloNota.Typeface = xamiFont8;
                    break;
                case "Permanent":
                    font = 10;
                    Typeface xamiFont9 = context.Resources.GetFont(Resource.Font.permanent_maker);
                    detalleNota.Typeface = xamiFont9;
                    tituloNota.Typeface = xamiFont9;
                    break;
                case "PresRegular":
                    font = 11;
                    Typeface xamiFont10 = context.Resources.GetFont(Resource.Font.presregular);
                    detalleNota.Typeface = xamiFont10;
                    tituloNota.Typeface = xamiFont10;
                    break;
                default:
                    font =idFont;
                    break;
                    

                    
            }
            return font;
        }
        public static void CambiarColorNotaHolder(View separador, CardView card,  int idColorNota)
        {
            switch (idColorNota)
            {
                case 1:
                    card.SetCardBackgroundColor(Android.Graphics.Color.LightYellow);
                    separador.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFC107"));
                    break;
                case 2:
                    card.SetCardBackgroundColor(Android.Graphics.Color.LightGreen);
                    separador.SetBackgroundColor(Android.Graphics.Color.ParseColor("#4CAF50"));
                    break;
                case 3:
                    card.SetCardBackgroundColor(Android.Graphics.Color.LightBlue);
                    separador.SetBackgroundColor(Android.Graphics.Color.ParseColor("#03A9F4"));
                    break;
                case 4:
                    card.SetCardBackgroundColor(Android.Graphics.Color.LightSalmon);
                    separador.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FF5722"));
                    break;
                case 5:
                    card.SetCardBackgroundColor(Android.Graphics.Color.ParseColor("#CE4BEB"));
                    separador.SetBackgroundColor(Android.Graphics.Color.ParseColor("#673AB7"));
                    break;

            }
        

        }
        public static int CambiarColorNotasGeneric(Activity activity,IMenuItem item, int idColor, int idActivity)
        {
            int color = 1;
            if (item != null)
            {
                switch (item.TitleFormatted.ToString())
                {
                    case "Amarillo":
                        color = 1;
                        CambiarColorControles(activity, "#FFC107", Android.Graphics.Color.LightYellow, idActivity);
                        break;
                    case "Verde":
                        color = 2;
                        CambiarColorControles(activity, "#4CAF50", Android.Graphics.Color.LightGreen, idActivity);
                        break;
                    case "Azul":
                        color = 3;
                        CambiarColorControles(activity, "#03A9F4", Android.Graphics.Color.LightBlue, idActivity);
                        break;
                    case "Naranja":
                        color = 4;
                        CambiarColorControles(activity, "#FF5722", Android.Graphics.Color.LightSalmon, idActivity);
                        break;
                    case "Violeta":
                        color = 5;
                        CambiarColorControles(activity, "#673AB7", Android.Graphics.Color.ParseColor("#CE4BEB"), idActivity);
                        break;
                    default:
                        color = idColor;
                        break;
                }
            }
            else {
                switch (idColor)
                {
                    case 1:
                        
                        CambiarColorControles(activity, "#FFC107", Android.Graphics.Color.LightYellow, idActivity);
                        break;
                    case 2:
                        
                        CambiarColorControles(activity, "#4CAF50", Android.Graphics.Color.LightGreen, idActivity);
                        break;
                    case 3:
                        
                        CambiarColorControles(activity, "#03A9F4", Android.Graphics.Color.LightBlue, idActivity);
                        break;
                    case 4:
                       
                        CambiarColorControles(activity, "#FF5722", Android.Graphics.Color.LightSalmon, idActivity);
                        break;
                    case 5:
                        
                        CambiarColorControles(activity, "#673AB7", Android.Graphics.Color.ParseColor("#CE4BEB"), idActivity);
                        break;
                    default:
                        CambiarColorControles(activity, "#FFC107", Android.Graphics.Color.LightYellow, idActivity);
                        break;

                }
            }
            return color;
        }
        public static void CambiarColorControles(Activity activity, String color, Android.Graphics.Color color1, int idActivity)
        {
            if (idActivity == 1)
            {
                //controles de activity NuevaNota
                EditText nuevaNota, tituloNota;
                LinearLayout nuevaNotaLinear, linearTituloNota;
                ColorDrawable colorBackground;
                nuevaNota = activity.FindViewById<EditText>(Resource.Id.nuevaNotaEditText);
                tituloNota = activity.FindViewById<EditText>(Resource.Id.tituloEditText);
                nuevaNotaLinear = activity.FindViewById<LinearLayout>(Resource.Id.nuevaNotaLinearLayout);
                linearTituloNota = activity.FindViewById<LinearLayout>(Resource.Id.linearTituloNuevaNota);

                nuevaNota.SetBackgroundColor(color1);
                tituloNota.SetBackgroundColor(color1);
                nuevaNotaLinear.SetBackgroundColor(color1);
                linearTituloNota.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
                colorBackground = new ColorDrawable(Android.Graphics.Color.ParseColor(color));
                activity.ActionBar.SetBackgroundDrawable(colorBackground);
                activity.Window.SetStatusBarColor(Android.Graphics.Color.ParseColor(color));
                //*******************************************************************************************
            }
            else if (idActivity == 2)
            {
                //Controles de Activity DetalleNota
                LinearLayout detalleLinear, tituloDetalleLinear, linearTitulo, linearFecha, linearFechaUpdate;
                EditText detalleNota;



                detalleNota = activity.FindViewById<EditText>(Resource.Id.detalleNotaEditText);

                detalleLinear = activity.FindViewById<LinearLayout>(Resource.Id.detalleNotaLinearLayout);
                tituloDetalleLinear = activity.FindViewById<LinearLayout>(Resource.Id.tituloDetalleLinear);
                linearTitulo = activity.FindViewById<LinearLayout>(Resource.Id.linearTitulo);
                linearFecha = activity.FindViewById<LinearLayout>(Resource.Id.linearFecha);
                linearFechaUpdate = activity.FindViewById<LinearLayout>(Resource.Id.linearFechaUpdate);
                ColorDrawable colorBackground;


                detalleNota.SetBackgroundColor(color1);
                detalleLinear.SetBackgroundColor(color1);
                //Colores de los Linear y Toolbar
                colorBackground = new ColorDrawable(Android.Graphics.Color.ParseColor(color));
                activity.ActionBar.SetBackgroundDrawable(colorBackground);
                tituloDetalleLinear.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
                linearFecha.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
                linearFechaUpdate.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
                linearTitulo.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
                //Cambiar el color de la barra de estado
                activity.Window.SetStatusBarColor(Android.Graphics.Color.ParseColor(color));
                //*****************************************************************************
            }
        }
        
        public static void FormatoTextoYFechaContenido(String tituloNota, string contenidoNota,DateTime fechaNota, TextView textNotas, TextView textFecha )
        {
            int cortar = 0;
            int maxCar = 30;
            string contenidoCortado = "";
            //Formato de fecha para mostrar en cardView notas
            string fechaFormateada = fechaNota.ToString("MMMM" + " dd", CultureInfo.CreateSpecificCulture("es-PE"));
            var fecha = fechaFormateada.Substring(0, 1).ToUpper() + fechaFormateada.Substring(1).ToLower();
            if (tituloNota == null || tituloNota == "")
            {
               
                if (contenidoNota.Length > maxCar)
                {
                    cortar = contenidoNota.Length - maxCar;
                    contenidoCortado = contenidoNota.Remove(maxCar, cortar);
                    textNotas.Text = contenidoCortado + "...";
                    textFecha.Text = fecha;


                }
                else if (contenidoNota.Length < maxCar)
                {
                    textNotas.Text = contenidoNota;
                    textFecha.Text = fecha;
                }
                else
                {
                    textNotas.Text = tituloNota;
                    textFecha.Text = fecha;
                }
            }
            else

            {
                if (tituloNota.Length > maxCar)
                {
                    cortar = tituloNota.Length - maxCar;
                    contenidoCortado = tituloNota.Remove(maxCar, cortar);
                    textNotas.Text = contenidoCortado + "...";
                    textFecha.Text = fecha;
                }
                else
                {
                    textNotas.Text = tituloNota;
                    textFecha.Text = fecha;
                }
            }
        }
    }
}