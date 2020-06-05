using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;

using Android.Text;
using Android.Text.Style;
using Android.Transitions;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using XamiNotes.Clases;
using XamiNotes.DataBase;
using XamiNotes.Modelo;

namespace XamiNotes
{
    [Activity(Label = "DetalleNotasActivity")]
    public class DetalleNotasActivity : Activity
    {
       
        Toolbar toolbarDetalle;
        EditText tituloDetalle, detalleNota ;
        TextView fechaNota, fechaModificacion;
        LinearLayout detalleLinear, tituloDetalleLinear, linearTitulo, linearFecha, linearFechaUpdate;
        
         int idNota, idColor=0, idFont=0;
         int color=0;
         int font = 0;
        string fechaNotaCreacion;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DetalleNotalayout);
            toolbarDetalle = FindViewById<Toolbar>(Resource.Id.toolbarDetalleNotas);
            tituloDetalle = FindViewById<EditText>(Resource.Id.tituloDetalleEditText);
            detalleNota = FindViewById<EditText>(Resource.Id.detalleNotaEditText);
            fechaNota = FindViewById<TextView>(Resource.Id.fechaTextView);
            fechaModificacion = FindViewById<TextView>(Resource.Id.fechaUpdateTextView);
            toolbarDetalle = FindViewById<Toolbar>(Resource.Id.toolbarDetalleNotas);
            detalleLinear = FindViewById<LinearLayout>(Resource.Id.detalleNotaLinearLayout);
            tituloDetalleLinear = FindViewById<LinearLayout>(Resource.Id.tituloDetalleLinear);
            linearTitulo = FindViewById<LinearLayout>(Resource.Id.linearTitulo);
            linearFecha = FindViewById<LinearLayout>(Resource.Id.linearFecha);
            linearFechaUpdate = FindViewById<LinearLayout>(Resource.Id.linearFechaUpdate);
            SetActionBar(toolbarDetalle);
            ActionBar.Title = "Detalle Nota";
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            CargarDetalleNota();
            //colorear fondo de la nota de acurerdo a  idColor de la tabla

            Metodos.CambiarColorNotasGeneric(this, null, idColor,2);
            
            habilitarControles(false, false, false, false);

            //cambiando font del detalle nota
            //cambiarFont();
            
            Window.EnterTransition = transicion();
            Window.ExitTransition = transicion();
            Window.ReturnTransition = transicion();
            //Window.AllowEnterTransitionOverlap = false;
           
        }
        
        private Slide transicion()
        {
            Slide slide = new Slide(GravityFlags.Right);
            slide.SetDuration(400);
            slide.SetInterpolator(new DecelerateInterpolator());
            return slide;
        }
        void habilitarControles(bool focusTitulo,bool focusNota, bool titulo, bool nota)
        {
            
            tituloDetalle.Enabled=titulo;
            detalleNota.Enabled = nota;
            detalleNota.FocusableInTouchMode = focusNota;
            tituloDetalle.FocusableInTouchMode = focusTitulo;

        }
             
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.DetalleNotaMenu,menu);
            //var xamifont = Typeface.CreateFromAsset(Assets, "@font/pacifico.ttf");
            return base.OnCreateOptionsMenu(menu);
            
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {

                FinishAfterTransition();
                return true;
            }
            else if (item.TitleFormatted.ToString() == "Editar")
            {
                EditarNota();
                habilitarControles(false, false, false, false);
            }
            else if (item.TitleFormatted.ToString() == "EditarNota")
            {
                habilitarControles(true, true, true, true);
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

            //Problema al usar dos métodos sin el condicional if solo el último guardaba los datos enteros
            //retornados por el método, ya que sin la condicion que lo aislaría este volvía a ingresar el item
            //que esta vez solo nos servia para el segundo método, quedando en opcion vacia y retornaba el entero cargado por 
            //defecto desde la db, siendo el último método llamado  el que guardaba los datos correctos
            //
            //color = Metodos.CambiarColorNotasGeneric(this, item, idColor, 2);
            font = Metodos.RegistrarFont(this, item, tituloDetalle, detalleNota, idFont);



            return base.OnOptionsItemSelected(item);
        }
        
        void cambiarColorControles(string color, Color color1)
        {
            
            ColorDrawable colorBackground;


            detalleNota.SetBackgroundColor(color1);
            detalleLinear.SetBackgroundColor(color1);
            //Colores de los Linear y Toolbar
            colorBackground = new ColorDrawable(Android.Graphics.Color.ParseColor(color));
            ActionBar.SetBackgroundDrawable(colorBackground);
            tituloDetalleLinear.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
            linearFecha.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
            linearFechaUpdate.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
            linearTitulo.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
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
            idFont = Intent.Extras.GetInt("IdFont");
            color = Intent.Extras.GetInt("IdColor");
            font= Intent.Extras.GetInt("IdFont");
            fechaNotaCreacion = Intent.Extras.GetString("FechaNota");
            fechaModificacion.Text= Intent.Extras.GetString("FechaModificacion");
            Metodos.CambiarFont(idFont, this, detalleNota, tituloDetalle);

            DateTime fechaNotaRegistrada = Convert.ToDateTime(fechaModificacion.Text);
            DateTime fechaNotaCorta = fechaNotaRegistrada.AddHours(23).AddMinutes(59).AddSeconds(59);
            var fechaRegistro = DateTime.Compare(fechaNotaCorta, DateTime.Now);
            if (fechaRegistro == -1)
            {
                DateTime fechaObj= Convert.ToDateTime(Intent.Extras.GetString("FechaModificacion"));
                fechaModificacion.Text ="Modificado" + fechaObj.ToString(" MMMM" +" dd", CultureInfo.CreateSpecificCulture("es-PE"));

            }
            else
            {
                
                String fechaEnLetras = "Modificado Hoy";
                fechaModificacion.Text = fechaEnLetras;
            }

        }
        public void EditarNota()
        {
            MisNotas objNota = new MisNotas()
            {
                IdNotas = idNota,
                Titulo = tituloDetalle.Text,
                Contenido = detalleNota.Text,
                FechaNota = Convert.ToDateTime(fechaNotaCreacion.ToString()),
                FechaModificacion = DateTime.Now,
                IdColor = color,
                IdFont = font



            };


            int respuesta = MisNotasDb.EditarNota(objNota);
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
            alert.SetButton("SI", (c, ev) =>
            {
                    int respuesta = MisNotasDb.EliminarNota(objNota);
                    if (respuesta > 0)
                    {   
                            Toast.MakeText(this, "Nota Eliminada", ToastLength.Long).Show();
                            Intent intentBack = new Intent(this, typeof(ListaNotasActivity));
                            StartActivity(intentBack,ActivityOptions.MakeSceneTransitionAnimation(this).ToBundle());
               
               
                    }
                    else
                        Toast.MakeText(this, "Se produjo un error", ToastLength.Short).Show();
                    });
            alert.SetButton2("NO", (c, ev) => {

                Toast.MakeText(this, "Cancelado", ToastLength.Long).Show();
            });
            alert.Show();
        }
       
    }
}