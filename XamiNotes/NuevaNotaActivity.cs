using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamiNotes.DataBase;
using XamiNotes.Modelo;

namespace XamiNotes
{
    [Activity(Label = "NuevaNotaActivity")]
    public class NuevaNotaActivity : Activity
    {
        EditText nuevaNota, tituloNota;
        MisNotas objNotas;
        LinearLayout nuevaNotaLinear;
        ColorDrawable colorBackground;
        //1 = Amarillo, Color por defecto de la nota si no se selecciona ningún color 
        int color = 1;   

        protected override void OnCreate(Bundle savedInstanceState)
        {

            

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NuevaNotalayout);
            var menuToolbar = FindViewById<Toolbar>(Resource.Id.toolbarMenuNotas);
            SetActionBar(menuToolbar);
            ActionBar.Title = "Nueva Nota";

            nuevaNota = FindViewById<EditText>(Resource.Id.nuevaNotaEditText);
            tituloNota = FindViewById<EditText>(Resource.Id.tituloEditText);
            nuevaNotaLinear = FindViewById<LinearLayout>(Resource.Id.nuevaNotaLinearLayout);
            nuevaNotaLinear.SetBackgroundColor(Android.Graphics.Color.LightYellow);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            // Create your application here
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.NuevaNotaMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
           
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            if (item.TitleFormatted.ToString() == "Guardar")
            {
                guardarNota();
                Intent intentReturn = new Intent(this, typeof(ListaNotasActivity));
                StartActivity(intentReturn);
            }
            else if (item.TitleFormatted.ToString() == "Amarillo")
            {
                color = 1;
                cambiarColorControles("#FFC107", Android.Graphics.Color.LightYellow);
            }
            else if (item.TitleFormatted.ToString() == "Verde")
            {
                color = 2;
                cambiarColorControles("#4CAF50", Android.Graphics.Color.LightGreen);
            }
            else if (item.TitleFormatted.ToString() == "Azul")
            {
                color = 3;
                cambiarColorControles("#03A9F4", Android.Graphics.Color.LightBlue);
            }
            else if (item.TitleFormatted.ToString() == "Naranja")
            {
                color = 4;
                cambiarColorControles("#FF5722", Android.Graphics.Color.LightSalmon);
            }
            else if (item.TitleFormatted.ToString() == "Violeta")
            {
                color = 5;
                cambiarColorControles("#673AB7", Android.Graphics.Color.ParseColor("#CE4BEB"));
            }
            return base.OnOptionsItemSelected(item);
        }
        void cambiarColorControles(string color, Color color1)
        {
            //Colores del layout
            nuevaNota.SetBackgroundColor(color1);
            nuevaNotaLinear.SetBackgroundColor(color1);
            //Colores de los Linear y Toolbar
            colorBackground = new ColorDrawable(Color.ParseColor(color));
            ActionBar.SetBackgroundDrawable(colorBackground);
            //Cambiar el color de la barra de estado
            Window.SetStatusBarColor(Android.Graphics.Color.ParseColor(color));

        }
        public void guardarNota()
        {
            objNotas = new MisNotas();
            objNotas.Titulo = tituloNota.Text;
            objNotas.Contenido = nuevaNota.Text;
            objNotas.FechaNota = DateTime.Now;
            objNotas.Recordatorio = DateTime.Now;
            objNotas.IdColor = color;

            int i = MisNotasDb.GuardarNota(objNotas);
            if (i > 0)
            {
                Toast.MakeText(this, "Nota Registrada", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "Error en Guardar", ToastLength.Long).Show();
            }
        }
    }
}