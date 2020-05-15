using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
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
            

            // Create your application here
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.NuevaNotaMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.TitleFormatted.ToString() == "Guardar")
            {
                guardarNota();
                Intent intentReturn = new Intent(this, typeof(ListaNotasActivity));
                StartActivity(intentReturn);
            }
            else if (item.TitleFormatted.ToString() == "Amarillo")
            {
                color = 1;
                nuevaNota.SetBackgroundColor(Android.Graphics.Color.LightYellow);
                nuevaNotaLinear.SetBackgroundColor(Android.Graphics.Color.LightYellow);
            }
            else if (item.TitleFormatted.ToString() == "Verde")
            {
                color = 2;
                nuevaNota.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                nuevaNotaLinear.SetBackgroundColor(Android.Graphics.Color.LightGreen);
            }
            else if (item.TitleFormatted.ToString() == "Azul")
            {
                color = 3;
                nuevaNota.SetBackgroundColor(Android.Graphics.Color.LightBlue);
                nuevaNotaLinear.SetBackgroundColor(Android.Graphics.Color.LightBlue);
            }
            else if (item.TitleFormatted.ToString() == "Naranja")
            {
                color = 4;
                nuevaNota.SetBackgroundColor(Android.Graphics.Color.LightSalmon);
                nuevaNotaLinear.SetBackgroundColor(Android.Graphics.Color.LightSalmon);
            }
            return base.OnOptionsItemSelected(item);
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