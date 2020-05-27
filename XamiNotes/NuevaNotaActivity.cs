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
using Android.Transitions;
using Android.Views;
using Android.Widget;
using XamiNotes.Clases;
using XamiNotes.DataBase;
using XamiNotes.Modelo;

namespace XamiNotes
{
    [Activity(Label = "NuevaNotaActivity")]
    public class NuevaNotaActivity : Activity
    {
        EditText nuevaNota, tituloNota;
        MisNotas objNotas;
        LinearLayout nuevaNotaLinear, linearTituloNota;
        ColorDrawable colorBackground;
        //1 = Amarillo, Color por defecto de la nota si no se selecciona ningún color 
        int color = 1;
        int font = 1;

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
            linearTituloNota = FindViewById<LinearLayout>(Resource.Id.linearTituloNuevaNota);
            nuevaNotaLinear.SetBackgroundColor(Android.Graphics.Color.LightYellow);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            color = Metodos.CambiarColorNotasGeneric(this, null, 0, 1);
            Explode explode = new Explode();
            explode.SetDuration(400);
            Window.EnterTransition = explode;
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
                Slide slide = new Slide();
                slide.SetDuration(500);
                Window.ExitTransition = slide;
                Intent intentReturn = new Intent(this, typeof(ListaNotasActivity));
                StartActivity(intentReturn, ActivityOptions.MakeSceneTransitionAnimation(this).ToBundle());
            }
            font = Metodos.RegistrarFont(this, item, tituloNota, nuevaNota, font);
            color=Metodos.CambiarColorNotasGeneric(this, item, color,1);
            return base.OnOptionsItemSelected(item);
        }
        void cambiarColorControles(string color, Color color1)
        {
            //Colores del layout
            nuevaNota.SetBackgroundColor(color1);
            nuevaNotaLinear.SetBackgroundColor(color1);
            linearTituloNota.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
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
            objNotas.FechaModificacion = DateTime.Now;
            objNotas.Recordatorio = DateTime.Now;
            objNotas.IdColor = color;
            objNotas.IdFont = font;

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