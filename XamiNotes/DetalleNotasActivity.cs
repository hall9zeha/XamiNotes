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
    [Activity(Label = "DetalleNotasActivity")]
    public class DetalleNotasActivity : Activity
    {

        Toolbar toolbarDetalle;
        EditText tituloDetalle, detalleNota;
        TextView fechaNota;
        LinearLayout detalleLinear;
        int idNota, idColor=2;
        int color = 1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DetalleNotalayout);
            toolbarDetalle = FindViewById<Toolbar>(Resource.Id.toolbarDetalleNotas);
            tituloDetalle = FindViewById<EditText>(Resource.Id.tituloDetalleEditText);
            detalleNota = FindViewById<EditText>(Resource.Id.detalleNotaEditText);
            fechaNota = FindViewById<TextView>(Resource.Id.fechaTextView);
            toolbarDetalle = FindViewById<Toolbar>(Resource.Id.toolbarDetalleNotas);
            detalleLinear = FindViewById<LinearLayout>(Resource.Id.detalleNotaLinearLayout);
            SetActionBar(toolbarDetalle);
            ActionBar.Title = "Detalle Nota";

            CargarDetalleNota();
            //colorear fondo de la nota de acurerdo a el idColor de la tabla
            cargarColorNotas();


        }
        void cargarColorNotas()
        {
            if (idColor == 1)
            {
                detalleNota.SetBackgroundColor(Android.Graphics.Color.LightYellow);
                detalleLinear.SetBackgroundColor(Android.Graphics.Color.LightYellow);
            }
            else if (idColor == 2)
            { detalleNota.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                detalleLinear.SetBackgroundColor(Android.Graphics.Color.LightGreen);
            }
            else if (idColor == 3)
            { detalleNota.SetBackgroundColor(Android.Graphics.Color.LightBlue);
                detalleLinear.SetBackgroundColor(Android.Graphics.Color.LightBlue);
            }
            else if (idColor == 4)
            { detalleNota.SetBackgroundColor(Android.Graphics.Color.LightSalmon);
                detalleLinear.SetBackgroundColor(Android.Graphics.Color.LightSalmon);
            }
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.DetalleNotaMenu,menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.TitleFormatted.ToString() == "Editar")
            {
                EditarNota();
            }
            else if (item.TitleFormatted.ToString() == "Eliminar")
            {
                EliminarNota();
            }

            else if (item.TitleFormatted.ToString() == "Amarillo")
            {
                color = 1;
                detalleNota.SetBackgroundColor(Android.Graphics.Color.LightYellow);
                detalleLinear.SetBackgroundColor(Android.Graphics.Color.LightYellow);
            }
            else if (item.TitleFormatted.ToString() == "Verde")
            {
                color = 2;
                detalleNota.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                detalleLinear.SetBackgroundColor(Android.Graphics.Color.LightGreen);
            }
            else if (item.TitleFormatted.ToString() == "Azul")
            {
                color = 3;
                detalleNota.SetBackgroundColor(Android.Graphics.Color.LightBlue);
                detalleLinear.SetBackgroundColor(Android.Graphics.Color.LightBlue);
            }
            else if (item.TitleFormatted.ToString() == "Naranja")
            {
                color = 4;
                detalleNota.SetBackgroundColor(Android.Graphics.Color.LightSalmon);
                detalleLinear.SetBackgroundColor(Android.Graphics.Color.LightSalmon);
            }

            return base.OnOptionsItemSelected(item);
        }
        public void CargarDetalleNota()
        {
            tituloDetalle.Text = Intent.Extras.GetString("Titulo");
            detalleNota.Text = Intent.Extras.GetString("Contenido");
            fechaNota.Text = Intent.Extras.GetString("FechaNota");
            //cargamos estas variables para usarlas en diversas funciones
            idNota = Intent.Extras.GetInt("IdNotas");
            idColor = Intent.Extras.GetInt("IdColor");

        }
        public void EditarNota()
        {
            MisNotas objNota = new MisNotas()
            {
                IdNotas=idNota,
                Titulo = tituloDetalle.Text,
                Contenido = detalleNota.Text,
                FechaNota = DateTime.Now,
                IdColor = color
                
            };
           int respuesta= MisNotasDb.EditarNota(objNota);
            if (respuesta > 0)
                Toast.MakeText(this, "Modificaciones Guardadas", ToastLength.Long).Show();
            else
                Toast.MakeText(this, "Se produjo un error", ToastLength.Short).Show();
        }
        public void EliminarNota()
        {
            MisNotas objNota = new MisNotas() {
                IdNotas = idNota
            };
            int respuesta = MisNotasDb.EliminarNota(objNota);
            if (respuesta > 0)
            {
                Toast.MakeText(this, "Nota Eliminada", ToastLength.Long).Show();
                Intent intentBack = new Intent(this, typeof(ListaNotasActivity));
                StartActivity(intentBack);
            }
            else
                Toast.MakeText(this, "Se produjo un error", ToastLength.Short).Show();
        }
    }
}