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
    [Activity(Label = "DetalleNotasActivity")]
    public class DetalleNotasActivity : Activity
    {

        Toolbar toolbarDetalle;
        EditText tituloDetalle, detalleNota;
        TextView fechaNota;
        LinearLayout detalleLinear, tituloDetalleLinear;
        ColorDrawable colorBackground;
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
            tituloDetalleLinear = FindViewById<LinearLayout>(Resource.Id.tituloDetalleLinear);
            SetActionBar(toolbarDetalle);
            ActionBar.Title = "Detalle Nota";
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            CargarDetalleNota();
            //colorear fondo de la nota de acurerdo a el idColor de la tabla
            cargarColorNotas();


        }
        void cargarColorNotas()
        {
            if (idColor == 1)
            {
                cambiarColorControles("#FFC107", Android.Graphics.Color.LightYellow);
            }
            else if (idColor == 2)
            {
                cambiarColorControles("#4CAF50", Android.Graphics.Color.LightGreen);
            }
            else if (idColor == 3)
            {
                cambiarColorControles("#03A9F4", Android.Graphics.Color.LightBlue);
            }
            else if (idColor == 4)
            {
                cambiarColorControles("#FF5722", Android.Graphics.Color.LightSalmon);
            }
            else if (idColor == 5)
            {
                cambiarColorControles("#673AB7", Android.Graphics.Color.ParseColor("#CE4BEB"));
            }
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.DetalleNotaMenu,menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
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
            detalleNota.SetBackgroundColor(color1);
            detalleLinear.SetBackgroundColor(color1);
            //Colores de los Linear y Toolbar
            colorBackground = new ColorDrawable(Color.ParseColor(color));
            ActionBar.SetBackgroundDrawable(colorBackground);
            tituloDetalleLinear.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
            //Cambiar el color de la barra de estado
            Window.SetStatusBarColor(Android.Graphics.Color.ParseColor(color));
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

            AlertDialog.Builder dialogo = new AlertDialog.Builder(this);
            AlertDialog alert = dialogo.Create();
            alert.SetTitle("Advertencia");
            alert.SetMessage("Realmente quiere eliminar esta nota?");
            alert.SetIcon(Resource.Mipmap.ic_action_warning);
            alert.SetButton("OK", (c, ev) =>
            {
                    int respuesta = MisNotasDb.EliminarNota(objNota);
                    if (respuesta > 0)
                    {   
                            Toast.MakeText(this, "Nota Eliminada", ToastLength.Long).Show();
                            Intent intentBack = new Intent(this, typeof(ListaNotasActivity));
                            StartActivity(intentBack);
               
               
                    }
                    else
                        Toast.MakeText(this, "Se produjo un error", ToastLength.Short).Show();
                    });
            alert.SetButton2("CANCEL", (c, ev) => {

                Toast.MakeText(this, "Cancelado", ToastLength.Long).Show();
            });
            alert.Show();
        }
       
    }
}